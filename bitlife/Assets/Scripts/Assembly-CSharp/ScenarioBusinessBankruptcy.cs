using System;

public class ScenarioBusinessBankruptcy : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimAnnualReport, double> _003C_003E9__2_0;

		internal double _003CRefreshScenario_003Eb__2_0(SimAnnualReport x)
		{
			return 0.0;
		}
	}

	protected SimBusiness _theBusiness;

	public ScenarioBusinessBankruptcy(SimBusiness theBusiness)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}
}
