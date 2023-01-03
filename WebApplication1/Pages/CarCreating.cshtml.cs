using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Pages;

public class CarCreating : PageModel
{
    [BindProperty] 
    public Car temp { get; set; } = new Car() { Name = "", SitCounter = 0 };
    public string Message { get; private set; } = "Добавление машины:";

    public void OnPost(TaxiDepotContext db)
    {
        Car? duplicate = db.Cars.FirstOrDefault(u=>u.Name==temp.Name);
        if (duplicate == null)
        {
           db.Cars.Add(temp);
           db.SaveChanges();
           Message = $"Добавлена машина:\n наименование: {temp.Name}, количество мест: {temp.SitCounter} ";
        }
        else
        {
            Message = "Данная машина уже существует";
        }
    }
}