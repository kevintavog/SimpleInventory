using System;
using System.Linq;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Notification_for_expired_item : FluentTest
	{
		TestContext context = new TestContext();

		public Notification_for_expired_item()
		{
			Given(An_expired_inventory_item_exists);
			When(The_item_is_removed).Throws();
			Then(There_is_a_notification_of_the_expired_item);
		}

		private void An_expired_inventory_item_exists()
		{
			context.SeedRepository(new InventoryModel { Type = "dog", Label = "dachsund", Expiration = DateTime.UtcNow.AddDays(-1) });
		}

		private void The_item_is_removed()
		{
			context.RemoveInventoryByLabel("dachsund");
		}

		private void There_is_a_notification_of_the_expired_item()
		{
			Catch<ItemExpired>();

			var allNotifications = context.GetNotification();
			allNotifications.Should().HaveCount(1);
			allNotifications.First().Type.Should().Be(NotificationType.InventoryItemExpired);
		}
}
}
