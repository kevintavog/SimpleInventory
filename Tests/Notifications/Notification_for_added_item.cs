using System;
using System.Linq;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Notification_for_added_item : FluentTest
	{
		InventoryModel itemToAdd;
		TestContext context = new TestContext();

		public Notification_for_added_item()
		{
			Given(An_inventory_item);
			When(The_item_is_added);
			Then(There_is_a_notification_of_the_added_item);
		}

		private void An_inventory_item()
		{
			itemToAdd = new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(1) };
		}

		private void The_item_is_added()
		{
			context.AddInventory(itemToAdd);
		}

		private void There_is_a_notification_of_the_added_item()
		{
			var allNotifications = context.GetNotification();
			allNotifications.Should().HaveCount(1);
			allNotifications.First().Type.Should().Be(NotificationType.InventoryItemAdded);
		}
}
}
