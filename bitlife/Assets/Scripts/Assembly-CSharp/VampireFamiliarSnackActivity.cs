using System;
using System.Collections.Generic;
using Game;

public class VampireFamiliarSnackActivity
{
	public enum ConfusedFamiliarAnswer
	{
		Insult = 0,
		Remind = 1,
		Hangup = 2
	}

	public enum SnackDrainResultScenario
	{
		Police = 0,
		Hunter = 1,
		Disease = 2,
		NotWorthIt = 3,
		Nothing = 4
	}

	public enum SendSnackAwayOutcomes
	{
		Nothing = 0,
		Argument = 1,
		Apology = 2
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__53_1;

		public static Func<string, string> _003C_003E9__53_2;

		internal string _003CBuildSnackOptions_003Eb__53_1(string opt)
		{
			return null;
		}

		internal string _003CBuildSnackOptions_003Eb__53_2(string opt)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass53_0
	{
		public List<string> options;

		internal bool _003CBuildSnackOptions_003Eb__0(string opt)
		{
			return false;
		}
	}

	private const int ChanceOfNothingHappeningAfterFeeding = 55;

	private const int HappinessImpactOnFeeding = 1;

	private const int HappinessImpactOnDraining = 2;

	private const int ChanceOfNothingHappeningAfterSendingSnackAway = 55;

	private SimPerson SnackPerson__BackingField;

	private bool FamiliarIsConfused__BackingField;

	private bool FamiliarGotCorrectSnack__BackingField;

	private SimVampireFamiliar Familiar__BackingField;

	private string Order__BackingField;

	private string TypeOfSnackBuilt__BackingField;

	private bool IsVictimDrained__BackingField;

	private SendSnackAwayOutcomes SendSnackAwayOutcome__BackingField;

	private SnackDrainResultScenario SnackDrainResult__BackingField;

	private Dictionary<string, string> _snackOptionDictionary;

	private readonly BracketParser _parser;

	private DataVampireFamiliarDump _vampireFamiliarDump;

	public List<string> SnackOptions => null;

	public SimPerson SnackPerson
	{
		get
		{
			return SnackPerson__BackingField;
		}
		private set
		{
			SnackPerson__BackingField = value;
		}
	}

	public bool FamiliarIsConfused
	{
		get
		{
			return FamiliarIsConfused__BackingField;
		}
		private set
		{
			FamiliarIsConfused__BackingField = value;
		}
	}

	public bool FamiliarGotCorrectSnack
	{
		get
		{
			return FamiliarGotCorrectSnack__BackingField;
		}
		private set
		{
			FamiliarGotCorrectSnack__BackingField = value;
		}
	}

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

	public string Order
	{
		get
		{
			return Order__BackingField;
		}
		private set
		{
			Order__BackingField = value;
		}
	}

	public string TypeOfSnackBuilt
	{
		get
		{
			return TypeOfSnackBuilt__BackingField;
		}
		private set
		{
			TypeOfSnackBuilt__BackingField = value;
		}
	}

	public bool IsVictimDrained
	{
		get
		{
			return IsVictimDrained__BackingField;
		}
		private set
		{
			IsVictimDrained__BackingField = value;
		}
	}

	public SendSnackAwayOutcomes SendSnackAwayOutcome
	{
		get
		{
			return SendSnackAwayOutcome__BackingField;
		}
		private set
		{
			SendSnackAwayOutcome__BackingField = value;
		}
	}

	public BracketParser BracketParser => null;

	public string DumpPlace => null;

	public SnackDrainResultScenario SnackDrainResult
	{
		get
		{
			return SnackDrainResult__BackingField;
		}
		private set
		{
			SnackDrainResult__BackingField = value;
		}
	}

	public VampireFamiliarSnackActivity(SimVampireFamiliar familiar)
	{
	}

	public void BuildSnackOptions()
	{
	}

	public void SelectSnackOption(string dropdownString)
	{
	}

	private void SetSnackHealth()
	{
	}

	private void BuildCompatiblePerson()
	{
	}

	private void BuildIncompatiblePerson()
	{
	}

	private void ReplaceSnackPersonsCareer(DataCareer newCareer)
	{
	}

	public void FindSnackPerson(ConfusedFamiliarAnswer answer)
	{
	}

	public string GetFamiliarConfusedString()
	{
		return null;
	}

	private bool SnackHasEnoughHealth()
	{
		return false;
	}

	public void DrainSnack()
	{
	}

	private bool HasToolOverrideResult()
	{
		return false;
	}

	private bool NothingHappensAfterFeeding()
	{
		return false;
	}

	private static bool HunterWillAppear()
	{
		return false;
	}

	private void GetDumpMethod()
	{
	}

	private bool WillGetCaught()
	{
		return false;
	}

	public void SuckSnackBlood()
	{
	}

	public bool CanFamiliarRequestConversion()
	{
		return false;
	}

	public void SendSnackAway()
	{
	}
}
