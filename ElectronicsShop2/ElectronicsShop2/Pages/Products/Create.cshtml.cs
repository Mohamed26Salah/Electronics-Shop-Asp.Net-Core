using System.Net.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
namespace ElectronicsShop2.Pages.Products
{
    public class CreateModel : PageModel
    {
        public ProductInfo productInfo = new ProductInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            productInfo.name = Request.Form["name"];
            productInfo.price = Request.Form["Price"];
            productInfo.discount = Request.Form["discount"];
            productInfo.quantity = Request.Form["Quantity"];
            productInfo.description = Request.Form["description"];
            productInfo.type = Request.Form["type"];
            if (productInfo.name.Length == 0 || productInfo.price.Length == 0 || productInfo.discount.Length == 0 || productInfo.quantity.Length == 0 || productInfo.description.Length == 0 || productInfo.type.Length == 0)
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
                    String sql = "INSERT INTO Products" +
                                 "(name, price, quantity, description, discount, type) VALUES " +
                                 "(@name, @price,@quantity, @description, @discount, @type);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", productInfo.name);
                        command.Parameters.AddWithValue("@price", productInfo.price);
                        command.Parameters.AddWithValue("@discount", productInfo.discount);
                        command.Parameters.AddWithValue("@quantity", productInfo.quantity);
                        command.Parameters.AddWithValue("@description", productInfo.description);
                        command.Parameters.AddWithValue("@type", productInfo.type);
                        command.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                return;
            }


            productInfo.name = "";
            productInfo.price = "";
            productInfo.quantity = "";
            successMessage = "New Product Added Correctly";
            Response.Redirect("/Products/Index1");

        }
    }

   
}
