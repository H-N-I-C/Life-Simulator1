namespace Tracking
{
	public class TempBitLifeUfmCoolDown : GameTelemetrySpecialEvent
	{
		public override float CoolDownSeconds => 0f;

		public TempBitLifeUfmCoolDown()
			: base(default, default)
		{
		}
	}
}
