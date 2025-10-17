using System.ComponentModel.DataAnnotations;

namespace MiniMag.Models
{
    public class Intake
    {
        public int ID { get; set; }

        public int ProductID { get; set; }
        public Product? Product { get; set; }

        public int SupplierID { get; set; }
        public Supplier? Supplier { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity {  get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
    }
}
