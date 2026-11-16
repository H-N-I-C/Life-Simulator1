using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioCustomAttack : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimAuctionItem, bool> _003C_003E9__48_0;

		internal bool _003CRefreshScenario_003Eb__48_0(SimAuctionItem x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass51_0
	{
		public List<string> actionStringArray;

		internal bool _003CAddFighterMovesToList_003Eb__0(SimFightClubMove move)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass51_1
	{
		public SimFightClubMove move;

		internal bool _003CAddFighterMovesToList_003Eb__1(string action)
		{
			return false;
		}
	}

	private int _victimGender;

	private bool _victimCanFightBack;

	private SimParent _parent;

	private SimSibling _sibling;

	private SimLover _lover;

	private SimChild _child;

	private SimPerson _friend;

	private SimSocialMediaTroll _troll;

	private SimCommunard _communard;

	private SimStreetPerson _streetPerson;

	private SimPerson _vampire;

	private SimFightClubFighter _fightClubFighter;

	private SimPerson _person;

	private string _personNameScenario;

	private string _personNamePossessiveScenario;

	private string _personNameNarrative;

	private string _personNamePossessiveNarrative;

	private string _personNameObit;

	private bool _victimDeservesIt;

	private bool _victimIsPrisonOfficial;

	private bool _canGetBusted;

	protected ArrayList _actionArray;

	private ArrayList _martialArtMoveArray;

	private Dictionary<string, SimFightClubMove> _fightClubMoves;

	private ArrayList _blackMarketWeaponArray;

	private ArrayList _bodyPartArray;

	private int _willKillOverride;

	private const int ActionCount = 5;

	private const int streetCredEarnings = 10;

	public ScenarioCustomAttack(string theDescription, int theVictimGender, bool theVictimCanFightBack, bool canGetBusted = true)
	{
	}

	public ScenarioCustomAttack(SimClassmate theBully = null)
	{
	}

	public ScenarioCustomAttack(int gender)
	{
	}

	public ScenarioCustomAttack(SimPrisoner thePrisoner, bool theVictimCanFightBack)
	{
	}

	public ScenarioCustomAttack(string theDescription, int theVictimGender)
	{
	}

	public ScenarioCustomAttack(SimClassmate theClassmate, bool theVictimCanFightBack)
	{
	}

	public ScenarioCustomAttack(SimTeacher theTeacher, bool theVictimCanFightBack)
	{
	}

	public ScenarioCustomAttack(SimCoworker theCoworker, bool theVictimCanFightBack)
	{
	}

	public ScenarioCustomAttack(SimParent theParent)
	{
	}

	public ScenarioCustomAttack(SimSibling theSibling)
	{
	}

	public ScenarioCustomAttack(SimLover theLover)
	{
	}

	public ScenarioCustomAttack(SimChild theChild)
	{
	}

	public ScenarioCustomAttack(SimPerson theFriend)
	{
	}

	public ScenarioCustomAttack(SimSocialMediaTroll theTroll)
	{
	}

	public ScenarioCustomAttack(SimStreetPerson theStreetPerson)
	{
	}

	public ScenarioCustomAttack(SimStreetPerson theStreetPerson, bool victimCanFightBack, bool canGetBusted = true, int willKillOverride = 0)
	{
	}

	public ScenarioCustomAttack(SimCommunard theCommunard, bool victimCanFightBack, bool canGetBusted = true, int willKillOverride = 0)
	{
	}

	public ScenarioCustomAttack(SimPerson theVampire, bool canFighBack, bool canGetBusted)
	{
	}

	public ScenarioCustomAttack(string theDescription, SimFightClubFighter theFighter, bool theVictimCanFightBack, bool canGetBusted = true)
	{
	}

	public override void RefreshScenario()
	{
	}

	private void PopulateActionList()
	{
	}

	private void RemoveDuplicateActions()
	{
	}

	private List<string> AddFighterMovesToList(List<string> actionStringArray)
	{
		return null;
	}

	private static bool IsHeroFighter()
	{
		return false;
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private void PushFrontRunFightClubFeedback(SimFightClub fightClub, BracketParser parser)
	{
	}

	private void PushFrontSnitchFightClubFeedback(BracketParser parser)
	{
	}

	private void PushFrontKilledFightClubFeedback(SimFightClub fightClub, BracketParser parser)
	{
	}

	private void PushFrontKilledAttackFeedback()
	{
	}

	public bool IsVictimRelative()
	{
		return false;
	}

	public void ApplyImpactsForAttackingRelative()
	{
	}

	public bool WillRelativeFightBack()
	{
		return false;
	}

	public void HandleDeathOfRelative()
	{
	}
}
