using System;
using Game;

public class CouncilPartyActivity
{
	public enum PartyOutcome
	{
		nothing = 0,
		bloodOffering = 1,
		hookup = 2
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimVampireGoal, bool> _003C_003E9__33_0;

		internal bool _003CCreateOfferingPerson_003Eb__33_0(SimVampireGoal goal)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass31_0
	{
		public SimHero hero;

		internal bool _003CApplyImpactOnThrowingParty_003Eb__0(ScenarioTimestamp q)
		{
			return false;
		}
	}

	private const int ChanceOfNothingHappening = 55;

	private const int ChanceOfBloodOffering = 75;

	private const int MinOfferingAge = 18;

	private const int MaxOfferingAge = 40;

	private const int MaxOfferingAgeRandom = 50;

	private const int ChanceOfferingCareerBeGoalCareer = 75;

	private const int OfferingDrainedHappinessImpact = 2;

	private const int OfferingDrainedNotorietyImpact = 2;

	private PartyOutcome Outcome__BackingField;

	private DataVampireCouncilPartyString CouncilPartyString__BackingField;

	private string PartyType__BackingField;

	private float Enjoyment__BackingField;

	private SimPerson Offering__BackingField;

	public PartyOutcome Outcome
	{
		get
		{
			return Outcome__BackingField;
		}
		private set
		{
			Outcome__BackingField = value;
		}
	}

	public DataVampireCouncilPartyString CouncilPartyString
	{
		get
		{
			return CouncilPartyString__BackingField;
		}
		private set
		{
			CouncilPartyString__BackingField = value;
		}
	}

	public string PartyType
	{
		get
		{
			return PartyType__BackingField;
		}
		private set
		{
			PartyType__BackingField = value;
		}
	}

	public float Enjoyment
	{
		get
		{
			return Enjoyment__BackingField;
		}
		private set
		{
			Enjoyment__BackingField = value;
		}
	}

	public SimPerson Offering
	{
		get
		{
			return Offering__BackingField;
		}
		private set
		{
			Offering__BackingField = value;
		}
	}

	public CouncilPartyActivity(DataVampireCouncilPartyType partyType)
	{
	}

	private void DefineOutcome()
	{
	}

	public void ApplyImpactOnThrowingParty()
	{
	}

	private int CalculateImpact(int maxImpact)
	{
		return 0;
	}

	private void CreateOfferingPerson()
	{
	}

	public void ApplyOfferDrainedImpact()
	{
	}
}
