using System.Collections.Generic;
using Backend.Authentication;

public class LocalNotificationABTest
{
	private static LocalNotificationABTest _inst;

	private readonly LocalNotificationV1Config _config;

	private LocalNotificationV1Manager _v1Manager;

	private CommandDispatcher _commandDispatcher;

	public static LocalNotificationABTest Inst => null;

	public ILocalNotificationManager Manager => null;

	private static void Init()
	{
	}

	private LocalNotificationABTest()
	{
	}

	public void AddLastRespondedNotificationToAppStartData(Dictionary<string, string> appStartData)
	{
	}

	private static void ConfigureABTestByConfidence(GetConfigResponse config)
	{
	}
}
