namespace SICAPIProject.ViewModels
{
    public class ProductVM
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; } = decimal.Zero;
    }
}
