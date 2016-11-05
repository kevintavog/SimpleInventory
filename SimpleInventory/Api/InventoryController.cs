using Nancy;
using Nancy.ModelBinding;

namespace SimpleInventory
{
	public class InventoryController : NancyModule
	{
		private InventoryService service;


		public InventoryController(InventoryService inventoryService)
		{
			service = inventoryService;

			/* How will inventory be modeled in a RESTful API?

				1. /inventory
					- Everything underneath is one collection (all types / labels, all items)
					- Query parameters can be used for getting label (& type) specific info
				2. /inventory/type/label
					- Since label can be part of URL, it'd need to be escaped properly
					- GET /inventory could return info on the entire inventory
					- GET /inventory/label could return info on that label
					- Less flexible for other types of queries without resorting to query parameters

			*/


			Get["/inventory"] = p =>
			{
				var list = service.Get();
				return Response.AsJson(list);
			};

			Put["/inventory"] = p =>
			{
				var request = this.Bind<InventoryModel>();
				service.Add(request);
				return HttpStatusCode.NoContent;
			};

			Delete["/inventory"] = p =>
			{
				var request = this.Bind<InventoryModel>();
				service.Remove(request.Label);
				return HttpStatusCode.NoContent;
			};
		}
	}
}
