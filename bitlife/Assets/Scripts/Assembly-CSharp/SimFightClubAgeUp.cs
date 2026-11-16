using System;

[Serializable]
public class SimFightClubAgeUp
{
	public const string AgeupCsvFile = "FightClub/Fight Club Scenarios - Ageups";

	private int AgeLastAnimalFight__BackingField;

	private int AgeLastTimeFamilyGotConcerned__BackingField;

	private int AgeLastTimeFamilyExpressedAdmiration__BackingField;

	private int AgeLastDumbScenario__BackingField;

	public int AgeLastAnimalFight
	{
		get
		{
			return AgeLastAnimalFight__BackingField;
		}
		set
		{
			AgeLastAnimalFight__BackingField = value;
		}
	}

	public int AgeLastTimeFamilyGotConcerned
	{
		get
		{
			return AgeLastTimeFamilyGotConcerned__BackingField;
		}
		set
		{
			AgeLastTimeFamilyGotConcerned__BackingField = value;
		}
	}

	public int AgeLastTimeFamilyExpressedAdmiration
	{
		get
		{
			return AgeLastTimeFamilyExpressedAdmiration__BackingField;
		}
		set
		{
			AgeLastTimeFamilyExpressedAdmiration__BackingField = value;
		}
	}

	public int AgeLastDumbScenario
	{
		get
		{
			return AgeLastDumbScenario__BackingField;
		}
		set
		{
			AgeLastDumbScenario__BackingField = value;
		}
	}

	public Scenario TryAddAgeUpScenario(Life life)
	{
		return null;
	}

	private Scenario BuildFightClubCoachDeathScenario()
	{
		return null;
	}

	private bool IsHeroLosing(SimFightClub fightClub)
	{
		return false;
	}

	private bool IsHeroWinning(SimFightClub fightClub)
	{
		return false;
	}

	private Scenario BuildFightClubAgeUpAdmirationFamilyActivity()
	{
		return null;
	}

	private Scenario BuildFightClubAgeUpConcernFamilyActivity()
	{
		return null;
	}

	private Scenario BuildFightClubAgeUpAnimalActivity()
	{
		return null;
	}
}
