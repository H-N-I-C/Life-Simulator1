namespace Tracking
{
	public sealed class ConfidenceVisualPromoTelemetryContext
	{
		private readonly string PromoID__BackingField;

		private readonly string Trigger__BackingField;

		private readonly string TemplateID__BackingField;

		private readonly string ArtID__BackingField;

		public const int Base64ArtIdPrefixLength = 10;

		public string PromoID => PromoID__BackingField;

		public string Trigger => Trigger__BackingField;

		public string TemplateID => TemplateID__BackingField;

		public string ArtID => ArtID__BackingField;

		public ConfidenceVisualPromoTelemetryContext(string promoID, string trigger, string templateID, string artID)
		{
		}

		public static string ResolveArtId(string configuredArtId, bool artIsBase64Payload)
		{
			return null;
		}
	}
}
