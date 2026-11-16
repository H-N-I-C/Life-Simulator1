namespace Tracking
{
	public static class ConfidenceVisualPromoTemplates
	{
		public const string LargeOneButton = "large-1";

		public const string LargeTwoButtons = "large-2";

		public const string MediumOneButton = "medium-1";

		public const string LegacyLargeOneButton = "large_primary_button_only";

		public const string LegacyLargeTwoButtons = "large_two_buttons";

		public const string LegacyMediumOneButton = "medium_primary_button_only";

		public static bool TryResolve(string configuredTemplateId, out string prefabTemplateId, out string telemetryTemplateId)
		{
			prefabTemplateId = null;
			telemetryTemplateId = null;
			return false;
		}
	}
}
