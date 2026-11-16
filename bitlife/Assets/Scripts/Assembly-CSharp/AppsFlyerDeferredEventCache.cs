using System.Collections.Generic;

public sealed class AppsFlyerDeferredEventCache
{
	private readonly Queue<AppsFlyerDeferredEvent> _events;

	public void Enqueue(string eventName, bool devMode, Dictionary<string, string> eventValues)
	{
	}

	public bool TryDequeue(out AppsFlyerDeferredEvent deferredEvent)
	{
		deferredEvent = default;
		return false;
	}
}
