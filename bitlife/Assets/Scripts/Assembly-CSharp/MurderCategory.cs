public class MurderCategory : UnlockTriggerCategory<MurderContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<MurderContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private static FightClubMoveContext ConvertNotCaught(MurderContext context)
	{
		return default;
	}

	private bool GetAwayWithMurder(MurderContext context)
	{
		return false;
	}

	private bool GetAwayWithStranglingToDeath(MurderContext context)
	{
		return false;
	}
}
