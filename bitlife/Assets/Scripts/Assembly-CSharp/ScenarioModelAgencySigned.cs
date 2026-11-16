using System;

public class ScenarioModelAgencySigned : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__6_0;

		internal bool _003CChoiceSelectedAtIndex_003Eb__6_0(Scenario x)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobModel _modelJob;

	private readonly SimModelingAgency _agency;

	private readonly float _salary;

	public ScenarioModelAgencySigned(SimModelingAgency agency, float salary)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
