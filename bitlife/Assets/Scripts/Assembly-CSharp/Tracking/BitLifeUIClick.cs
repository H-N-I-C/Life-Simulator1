using System.Collections.Generic;

namespace Tracking
{
	public class BitLifeUIClick : IGameTelemetryEvent
	{
		private string ClickedOnCategory__BackingField;

		private string ClickedOnSubcategory__BackingField;

		public string ClickedOnCategory
		{
			get
			{
				return ClickedOnCategory__BackingField;
			}
			set
			{
				ClickedOnCategory__BackingField = value;
			}
		}

		public string ClickedOnSubcategory
		{
			get
			{
				return ClickedOnSubcategory__BackingField;
			}
			set
			{
				ClickedOnSubcategory__BackingField = value;
			}
		}

		public BitLifeUIClick(BitLifeUIClicks clickType, string subCategory)
		{
		}

		public Dictionary<string, string> GetData()
		{
			return null;
		}
	}
}
