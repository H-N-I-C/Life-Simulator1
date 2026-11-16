using System.Collections.Generic;

public class FightClubFighterRelationshipListItems : ListItemProvider
{
	protected readonly SimFightClubFighter fighter;

	private readonly Life _life;

	public FightClubFighterRelationshipListItems(SimFightClubFighter fightClubFighter)
	{
	}

	public override List<ListItem> GetListItems()
	{
		return null;
	}

	public string LevelAndStyleForFighter()
	{
		return null;
	}

	protected virtual ListItem CreateProfileItem()
	{
		return null;
	}

	private ListItem CreateEditItem()
	{
		return null;
	}

	private ListItem CreateTrashTalkItem()
	{
		return null;
	}

	protected virtual ListItem CreateFightOption()
	{
		return null;
	}
}
