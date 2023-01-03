using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiGroupCreating : PageModel
{
    private TaxiDepotContext context;
    [BindProperty] 
    private TaxiDepot taxidepot { get; set; } = new TaxiDepot();

    public TaxiGroupCreating(TaxiDepotContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
    }

    public IActionResult OnPost(List<TaxiGroup> partsData)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        taxidepot = context.TaxiDepots.FirstOrDefault(u => u.Address == name);
        List<TaxiGroup> taxigroupsforDB = new List<TaxiGroup>();
        foreach (var VARIABLE in partsData)
        {
            VARIABLE.TaxiDepot_ = taxidepot;
            VARIABLE.TaxiDepotId = taxidepot.Id;
            Car? detail = context.Cars.FirstOrDefault(u => u.Name == VARIABLE.CarName);
            if (detail != null)
            {
                VARIABLE.Car_ = detail;
                VARIABLE.CarId = detail.Id;
                taxigroupsforDB.Add(VARIABLE);
            }
            else
            {
                return Redirect($"/NoCarEdit/{VARIABLE.CarName}");
            }
        }
        context.TaxiGroups.AddRange(taxigroupsforDB);
        context.SaveChanges();
        return Redirect("/TaxiDepotCreatingOk");
    }
}