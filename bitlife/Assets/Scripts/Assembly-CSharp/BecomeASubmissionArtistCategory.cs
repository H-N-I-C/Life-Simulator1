public class BecomeASubmissionArtistCategory : UnlockTriggerCategory<FightClubMoveContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<FightClubMoveContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
