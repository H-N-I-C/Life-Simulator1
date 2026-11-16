public class WrestlingTeamCategory : UnlockTriggerCategory<JoinSchoolActivityContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<JoinSchoolActivityContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateJoinedWrestlingTeamInCollege(JoinSchoolActivityContext context)
	{
		return false;
	}
}
