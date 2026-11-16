using System;

public class ScenarioBusinessIndustryProfile : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCompetitor, string> _003C_003E9__3_0;

		public static Func<SimCompetitor, string> _003C_003E9__3_1;

		internal string _003CRefreshScenario_003Eb__3_0(SimCompetitor x)
		{
			return null;
		}

		internal string _003CRefreshScenario_003Eb__3_1(SimCompetitor x)
		{
			return null;
		}
	}

	protected SimSpecialJobBusiness _currentBusinessJob;

	protected int _industryType;

	public ScenarioBusinessIndustryProfile(SimSpecialJobBusiness currentBusinessJob, int industryType)
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
