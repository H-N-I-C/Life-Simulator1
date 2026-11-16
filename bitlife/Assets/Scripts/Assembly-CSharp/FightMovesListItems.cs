using System;
using System.Collections.Generic;

public class FightMovesListItems : ListItemProvider
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimFightClubMove, float> _003C_003E9__2_0;

		public static Func<SimFightClubMove, float> _003C_003E9__2_1;

		public static Func<SimFightClubMove, float> _003C_003E9__2_2;

		internal float _003CGetListItems_003Eb__2_0(SimFightClubMove x)
		{
			return 0f;
		}

		internal float _003CGetListItems_003Eb__2_1(SimFightClubMove x)
		{
			return 0f;
		}

		internal float _003CGetListItems_003Eb__2_2(SimFightClubMove x)
		{
			return 0f;
		}
	}

	private readonly SimFightClub _fightClub;

	public override List<ListItem> GetListItems()
	{
		return null;
	}

	private ListItem CreateMoveItem(SimFightClubMove move, Scenario scenario)
	{
		return null;
	}
}
