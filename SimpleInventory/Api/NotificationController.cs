using System;
using Nancy;

namespace SimpleInventory
{
	public class NotificationController : NancyModule
	{
		readonly INotifications notifications;


		public NotificationController(INotifications notifications)
		{
			this.notifications = notifications;

			Get["/notifications"] = p =>
			{
				var list = this.notifications.Get();
				return Response.AsJson(list);
			};
		}
	}
}
