public class StartBabysitCategory : UnlockTriggerCategory<PartTimeJobContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<PartTimeJobContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private static FightClubMoveContext ConvertBabysitToFightClubMoveContext(PartTimeJobContext context)
	{
		return default;
	}

	private bool ValidateIsBabysittingJob(PartTimeJobContext jobContext)
	{
		return false;
	}
}
