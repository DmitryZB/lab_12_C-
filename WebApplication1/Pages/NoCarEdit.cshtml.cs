using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages;

public class ThereNoCar : PageModel
{
    [BindProperty]
    public string Message { get; set; } = "";
    public void OnGet()
    {
        object? data = RouteData.Values["Name"];
        Message = data.ToString();
    }

    public IActionResult OnPostYes()
    {
        return Redirect("/CarCreating");
    }
    public IActionResult OnPostNo()
    {
        return Redirect("/Index");
    }
}