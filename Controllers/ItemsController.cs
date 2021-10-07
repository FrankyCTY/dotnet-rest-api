using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Dtos;
using Catalog.Entities;
using Catalog.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemsController : ControllerBase
	{
		private readonly IItemsRepository _repository;

		public ItemsController(IItemsRepository repository) {
			_repository = repository;
		}

	  // GET /items
		[HttpGet]
		public IEnumerable<ItemDto> GetItems()
		{
				var items = _repository.GetItems().Select(item => item.ToItemDto());
				return items;
		}

		// GET /items/id
		[HttpGet("{id}")]
		public ActionResult<ItemDto> GetItem(Guid id)
		{
			var item = _repository.GetItem(id);

			if (item is null)
			{
				return NotFound();
			}

			return Ok(item.ToItemDto());
		}

		// POST /items
		[HttpPost]
		public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
		{
			Item item = new() {
				Id = Guid.NewGuid(),
				Name = itemDto.Name,
				Price = itemDto.Price,
				CreatedDate = DateTimeOffset.UtcNow
			};

			_repository.CreateItem(item);

			return CreatedAtAction(nameof(GetItem), new {id = item.Id}, item.ToItemDto());
		}

		// PUT /items/
		[HttpPut("{id}")]
		public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
		{
			var existingItem = _repository.GetItem(id);

			if (existingItem is null)
			{
				return NotFound();
			}

			Item updatedItem = existingItem with {
				Name = itemDto.Name,
				Price = itemDto.Price,
			};

			_repository.UpdateItem(updatedItem);
			return NoContent();
		}

		[HttpDelete("{id}")]
		public ActionResult DeleteItem(Guid id)
		{
			var existingItem = _repository.GetItem(id);

			if(existingItem is null)
			{
				return NotFound();
			}

			_repository.DeleteItem(id);
			return NoContent();
		}
	}
}