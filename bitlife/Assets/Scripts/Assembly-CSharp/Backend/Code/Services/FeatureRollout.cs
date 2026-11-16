namespace Backend.Code.Services
{
	public static class FeatureRollout
	{
		public static bool IsEnrolled(string userId, int percentage)
		{
			return false;
		}

		public static int EnrolledPercentage(string userId)
		{
			return 0;
		}
	}
}
