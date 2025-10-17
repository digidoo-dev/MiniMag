using System.ComponentModel.DataAnnotations;

namespace MiniMag.Models;

public class Product
{
    public int ID { get; set; }
    public string Name { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }
    public string Location { get; set; }



    public ICollection<Intake> Intakes { get; set; } = new List<Intake>();
    public ICollection<Issue> Issues { get; set; } = new List<Issue>();
}
