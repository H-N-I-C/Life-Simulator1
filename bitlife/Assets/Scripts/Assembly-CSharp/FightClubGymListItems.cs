using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class FightClubGymListItems : ListItemProvider
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action _003C_003E9__6_1;

		public static Action _003C_003E9__6_0;

		public static Action _003C_003E9__7_0;

		public static Action _003C_003E9__8_0;

		public static Action _003C_003E9__9_0;

		public static Action _003C_003E9__11_0;

		public static Action _003C_003E9__12_0;

		public static Func<DataFightIngMove, int> _003C_003E9__13_0;

		public static Func<DataFightIngMove, int> _003C_003E9__13_1;

		public static Func<DataFightIngMove, int> _003C_003E9__13_2;

		public static Action _003C_003E9__13_3;

		public static Action _003C_003E9__15_0;

		public static Action _003C_003E9__16_0;

		public static Action _003C_003E9__17_0;

		public static Action _003C_003E9__18_0;

		internal void _003CGetLeagueFightingItem_003Eb__6_1()
		{
		}

		internal void _003CGetLeagueFightingItem_003Eb__6_0()
		{
		}

		internal void _003CGetStreetFightingItem_003Eb__7_0()
		{
		}

		internal void _003CGetMoveSetItem_003Eb__8_0()
		{
		}

		internal void _003CGetCoachListItem_003Eb__9_0()
		{
		}

		internal void _003CGetRechargeItem_003Eb__11_0()
		{
		}

		internal void _003CGetPhysicalTherapyItem_003Eb__12_0()
		{
		}

		internal int _003CGetSkillShopItem_003Eb__13_0(DataFightIngMove x)
		{
			return 0;
		}

		internal int _003CGetSkillShopItem_003Eb__13_1(DataFightIngMove x)
		{
			return 0;
		}

		internal int _003CGetSkillShopItem_003Eb__13_2(DataFightIngMove x)
		{
			return 0;
		}

		internal void _003CGetSkillShopItem_003Eb__13_3()
		{
		}

		internal void _003CGetSteroidsItem_003Eb__15_0()
		{
		}

		internal void _003CGetSurgeryItem_003Eb__16_0()
		{
		}

		internal void _003CGetTrainItem_003Eb__17_0()
		{
		}

		internal void _003CGetRetireItem_003Eb__18_0()
		{
		}
	}

	private readonly Life _life;

	private readonly SimHero _hero;

	private readonly SimFightClub _fightClub;

	private BracketParser _parser;

	public override List<ListItem> GetListItems()
	{
		return null;
	}

	private ListItem GetLeagueFightingItem()
	{
		return null;
	}

	private ListItem GetStreetFightingItem()
	{
		return null;
	}

	private ListItem GetMoveSetItem()
	{
		return null;
	}

	private ListItem GetCoachListItem()
	{
		return null;
	}

	private ListItem GetProfileItem()
	{
		return null;
	}

	private ListItem GetRechargeItem()
	{
		return null;
	}

	private ListItem GetPhysicalTherapyItem()
	{
		return null;
	}

	private ListItem GetSkillShopItem()
	{
		return null;
	}

	private ListItem CreateShopItem(DataFightIngMove move, FIGHT_CLUB_FIGHTING_STYLE moveStyle)
	{
		return null;
	}

	private ListItem GetSteroidsItem()
	{
		return null;
	}

	private ListItem GetSurgeryItem()
	{
		return null;
	}

	private ListItem GetTrainItem()
	{
		return null;
	}

	private ListItem GetRetireItem()
	{
		return null;
	}

	private ArrayList GetStreetFightingItemArray()
	{
		return null;
	}

	private ListItem GetFighterRelationshipItem(SimFightClubStreetFighter fighter)
	{
		return null;
	}

	private ListItem GetBrawlItem()
	{
		return null;
	}
}
