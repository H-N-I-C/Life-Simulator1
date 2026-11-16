public class JoinSchoolActivityCategory : UnlockTriggerCategory<JoinSchoolActivityContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<JoinSchoolActivityContext>[] GetAllContentUnlocked()
	{
		return null;
	}
}
