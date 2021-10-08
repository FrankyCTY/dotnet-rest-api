using Catalog.Api.Dtos;
using Catalog.Api.Entities;

namespace Catalog.Api
{
	public static class DtoExtensions {
		public static ItemDto ToItemDto(this Item item)
		{
			return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
		}
	}
}