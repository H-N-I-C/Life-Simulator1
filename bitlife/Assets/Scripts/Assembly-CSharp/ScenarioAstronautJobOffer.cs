using System;

public class ScenarioAstronautJobOffer : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<Scenario, bool> _003C_003E9__10_0;

		internal bool _003CAcceptJobOffer_003Eb__10_0(Scenario x)
		{
			return false;
		}
	}

	private readonly SimSpecialJobAstronaut _specialJob;

	private readonly DataAstronautCountries _agency;

	private string _narrative;

	private readonly int _rank;

	private readonly float _salary;

	private readonly DataAstronautCareerRank _rankData;

	private readonly string _rankString;

	public ScenarioAstronautJobOffer(SimSpecialJobAstronaut specialJob, int rank, DataAstronautCountries agencyData, float salary)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void AcceptJobOffer()
	{
	}
}
