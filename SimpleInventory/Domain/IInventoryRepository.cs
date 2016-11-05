using System.Collections.Generic;

namespace SimpleInventory
{
	public interface IInventoryRepository
	{
		IEnumerable<InventoryModel> Get();
		int ItemsWithLabel(string label);
		void Add(InventoryModel model);
		InventoryModel RemoveOldestItem(string label);
	}
}
