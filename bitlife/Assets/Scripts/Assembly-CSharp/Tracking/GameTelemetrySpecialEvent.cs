using System;

namespace Tracking
{
	public abstract class GameTelemetrySpecialEvent : GameTelemetryConditionalEvent
	{
		protected readonly DateTime startUtc;

		protected readonly DateTime endUtc;

		public override float CoolDownSeconds => 0f;

		public GameTelemetrySpecialEvent(DateTime startUtc, DateTime endUtc)
		{
		}

		public override bool CanSend()
		{
			return false;
		}
	}
}
