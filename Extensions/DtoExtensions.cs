using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
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