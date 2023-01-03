using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class TaxiDepotEditingForm : PageModel
{
    private TaxiDepotContext context;
    [BindProperty]
    public TaxiDepot? taxidepot { get; set; } = new TaxiDepot() { Address = "", Id = 0};
    public List<TaxiGroup> taxigroupstemp { get; set; } = new List<TaxiGroup>();

    public TaxiDepotEditingForm(TaxiDepotContext db)
    {
        context = db;
    }
    
    public void OnGet()
    {
        object? data = RouteData.Values["Address"];
        string? name = data.ToString();
        taxidepot = context.TaxiDepots.FirstOrDefault(u => u.Address == name);
        context.TaxiGroups.Where(u => u.TaxiDepotId == taxidepot.Id).Load();
        taxigroupstemp = taxidepot.TaxiGroups.ToList();
    }

    public IActionResult OnPost(List<TaxiGroup> TaxiGroupsData)
    {
        List<TaxiGroup> OldTaxiGroups = context.TaxiGroups.Where(u=>u.TaxiDepotId==taxidepot.Id).ToList();
        List<TaxiGroup> TaxiGroupsToDelete = new List<TaxiGroup>();
        List<TaxiGroup> TaxiGroupsToAdd = new List<TaxiGroup>();
        List<TaxiGroup> TaxiGroupsToUpdate = new List<TaxiGroup>();
        if (TaxiGroupsData.Count < OldTaxiGroups.Count)
        {
            foreach (var VARIABLE in OldTaxiGroups)
            {
                if (!TaxiGroupsData.Exists(u => u.CarName == VARIABLE.CarName))
                {
                    TaxiGroupsToDelete.Add(VARIABLE);
                }
                else
                {
                    TaxiGroupsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (TaxiGroupsData.Count > OldTaxiGroups.Count)
        {
            foreach (var VARIABLE in TaxiGroupsData)
            {
                if (!OldTaxiGroups.Exists(u => u.CarName == VARIABLE.CarName))
                {
                    Car? Car = context.Cars.FirstOrDefault(u => u.Name == VARIABLE.CarName);
                    if (Car != null)
                    {
                      TaxiGroupsToAdd.Add(VARIABLE);  
                    }
                    else
                    {
                        return Redirect($"/NoCarEdit/{VARIABLE.CarName}");
                    }
                }
                else
                {
                    TaxiGroupsToUpdate.Add(VARIABLE);
                }
            }
        }

        if (TaxiGroupsData.Count == OldTaxiGroups.Count)
        {
            TaxiGroupsToUpdate = TaxiGroupsData;
        }

        if (TaxiGroupsToDelete.Count != 0)
        {
            context.TaxiGroups.RemoveRange(TaxiGroupsToDelete);
        }
        
        if (TaxiGroupsToAdd.Count != 0)
        {
            for (int i = 0; i < TaxiGroupsToAdd.Count; i++)
            {
                TaxiGroupsToAdd[i].TaxiDepotId = taxidepot.Id;
                Car? Car = context.Cars.FirstOrDefault(u => u.Name == TaxiGroupsToAdd[i].CarName);
                TaxiGroupsToAdd[i].CarId = Car.Id;
                TaxiGroupsToAdd[i].Car_ = Car;
            }
            context.TaxiGroups.AddRange(TaxiGroupsToAdd); 
        }

        if (TaxiGroupsToUpdate.Count != 0)
        {
            for (int i = 0; i < TaxiGroupsToUpdate.Count; i++)
            {
                OldTaxiGroups[OldTaxiGroups.FindIndex(u => u.CarName == TaxiGroupsToUpdate[i].CarName)].Quantity =
                    TaxiGroupsToUpdate[i].Quantity;
            }
            context.TaxiGroups.UpdateRange(OldTaxiGroups);
        }
        else
        {
            context.TaxiGroups.UpdateRange(OldTaxiGroups);
        }

        taxidepot.TaxiGroups = context.TaxiGroups.Where(u => u.TaxiDepotId == taxidepot.Id).ToList();
        context.TaxiDepots.Update(taxidepot);
        context.SaveChanges();
        return Redirect("/TaxiDepotEditingOk");
    }
}