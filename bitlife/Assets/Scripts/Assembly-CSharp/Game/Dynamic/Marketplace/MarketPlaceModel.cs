using System.Collections.Generic;

namespace Game.Dynamic.Marketplace
{
	public class MarketPlaceModel
	{
		private string ID__BackingField;

		private string MinVersion__BackingField;

		private List<MarketPlaceEntry> MarketPlaceEntries__BackingField;

		public string ID
		{
			get
			{
				return ID__BackingField;
			}
			set
			{
				ID__BackingField = value;
			}
		}

		public string MinVersion
		{
			get
			{
				return MinVersion__BackingField;
			}
			set
			{
				MinVersion__BackingField = value;
			}
		}

		public List<MarketPlaceEntry> MarketPlaceEntries
		{
			get
			{
				return MarketPlaceEntries__BackingField;
			}
			set
			{
				MarketPlaceEntries__BackingField = value;
			}
		}
	}
}
