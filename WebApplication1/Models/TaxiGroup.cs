namespace WebApplication1.Models;

public class TaxiGroup
{
    public int Id { get; set; }
    public int TaxiDepotId { get; set; }
    public TaxiDepot TaxiDepot_ { get; set; }
    public int CarId { get; set; }
    public string CarName { get; set; }
    public Car Car_ { get; set; }
    public int Quantity { get; set; }
}