using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
namespace WebApplication1.Pages;

public class TaxiDepotCreate : PageModel
{
    [BindProperty]
    public TaxiDepot taxidepot { get; set; } = new TaxiDepot() {Address = ""};
    public string Message { get; set; } = "";

    public void OnGet(TaxiDepotContext db)
    {
    }

    public IActionResult OnPost(TaxiDepotContext db)
    {
        TaxiDepot? duplicate = db.TaxiDepots.FirstOrDefault(u=>u.Address==taxidepot.Address);
        if (duplicate == null)
        {
            db.TaxiDepots.Add(taxidepot);
            db.SaveChanges();
            return Redirect($"/TaxiGroupCreating/{taxidepot.Address}");
        }
        else
        {
            Message = "Машина с таким наименованием уже существует";
            return Redirect("/Index");
        }
    }
}