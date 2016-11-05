namespace SimpleInventory
{
	public enum NotificationType
	{
		InventoryItemAdded,
		InventoryItemRemoved,
		InventoryItemExpired
	}
	
	public class NotificationModel
	{
		public NotificationType Type;
		public string Message;
	}
}
