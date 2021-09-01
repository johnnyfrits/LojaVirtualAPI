using System;

namespace LojaVirtualAPI.Models
{
	public class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public long CPF { get; set; }
		public string Address { get; set; }
		public string Number { get; set; }
		public string Neighborhood { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public long ZipCode { get; set; }
		public long Phone { get; set; }
		public DateTime? Birth { get; set; }
	}
}
