using Tracking;

namespace Genesis.Telemetry
{
	public interface IGenesisTelemetryEvent : IGameTelemetryEvent
	{
		string EventName { get; }
	}
}
