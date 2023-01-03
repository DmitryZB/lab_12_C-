using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class AllCars : PageModel
{
    public List<Car> DisplayedCars = new List<Car>();
    public void OnGet(TaxiDepotContext db)
    {
        DisplayedCars = db.Cars.ToList();
    }
}