using System.Collections.Generic;

public class HousePartyActivity
{
	private const string BecauseTag = "<because>";

	private readonly SimHero Hero__BackingField;

	private HousePartyPermissionStatus PermissionStatus__BackingField;

	private readonly SimOccupationStudent _occupation;

	private readonly Life _life;

	private readonly BracketParser _parser;

	private readonly Dictionary<string, string> _bracketDictionary;

	private string PartySize__BackingField;

	private string RowdinessLevel__BackingField;

	private int _numberOfGuests;

	public SimHero Hero => Hero__BackingField;

	public HousePartyPermissionStatus PermissionStatus
	{
		get
		{
			return PermissionStatus__BackingField;
		}
		private set
		{
			PermissionStatus__BackingField = value;
		}
	}

	public string PartySize
	{
		get
		{
			return PartySize__BackingField;
		}
		private set
		{
			PartySize__BackingField = value;
		}
	}

	public string RowdinessLevel
	{
		get
		{
			return RowdinessLevel__BackingField;
		}
		private set
		{
			RowdinessLevel__BackingField = value;
		}
	}

	public bool IsOrphan()
	{
		return false;
	}

	public bool WillParentsAllowParty()
	{
		return false;
	}

	public void ParentsAllow()
	{
	}

	public float GetPopularity()
	{
		return 0f;
	}

	public void ImpactHappiness(int impact)
	{
	}

	public void ImpactPopularity(int impact)
	{
	}

	public void ImpactFamilyRelationship(int impact)
	{
	}

	public void SetHousePartyPermission(HousePartyPermissionStatus permissionStatus)
	{
	}

	public void IncrementPartyCount()
	{
	}

	public SimPerson GenerateNewFriend()
	{
		return null;
	}

	public bool CanThrowParty()
	{
		return false;
	}

	public void SetPartyPlan(string size, string rowdiness)
	{
	}

	public Scenario GetPartyFeedback()
	{
		return null;
	}

	private HousePartyOutcomeScenario GetRandomOutcome()
	{
		return HousePartyOutcomeScenario.Unknown;
	}

	private Scenario GetScenarioByOutcome(HousePartyOutcomeScenario outcome)
	{
		return null;
	}

	private HousePartyOutcome CalculatePartyOutcome()
	{
		return default;
	}

	private int CalculateEnjoymentScore()
	{
		return 0;
	}

	public List<SimPerson> GetParentsList()
	{
		return null;
	}

	private int CalculateGuestCount()
	{
		return 0;
	}

	public void LogNarrative(string narrative)
	{
	}

	public string ParseBody(string body)
	{
		return null;
	}

	private string FormatBecause()
	{
		return null;
	}
}
