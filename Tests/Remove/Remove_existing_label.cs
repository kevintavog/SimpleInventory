using System;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Remove_existing_label : FluentTest
	{
		TestContext context = new TestContext();


		public Remove_existing_label()
		{
			Given(An_inventory_item_exists);
			When(The_item_is_removed);
			Then(The_item_is_not_in_the_inventory);
		}

		private void An_inventory_item_exists()
		{
			context.AddInventory(new InventoryModel { Type = "dog", Label = "dachsund", Expiration = DateTime.UtcNow.AddYears(3) });
		}

		private void The_item_is_removed()
		{
			context.RemoveInventoryByLabel("dachsund");
		}

		private void The_item_is_not_in_the_inventory()
		{
			var allInventory = context.GetInventory();
			allInventory.Should().HaveCount(0);
		}
	}
}
