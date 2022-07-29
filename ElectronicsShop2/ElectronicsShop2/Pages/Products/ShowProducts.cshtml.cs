
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;



namespace ElectronicsShop2.Pages.Products
{
    
    public class ShowProductsModel : PageModel
    {
        
        public List<ProductInfo> ListProducts = new List<ProductInfo>();
        public void OnGet()
        {
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElectronicsShop2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Products";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ProductInfo productInfo = new ProductInfo();
                                productInfo.id = "" + reader.GetInt32(0);
                                productInfo.name = reader.GetString(1);
                                productInfo.price = reader.GetString(2);
                                productInfo.quantity = reader.GetString(3);
                                productInfo.description = reader.GetString(4);
                                productInfo.discount = reader.GetString(5);
                                productInfo.type = reader.GetString(6);
                                productInfo.created_at = reader.GetDateTime(7).ToString();

                                ListProducts.Add(productInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.ToString());
                Console.WriteLine("Failed");
            }
        }
        
    }
}
