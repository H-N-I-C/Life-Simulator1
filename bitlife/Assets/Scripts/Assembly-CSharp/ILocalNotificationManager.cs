public interface ILocalNotificationManager
{
	void RegisterNotifications();

	void CheckForIntent();

	void SetLocalNotificationsOnClose();

	void UpdateNotificationsLanguage();
}
