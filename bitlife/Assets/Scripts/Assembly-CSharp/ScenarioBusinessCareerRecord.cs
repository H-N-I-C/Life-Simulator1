using System;

public class ScenarioBusinessCareerRecord : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimAnnualReport, double> _003C_003E9__3_0;

		public static Func<SimAnnualReport, double> _003C_003E9__3_1;

		public static Func<SimAnnualReport, double> _003C_003E9__3_2;

		internal double _003CRefreshScenario_003Eb__3_0(SimAnnualReport x)
		{
			return 0.0;
		}

		internal double _003CRefreshScenario_003Eb__3_1(SimAnnualReport x)
		{
			return 0.0;
		}

		internal double _003CRefreshScenario_003Eb__3_2(SimAnnualReport x)
		{
			return 0.0;
		}
	}

	protected SimBusiness _theBusiness;

	protected bool _isCurrentBusiness;

	public ScenarioBusinessCareerRecord(SimBusiness theBusiness)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
