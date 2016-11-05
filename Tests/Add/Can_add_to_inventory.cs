using Kekiri;
using SimpleInventory;
using FluentAssertions;
using System.Linq;
using System;

namespace Tests
{
	public class Can_add_to_inventory : FluentTest
	{
		InventoryModel itemToAdd;
		TestContext context = new TestContext();
		
		
		public Can_add_to_inventory()
		{
			Given(An_inventory_item);
			When(The_item_is_added);
			Then(The_item_is_in_the_inventory);
		}


		private void An_inventory_item()
		{
			itemToAdd = new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(1) };
		}

		private void The_item_is_added()
		{
			context.AddInventory(itemToAdd);
		}

		private void The_item_is_in_the_inventory()
		{
			var allInventory = context.GetInventory();
			allInventory.Should().HaveCount(1);

			var first = allInventory.First();
			first.Label.Should().Equals(itemToAdd.Label);
			first.Type.Should().Equals(itemToAdd.Type);
			first.Expiration.Should().Equals(itemToAdd.Expiration);
		}
	}
}
