using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LojaVirtualAPI.Data;
using LojaVirtualAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtualAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class OrderItemsController : ControllerBase
	{
		private readonly LojaVirtualContext _context;

		public OrderItemsController(LojaVirtualContext context)
		{
			_context = context;
		}

		// GET: OrderItems
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItem()
		{
			return await _context.OrderItem.ToListAsync();
		}

		// GET: OrderItems/5
		[HttpGet("{id}")]
		public async Task<ActionResult<OrderItemDTO>> GetOrderItem(int id)
		{
			var orderItem = await _context.OrderItem.Include(o => o.Product)
													.FirstOrDefaultAsync(o => o.Id == id);

			if (orderItem == null)
			{
				return NotFound();
			}

			return OrderItemToDTO(orderItem);
		}

		// PUT: OrderItems/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutOrderItem(int id, OrderItem orderItem)
		{
			if (id != orderItem.Id)
			{
				return BadRequest();
			}

			_context.Entry(orderItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderItemExists(id))
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

		// POST: OrderItems
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<OrderItem>> PostOrderItem(OrderItem orderItem)
		{
			_context.OrderItem.Add(orderItem);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetOrderItem", new { id = orderItem.Id }, orderItem);
		}

		// DELETE: OrderItems/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteOrderItem(int id)
		{
			var orderItem = await _context.OrderItem.FindAsync(id);
			if (orderItem == null)
			{
				return NotFound();
			}

			_context.OrderItem.Remove(orderItem);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool OrderItemExists(int id)
		{
			return _context.OrderItem.Any(e => e.Id == id);
		}

		public static IEnumerable<OrderItemDTO> OrderItemToDTO(IEnumerable<OrderItem> orderItems)
		{
			var orderItemsDTO = new List<OrderItemDTO>();

			foreach (OrderItem orderItem in orderItems)
			{
				var orderItemDTO = new OrderItemDTO
				{
					Id          = orderItem.Id,
					OrderId     = orderItem.OrderId,
					ProductId   = orderItem.ProductId,
					ProductName = orderItem.Product.Name,
					Price       = orderItem.Price,
					Quantity    = orderItem.Quantity,
					Total       = orderItem.Total
				};

				orderItemsDTO.Add(orderItemDTO);
			}

			return orderItemsDTO;
		}

		public static OrderItemDTO OrderItemToDTO(OrderItem orderItem)
		{
			var orderItemDTO = new OrderItemDTO
			{
				Id          = orderItem.Id,
				OrderId     = orderItem.OrderId,
				ProductId   = orderItem.ProductId,
				ProductName = orderItem.Product.Name,
				Price       = orderItem.Price,
				Quantity    = orderItem.Quantity,
				Total       = orderItem.Total
			};


			return orderItemDTO;
		}
	}
}
