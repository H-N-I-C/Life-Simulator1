using Backend.Authentication;
using Game.Dynamic.CustomLives;
using Game.Dynamic.Marketplace;
using Game.Dynamic.MyStuff;

namespace Game.Dynamic
{
	public class DynamicController
	{
		private static MarketPlaceController MarketPlaceController__BackingField;

		private static CustomLifeController CustomLifeController__BackingField;

		private static MyStuffDataController MyStuffDataController__BackingField;

		public static MarketPlaceController MarketPlaceController
		{
			get
			{
				return MarketPlaceController__BackingField;
			}
			private set
			{
				MarketPlaceController__BackingField = value;
			}
		}

		public static CustomLifeController CustomLifeController
		{
			get
			{
				return CustomLifeController__BackingField;
			}
			private set
			{
				CustomLifeController__BackingField = value;
			}
		}

		public static MyStuffDataController MyStuffDataController
		{
			get
			{
				return MyStuffDataController__BackingField;
			}
			private set
			{
				MyStuffDataController__BackingField = value;
			}
		}

		public static void Init()
		{
		}

		private static void InitConfidenceTranslation()
		{
		}

		private static void InitConfidenceMarketPlace()
		{
		}

		private static void SetupGameData(GetConfigResponse data)
		{
		}

		private static void SetConfidenceTranslation()
		{
		}

		private static void SetConfidenceTranslation(bool enabled)
		{
		}
	}
}
