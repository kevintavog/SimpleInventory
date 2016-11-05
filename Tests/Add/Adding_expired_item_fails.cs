using Kekiri;
using SimpleInventory;
using FluentAssertions;
using System;

namespace Tests
{
	public class Adding_expired_item_fails : FluentTest
	{
		InventoryModel itemToAdd;
		TestContext context = new TestContext();
		
		
		public Adding_expired_item_fails()
		{
			Given(An_expired_inventory_item);
			When(The_item_is_added).Throws();
			Then(An_error_is_returned)
				.And(The_item_is_not_added_to_the_inventory);
		}


		private void An_expired_inventory_item()
		{
			itemToAdd = new InventoryModel { Type = "dog", Label = "beagle", Expiration = DateTime.UtcNow.AddYears(-1) };
		}

		private void The_item_is_added()
		{
			context.AddInventory(itemToAdd);
		}

		private void An_error_is_returned()
		{
			Catch<InvalidExpirationParameter>();
		}

		private void The_item_is_not_added_to_the_inventory()
		{
			var allInventory = context.GetInventory();
			allInventory.Should().HaveCount(0);
		}
	}
}
