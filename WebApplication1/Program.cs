using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<TaxiDepotContext>(options=>options.UseSqlite(connection));

// using (AssemblyContext db = new())
// {
// }

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Razor pages Mapping
app.MapRazorPages();

app.Run();