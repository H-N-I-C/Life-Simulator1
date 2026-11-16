public static class BossModeCopy
{
	public const string ControlDescription = "Bundle and save! Unlock all 11 Job Packs!";

	public const string TreatmentDescription = "Bundle and save! Get these 11 Job Packs!";

	public const string ControlPackLead = "Unlock every single job pack, including the {0}!";

	public const string TreatmentPackLead = "Unlock these 11 Job Packs, including the {0}!";

	public static bool Treatment => false;

	public static string Description()
	{
		return null;
	}

	public static string Lead(string careerTitle)
	{
		return null;
	}

	public static bool IsCareerPaywall(int type)
	{
		return false;
	}
}
