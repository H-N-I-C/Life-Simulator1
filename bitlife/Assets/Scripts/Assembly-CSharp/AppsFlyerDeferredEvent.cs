using System.Collections.Generic;

public struct AppsFlyerDeferredEvent
{
	public readonly string EventName;

	public readonly bool DevMode;

	public readonly Dictionary<string, string> EventValues;

	public AppsFlyerDeferredEvent(string eventName, bool devMode, Dictionary<string, string> eventValues)
	{
		EventName = null;
		DevMode = false;
		EventValues = null;
	}
}
