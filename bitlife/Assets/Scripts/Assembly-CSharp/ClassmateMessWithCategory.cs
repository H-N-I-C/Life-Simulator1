public class ClassmateMessWithCategory : UnlockTriggerCategory<ClassmateMischiefContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<ClassmateMischiefContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private bool ValidateIsCollegeClassmate(ClassmateMischiefContext context)
	{
		return false;
	}
}
