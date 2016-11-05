using System;
using FluentAssertions;
using Kekiri;
using SimpleInventory;

namespace Tests
{
	public class Remove_with_multiple_matching_labels : FluentTest
	{
		TestContext context = new TestContext();

		public Remove_with_multiple_matching_labels()
		{
			Given(Multiple_labels_with_different_types);
			When(The_label_is_removed);
			Then(The_request_);
		}

		private void Multiple_labels_with_different_types()
		{
			context.AddInventory(new InventoryModel { Type = "dog", Label = "fido", Expiration = DateTime.UtcNow.AddYears(1) });
			context.AddInventory(new InventoryModel { Type = "primate", Label = "fido", Expiration = DateTime.UtcNow.AddYears(1) });
		}

		private void The_label_is_removed()
		{
			context.RemoveInventoryByLabel("fido");
		}

		private void The_request_()
		{
			// What should happen in this case? Should it fail? Should it succeed by removing either of the items?
		}
	}
}
