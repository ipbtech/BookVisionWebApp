using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyFirstWebApp.Views
{
    public class HomeModel : PageModel
    {
        public IActionResult OnGet()
        {
            return RedirectToPagePermanent("/Index");
        }
    }
}
