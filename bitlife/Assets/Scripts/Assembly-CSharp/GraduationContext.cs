public struct GraduationContext
{
	private readonly SimOccupationStudent Student__BackingField;

	public SimOccupationStudent Student => Student__BackingField;

	public GraduationContext(SimOccupationStudent student)
	{
		Student__BackingField = null;
	}
}
