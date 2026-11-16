using System;

namespace Tracking
{
	public abstract class TempBitLife1asMigrationEvent : GameTelemetrySpecialEvent, IFirebaseGameTelemetryEvent, IGameTelemetryEvent
	{
		public static readonly DateTime StartUtc;

		public static readonly DateTime EndUtc;

		protected TempBitLife1asMigrationEvent()
			: base(default, default)
		{
		}

		public static bool IsWithinActiveWindow(DateTime utcNow)
		{
			return false;
		}

		public override bool CanSend()
		{
			return false;
		}
	}
}
