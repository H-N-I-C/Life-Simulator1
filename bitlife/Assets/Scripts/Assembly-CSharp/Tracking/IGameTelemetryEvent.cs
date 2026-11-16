using System.Collections.Generic;

namespace Tracking
{
	public interface IGameTelemetryEvent
	{
		Dictionary<string, string> GetData();
	}
}
