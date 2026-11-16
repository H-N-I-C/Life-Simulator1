public struct JoinSchoolActivityContext
{
	private readonly SimOccupationStudent Student__BackingField;

	private readonly SimSchoolActivity SchoolActivity__BackingField;

	public SimOccupationStudent Student => Student__BackingField;

	public SimSchoolActivity SchoolActivity => SchoolActivity__BackingField;

	public JoinSchoolActivityContext(SimOccupationStudent student, SimSchoolActivity schoolActivity)
	{
		Student__BackingField = null;
		SchoolActivity__BackingField = null;
	}
}
