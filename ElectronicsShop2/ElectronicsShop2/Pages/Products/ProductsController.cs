using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace ElectronicsShop2.Pages.Products;

public class ProductsController : Controller
{
    [HttpGet]
    public JsonResult GetNames()
    {
        var names = new string[3]
        {
            "Clara",
            "Marc",
            "judy"
        };
        return new JsonResult(Ok(names));
    }
    [HttpPost]
    public JsonResult PostName(string type)
    {
        return new JsonResult(Ok());

    }

    [HttpPost]
    public int Add(int number1, int number2)
    {
        return number1 + number2;

    }

}