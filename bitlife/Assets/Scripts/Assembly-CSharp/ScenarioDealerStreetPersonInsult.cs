using System;

public class ScenarioDealerStreetPersonInsult : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__4_0;

		internal string _003CRefreshScenario_003Eb__4_0(string i, string j)
		{
			return null;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	public ScenarioDealerStreetPersonInsult(SimDealerStreetPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void DismissSelected()
	{
	}
}
