using System;
using Game;

public class ScenarioOutdoorOffRoadEncounter : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string, string> _003C_003E9__11_0;

		internal string _003CHelpOutButAttacked_003Eb__11_0(string i, string j)
		{
			return null;
		}
	}

	private DataStreetPerson _PersonInNeed;

	private DataOutdoorOffRoadingEncounter _Encounter;

	private readonly SimOutdoorOffRoadingActivity _offRoadingActivity;

	private string _narrative;

	public ScenarioOutdoorOffRoadEncounter(SimOutdoorOffRoadingActivity offRoadingActivity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void RidePast()
	{
	}

	private void RidePastHitTree()
	{
	}

	private void RidePastNoIncident()
	{
	}

	private void HelpOut()
	{
	}

	private void HelpOutButAttacked()
	{
	}

	private void HelpOutButFailed()
	{
	}

	private void HelpOutRewarded()
	{
	}

	private void CallPolice()
	{
	}

	private void Mug()
	{
	}

	private void SuccessfulMugging()
	{
	}

	private void UnsuccessfulMugging()
	{
	}

	private bool WillpowerCheck(int willPower)
	{
		return false;
	}

	private bool KarmaCheck(int karma)
	{
		return false;
	}

	private int GetMuggingDollarAmount(int score)
	{
		return 0;
	}

	private string GetGenderPronoun(int gender, bool possessive = false)
	{
		return null;
	}

	private string GetGenderPronoun(GENDER gender, bool possessive = false)
	{
		return null;
	}

	private string LocationString()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
