namespace Genesis.Telemetry
{
	public enum GenesisTelemetrySendDecision
	{
		Send = 0,
		DropExpired = 1,
		DropNotYetActive = 2,
		DropIneligibleStatus = 3,
		NotRegistered = 4
	}
}
