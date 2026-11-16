public static class GenderExtensions
{
	public static bool AreRelativelyEqualTo(GENDER gender, GENDER other)
	{
		return false;
	}

	public static GENDER ConvertToGender(string genderString)
	{
		return GENDER.MALE;
	}

	public static GENDER ConvertToGender(int value)
	{
		return GENDER.MALE;
	}
}
