using System;

namespace Tracking
{
	public class TempBitLifeUfm : GameTelemetrySpecialEvent
	{
		public static DateTime StartUtc;

		public static DateTime EndUtc;

		public TempBitLifeUfm()
			: base(default, default)
		{
		}
	}
}
