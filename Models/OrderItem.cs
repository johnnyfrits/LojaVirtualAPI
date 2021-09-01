using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtualAPI.Models
{
	public class OrderItem
	{
		public int Id { get; set; }
		public int OrderId { get; set; }
		public int ProductId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Column(TypeName = "decimal(18,3)")]
		public decimal Quantity { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Total { get; set; }

		public Product Product { get; set; }
	}
}
