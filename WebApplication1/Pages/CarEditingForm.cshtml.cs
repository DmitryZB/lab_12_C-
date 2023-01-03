using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class EditingForm : PageModel
{
    private TaxiDepotContext context;
    [BindProperty] 
    public Car? car { get; set; } = new Car() { Name = "", SitCounter = 0 };

    public EditingForm(TaxiDepotContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
      object? data = RouteData.Values["Name"];
      string? name = data.ToString();
      car = context.Cars.FirstOrDefault(u => u.Name == name);
    }

    public IActionResult OnPost()
    {
        context.Cars.Update(car);
        context.SaveChanges();
        return Redirect("/CarEditedOK");
    }
}