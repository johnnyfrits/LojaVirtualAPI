using System;
using System.Collections.Generic;

namespace LojaVirtualAPI.Models
{
	public class Order
	{
		public int Id { get; set; }
		public int CustomerId { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<OrderItem> OrderItem { get; set; }
		public Customer Customer { get; set; }
	}
}
