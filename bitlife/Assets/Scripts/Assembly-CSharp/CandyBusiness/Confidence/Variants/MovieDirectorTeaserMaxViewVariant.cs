using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class MovieDirectorTeaserMaxViewVariant : ConfidenceVariant
	{
		private readonly ConfidenceProperty<int> MaxViews__BackingField;

		public override ConfidenceFlag Flag => null;

		public ConfidenceProperty<int> MaxViews => MaxViews__BackingField;
	}
}
