using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence.Variants
{
	public class MovieDirectorMasterControlVariant : ConfidenceVariant
	{
		private readonly ConfidenceProperty<bool> MasterEnabled__BackingField;

		public ConfidenceProperty<bool> MasterEnabled => MasterEnabled__BackingField;

		public override ConfidenceFlag Flag => null;
	}
}
