using System.Collections.Generic;
using Game;

public class VampireFamiliarActivity
{
	private const int MinRelationshipToMakeFamiliarStay = 25;

	private const int FamiliarLeaveOdds = 10;

	private const int InsultRelationshipImpact = -1;

	private const int ReassureRelationshipImpact = 2;

	private const int ApologizeRelationshipImpact = 1;

	private SimVampireFamiliar Familiar__BackingField;

	private bool FamiliarComplimentBack__BackingField;

	private float FamiliarComplimentAppreciation__BackingField;

	private bool FamiliarRequestConversion__BackingField;

	private DataVampireFamiliarThreat _threatData;

	private readonly BracketParser _parser;

	private DataVampireFamiliarCommand _command;

	public SimVampireFamiliar Familiar
	{
		get
		{
			return Familiar__BackingField;
		}
		private set
		{
			Familiar__BackingField = value;
		}
	}

	public bool FamiliarComplimentBack
	{
		get
		{
			return FamiliarComplimentBack__BackingField;
		}
		private set
		{
			FamiliarComplimentBack__BackingField = value;
		}
	}

	public float FamiliarComplimentAppreciation
	{
		get
		{
			return FamiliarComplimentAppreciation__BackingField;
		}
		private set
		{
			FamiliarComplimentAppreciation__BackingField = value;
		}
	}

	public bool FamiliarRequestConversion
	{
		get
		{
			return FamiliarRequestConversion__BackingField;
		}
		private set
		{
			FamiliarRequestConversion__BackingField = value;
		}
	}

	private bool IsFreshCompliment => false;

	public string CommandBodyString => null;

	public string CommandNarrativeString => null;

	public int CommandEnjoyment => 0;

	public string FamiliarThreatBodyString => null;

	public string FamiliarThreatNarrativeString => null;

	public BracketParser BracketParser => null;

	public static Dictionary<string, string> GetFamiliarBrackets(SimVampireFamiliar familiar, string yourOrMy)
	{
		return null;
	}

	public VampireFamiliarActivity(SimVampireFamiliar familiar)
	{
	}

	public string GetRandomFamiliarCompliment()
	{
		return null;
	}

	public string GetRandomFamiliarComplimentBack()
	{
		return null;
	}

	public void ComplimentFamiliar()
	{
	}

	private void CalculateFamiliarComplimentAppreciation()
	{
	}

	private void UpdateComplimentVariables()
	{
	}

	private void WillFamiliarRequestConversion()
	{
	}

	public bool WillFamiliarLeaveHero()
	{
		return false;
	}

	public void ThreatenFamiliar()
	{
	}

	public void InsultFamiliar()
	{
	}

	public void ReasureFamiliar()
	{
	}

	public void ApologizeForRefusingSnack()
	{
	}

	public void GiveCommand()
	{
	}

	public bool TryConvertFamiliar()
	{
		return false;
	}
}
