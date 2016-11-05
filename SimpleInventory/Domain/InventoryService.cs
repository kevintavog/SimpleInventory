using System;
using System.Collections.Generic;

namespace SimpleInventory
{
	public class InventoryService
	{
		readonly IInventoryRepository repository;
		readonly INotifications notifications;

		public InventoryService(IInventoryRepository repository, INotifications notifications)
		{
			this.notifications = notifications;
			this.repository = repository;
		}
		
		public IEnumerable<InventoryModel> Get()
		{
			return repository.Get();
		}

		public void Add(InventoryModel item)
		{
			if (item.Expiration < DateTime.UtcNow)
			{
				throw new InvalidExpirationParameter(item);
			}

			repository.Add(item);

			notifications.Add(new NotificationModel { 
				Type = NotificationType.InventoryItemAdded, 
				Message = $"Added {item.Type}, {item.Label}, {item.Expiration}" });
		}

		public void Remove(string label)
		{
			int matchingLabels = repository.ItemsWithLabel(label);
			if (matchingLabels == 0)
			{
				throw new ItemNotInStock(label);
			}

			// What should happen if more than one Type has the same label?

			var item = repository.RemoveOldestItem(label);
			if (item.Expiration < DateTime.UtcNow)
			{
				notifications.Add(new NotificationModel
				{
					Type = NotificationType.InventoryItemExpired,
					Message = $"Item expired: {item.Type}, {item.Label}, {item.Expiration}"
				});
				throw new ItemExpired(item);
			}

			notifications.Add(new NotificationModel
			{
				Type = NotificationType.InventoryItemRemoved,
				Message = $"Removed {item.Type}, {item.Label}, {item.Expiration}"
			});
		}
	}
}
