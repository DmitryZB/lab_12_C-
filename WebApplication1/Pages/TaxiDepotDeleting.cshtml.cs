using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotDeleting : PageModel
{
    [BindProperty]
    public string address { get; set; } = "";

    public void OnGet()
    {
    }

    public IActionResult OnPost(TaxiDepotContext db)
    {
        TaxiDepot? temp = db.TaxiDepots.FirstOrDefault(u => u.Address == address);
        db.TaxiGroups.Where(u => u.TaxiDepotId == temp.Id).Load();
        if (temp != null)
        {
            db.TaxiDepots.Remove(temp);
            db.SaveChanges();
            return Redirect("/TaxiDepotDeletedOk");
        }
        else
        {
            return Redirect("/NoTaxiDepotDelete");
        }
    }
}