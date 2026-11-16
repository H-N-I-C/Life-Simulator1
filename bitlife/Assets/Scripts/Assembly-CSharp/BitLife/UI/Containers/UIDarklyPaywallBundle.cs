using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallBundle : UIDarklyMarketPlace
	{
		private sealed class _003C_003Ec__DisplayClass79_0
		{
			public IAP_TYPE iapType;

			public int bundleType;

			public UIDarklyPaywallBundle _003C_003E4__this;

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

		private readonly Vector2 EMOJI_SIZE_IMAGES;

		private const float ENTRY_TOTAL_MARGIN_VERTICAL = 60f;

		private const float ENTRY_TOTAL_MARGIN_VERTICAL_BIG_CONTENT = 30f;

		private const float CONTENT_POS_Y_FROM_TOP = 640f;

		private const float CONTENT_POS_Y_FROM_TOP_FAME = 650f;

		private const float BOTTOM_SPACE = 100f;

		private const float BOTTOM_SPACE_SMALL = 10f;

		private const float EVEN_ENTRY_BG_COLOR_ALPHA = 0.28f;

		private const float HEADER_POS_FAME = -498f;

		private const string COLOR_WHITE = "FFFFFF";

		private const string COLOR_GREEN = "69DE1C";

		private const string COLOR_DARK_GREEN = "153B1C";

		private const string COLOR_DARK_BLUE = "000B1B";

		private const string COLOR_YELLOW = "FFF712";

		private const string COLOR_GOLDEN_YELLOW = "DA9700";

		private const string COLOR_GOLDEN_ORANGE = "813b1C";

		private const string COLOR_GOLDEN_YELLOW_ORANGE = "FFBA00";

		private const string COLOR_NEON_DARK_BLUE = "242AA3";

		private const string COLOR_NEON_DARK_PURPLE = "381A65";

		private const string COLOR_PARTY_PURPLE = "D994FF";

		private const string COLOR_RED = "FA0000";

		private const string BG_BILLIONAIRE = "a16_darkBundleBillionaireBg";

		private const string LOGO_BILLIONAIRE = "a16_darkBundleBillionaireLogo";

		private const string UAP_TITLE_BILLIONAIRE = "Billionaire Bundle";

		private const string BG_CRIME = "a16_darkBundleCrimeBg";

		private const string LOGO_CRIME = "a16_darkBundleCrimeLogo";

		private const string UAP_TITLE_CRIME = "Crime Pays Bundle";

		private const string BG_FAME = "a16_darkBundleFameBg";

		private const string LOGO_FAME = "a16_darkBundleFameLogo";

		private const string UAP_TITLE_FAME = "Fame And Fortune Bundle";

		private const string BG_PARTY = "a16_darkBundlePartyAnimalBg";

		private const string LOGO_PARTY = "a16_darkBundlePartyAnimalLogo";

		private const string UAP_TITLE_PARTY = "Party Animal Bundle";

		private const string UAP_TITLE_TIME = "Time is Money Bundle";

		private const string BG_TIME = "a16_darkBundleTimeBg";

		private const string LOGO_TIME = "a16_darkBundleTimeLogo";

		private const string UAP_TITLE_STARTER = "Blinged-Out Birthday Bundle";

		private const string BG_STARTER = "a16_darkBundleBlingedOutBdayBg";

		private const string LOGO_STARTER = "a16_darkBundleBlingedOutBday";

		private const string UAP_TITLE_LEGENDS = "Villain Era Birthday Bundle";

		private const string BG_LEGENDS = "a16_darkBundleVillainEraBg";

		private const string LOGO_LEGENDS = "a16_darkBundleVillainEra";

		private const string UAP_TITLE_STARTER1 = "Expansion Starter Set";

		private const string BG_STARTER1 = "a16_darkBundleStarterBg";

		private const string LOGO_STARTER1 = "a16_darkBundleStarter";

		private const string UAP_TITLE_STARTER2 = "Expansion Master Set";

		private const string BG_STARTER2 = "a16_darkBundleStarterMasterBg";

		private const string LOGO_STARTER2 = "a16_darkBundleStarterMaster";

		private const string UAP_TITLE_MD_BUNDLE = "Movie Director Bundle";

		private const string BG_MD_BUNDLE = "a16_darkBundleMDMasterBg";

		private const string LOGO_MD_BUNDLE = "a16_darkBundleMDLogo";

		private RectTransform stack;

		private CandyImage backgroundBorder;

		private CandyImage backgroundColor;

		private CandyImage backgroundImage;

		private CandyImage logoImage;

		private CandyText headerText;

		private UIDarklyContentStackEntryTemplate entryTemplate;

		private List<UIDarklyContentStackEntryTemplate> instances;

		private RectTransform contentParent;

		private CandyText extraBenefit;

		private CandyImage bottomAccentFeature;

		private RectTransform spacer5;

		private CandyButton buyButton;

		private CandyImage buyButtonEmoji;

		private HorizontalLayoutGroup buyButtonTitleGroup;

		private CandyButton restorePurchaseButton;

		private UIAreaToggle singlePurchaseToggle;

		private int darklyType;

		private int IAP_Origin;

		private string price;

		private int bundleType;

		private bool _hasPurchasedAtLeastOne;

		private float runningHeight;

		private float margin;

		private string currentPriceText;

		private float _bottomSpace;

		private float runningHeightBeforeEntriesSetup;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int bundleType = 0)
		{
		}

		private void UpdateEmojiPosition(float height)
		{
		}

		private Dictionary<string, List<string>> GetBulletPoints_Billionaire()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_Crime()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_FameAndFortune()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_MovieDirectorHollywoodStar()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_Starter()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_StarterPack1()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_StarterPack2()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_Legends()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_PartyAnimal()
		{
			return null;
		}

		private Dictionary<string, List<string>> GetBulletPoints_TimeIsMoney()
		{
			return null;
		}

		private void RestoreButtonPress()
		{
		}

		protected override void BuyButtonPress()
		{
		}

		protected override void BuyWithBitLifeButtonPress()
		{
		}

		public override void RebuildUI()
		{
		}
	}
}
