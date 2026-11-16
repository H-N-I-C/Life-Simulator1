public static class AuraRing
{
	public const float RelationshipBuff = 50f;

	public const string Title = "Aura Ring";

	public const string Emoji = "aura-ring";

	public const string DescriptionItem = "Boost all of your relationships!";

	public const string DescriptionNeeds = "Generate swag without trying!";

	public const string FullDescription = "This one-time purchase will always keep your relationship with lovers at max AND give your other relationships a boost!\n\nYou can toggle this feature in the main menu.\n\nYour vibes are unmatched!";

	public static bool HasEnabled()
	{
		return false;
	}

	public static bool CanOffer()
	{
		return false;
	}

	public static void SetRelationshipsListTheme(ListItem item)
	{
	}

	public static Scenario CreatePostPurchaseScenario()
	{
		return null;
	}
}
