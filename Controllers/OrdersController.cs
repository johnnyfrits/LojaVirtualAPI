using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtualAPI.Data;
using LojaVirtualAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly LojaVirtualContext _context;

		public OrdersController(LojaVirtualContext context)
		{
			_context = context;
		}

		// GET: api/Order
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrder()
		{
			return await _context.Order.Include(o => o.OrderItem)
									   .ThenInclude(i => i.Product)
									   .Include(o => o.Customer)
									   .Select(o => OrderToDTO(o))
									   .ToListAsync();
		}

		// GET: api/Order/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderDTO>> GetOrder(int id)
		{
			var order = await _context.Order.Include(o => o.OrderItem)
											.ThenInclude(i => i.Product)
											.Include(o => o.Customer)
											.FirstOrDefaultAsync(o => o.Id == id);

			if (order == null)
			{
				return NotFound();
			}

			return OrderToDTO(order);
		}

		// PUT: api/Order/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrder(int id, Order order)
		{
			if (id != order.Id)
			{
				return BadRequest();
			}

			_context.Entry(order).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Order
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Order>> PostOrder(Order order)
		{
			order.Date = DateTime.Now;

			_context.Order.Add(order);

			await _context.SaveChangesAsync();

			return CreatedAtAction("GetOrder", new { id = order.Id }, order);
		}

		// DELETE: api/Order/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrder(int id)
		{
			var order = await _context.Order.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}

			_context.Order.Remove(order);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool OrderExists(int id)
		{
			return _context.Order.Any(e => e.Id == id);
		}

		private static OrderDTO OrderToDTO(Order order) =>
			new OrderDTO
			{
				Id           = order.Id,
				CustomerId   = order.CustomerId,
				CustomerName = order.Customer.Name,
				Date         = order.Date,
				Total        = order.OrderItem.Sum(oi => oi.Total),
				OrderItems   = OrderItemsController.OrderItemToDTO(order.OrderItem),
			};
	}
}
