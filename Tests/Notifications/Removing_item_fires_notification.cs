using System;
using System.Linq;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Removing_item_fires_notification : FluentTest
	{
		TestContext context = new TestContext();


		public Removing_item_fires_notification()
		{
			Given(An_inventory_item_exists);
			When(The_item_is_removed);
			Then(There_is_a_notification_of_the_removed_item);
		}

		private void An_inventory_item_exists()
		{
			context.AddInventory(new InventoryModel { Type = "dog", Label = "dachsund", Expiration = DateTime.UtcNow.AddYears(3) });
			context.ClearNotifications();
		}

		private void The_item_is_removed()
		{
			context.RemoveInventoryByLabel("dachsund");
		}

		private void There_is_a_notification_of_the_removed_item()
		{
			var allNotifications = context.GetNotification();
			allNotifications.Should().HaveCount(1);
			allNotifications.First().Type.Should().Be(NotificationType.InventoryItemRemoved);
		}
	}
}
