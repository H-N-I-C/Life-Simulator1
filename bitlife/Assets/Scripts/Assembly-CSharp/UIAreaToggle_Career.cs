using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.Events;

public class UIAreaToggle_Career : MonoBehaviour
{
	private const string TITLE_BUSINESS_EMOJI_NAME = "a16_tableLive";

	private const string TITLE_BOSS_MODE = "Boss Mode";

	private const string TITLE_ATHLETE = "Pro Athlete Job Pack";

	private const string TITLE_MAFIA = "Mafia Job Pack";

	private const string TITLE_MUSICIAN = "Musician Job Pack";

	private const string TITLE_MUSIC_PRODUCER = "Music Producer Job Pack";

	private const string TITLE_POLITICIAN = "Politician Job Pack";

	private const string TITLE_ACTOR = "Actor Job Pack";

	private const string TITLE_HUSTLER = "Street Hustler Job Pack";

	private const string TITLE_BUSINESS = "Business Job Pack";

	private const string TITLE_ASTRONAUT = "Astronaut Job Pack";

	private const string TITLE_MODELING = "Model Job Pack";

	private const string TITLE_DEALER = "Dealer Job Pack";

	private const string DETAILS_ATHLETE = "Are you a baller like LeBron? Are you the next Messi? Become the MVP of your favorite sport with this job pack!";

	private const string DETAILS_MAFIA = "Does being bad tempt you? Do you have the chops to dodge the cops and become Godfather? Rise to notoriety with this job pack!";

	private const string DETAILS_MUSICIAN = "Are you the next Beyoncé, just waiting to be discovered? Sing your way to the top of the charts with this job pack!";

	private const string DETAILS_MUSIC_PRODUCER = "Are you the next hit producer, just waiting to be discovered? Produce your way to the top of the charts with this job pack!";

	private const string DETAILS_POLITICIAN = "Are you a natural-born leader? Do you dream of being President? Your rise to power begins with this job pack!";

	private const string DETAILS_ACTOR = "Are you the center of attention? Do you live for drama? Hire an agent, book an acting gig, and rise to fame with this job pack!";

	private const string DETAILS_HUSTLER = "Do you dream in dollar signs? Does a good scam excite you? Swindle the masses out of their riches with this job pack!";

	private const string DETAILS_BUSINESS = "Are you a natural-born leader with a hunger for profit? Start up, acquire, or lead a company in this job pack!";

	private const string DETAILS_ASTRONAUT = "Are you obsessed with unlocking the mysteries of the universe? Explore new planets, discover alien artifacts, and much more in this job pack!";

	private const string DETAILS_MODELING = "Are you a perfect 10 in a world full of 3s? Start your modeling career and strut your way to the top of the industry in this job pack!";

	private const string DETAILS_DEALER = "Does a life of wheeling and dealing tempt you? Start your career as a dealer for a cartel and eventually run your city as kingpin in this job pack!";

	private const string PREFIX = "a16_darkBossMode_";

	private const string IMAGE_BLDG = "a16_darkBossMode_romanBldg";

	private const string IMAGE_CHART = "a16_darkBossMode_chart";

	private const string IMAGE_HELMET = "a16_darkBossMode_spaceHelmet";

	private const string IMAGE_GUN = "a16_darkBossMode_gun";

	private const string IMAGE_MUSIC = "a16_darkBossMode_music";

	private const string IMAGE_MUSIC_PRODUCER = "a16_emoji-music-producer-icon";

	private const string IMAGE_DRAMA = "a16_darkBossMode_drama";

	private const string IMAGE_BALL = "a16_darkBossMode_basketball";

	private const string IMAGE_BOX = "a16_darkBossMode_box";

	private const string IMAGE_HUSTLER = "a16_darkBossMode_hustler";

	private const string IMAGE_BUSINESS = "a16_darkBossMode_suitcase";

	private const string IMAGE_MODELING = "a16_darkBossMode_camera";

	private const string IMAGE_DEALER = "a16_darkBossMode_weed";

	private CandyButton button;

	private AccessibleCandyButton uapButton;

	private CandyImage background;

	private CandyImage backgroundBorder;

	private CandyImage checkBox;

	private CandyImage checkBackingTop;

	private CandyImage checkBackingBottom;

	private CandyImage imageTop1;

	private CandyImage imageTop2;

	private CandyImage imageTop3;

	private CandyImage imageTop4;

	private CandyImage imageBottom1;

	private CandyImage imageBottom2;

	private CandyImage imageBottom3;

	private CandyImage imageBottom4;

	private CandyImage topLeftBanner;

	private CandyText title;

	private CandyText details;

	private CandyText priceTag;

	private CandyText priceTagMarkOut;

	private RectTransform priceContainer;

	private IAP_TYPE iapType;

	private Sprite imageCheckActive;

	private Sprite imageCheckInactive;

	private Color colorBgActive;

	private Color colorBgInactive;

	private Color colorBorderActive;

	private Color colorBorderInactive;

	private Color colorCheckBackingActive;

	private Color colorCheckBackingInactive;

	private string titleString;

	private string detailsString;

	private string priceString;

	public IAP_TYPE IAPType => IAP_TYPE.NONE;

	public void SetLogoPos(float y)
	{
	}

	public void UAP_ToggleCallbackOnSelection()
	{
	}

	public void Setup(bool isFeaturedToggle, int darklyType, string price, string markOutPrice = null)
	{
	}

	public void SetUAPLabel(string label)
	{
	}

	public void SetButtonSound(SOUND sound)
	{
	}

	public void Select()
	{
	}

	public void Unselect()
	{
	}

	public void SetOnClickDelegateMethodChoiceIndex(int index)
	{
	}

	public void SetOnClickDelegateMethod(UnityAction<int> callback)
	{
	}
}
