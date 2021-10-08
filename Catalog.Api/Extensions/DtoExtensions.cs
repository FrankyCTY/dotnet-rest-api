using Catalog.Api.Dtos;
using Catalog.Api.Entities;

namespace Catalog.Api
{
	public static class DtoExtensions {
		public static ItemDto ToItemDto(this Item item)
		{
			return new ItemDto
			{
					Id = item.Id,
					Name = item.Name,
					Price = item.Price,
					CreatedDate = item.CreatedDate
			};
		}
	}
}