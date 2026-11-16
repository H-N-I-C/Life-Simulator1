public interface ILocalNotificationAdapter
{
	void RegisterNotifications();

	void ScheduleNotification(LocalNotificationV1 notification);

	void CancelAllNotifications();

	string GetLastRespondedDataJson();

	NOTIFICATION_PERMISSION GetNotificationPermission();
}
