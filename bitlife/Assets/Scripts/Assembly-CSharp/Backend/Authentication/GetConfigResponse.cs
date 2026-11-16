using System.Collections.Generic;
using Game.Dynamic.CustomLives;
using Game.Dynamic.Marketplace;
using Game.Dynamic.MyStuff;

namespace Backend.Authentication
{
	public class GetConfigResponse
	{
		public string AwsString;

		public string AwsC;

		public bool IsActive;

		public bool Reconnect;

		public List<string> AllowedRegions;

		public bool ShowUpdateMessage;

		public List<string> Providers;

		public string OriginalCountry;

		public string OriginalRegion;

		public List<MarketPlaceModel> MarketPlace;

		public CustomLivesModel CustomLives;

		public MyStuffDataModel MyStuff;

		public bool GuestOnStart;

		public string GuestPoolId;

		public Dictionary<string, GameSettings> GameSettingsPool;

		public WebShopItem WebShop;

		private GenesisSettings GenesisSettings__BackingField;

		private SeasonsSettings SeasonsSettings__BackingField;

		private AgeRangeSettings AgeRangeSettings__BackingField;

		public GameSettings GameSettings => null;

		public GenesisSettings GenesisSettings
		{
			get
			{
				return GenesisSettings__BackingField;
			}
			set
			{
				GenesisSettings__BackingField = value;
			}
		}

		public SeasonsSettings SeasonsSettings
		{
			get
			{
				return SeasonsSettings__BackingField;
			}
			set
			{
				SeasonsSettings__BackingField = value;
			}
		}

		public AgeRangeSettings AgeRangeSettings
		{
			get
			{
				return AgeRangeSettings__BackingField;
			}
			set
			{
				AgeRangeSettings__BackingField = value;
			}
		}

		public bool CanEnableWebshopNonUs()
		{
			return false;
		}

		private GameSettings GetGameSettings()
		{
			return null;
		}

		private bool _003CCanEnableWebshopNonUs_003Eb__30_0(string t)
		{
			return false;
		}
	}
}
