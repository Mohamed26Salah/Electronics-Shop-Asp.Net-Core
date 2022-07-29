using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;

namespace ElectronicsShop2.Pages.Products
{
    public class EditModel : PageModel
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
            productInfo.id = Request.Form["id"];
            productInfo.name = Request.Form["name"];
            productInfo.price = Request.Form["price"];
            productInfo.discount = Request.Form["discount"];
            productInfo.quantity = Request.Form["quantity"];
            productInfo.description = Request.Form["description"];
            productInfo.type = Request.Form["type"];
            if (productInfo.id.Length == 0 || productInfo.name.Length == 0 || productInfo.price.Length == 0 || productInfo.discount.Length == 0 || productInfo.quantity.Length == 0 || productInfo.description.Length == 0 || productInfo.type.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElectronicsShop2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Products " +
                                 "SET name=@name, price=@price, quantity=@quantity, description=@description, discount=@discount, type=@type " +
                                 "WHERE id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productInfo.name);
                        command.Parameters.AddWithValue("@price", productInfo.price);
                        command.Parameters.AddWithValue("@discount", productInfo.discount);
                        command.Parameters.AddWithValue("@quantity", productInfo.quantity);
                        command.Parameters.AddWithValue("@description", productInfo.description);
                        command.Parameters.AddWithValue("@type", productInfo.type);
                        command.Parameters.AddWithValue("@id", productInfo.id);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }


           
            Response.Redirect("/Products/Index1");
        }
    }
}
