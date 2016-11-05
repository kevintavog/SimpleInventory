using System;
using Nancy;
using Nancy.Bootstrapper;

namespace SimpleInventory
{
	// Using Nancy, this allows SimpleInventoryException to be returned as a bad request and all other errors
	// to be internal server error. And also logs the exception on the server, not allowing the client to see
	// the stack.
	public class ApiStartup : IApplicationStartup
	{
		public void Initialize(IPipelines pipelines)
		{
			pipelines.OnError += (ctx, ex) =>
			{
				Console.WriteLine($"Exception: {ex}");
				var six = ex as SimpleInventoryException;
				if (six != null)
				{
					var r = (Response)ex.Message;
					r.StatusCode = HttpStatusCode.BadRequest;
					return r;
				}

				var s = (Response)ex.Message;
				s.StatusCode = HttpStatusCode.InternalServerError;
				return s;
			};
		}
	}
}
