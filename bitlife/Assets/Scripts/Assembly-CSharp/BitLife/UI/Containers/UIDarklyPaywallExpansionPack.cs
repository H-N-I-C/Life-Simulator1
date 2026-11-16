using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallExpansionPack : UIDarklyMarketPlace
	{
		private sealed class _003C_003Ec__DisplayClass97_0
		{
			public int exPackType;

			public UIDarklyPaywallExpansionPack _003C_003E4__this;

			internal bool _003CSetup_003Eb__0()
			{
				return false;
			}

			internal string _003CSetup_003Eb__1()
			{
				return null;
			}

			internal string _003CSetup_003Eb__2()
			{
				return null;
			}
		}

		private readonly Vector2 EMOJI_SIZE_LARGE;

		private readonly Vector2 EMOJI_SIZE_SMALL;

		public const float GAP_SIZE_LARGE = 45f;

		public const float GAP_SIZE_SMALL = 33f;

		private const float ITEM_HEIGHT_LARGE = 160f;

		private const float ITEM_HEIGHT_SMALL = 91f;

		private const float STACK_HEIGHT_LARGE = 1846f;

		public const float ENTRY_TOTAL_MARGIN_VERTICAL = 60f;

		private const float CONTENT_POSY_BLACKMARKET = -520f;

		public const float ENTRY_TEMPLATE_TEXT_MAX_SIZE = 750f;

		private const string COLOR_WHITE = "FFFFFF";

		private const string COLOR_GREEN = "2C9B42";

		private const string COLOR_DARK_GREEN = "19221D";

		private const string COLOR_YELLOW = "FFEE00";

		private const string COLOR_YELLOW_ORANGE = "FFC73A";

		private const string COLOR_SKY_BLUE = "3EBCFF";

		private const string COLOR_DARK_GRAY_BLUE = "1D495E";

		private const string COLOR_ORANGE = "CB5D2B";

		private const string COLOR_DARK_ORANGE = "45342B";

		private const string COLOR_BORDER_RED = "97100E";

		private const string COLOR_ENTRY_RED = "650000";

		private const string COLOR_DARK_RED = "2F0A09";

		private const string COLOR_MUTED_PURPLE = "2E1E2C";

		private const string COLOR_BLACK = "000000";

		private const string COLOR_MUTED_GOLD = "F4CA20";

		private const string COLOR_LIME_YELLOW = "F7EB53";

		private const string COLOR_LIME = "D5FF00";

		private const string COLOR_MIDNIGHT_LIME = "030A00";

		private const string BG_INVESTOR = "a16_darkExpansionPackInvestorBg";

		private const string LOGO_INVESTOR = "a16_darkExpansionPackInvestorLogo";

		private const string UAP_TITLE_INVESTOR = "Investor Expansion Pack";

		private const string BULLET_NAME_INVESTOR = "chart-bar";

		private const string BG_LANDLORD = "a16_darkExpansionPackLandLordBg";

		private const string LOGO_LANDLORD = "a16_darkExpansionPackLandLordLogo0";

		private const string UAP_TITLE_LANDLORD = "LandLord Expansion Pack";

		private const string BULLET_NAME_LANDLORD = "house";

		private const string BG_BLACKMARKET = "a16_darkExpansionPackBlackMarketBg";

		private const string LOGO_BLACKMARKET = "a16_darkExpansionPackBlackMarketLogo";

		private const string UAP_TITLE_BLACKMARKET = "Black Market Expansion Pack";

		private const string BULLET_NAME_BLACKMARKET = "BlackMarket-dot1";

		private const string BG_CULT = "a16_darkExpansionPackCultBg";

		private const string LOGO_CULT = "a16_darkExpansionPackCultLogo";

		private const string UAP_TITLE_CULT = "Cult Expansion Pack";

		private const string BULLET_NAME_CULT = "a16_darkBullet";

		private const string BG_SECRET_AGENT = "a16_darkExpansionPackSecretAgentBg";

		private const string LOGO_SECRET_AGENT = "a16_darkExpansionPackSecretAgentLogo";

		private const string UAP_TITLE_SECRET_AGENT = "Secret Agent Expansion Pack";

		private const string BULLET_NAME_SECRET_AGENT = "a16_darkExpansionPackSecretAgentBullet";

		private const string BG_ZOO = "a16_darkExpansionPackZooBg";

		private const string LOGO_ZOO = "a16_darkExpansionPackZooLogo";

		private const string UAP_TITLE_ZOO = "Zoo Expansion Pack";

		private const string BULLET_NAME_ZOO = "a16_darkExpansionPackZooBullet";

		private const string BG_CASINO = "a16_darkExpansionPackCasinoBg";

		private const string LOGO_CASINO = "a16_darkExpansionPackCasinoLogo";

		private const string UAP_TITLE_CASINO = "Casino Expansion Pack";

		private const string BULLET_NAME_CASINO = "a16_darkExpansionPackCasinoBullet";

		private const string BG_LUXURY = "a16_darkExpansionPackLuxuryBg";

		private const string LOGO_LUXURY = "a16_darkExpansionPackLuxuryLogo";

		private const string UAP_TITLE_LUXURY = "Luxury LifeStyle";

		private const string BULLET_NAME_LUXURY = "a16_darkExpansionPackLuxuryBullet";

		private const string BG_RACING = "a16_darkExpansionPackRacingBg";

		private const string LOGO_RACING = "a16_darkExpansionPackRacingLogo";

		private const string UAP_TITLE_RACING = "Racing Expansion Pack";

		private const string BULLET_NAME_RACING = "a16_darkExpansionPackRacingBullet";

		private const string BG_OUTDOOR = "a16_darkExpansionPackOutdoorBg";

		private const string LOGO_OUTDOOR = "a16_darkExpansionPackOutdoorLogo";

		private const string UAP_TITLE_OUTDOOR = "Outdoor Lifestyle";

		private const string BULLET_NAME_OUTDOOR = "a16_darkExpansionPackOutdoorBullet";

		private const string BG_VAMPIRE = "a16_darkExpansionPackVampireBg";

		private const string LOGO_VAMPIRE = "a16_darkExpansionPackVampireLogo";

		private const string UAP_TITLE_VAMPIRE = "Vampire Mode";

		private const string BULLET_NAME_VAMPIRE = "a16_darkExpansionPackVampireBullet";

		private RectTransform stack;

		private CandyImage backgroundBorder;

		private CandyImage backgroundColor;

		private CandyImage backgroundImage;

		private CandyImage logoImage;

		private UIDarklyPaywallEntry entryTemplate;

		private Color itemBgColor;

		private RectTransform contentParent;

		private CandyText extraBenefit;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle singlePurchaseToggle;

		private List<UIDarklyPaywallEntry> entryInstances;

		private int darklyType;

		private int IAP_Origin;

		private string price;

		private string currentPrice;

		private int exPackType;

		private int expansionPackIAPOrigin;

		private float runningHeight;

		private float runningHeightBeforeEntriesSetup;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int exPackType = 0, int expansionPackIAPOrigin = -1)
		{
		}

		private void OutdoorSetup()
		{
		}

		private Dictionary<string, List<string>> InvestorBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> LandLordBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> BlackMarketBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> CultBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> SecretAgentBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> ZooBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> CasinoBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> VampireBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> LuxuryBulletPoints()
		{
			return null;
		}

		private Dictionary<string, List<string>> RacingBulletPoints()
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		protected override void BuyWithBitLifeButtonPress()
		{
		}

		protected override void BuyButtonPress()
		{
		}

		public override void RebuildUI()
		{
		}
	}
}
