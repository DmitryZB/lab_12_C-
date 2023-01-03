using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class CarDelete : PageModel
{
    [BindProperty]
    public string Name { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(TaxiDepotContext db)
    {
        Car? temp = db.Cars.FirstOrDefault(u => u.Name == Name);
        if (temp != null)
        {
            db.Cars.Remove(temp);
            db.SaveChanges();
            return Redirect("/CarDeletedOk");
        }
        else
        {
            return Redirect("/NoCarDelete");
        }
    }
}