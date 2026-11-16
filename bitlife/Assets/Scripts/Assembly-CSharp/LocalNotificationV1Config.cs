using Backend.Authentication;

public class LocalNotificationV1Config
{
	public const int OneHourInSeconds = 3600;

	public const int OneDayInSeconds = 86400;

	private const int MaxPerDay = 2;

	private const int IncludingToday = 1;

	private const int IosMaxPendingNotifications = 64;

	private const int IosSafePendingNotifications = 63;

	public const int MaxDays = 30;

	public bool IsABTestAssigned;

	public bool V1Enabled;

	public string GeneralTimeOfDay;

	public string BitPassTimeOfDay;

	public int SecondsPerDay;

	public int ScheduleMaxDays;

	public int DebounceSeconds;

	public int OffsetDaysLeftInBitPass;

	public NOTIFICATION_PERMISSION Permission;

	public bool ShouldSchedule;

	public void ConfigureABTestByConfidence(GetConfigResponse config)
	{
	}

	public override string ToString()
	{
		return null;
	}
}
