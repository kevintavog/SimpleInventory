using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Remove_from_empty_inventory_returns_error : FluentTest
	{
		TestContext context = new TestContext();

		public Remove_from_empty_inventory_returns_error()
		{
			Given(An_empty_inventory);
			When(An_item_is_removed).Throws();
			Then(The_request_fails);
		}

		private void An_empty_inventory()
		{
		}
	
		private void An_item_is_removed()
		{
			context.RemoveInventoryByLabel("primate");
		}

		private void The_request_fails()
		{
			Catch<ItemNotInStock>();
		}
	}
}
