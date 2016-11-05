using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleInventory
{
	public class InMemoryInventoryRepository : IInventoryRepository
	{
		List<InventoryModel> inventory = new List<InventoryModel>();


		public IEnumerable<InventoryModel> Get()
		{
			return inventory;
		}

		public int ItemsWithLabel(string label)
		{
			return inventory.Where((i) => i.Label.Equals(label, StringComparison.OrdinalIgnoreCase)).Count();
		}

		public void Add(InventoryModel model)
		{
			inventory.Add(model);
		}

		public InventoryModel RemoveOldestItem(string label)
		{
			// Ignores the same label with for different types - it will remove the oldest one.
			var labelInventory = inventory
				.Where((i) => i.Label.Equals(label, StringComparison.OrdinalIgnoreCase))
				.OrderBy((arg) => arg.Expiration);
			if (labelInventory.Count() == 0)
			{
				return null;
			}

			var removed = labelInventory.First();
			inventory.Remove(removed);
			return removed;
		}
	}
}
