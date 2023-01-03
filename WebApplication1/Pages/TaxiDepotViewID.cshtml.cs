using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotViewID : PageModel
{
    [BindProperty]
    public TaxiDepot? TaxiDepot { get; set; }

    public List<TaxiGroup> TaxiGroups { get; set; } = new List<TaxiGroup>();

    public void OnGet(TaxiDepotContext db)
    {
        object? data = RouteData.Values["Name"];
        string? name = data.ToString();
        TaxiDepot = db.TaxiDepots.FirstOrDefault(u => u.Address == name);
        db.TaxiGroups.Where(u=>u.TaxiDepotId==TaxiDepot.Id).Load();
        TaxiGroups = TaxiDepot.TaxiGroups;
    }
}