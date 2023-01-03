using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotView : PageModel
{
    [BindProperty]
    public string TaxiDepotAddress { get; set; } = "";
    public void OnGet()
    {
    }
    public IActionResult OnPostID()
    {
        return Redirect($"/TaxiDepotViewID/{TaxiDepotAddress}");
    }
    public IActionResult OnPostAll()
    {
        return Redirect("/TaxiDepotViewAll");
    }
}