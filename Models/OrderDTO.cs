using System;
using System.Collections.Generic;

namespace LojaVirtualAPI.Models
{
	public class OrderDTO
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
		public DateTime Date { get; set; }
		public decimal Total { get; set; }
		public IEnumerable<OrderItemDTO> OrderItems { get; set; }
	}
}
