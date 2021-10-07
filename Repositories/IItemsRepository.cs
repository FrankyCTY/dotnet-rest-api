using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IItemsRepository
    {
		Item GetItem(Guid id);
		IEnumerable<Item> GetItems();

    }
}