using System.Globalization;
using ElectronicsShop2.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ElectronicsShop2.Pages.Products
{
    public class OrderModel : MyPage
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            
            String id = Request.Query["id"];
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElectronicsShop2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Products WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.name = reader.GetString(1);
                                productInfo.price = reader.GetString(2);
                                productInfo.quantity = reader.GetString(3);
                                productInfo.description = reader.GetString(4);
                                productInfo.discount = reader.GetString(5);
                                productInfo.type = reader.GetString(6);
                                productInfo.created_at = reader.GetDateTime(7).ToString();
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
        }
        public void OnPost()
        {
            string id = Request.Form["id"];
            string oldPrice = Request.Form["price"];
            string oldDiscount = Request.Form["discount"];
            string OrderQuantity = Request.Form["quantity"];
            string Address = Request.Form["address"];
            string PhoneNumber = Request.Form["phoneNumber"];
            
           
            if (OrderQuantity.Length == 0 || Address.Length == 0 || PhoneNumber.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            
            var discount = float.Parse(oldDiscount, CultureInfo.InvariantCulture.NumberFormat);
            var price = float.Parse(oldPrice, CultureInfo.InvariantCulture.NumberFormat);
            var newprice = price - (price * (discount / 100));
            var x = newprice;
            if (OrderQuantity != "1")
            {
                Console.WriteLine("A7a");
                x *= (float)(15.0 / 100.0);
                newprice = newprice-x;

            }

            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElectronicsShop2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Orders" +
                                 "(phone, address, OrderQuantity, orderPrice, userID, ProductID) VALUES " +
                                 "(@phone, @address,@OrderQuantity, @orderPrice, @userID, @ProductID);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@phone", PhoneNumber);
                        command.Parameters.AddWithValue("@address", Address);
                        command.Parameters.AddWithValue("@OrderQuantity", OrderQuantity);
                        command.Parameters.AddWithValue("@orderPrice", newprice);
                        command.Parameters.AddWithValue("@userID", UserId);
                        command.Parameters.AddWithValue("@ProductID", id);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }



            Response.Redirect("/Products/ShowProducts");
        }

    }
}
