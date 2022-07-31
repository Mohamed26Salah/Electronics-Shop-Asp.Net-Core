using ElectronicsShop2.Pages.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace ElectronicsShop2.Pages
{
    public class IndexModel : MyPage
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            HttpContext.Session.SetString("SessionKeyName", "The Doctor");
            string? id = UserId;
            try
            {
                String connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ElectronicsShop2;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM AspNetUsers WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                HttpContext.Session.SetString("FirstName", reader.GetString(1));
                                HttpContext.Session.SetString("LastName", reader.GetString(2));
                                HttpContext.Session.SetString("UserName", reader.GetString(3));
                                HttpContext.Session.SetString("Role", reader.GetString(17));
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {
               
            }
        }
      
    }
}