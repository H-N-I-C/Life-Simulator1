using System;

public class ScenarioAstronautTransmissionIncoming : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDiscovery, bool> _003C_003E9__4_0;

		public static Func<SimDiscovery, bool> _003C_003E9__4_1;

		internal bool _003CRefreshScenario_003Eb__4_0(SimDiscovery x)
		{
			return false;
		}

		internal bool _003CRefreshScenario_003Eb__4_1(SimDiscovery x)
		{
			return false;
		}
	}

	private readonly SimTransmission _transmission;

	private readonly SimSpecialJobAstronaut _special;

	private string _narrative;

	public ScenarioAstronautTransmissionIncoming(SimTransmission transmission)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}
}
