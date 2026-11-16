using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class HomeScreenVariant : ConfidenceVariant
	{
		public static class Defaults
		{
			public const RIGHT_SIDE_BUTTON_TYPE BottomRightArtStyle = RIGHT_SIDE_BUTTON_TYPE.VAMPIRE;

			public const string BottomRightDeepLink = "bitlife://route?trigger=MARKETPLACE&purchase=no";
		}

		private readonly ConfidenceProperty<RIGHT_SIDE_BUTTON_TYPE> BottomRightArtStyle__BackingField;

		private readonly ConfidenceProperty<string> BottomRightDeepLink__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<RIGHT_SIDE_BUTTON_TYPE> BottomRightArtStyle => BottomRightArtStyle__BackingField;

		public ConfidenceProperty<string> BottomRightDeepLink => BottomRightDeepLink__BackingField;
	}
}
