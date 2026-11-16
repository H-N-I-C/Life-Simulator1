using CandyBusiness.Confidence.Flags;
using CandyBusiness.Confidence.Properties;

namespace CandyBusiness.Confidence
{
	public class EnabledVariant : ConfidenceVariant
	{
		private readonly ConfidenceFlag Flag__BackingField;

		private readonly ConfidenceProperty<bool> Enabled__BackingField;

		public sealed override ConfidenceFlag Flag => Flag__BackingField;

		public ConfidenceProperty<bool> Enabled => Enabled__BackingField;

		protected EnabledVariant(ConfidenceFlag flag)
		{
		}
	}
}
