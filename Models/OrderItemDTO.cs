namespace LojaVirtualAPI.Models
{
	public class OrderItemDTO
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public decimal Quantity { get; set; }
		public decimal Total { get; set; }
	}
}
