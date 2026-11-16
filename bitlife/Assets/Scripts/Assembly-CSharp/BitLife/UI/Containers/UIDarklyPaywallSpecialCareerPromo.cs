using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIDarklyPaywallSpecialCareerPromo : UIDarkly
	{
		private const string TITLE_ATHLETE = "Pro Athlete Job Pack";

		private const string TITLE_MAFIA = "Mafia Job Pack";

		private const string TITLE_MUSICIAN = "Musician Job Pack";

		private const string TITLE_POLITICIAN = "Politician Job Pack";

		private const string TITLE_ACTOR = "Actor Job Pack";

		private const string TITLE_HUSTLER = "Street Hustler Job Pack";

		private const string TITLE_BUSINESS = "Business Job Pack";

		private const string DETAILS_ATHLETE = "Are you a baller like LeBron?\nAre you the next Messi? Become the MVP of your favorite sport with this job pack!";

		private const string DETAILS_MAFIA = "Does being bad tempt you?\nDo you have the chops to dodge the cops and become Godfather? Rise to notoriety with this job pack!";

		private const string DETAILS_MUSICIAN = "Are you the next Beyoncé, just waiting to be discovered? Sing your way to the top of the charts with this job pack!";

		private const string DETAILS_POLITICIAN = "Are you a natural-born leader?\nDo you dream of being President? Your rise to power begins with this job pack!";

		private const string DETAILS_ACTOR = "Are you the center of attention?\nDo you live for drama? Hire an agent, book an acting gig, and rise to fame with this job pack!";

		private const string DETAILS_HUSTLER = "Do you dream in dollar signs? Does a good scam excite you? Swindle the masses out of their riches with this job pack!";

		private const string DETAILS_BUSINESS = "Are you a natural-born leader with a hunger for profit? Start up, acquire, or lead a company in this job pack!";

		private const string DOWNLOAD_BUTTON_TEXT = "Unlock on this life";

		private const string DOWNLOAD_BUTTON_IMAGE = "a16_alertButtonVideoIcon";

		private const string BUY_BUTTON_FORMAT_TEXT = "buy it for {0} instead";

		private const string PREFIX = "a16_darkBossMode_";

		private const string IMAGE_BLDG = "a16_darkBossMode_romanBldg";

		private const string IMAGE_GUN = "a16_darkBossMode_gun";

		private const string IMAGE_MUSIC = "a16_darkBossMode_music";

		private const string IMAGE_DRAMA = "a16_darkBossMode_drama";

		private const string IMAGE_BALL = "a16_darkBossMode_basketball";

		private const string IMAGE_HUSTLER = "a16_darkBossMode_hustler";

		private const string IMAGE_CHART = "a16_darkBossMode_chart";

		private const string IMAGE_BUSINESS = "a16_darkBossMode_suitcase";

		private RectTransform layoutGroupRect;

		private CandyImage headerImage;

		private CandyText headerTitle;

		private CandyText headerDetails;

		private CandyText dealDetails;

		private CandyButton downloadButton;

		private CandyImage downloadButtonEmoji;

		private HorizontalLayoutGroup downloadButtonTitleGroup;

		private CandyButton buyButton;

		private bool isInitialDarkly;

		private int darklyType;

		private int IAP_Origin;

		private int jobType;

		public void ReSetup()
		{
		}

		public void Setup(bool isInitialDarkly, int darklyType, int IAP_Origin, int jobType)
		{
		}

		private void DownloadButtonPress()
		{
		}

		private void BuyButtonPress()
		{
		}

		private void ExitPaywall()
		{
		}
	}
}
