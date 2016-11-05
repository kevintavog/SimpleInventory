using System.Collections.Generic;

namespace SimpleInventory
{
	public interface INotifications
	{
		void Add(NotificationModel notification);
		IEnumerable<NotificationModel> Get();
	}
}
