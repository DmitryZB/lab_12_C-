using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotEditing : PageModel
{
    [BindProperty] 
    public string TaxiDepotName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(TaxiDepotContext db)
    {
        TaxiDepot? find = db.TaxiDepots.FirstOrDefault(u => u.Address == TaxiDepotName);
        if (find != null)
        {
            return Redirect($"/TaxiDepotEditingForm/{TaxiDepotName}");
        }
        else
        {
            return Redirect($"/NoCarEdit/{TaxiDepotName}");  
        }
        
    }
}