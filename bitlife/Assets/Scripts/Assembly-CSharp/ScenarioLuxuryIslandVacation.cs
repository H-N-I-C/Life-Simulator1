using System.Collections;
using System.Collections.Generic;

public class ScenarioLuxuryIslandVacation : Scenario
{
	private string _narrative;

	private SimLuxury _luxury;

	private readonly SimLuxuryIsland _island;

	private List<SimPerson> _guests;

	private List<SimPerson> _peopleWhoDontWantToGo;

	private string _privateJetPrice;

	private string _privateYachtPrice;

	private string _privateHelicopterPrice;

	public ScenarioLuxuryIslandVacation(SimLuxuryIsland island)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void OpportunityToHitTheRocks(bool skipSound)
	{
	}

	public static List<SimPerson> FamilyMemberArray()
	{
		return null;
	}

	public static List<SimPerson> FriendList()
	{
		return null;
	}

	public static List<SimPerson> ExLoverList()
	{
		return null;
	}

	public static List<SimPerson> ClassmateList()
	{
		return null;
	}

	public static List<SimPerson> CoWorkerList()
	{
		return null;
	}

	public override string EmojiString()
	{
		return null;
	}
}
