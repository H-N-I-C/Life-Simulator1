using System;
using System.Collections;
using System.Collections.Generic;

public class ScenarioDealerStreetPersonAttack : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimAuctionItem, bool> _003C_003E9__11_0;

		public static Func<string, string, string> _003C_003E9__12_2;

		internal bool _003CRefreshScenario_003Eb__11_0(SimAuctionItem x)
		{
			return false;
		}

		internal string _003CChoiceSelectedAtIndex_003Eb__12_2(string i, string j)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass12_0
	{
		public string moveDropDownString;

		internal bool _003CChoiceSelectedAtIndex_003Eb__0(DataCustomAttackAction t)
		{
			return false;
		}

		internal bool _003CChoiceSelectedAtIndex_003Eb__1(SimAuctionItem t)
		{
			return false;
		}
	}

	private string _narrative;

	private SimSpecialJobDealer _dealerJob;

	private readonly SimDealerStreetPerson _person;

	private List<DataCustomAttackAction> _moves;

	private ArrayList _movesDropDownContents;

	private List<DataCustomAttackBodyPart> _bodyParts;

	private ArrayList _bodyPartsDropDownContents;

	private List<DataMartialArtMove> _martialArtMoveArray;

	private List<string> _martialArtStringArray;

	private List<SimAuctionItem> _blackMarketWeaponArray;

	public ScenarioDealerStreetPersonAttack(SimDealerStreetPerson person)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	private (int, string) GetDamageFromWeapon(SimAuctionItem weaponAttack)
	{
		return default;
	}

	private int GetDamageFromMartialMove(string text, DataMartialArtMove move)
	{
		return 0;
	}
}
