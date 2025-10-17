namespace MiniMag.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public ICollection<Intake> Intakes { get; set; } = new List<Intake>();
    }
}
