using System;

public class ScenarioFinancialInsiderBuy : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimStock, bool> _003C_003E9__5_0;

		internal bool _003CRefreshScenario_003Eb__5_0(SimStock x)
		{
			return false;
		}
	}

	protected SimPerson _person;

	protected DataFinancialInsiderBuyScenario _insiderKnowledge;

	protected SimStock _stock;

	protected BracketParser _parser;

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public new static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}
}
