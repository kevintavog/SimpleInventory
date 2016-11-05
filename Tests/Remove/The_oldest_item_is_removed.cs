using System;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class The_oldest_item_is_removed : FluentTest
	{
		TestContext context = new TestContext();

		public The_oldest_item_is_removed()
		{
			Given(Multiple_inventory_item_exist);
			When(The_item_is_removed);
			Then(The_item_is_not_in_the_inventory);
		}

		private void Multiple_inventory_item_exist()
		{
			context.AddInventory(new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(3) });
			context.AddInventory(new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(1) }); // <-- Oldest one to be removed
			context.AddInventory(new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(2) });
		}

		private void The_item_is_removed()
		{
			context.RemoveInventoryByLabel("beagle");
		}

		private void The_item_is_not_in_the_inventory()
		{
			var allInventory = context.GetInventory();
			allInventory.Should().HaveCount(2);

			allInventory.Should().NotContain(x => x.Expiration.Year == DateTime.UtcNow.AddYears(1).Year);
		}
	}
}
