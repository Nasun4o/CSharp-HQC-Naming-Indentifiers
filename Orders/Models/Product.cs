namespace StoreCalculator.Models
{
    public class Product : AbstractModel
    {
        public int CatId { get; set; }

        public decimal UnitPrice { get; set; }

        public int UnitsInStock { get; set; }
    }
}
