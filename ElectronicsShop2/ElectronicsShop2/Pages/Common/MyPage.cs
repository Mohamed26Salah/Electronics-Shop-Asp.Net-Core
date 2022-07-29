using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ElectronicsShop2.Pages.Common
{
    public class MyPage:PageModel
    {
       
        public string? UserId => User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
