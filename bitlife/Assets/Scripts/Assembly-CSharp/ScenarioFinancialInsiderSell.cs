using System;

public class ScenarioFinancialInsiderSell : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimInvestment, bool> _003C_003E9__8_0;

		public static Func<SimInvestment, bool> _003C_003E9__8_1;

		public static Func<SimInvestment, double> _003C_003E9__8_2;

		internal bool _003CMostApplicableInvestment_003Eb__8_0(SimInvestment x)
		{
			return false;
		}

		internal bool _003CMostApplicableInvestment_003Eb__8_1(SimInvestment x)
		{
			return false;
		}

		internal double _003CMostApplicableInvestment_003Eb__8_2(SimInvestment x)
		{
			return 0.0;
		}
	}

	protected SimPerson _person;

	protected DataFinancialInsiderSellScenario _insiderKnowledge;

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

	public static SimInvestment MostApplicableInvestment()
	{
		return null;
	}
}
