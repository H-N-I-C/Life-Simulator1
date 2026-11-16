using System;
using Game;

public class ScenarioVampireDrinkBloodDrainedFeedback : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<DataVampireBodyDumpColor, string> _003C_003E9__7_1;

		internal string _003CRefreshScenario_003Eb__7_1(DataVampireBodyDumpColor dump)
		{
			return null;
		}
	}

	private string _narrative;

	private readonly SimPerson _person;

	private string _location;

	private readonly BracketParser _bracketParser;

	private readonly DrinkBloodActivity _activity;

	private readonly HeroKillingAnNPC _heroKillingAnNPC;

	public ScenarioVampireDrinkBloodDrainedFeedback(DrinkBloodActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}

	private bool _003CRefreshScenario_003Eb__7_0(DataVampireBodyDumpColor dump)
	{
		return false;
	}
}
