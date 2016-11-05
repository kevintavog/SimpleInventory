using System;
namespace SimpleInventory
{
	public class SimpleInventoryException : Exception
	{
		public SimpleInventoryException(string message) : base(message)
		{
		}
	}

	public class ItemNotFound : SimpleInventoryException
	{
		public ItemNotFound(string label) : base($"No inventory item: {label}")
		{
		}
	}

	public class ItemNotInStock : SimpleInventoryException
	{
		public ItemNotInStock(string label) : base($"Item not in stock: {label}")
		{
		}
	}

	public class ItemExpired : SimpleInventoryException
	{
		public ItemExpired(InventoryModel item) 
			: base($"Expired item removed from inventory: {item.Label}, {item.Type}, {item.Expiration}")
		{
		}
	}

	public class InvalidExpirationParameter : SimpleInventoryException
	{
		public InvalidExpirationParameter(InventoryModel item)
			: base($"The expired date is in the past: {item.Expiration}")
		{
		}
	}
}
