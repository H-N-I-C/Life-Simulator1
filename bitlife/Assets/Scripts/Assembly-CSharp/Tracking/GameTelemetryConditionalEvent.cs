namespace Tracking
{
	public abstract class GameTelemetryConditionalEvent : GameTelemetryEvent
	{
		public abstract float CoolDownSeconds { get; }

		public abstract bool CanSend();
	}
}
