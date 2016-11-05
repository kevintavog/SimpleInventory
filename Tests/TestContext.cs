using System.Collections.Generic;
using SimpleInventory;

namespace Tests
{
	public class TestContext
	{
		private InventoryService service;
		private NotificationInMemoryQueue notifications;
		private InMemoryInventoryRepository repository;


		private NotificationInMemoryQueue Notifications
		{
			get
			{
				if (notifications == null)
				{
					notifications = new NotificationInMemoryQueue();
				}
				return notifications;
			}
		}

		private InMemoryInventoryRepository Repository
		{
			get
			{
				if (repository == null)
				{
					repository = new InMemoryInventoryRepository();
				}
				return repository;
			}
		}

		private InventoryService InventoryService
		{
			get
			{
				if (service == null)
				{
					service = new InventoryService(Repository, Notifications);
				}
				return service;
			}
		}


		public IEnumerable<InventoryModel> GetInventory()
		{
			return InventoryService.Get();
		}

		// Add directly to the repository - allows adding expired items (bypasses Domain)
		public void SeedRepository(InventoryModel model)
		{
			Repository.Add(model);
		}

		public void AddInventory(InventoryModel model)
		{
			InventoryService.Add(model);
		}

		public void RemoveInventoryByLabel(string label)
		{
			InventoryService.Remove(label);
		}


		public IEnumerable<NotificationModel> GetNotification()
		{
			return Notifications.Get();
		}

		public void ClearNotifications()
		{
			Notifications.Get();
		}
	}
}
