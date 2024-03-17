namespace TechStore.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public decimal TotalPrice { get; set; }
        public int CardNum { get; set; }
        public DateTime OrderTime { get; set; }
    }
}
