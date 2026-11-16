using System.Collections.Generic;

namespace Tracking
{
	public class BitLifePurchaseIAP : GameTelemetryEvent
	{
		private string Purchased__BackingField;

		private string Store__BackingField;

		private string Origin__BackingField;

		public string Purchased
		{
			get
			{
				return Purchased__BackingField;
			}
			set
			{
				Purchased__BackingField = value;
			}
		}

		public string Store
		{
			get
			{
				return Store__BackingField;
			}
			set
			{
				Store__BackingField = value;
			}
		}

		public string Origin
		{
			get
			{
				return Origin__BackingField;
			}
			set
			{
				Origin__BackingField = value;
			}
		}

		public override Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
