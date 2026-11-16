using System;

namespace Tracking
{
	public class GameTelemetryCoolDownTimer
	{
		private readonly TimeSpan cooldown;

		private DateTime lastTime;

		public GameTelemetryCoolDownTimer(float seconds)
		{
		}

		public bool Update()
		{
			return false;
		}
	}
}
