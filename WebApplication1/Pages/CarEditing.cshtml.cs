using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class CarEditing : PageModel
{
    [BindProperty] public string CarName { get; set; } = "";
    public void OnGet()
    {
    }

    public IActionResult OnPost(TaxiDepotContext db)
    {
        Car? find = db.Cars.FirstOrDefault(u => u.Name == CarName);
        if (find != null)
        {
            return Redirect($"/CarEditingForm/{CarName}");
        }
        else
        {
            return Redirect($"/NoCarEdit/{CarName}");  
        }
        
    }
}