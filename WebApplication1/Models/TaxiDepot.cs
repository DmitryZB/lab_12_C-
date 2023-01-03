namespace WebApplication1.Models;

public class TaxiDepot
{
    public int Id { get; set; }
    public string? Address { get; set; }
    public List<TaxiGroup> TaxiGroups { get; set; } = new();
}