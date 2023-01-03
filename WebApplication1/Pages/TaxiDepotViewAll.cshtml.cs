using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotViewAll : PageModel
{
    [BindProperty] 
    public List<TaxiDepot> TaxiDepots { get; set; } = new List<TaxiDepot>();

    public void OnGet(TaxiDepotContext db)
    {
        TaxiDepots = db.TaxiDepots.ToList();
        db.TaxiGroups.Load();
    }
}