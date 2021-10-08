using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Api.Dtos;
using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Catalog.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemsController : ControllerBase
	{
		private readonly IItemsRepository _repository;
		private readonly ILogger<ItemsController> _logger;

		public ItemsController(IItemsRepository repository, ILogger<ItemsController> logger) {
			_repository = repository;
			_logger = logger;
		}

	  // GET /items
		[HttpGet]
		public async Task<IEnumerable<ItemDto>> GetItemsAsync(string nameToMatch = null)
		{
				var items = (await _repository.GetItemsAsync())
				.Select(item => item.ToItemDto());

				if(!string.IsNullOrWhiteSpace(nameToMatch))
				{
					items = items.Where(item => item.Name.Contains(nameToMatch, StringComparison.OrdinalIgnoreCase));
				}

				_logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {items}");

				return items;
		}

		// GET /items/id
		[HttpGet("{id}")]
		public async Task<ActionResult<ItemDto>> GetItemAsync(Guid id)
		{
			var item = await _repository.GetItemAsync(id);

			if (item is null)
			{
				return NotFound();
			}

			return Ok(item.ToItemDto());
		}

		// POST /items
		[HttpPost]
		public async Task<ActionResult<ItemDto>> CreateItemAsync(CreateItemDto itemDto)
		{
			Item item = new() {
				Id = Guid.NewGuid(),
				Name = itemDto.Name,
				Description = itemDto.Description,
				Price = itemDto.Price,
				CreatedDate = DateTimeOffset.UtcNow
			};

			await _repository.CreateItemAsync(item);

			return CreatedAtAction(nameof(GetItemAsync), new {id = item.Id}, item.ToItemDto());
		}

		// PUT /items/
		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateItemAsync(Guid id, UpdateItemDto itemDto)
		{
			var existingItem = await _repository.GetItemAsync(id);

			if (existingItem is null)
			{
				return NotFound();
			}
			
			existingItem.Name = itemDto.Name;
			existingItem.Price = itemDto.Price;

			await _repository.UpdateItemAsync(existingItem);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteItemAsync(Guid id)
		{
			var existingItem = await _repository.GetItemAsync(id);

			if(existingItem is null)
			{
				return NotFound();
			}

			await _repository.DeleteItemAsync(id);
			return NoContent();
		}
	}
}