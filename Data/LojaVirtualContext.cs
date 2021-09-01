using LojaVirtualAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Data
{
	public class LojaVirtualContext : DbContext
	{
		public LojaVirtualContext(DbContextOptions<LojaVirtualContext> options) : base(options)
		{
		}

		public DbSet<Customer> Customer { get; set; }
		public DbSet<Product> Product { get; set; }
		public DbSet<Order> Order { get; set; }
		public DbSet<OrderItem> OrderItem { get; set; }
	}
}
