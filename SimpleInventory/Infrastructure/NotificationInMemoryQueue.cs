using System.Collections.Concurrent;
using System.Collections.Generic;

namespace SimpleInventory
{
	public class NotificationInMemoryQueue : INotifications
	{
		private ConcurrentQueue<NotificationModel> messages = new ConcurrentQueue<NotificationModel>();


		public void Add(NotificationModel notification)
		{
			messages.Enqueue(notification);
		}

		public IEnumerable<NotificationModel> Get()
		{
			var result = new List<NotificationModel>();

			NotificationModel note;
			while (messages.TryDequeue(out note))
			{
				result.Add(note);
			}
			return result;
		}
	}
}
