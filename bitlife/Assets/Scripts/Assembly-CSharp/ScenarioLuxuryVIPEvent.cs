using System;
using System.Collections.Generic;
using Game;

public class ScenarioLuxuryVIPEvent : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimCelebrityLover, string> _003C_003E9__11_0;

		public static Func<string, string, string> _003C_003E9__11_1;

		internal string _003CGetGuestListString_003Eb__11_0(SimCelebrityLover t)
		{
			return null;
		}

		internal string _003CGetGuestListString_003Eb__11_1(string i, string j)
		{
			return null;
		}
	}

	private string _narrative;

	private SimLuxury _luxury;

	private readonly LUXURY_VIP_EVENT_TYPE _eventType;

	private readonly int _count;

	private readonly float _cost;

	private Scenario _feedbackScenario;

	private List<SimCelebrityLover> _guestList;

	public ScenarioLuxuryVIPEvent(LUXURY_VIP_EVENT_TYPE eventType, float cost, int count = 0)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int index)
	{
	}

	private (string, Dictionary<string, string>) GetTitleForEventType()
	{
		return default;
	}

	private string GetGuestListString()
	{
		return null;
	}

	private List<SimCelebrityLover> GetWeddingList()
	{
		return null;
	}

	private void ImpactHero()
	{
	}

	public SimCelebrityLover GetLoverCandidateWithMinAgeAndMaxAgeAndGenderAndCelebrityType(int ageMin, int ageMax, int gender, string celebrityType, int sexuality)
	{
		return null;
	}
}
