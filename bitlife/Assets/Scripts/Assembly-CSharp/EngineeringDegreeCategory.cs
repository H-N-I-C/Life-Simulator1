public class EngineeringDegreeCategory : UnlockTriggerCategory<GraduationContext>
{
	private readonly string TriggerID__BackingField;

	public override string TriggerID => TriggerID__BackingField;

	protected override IUnlockTriggerWithContext<GraduationContext>[] GetAllContentUnlocked()
	{
		return null;
	}

	private static FightClubMoveContext ConvertEngineeringDegree(GraduationContext context)
	{
		return default;
	}

	private bool ValidateIsEngineeringDegree(GraduationContext context)
	{
		return false;
	}
}
