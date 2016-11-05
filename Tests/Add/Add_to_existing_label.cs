using System;
using System.Linq;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Add_to_existing_label : FluentTest
	{
		InventoryModel itemToAdd;
		TestContext context = new TestContext();


		public Add_to_existing_label()
		{
			Given(An_inventory_item)
				.And(Existing_inventory_items);
			When(The_item_is_added);
			Then(The_item_is_in_the_inventory);
		}

		private void An_inventory_item()
		{
			itemToAdd = new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(1) };
		}

		private void Existing_inventory_items()
		{
			context.AddInventory(new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(3) });
		}

		private void The_item_is_added()
		{
			context.AddInventory(itemToAdd);
		}

		private void The_item_is_in_the_inventory()
		{
			var allInventory = context.GetInventory();
			allInventory.Should().HaveCount(2);

			var second = allInventory.Skip(1).First();
			second.Label.Should().Equals(itemToAdd.Label);
			second.Type.Should().Equals(itemToAdd.Type);
			second.Expiration.Should().Equals(itemToAdd.Expiration);
		}
	}
}
