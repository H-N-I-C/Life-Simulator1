public class CommandDispatcher : ILocalNotificationManager
{
	private bool _hasExecutedCommands;

	private bool _shouldRegisterNotifications;

	private bool _shouldCheckForIntent;

	public void RegisterNotifications()
	{
	}

	public void CheckForIntent()
	{
	}

	public void SetLocalNotificationsOnClose()
	{
	}

	public void UpdateNotificationsLanguage()
	{
	}

	public void ExecutePendingCommands(ILocalNotificationManager manager)
	{
	}
}
