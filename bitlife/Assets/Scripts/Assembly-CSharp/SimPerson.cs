using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class SimPerson : SimThirdPersonScriptDelegate
{
	public static int NumIncrementAges;

	private SimPersonName Name__BackingField;

	private bool Alive__BackingField;

	private int Gender__BackingField;

	private int Age__BackingField;

	private int Sexuality__BackingField;

	private bool IsValentine__BackingField;

	public string PlaceOfResidenceString;

	[NonSerialized]
	private SimPlace _PlaceOfResidence;

	private int YearsSinceDeath__BackingField;

	private int LgbtqGenderIdentity__BackingField;

	private SimPregnancy Pregnancy__BackingField;

	private bool OnBirthControl__BackingField;

	private int AccessoryHeadwearIndex__BackingField;

	private int AccessorySunglassesIndex__BackingField;

	private int AccessoryMaskIndex__BackingField;

	private int AccessoryForeheadTattooIndex__BackingField;

	private int AccessoryLeftCheekTattooIndex__BackingField;

	private int AccessoryRightCheekTattooIndex__BackingField;

	private bool HeroInvitedToSleepoverThisYear__BackingField;

	private SimThirdPersonAssets Assets__BackingField;

	private SimThirdPersonScript _Script;

	private PersonEmojiScript EmojiScript__BackingField;

	private float Att_happiness__BackingField;

	private float Att_health__BackingField;

	private float Att_intelligence__BackingField;

	private float Att_appearance__BackingField;

	private float Att_discipline__BackingField;

	private float Att_karma__BackingField;

	private float Att_homo__BackingField;

	private float Att_fertility__BackingField;

	private float Att_willpower__BackingField;

	private float Att_wiliness__BackingField;

	private float Att_generosity__BackingField;

	private float Att_fame__BackingField;

	private float Att_athleticism__BackingField;

	private float Att_money__BackingField;

	private float Att_craziness__BackingField;

	private float Att_loyalty__BackingField;

	private float Att_connection__BackingField;

	private float Att_charisma__BackingField;

	private float Att_music__BackingField;

	private float Att_acting__BackingField;

	private float Att_modeling__BackingField;

	private float Att_dealing__BackingField;

	private float ShortTermAppearancePoints__BackingField;

	public string PlaceOfBirthString;

	[NonSerialized]
	private SimPlace _PlaceOfBirth;

	private int GenderAtBirth__BackingField;

	private SimPersonName NameAtBirth__BackingField;

	[NonSerialized]
	public SimPerson PersonAtBirth;

	private int SkinColor__BackingField;

	private int LgbtqGenderInclination__BackingField;

	private float HeroRelationshipStrength__BackingField;

	private int HeroAgeAtLastConversation__BackingField;

	private int HeroAgeAtLastSpendTime__BackingField;

	private int HeroAgeAtLastMourn__BackingField;

	private int HeroAgeAtLastAskForMoney__BackingField;

	private int HeroTimesAskedForMoneyThisYear__BackingField;

	private int HeroAgeAtLastAskForBicycle__BackingField;

	private int HeroTimesAskedForBicycleThisYear__BackingField;

	private int HeroTimesRefusedBicycleThisYear__BackingField;

	private int HeroTimesAskedForMoney__BackingField;

	private int HeroAgeAtLastGiveMoney__BackingField;

	private int HeroAgeAtLastCompliment__BackingField;

	private int HeroAgeAtLastMovie__BackingField;

	private bool HeroGivenPoliceWarning__BackingField;

	private int _HeroAgeAtLastHeroGaveGift;

	private int HeroAgeAtLastWroteLetterFromPrison__BackingField;

	private int HeroAgeAtLastMurderAttempt__BackingField;

	private int HeroAgeAtLastWatchedYouTube__BackingField;

	private int HeroAgeAtLastUnfriendAttempt__BackingField;

	private int HeroAgeWhenLastSued__BackingField;

	private int HeroAgeAtLastConcert__BackingField;

	private int HeroAgeAtLastHeroCheatedWithPartner__BackingField;

	private int HeroAgeAtLastDelinquentBehavior__BackingField;

	private int HeroAgeAtLastRejectedBigTicketItem__BackingField;

	private int HeroAgeAtLastGiveHouse__BackingField;

	private int HeroAgeAtLastGiveCar__BackingField;

	private int HeroAgeAtLastStartedRumor__BackingField;

	private int FriendStatus__BackingField;

	private int YearsFriends__BackingField;

	private bool ChildhoodFriend__BackingField;

	private int HeroAgeAtLastBestFriendRequest__BackingField;

	private int HeroAgeAtLastBecameEnemies__BackingField;

	private int HeroAgeAtLastInsult__BackingField;

	private int HeroAgeAtLastRejectedMarketAdvice__BackingField;

	private int HeroAgeAtLastGaveMarketAdvice__BackingField;

	private string MarketAdviceString__BackingField;

	private string MarketTipString__BackingField;

	private bool HasWarnedHeroAboutDealingDrugs__BackingField;

	private int TimesRejectedHeroForDealingThemNonDrugs__BackingField;

	private int TimesRejectedHeroForDealingThemDrugs__BackingField;

	private Dictionary<string, AssetAdviceDict> SpecificAssetAdviceDict__BackingField;

	private SimPerson Proxy__BackingField;

	private int HeroAgeAtLastGiveAuctionItem__BackingField;

	private SimVampire Vampire__BackingField;

	private bool VampireDiseaseCarrier__BackingField;

	public bool IsDestinedToBecomeVampire;

	[NonSerialized]
	private int _lastIncrementAges;

	private static readonly Dictionary<string, PRONOUN_TYPE> PronounMap;

	public SimPersonName Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public bool Alive
	{
		get
		{
			return Alive__BackingField;
		}
		set
		{
			Alive__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public int Sexuality
	{
		get
		{
			return Sexuality__BackingField;
		}
		set
		{
			Sexuality__BackingField = value;
		}
	}

	public bool IsValentine
	{
		get
		{
			return IsValentine__BackingField;
		}
		set
		{
			IsValentine__BackingField = value;
		}
	}

	public SimPlace PlaceOfResidence
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int YearsSinceDeath
	{
		get
		{
			return YearsSinceDeath__BackingField;
		}
		set
		{
			YearsSinceDeath__BackingField = value;
		}
	}

	public int LgbtqGenderIdentity
	{
		get
		{
			return LgbtqGenderIdentity__BackingField;
		}
		set
		{
			LgbtqGenderIdentity__BackingField = value;
		}
	}

	public SimPregnancy Pregnancy
	{
		get
		{
			return Pregnancy__BackingField;
		}
		set
		{
			Pregnancy__BackingField = value;
		}
	}

	public bool OnBirthControl
	{
		get
		{
			return OnBirthControl__BackingField;
		}
		set
		{
			OnBirthControl__BackingField = value;
		}
	}

	public int AccessoryHeadwearIndex
	{
		get
		{
			return AccessoryHeadwearIndex__BackingField;
		}
		set
		{
			AccessoryHeadwearIndex__BackingField = value;
		}
	}

	public int AccessorySunglassesIndex
	{
		get
		{
			return AccessorySunglassesIndex__BackingField;
		}
		set
		{
			AccessorySunglassesIndex__BackingField = value;
		}
	}

	public int AccessoryMaskIndex
	{
		get
		{
			return AccessoryMaskIndex__BackingField;
		}
		set
		{
			AccessoryMaskIndex__BackingField = value;
		}
	}

	public int AccessoryForeheadTattooIndex
	{
		get
		{
			return AccessoryForeheadTattooIndex__BackingField;
		}
		set
		{
			AccessoryForeheadTattooIndex__BackingField = value;
		}
	}

	public int AccessoryLeftCheekTattooIndex
	{
		get
		{
			return AccessoryLeftCheekTattooIndex__BackingField;
		}
		set
		{
			AccessoryLeftCheekTattooIndex__BackingField = value;
		}
	}

	public int AccessoryRightCheekTattooIndex
	{
		get
		{
			return AccessoryRightCheekTattooIndex__BackingField;
		}
		set
		{
			AccessoryRightCheekTattooIndex__BackingField = value;
		}
	}

	public bool HeroInvitedToSleepoverThisYear
	{
		get
		{
			return HeroInvitedToSleepoverThisYear__BackingField;
		}
		set
		{
			HeroInvitedToSleepoverThisYear__BackingField = value;
		}
	}

	public SimThirdPersonAssets Assets
	{
		get
		{
			return Assets__BackingField;
		}
		set
		{
			Assets__BackingField = value;
		}
	}

	public SimThirdPersonScript Script
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public PersonEmojiScript EmojiScript
	{
		get
		{
			return EmojiScript__BackingField;
		}
		set
		{
			EmojiScript__BackingField = value;
		}
	}

	public float Att_happiness
	{
		get
		{
			return Att_happiness__BackingField;
		}
		set
		{
			Att_happiness__BackingField = value;
		}
	}

	public float Att_health
	{
		get
		{
			return Att_health__BackingField;
		}
		set
		{
			Att_health__BackingField = value;
		}
	}

	public float Att_intelligence
	{
		get
		{
			return Att_intelligence__BackingField;
		}
		set
		{
			Att_intelligence__BackingField = value;
		}
	}

	public float Att_appearance
	{
		get
		{
			return Att_appearance__BackingField;
		}
		set
		{
			Att_appearance__BackingField = value;
		}
	}

	public float Att_discipline
	{
		get
		{
			return Att_discipline__BackingField;
		}
		set
		{
			Att_discipline__BackingField = value;
		}
	}

	public float Att_karma
	{
		get
		{
			return Att_karma__BackingField;
		}
		set
		{
			Att_karma__BackingField = value;
		}
	}

	public float Att_homo
	{
		get
		{
			return Att_homo__BackingField;
		}
		set
		{
			Att_homo__BackingField = value;
		}
	}

	public float Att_fertility
	{
		get
		{
			return Att_fertility__BackingField;
		}
		set
		{
			Att_fertility__BackingField = value;
		}
	}

	public float Att_willpower
	{
		get
		{
			return Att_willpower__BackingField;
		}
		set
		{
			Att_willpower__BackingField = value;
		}
	}

	public float Att_wiliness
	{
		get
		{
			return Att_wiliness__BackingField;
		}
		set
		{
			Att_wiliness__BackingField = value;
		}
	}

	public float Att_generosity
	{
		get
		{
			return Att_generosity__BackingField;
		}
		set
		{
			Att_generosity__BackingField = value;
		}
	}

	public float Att_fame
	{
		get
		{
			return Att_fame__BackingField;
		}
		set
		{
			Att_fame__BackingField = value;
		}
	}

	public float Att_athleticism
	{
		get
		{
			return Att_athleticism__BackingField;
		}
		set
		{
			Att_athleticism__BackingField = value;
		}
	}

	public float Att_money
	{
		get
		{
			return Att_money__BackingField;
		}
		set
		{
			Att_money__BackingField = value;
		}
	}

	public float Att_craziness
	{
		get
		{
			return Att_craziness__BackingField;
		}
		set
		{
			Att_craziness__BackingField = value;
		}
	}

	public float Att_loyalty
	{
		get
		{
			return Att_loyalty__BackingField;
		}
		set
		{
			Att_loyalty__BackingField = value;
		}
	}

	public float Att_connection
	{
		get
		{
			return Att_connection__BackingField;
		}
		set
		{
			Att_connection__BackingField = value;
		}
	}

	public float Att_charisma
	{
		get
		{
			return Att_charisma__BackingField;
		}
		set
		{
			Att_charisma__BackingField = value;
		}
	}

	public float Att_music
	{
		get
		{
			return Att_music__BackingField;
		}
		set
		{
			Att_music__BackingField = value;
		}
	}

	public float Att_acting
	{
		get
		{
			return Att_acting__BackingField;
		}
		set
		{
			Att_acting__BackingField = value;
		}
	}

	public float Att_modeling
	{
		get
		{
			return Att_modeling__BackingField;
		}
		set
		{
			Att_modeling__BackingField = value;
		}
	}

	public float Att_dealing
	{
		get
		{
			return Att_dealing__BackingField;
		}
		set
		{
			Att_dealing__BackingField = value;
		}
	}

	public float ShortTermAppearancePoints
	{
		get
		{
			return ShortTermAppearancePoints__BackingField;
		}
		set
		{
			ShortTermAppearancePoints__BackingField = value;
		}
	}

	public SimPlace PlaceOfBirth
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int GenderAtBirth
	{
		get
		{
			return GenderAtBirth__BackingField;
		}
		set
		{
			GenderAtBirth__BackingField = value;
		}
	}

	public SimPersonName NameAtBirth
	{
		get
		{
			return NameAtBirth__BackingField;
		}
		set
		{
			NameAtBirth__BackingField = value;
		}
	}

	public int SkinColor
	{
		get
		{
			return SkinColor__BackingField;
		}
		set
		{
			SkinColor__BackingField = value;
		}
	}

	public int LgbtqGenderInclination
	{
		get
		{
			return LgbtqGenderInclination__BackingField;
		}
		set
		{
			LgbtqGenderInclination__BackingField = value;
		}
	}

	public float HeroRelationshipStrength
	{
		get
		{
			return HeroRelationshipStrength__BackingField;
		}
		set
		{
			HeroRelationshipStrength__BackingField = value;
		}
	}

	public int HeroAgeAtLastConversation
	{
		get
		{
			return HeroAgeAtLastConversation__BackingField;
		}
		set
		{
			HeroAgeAtLastConversation__BackingField = value;
		}
	}

	public int HeroAgeAtLastSpendTime
	{
		get
		{
			return HeroAgeAtLastSpendTime__BackingField;
		}
		set
		{
			HeroAgeAtLastSpendTime__BackingField = value;
		}
	}

	public int HeroAgeAtLastMourn
	{
		get
		{
			return HeroAgeAtLastMourn__BackingField;
		}
		set
		{
			HeroAgeAtLastMourn__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskForMoney
	{
		get
		{
			return HeroAgeAtLastAskForMoney__BackingField;
		}
		set
		{
			HeroAgeAtLastAskForMoney__BackingField = value;
		}
	}

	public int HeroTimesAskedForMoneyThisYear
	{
		get
		{
			return HeroTimesAskedForMoneyThisYear__BackingField;
		}
		set
		{
			HeroTimesAskedForMoneyThisYear__BackingField = value;
		}
	}

	public int HeroAgeAtLastAskForBicycle
	{
		get
		{
			return HeroAgeAtLastAskForBicycle__BackingField;
		}
		set
		{
			HeroAgeAtLastAskForBicycle__BackingField = value;
		}
	}

	public int HeroTimesAskedForBicycleThisYear
	{
		get
		{
			return HeroTimesAskedForBicycleThisYear__BackingField;
		}
		set
		{
			HeroTimesAskedForBicycleThisYear__BackingField = value;
		}
	}

	public int HeroTimesRefusedBicycleThisYear
	{
		get
		{
			return HeroTimesRefusedBicycleThisYear__BackingField;
		}
		set
		{
			HeroTimesRefusedBicycleThisYear__BackingField = value;
		}
	}

	public int HeroTimesAskedForMoney
	{
		get
		{
			return HeroTimesAskedForMoney__BackingField;
		}
		set
		{
			HeroTimesAskedForMoney__BackingField = value;
		}
	}

	public int HeroAgeAtLastGiveMoney
	{
		get
		{
			return HeroAgeAtLastGiveMoney__BackingField;
		}
		set
		{
			HeroAgeAtLastGiveMoney__BackingField = value;
		}
	}

	public int HeroAgeAtLastCompliment
	{
		get
		{
			return HeroAgeAtLastCompliment__BackingField;
		}
		set
		{
			HeroAgeAtLastCompliment__BackingField = value;
		}
	}

	public int HeroAgeAtLastMovie
	{
		get
		{
			return HeroAgeAtLastMovie__BackingField;
		}
		set
		{
			HeroAgeAtLastMovie__BackingField = value;
		}
	}

	public bool HeroGivenPoliceWarning
	{
		get
		{
			return HeroGivenPoliceWarning__BackingField;
		}
		set
		{
			HeroGivenPoliceWarning__BackingField = value;
		}
	}

	public int HeroAgeAtLastHeroGaveGift
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public int HeroAgeAtLastWroteLetterFromPrison
	{
		get
		{
			return HeroAgeAtLastWroteLetterFromPrison__BackingField;
		}
		set
		{
			HeroAgeAtLastWroteLetterFromPrison__BackingField = value;
		}
	}

	public int HeroAgeAtLastMurderAttempt
	{
		get
		{
			return HeroAgeAtLastMurderAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastMurderAttempt__BackingField = value;
		}
	}

	public int HeroAgeAtLastWatchedYouTube
	{
		get
		{
			return HeroAgeAtLastWatchedYouTube__BackingField;
		}
		set
		{
			HeroAgeAtLastWatchedYouTube__BackingField = value;
		}
	}

	public int HeroAgeAtLastUnfriendAttempt
	{
		get
		{
			return HeroAgeAtLastUnfriendAttempt__BackingField;
		}
		set
		{
			HeroAgeAtLastUnfriendAttempt__BackingField = value;
		}
	}

	public int HeroAgeWhenLastSued
	{
		get
		{
			return HeroAgeWhenLastSued__BackingField;
		}
		set
		{
			HeroAgeWhenLastSued__BackingField = value;
		}
	}

	public int HeroAgeAtLastConcert
	{
		get
		{
			return HeroAgeAtLastConcert__BackingField;
		}
		set
		{
			HeroAgeAtLastConcert__BackingField = value;
		}
	}

	public int HeroAgeAtLastHeroCheatedWithPartner
	{
		get
		{
			return HeroAgeAtLastHeroCheatedWithPartner__BackingField;
		}
		set
		{
			HeroAgeAtLastHeroCheatedWithPartner__BackingField = value;
		}
	}

	public int HeroAgeAtLastDelinquentBehavior
	{
		get
		{
			return HeroAgeAtLastDelinquentBehavior__BackingField;
		}
		set
		{
			HeroAgeAtLastDelinquentBehavior__BackingField = value;
		}
	}

	public int HeroAgeAtLastRejectedBigTicketItem
	{
		get
		{
			return HeroAgeAtLastRejectedBigTicketItem__BackingField;
		}
		set
		{
			HeroAgeAtLastRejectedBigTicketItem__BackingField = value;
		}
	}

	public int HeroAgeAtLastGiveHouse
	{
		get
		{
			return HeroAgeAtLastGiveHouse__BackingField;
		}
		set
		{
			HeroAgeAtLastGiveHouse__BackingField = value;
		}
	}

	public int HeroAgeAtLastGiveCar
	{
		get
		{
			return HeroAgeAtLastGiveCar__BackingField;
		}
		set
		{
			HeroAgeAtLastGiveCar__BackingField = value;
		}
	}

	public int HeroAgeAtLastStartedRumor
	{
		get
		{
			return HeroAgeAtLastStartedRumor__BackingField;
		}
		set
		{
			HeroAgeAtLastStartedRumor__BackingField = value;
		}
	}

	public int FriendStatus
	{
		get
		{
			return FriendStatus__BackingField;
		}
		set
		{
			FriendStatus__BackingField = value;
		}
	}

	public int YearsFriends
	{
		get
		{
			return YearsFriends__BackingField;
		}
		set
		{
			YearsFriends__BackingField = value;
		}
	}

	public bool ChildhoodFriend
	{
		get
		{
			return ChildhoodFriend__BackingField;
		}
		set
		{
			ChildhoodFriend__BackingField = value;
		}
	}

	public int HeroAgeAtLastBestFriendRequest
	{
		get
		{
			return HeroAgeAtLastBestFriendRequest__BackingField;
		}
		set
		{
			HeroAgeAtLastBestFriendRequest__BackingField = value;
		}
	}

	public int HeroAgeAtLastBecameEnemies
	{
		get
		{
			return HeroAgeAtLastBecameEnemies__BackingField;
		}
		set
		{
			HeroAgeAtLastBecameEnemies__BackingField = value;
		}
	}

	public int HeroAgeAtLastInsult
	{
		get
		{
			return HeroAgeAtLastInsult__BackingField;
		}
		set
		{
			HeroAgeAtLastInsult__BackingField = value;
		}
	}

	public int HeroAgeAtLastRejectedMarketAdvice
	{
		get
		{
			return HeroAgeAtLastRejectedMarketAdvice__BackingField;
		}
		set
		{
			HeroAgeAtLastRejectedMarketAdvice__BackingField = value;
		}
	}

	public int HeroAgeAtLastGaveMarketAdvice
	{
		get
		{
			return HeroAgeAtLastGaveMarketAdvice__BackingField;
		}
		set
		{
			HeroAgeAtLastGaveMarketAdvice__BackingField = value;
		}
	}

	public string MarketAdviceString
	{
		get
		{
			return MarketAdviceString__BackingField;
		}
		set
		{
			MarketAdviceString__BackingField = value;
		}
	}

	public string MarketTipString
	{
		get
		{
			return MarketTipString__BackingField;
		}
		set
		{
			MarketTipString__BackingField = value;
		}
	}

	public bool HasWarnedHeroAboutDealingDrugs
	{
		get
		{
			return HasWarnedHeroAboutDealingDrugs__BackingField;
		}
		set
		{
			HasWarnedHeroAboutDealingDrugs__BackingField = value;
		}
	}

	public int TimesRejectedHeroForDealingThemNonDrugs
	{
		get
		{
			return TimesRejectedHeroForDealingThemNonDrugs__BackingField;
		}
		set
		{
			TimesRejectedHeroForDealingThemNonDrugs__BackingField = value;
		}
	}

	public int TimesRejectedHeroForDealingThemDrugs
	{
		get
		{
			return TimesRejectedHeroForDealingThemDrugs__BackingField;
		}
		set
		{
			TimesRejectedHeroForDealingThemDrugs__BackingField = value;
		}
	}

	public Dictionary<string, AssetAdviceDict> SpecificAssetAdviceDict
	{
		get
		{
			return SpecificAssetAdviceDict__BackingField;
		}
		set
		{
			SpecificAssetAdviceDict__BackingField = value;
		}
	}

	public SimPerson Proxy
	{
		get
		{
			return Proxy__BackingField;
		}
		set
		{
			Proxy__BackingField = value;
		}
	}

	public int HeroAgeAtLastGiveAuctionItem
	{
		get
		{
			return HeroAgeAtLastGiveAuctionItem__BackingField;
		}
		set
		{
			HeroAgeAtLastGiveAuctionItem__BackingField = value;
		}
	}

	public SimVampire Vampire
	{
		get
		{
			return Vampire__BackingField;
		}
		set
		{
			Vampire__BackingField = value;
		}
	}

	public bool VampireDiseaseCarrier
	{
		get
		{
			return VampireDiseaseCarrier__BackingField;
		}
		set
		{
			VampireDiseaseCarrier__BackingField = value;
		}
	}

	public bool HasCult()
	{
		return false;
	}

	public virtual bool WillAcceptHookup()
	{
		return false;
	}

	public string Description()
	{
		return null;
	}

	public SimPerson()
	{
	}

	public SimPerson(bool initHere)
	{
	}

	public SimPerson(SimPerson otherPerson)
	{
	}

	public void SetRandomHeroRelationshipStrength(int min, int max, int offset = 0)
	{
	}

	public float GetTotalHeroRelationshipStrength()
	{
		return 0f;
	}

	public float GetRawTotalHeroRelationshipStrength()
	{
		return 0f;
	}

	protected void MarkIncrementAgeForCurrentGeneration()
	{
	}

	public virtual void IncrementAge()
	{
	}

	public virtual void OnLifeIncrementAgeEnded()
	{
	}

	private bool DidIncrementAge()
	{
		return false;
	}

	private void IncrementAgeOrDeath()
	{
	}

	public void IncrementYearsSinceDeath()
	{
	}

	public virtual void Die()
	{
	}

	public virtual Scenario TryGetPostDeathScenario()
	{
		return null;
	}

	public virtual void AnnualAttributeAdjustment()
	{
	}

	public virtual bool ShouldSufferMiscarriage()
	{
		return false;
	}

	public virtual float AnnualCostOfLiving()
	{
		return 0f;
	}

	public string FriendRelationshipStatusString()
	{
		return null;
	}

	public virtual string GetRelationshipString()
	{
		return null;
	}

	public virtual string GetFormattedRelationshipString()
	{
		return null;
	}

	public virtual string GetPreferredName()
	{
		return null;
	}

	public string RelationshipString()
	{
		return null;
	}

	public string RelationshipStringTitle()
	{
		return null;
	}

	public string RelationshipTemplate()
	{
		return null;
	}

	public void IncrementYearsAsFriend()
	{
	}

	public bool IsFamilyMember()
	{
		return false;
	}

	public bool IsFriend()
	{
		return false;
	}

	public void Unfriend()
	{
	}

	public bool HeroDeservesToBeUnfriended()
	{
		return false;
	}

	public virtual int FeelingsForHeroScore()
	{
		return 0;
	}

	public bool SexualityAlignedWithHero()
	{
		return false;
	}

	public bool IsCurrentLover()
	{
		return false;
	}

	public bool IsExLover()
	{
		return false;
	}

	public bool DoesLoverProxyExist()
	{
		return false;
	}

	public bool IsForeign()
	{
		return false;
	}

	public bool IsGay()
	{
		return false;
	}

	public bool HasIdentifiedAsDifferentGender()
	{
		return false;
	}

	public bool HasIdentifiedAsNonBinary()
	{
		return false;
	}

	public int EffectiveGender()
	{
		return 0;
	}

	public void SetHeroAgeAtLastGift()
	{
	}

	public virtual bool WillAgreeToSpendTime()
	{
		return false;
	}

	public virtual void ImpactHappiness(int impactScore)
	{
	}

	public virtual void ImpactHealth(int impactScore)
	{
	}

	public virtual void ImpactWillpower(int impactScore)
	{
	}

	public virtual void ImpactIntelligence(int impactScore)
	{
	}

	public virtual void ImpactAppearance(int impactScore)
	{
	}

	public void ImpactKarma(int impactScore)
	{
	}

	public void ImpactDiscipline(int impactScore)
	{
	}

	public virtual void ImpactFame(int impactScore)
	{
	}

	public void ImpactAthleticism(int impactScore)
	{
	}

	public void ImpactCraziness(int impactScore)
	{
	}

	public void ImpactLoyalty(int impactScore)
	{
	}

	public void ImpactMusic(int impactScore)
	{
	}

	public void ImpactActing(int impactScore)
	{
	}

	public void ImpactModeling(int impactScore)
	{
	}

	public void ImpactDealing(int impactScore)
	{
	}

	public void ImpactFertility(int impactScore)
	{
	}

	public void ImpactCharisma(int impactScore)
	{
	}

	public void ShortTermImpactAppearance(int impactScore)
	{
	}

	public string FullName()
	{
		return null;
	}

	public string FirstName()
	{
		return null;
	}

	public bool IsCult()
	{
		return false;
	}

	public string GenderString()
	{
		return null;
	}

	public string SexualityString()
	{
		return null;
	}

	public virtual string EducationString()
	{
		return null;
	}

	public virtual bool IsOccupationFamousMusician()
	{
		return false;
	}

	public virtual bool IsOccupationFamousActor()
	{
		return false;
	}

	public virtual bool IsFamousSinger()
	{
		return false;
	}

	public virtual bool IsRoyal()
	{
		return false;
	}

	public virtual bool IsRoyalty()
	{
		return false;
	}

	public virtual bool IsActor()
	{
		return false;
	}

	public bool IsBasketballPlayer()
	{
		return false;
	}

	public virtual bool IsSpecificSportsPlayer(SPORT_TYPE sportsType)
	{
		return false;
	}

	public virtual bool IsPolitician()
	{
		return false;
	}

	public virtual string OccupationString()
	{
		return null;
	}

	public string DescriptiveOccupationString()
	{
		return null;
	}

	public string EmployerName()
	{
		return null;
	}

	public string ShortOccupationString()
	{
		return null;
	}

	public string PronounStringForType(PRONOUN_TYPE type)
	{
		return null;
	}

	public string Pronoun(int type)
	{
		return null;
	}

	public string PronounStringForType(int type)
	{
		return null;
	}

	public string ReplacePronounsInString(string text)
	{
		return null;
	}

	public void ReplacePronounsInStringBuilder(StringBuilder sb)
	{
	}

	public bool LivesInSamePlaceAsHero()
	{
		return false;
	}

	public bool LivesInSamePlaceAsPerson(SimPerson person)
	{
		return false;
	}

	public bool HasProxyWhoIsPerson(SimPerson person)
	{
		return false;
	}

	public bool IsProbablyTheSameAsPerson(SimPerson person)
	{
		return false;
	}

	public static bool ArePersonsEqual(SimPerson left, SimPerson right)
	{
		return false;
	}

	private bool IsProxyEqual(SimPerson person)
	{
		return false;
	}

	public virtual void ImpactRelationshipStrength(int impactScore)
	{
	}

	public virtual float AskForMoneyAmount()
	{
		return 0f;
	}

	public SimPerson ThirdPerson()
	{
		return null;
	}

	public virtual string ThirdPersonLogIdentityStringWithStartOfYearName(SimPersonName cachedName)
	{
		return null;
	}

	public virtual float ThirdPersonSuccessPropensityScore()
	{
		return 0f;
	}

	public virtual string ThirdPersonSexualityString()
	{
		return null;
	}

	public virtual void ThirdPersonComesOutOfTheCloset()
	{
	}

	public virtual bool ThirdPersonIsEligibleToComeOutOfTheCloset()
	{
		return false;
	}

	public virtual void ThirdPersonIdentifiesGenderTrans()
	{
	}

	public virtual void ThirdPersonChangesNameAfterIdentifiesGenderTrans()
	{
	}

	public virtual void ThirdPersonIdentifiesGenderOther()
	{
	}

	public virtual string ThirdPersonGenderIdentityString()
	{
		return null;
	}

	public virtual void ThirdPersonStartsWearingAccessoryHeadwear()
	{
	}

	public virtual void ThirdPersonStopsWearingAccessoryHeadwear()
	{
	}

	public virtual void ThirdPersonStartsWearingAccessorySunglasses()
	{
	}

	public virtual void ThirdPersonStopsWearingAccessorySunglasses()
	{
	}

	public virtual void ThirdPersonStartsWearingAccessoryMask()
	{
	}

	public virtual void ThirdPersonStopsWearingAccessoryMask()
	{
	}

	public virtual bool ThirdPersonShouldObserveVaccination()
	{
		return false;
	}

	public virtual void ThirdPersonEarnsIncome(float income)
	{
	}

	public virtual bool ThirdPersonMovesOutAndShouldAnnounce()
	{
		return false;
	}

	public virtual void ThirdPersonMarries()
	{
	}

	public virtual void ThirdPersonChangesNameAfterMarriage()
	{
	}

	public virtual void ThirdPersonDivorces()
	{
	}

	public virtual void ThirdPersonChangesNameAfterDivorce()
	{
	}

	public virtual bool ThirdPersonTriesForBaby()
	{
		return false;
	}

	public virtual void ThirdPersonHasHerosUnknownBaby()
	{
	}

	public virtual string ThirdPersonBabyBirthString()
	{
		return null;
	}

	public virtual string ThirdPersonSpouseIdentityStringWithDescription(bool includeDescription)
	{
		return null;
	}

	public virtual void ThirdPersonBreaksUp()
	{
	}

	public bool IsVampire()
	{
		return false;
	}

	public bool IsSourceVampireOfHero()
	{
		return false;
	}

	public bool HeroIsSourceVampire()
	{
		return false;
	}

	public virtual void BecomeVampire(int infectedAge, bool shouldGetVampireName, SimPerson sourceVampire = null)
	{
	}

	public bool CanBecomeVampire()
	{
		return false;
	}

	public virtual bool IsNearbyAndCanDie()
	{
		return false;
	}

	public void SuckBloodFromPerson(SimPerson person, bool fullyDrain = false)
	{
	}

	private void OnHeroDrainedBlood(SimPerson person, bool fullyDrain)
	{
	}

	public void ConvertPersonToVampire(SimPerson person, string reason = "")
	{
	}

	public void ImpactByDamage(int damage)
	{
	}

	public virtual bool CanBeKilledByDeathReason(DeathActivity deathActivity)
	{
		return false;
	}

	public static bool IsCompatibleCouple(SimPerson lover, SimPerson hero)
	{
		return false;
	}

	public string GenderDescriptor()
	{
		return null;
	}

	private bool _003CDie_003Eb__397_0(SimCommunard x)
	{
		return false;
	}
}
