using System;

public class ScenarioBusinessJobOffer : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimProduct, double> _003C_003E9__4_0;

		internal double _003CRefreshScenario_003Eb__4_0(SimProduct x)
		{
			return 0.0;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public DataEmployeeType employeeType;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(SimEmployee x)
		{
			return false;
		}
	}

	protected SimBusiness _theBusiness;

	protected bool _eligibleForRaise;

	public ScenarioBusinessJobOffer()
	{
	}

	public ScenarioBusinessJobOffer(SimBusiness theBusiness)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public static int GetBusinessTierForJobOffer()
	{
		return 0;
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public override string EmojiString()
	{
		return null;
	}
}
