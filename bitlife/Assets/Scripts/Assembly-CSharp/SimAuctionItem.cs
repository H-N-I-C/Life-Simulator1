using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimAuctionItem
{
	private sealed class _003C_003Ec__DisplayClass116_0
	{
		public DataBlackMarketAntique data;

		internal bool _003CGenerateNewAntiqueAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass117_0
	{
		public DataBlackMarketWeapon data;

		internal bool _003CGenerateNewWeaponAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass118_0
	{
		public DataBlackMarketArt data;

		internal bool _003CGenerateNewArtAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass120_0
	{
		public DataBlackMarketSportsMemorabilia data;

		internal bool _003CGenerateNewSportAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass121_0
	{
		public DataBlackMarketHistoricalArtifact data;

		internal bool _003CGenerateNewHistoricalAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass122_0
	{
		public DataBlackMarketFashion data;

		internal bool _003CGenerateNewFashionAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass123_0
	{
		public DataBlackMarketAutomobiles data;

		internal bool _003CGenerateNewAutoAuctionItem_003Eb__0(SimAuctionItem t)
		{
			return false;
		}
	}

	private int Type__BackingField;

	private int UniqueID__BackingField;

	private int Age__BackingField;

	private float Condition__BackingField;

	private int AgeAtLastRefurbish__BackingField;

	private bool EverBeenRefurbished__BackingField;

	private bool IsFake__BackingField;

	private bool HeroKnowsAuthenticity__BackingField;

	private bool IsInMuseum__BackingField;

	private int AuctionOrigin__BackingField;

	private string AuctionOriginName__BackingField;

	private double ValueAtAcquisition__BackingField;

	private double AppraisalValue__BackingField;

	private int YearsInHeroPossession__BackingField;

	private int HeroAgeAtAcquisition__BackingField;

	private int HeroAgeLastPlayWithItem__BackingField;

	private float JewelCarat__BackingField;

	private bool HeroConsumedSubstance__BackingField;

	private SimPerson Person__BackingField;

	private SimZooAnimal ZooAnimal__BackingField;

	private SimPet Pet__BackingField;

	private string MetaHeroID__BackingField;

	private float TaxidermyValue__BackingField;

	private string DrugName__BackingField;

	private bool IsMurderWeapon__BackingField;

	private SimRaceCarDetails RaceCarDetails__BackingField;

	private SimCarRepair ActiveRepair__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int UniqueID
	{
		get
		{
			return UniqueID__BackingField;
		}
		set
		{
			UniqueID__BackingField = value;
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

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public int AgeAtLastRefurbish
	{
		get
		{
			return AgeAtLastRefurbish__BackingField;
		}
		set
		{
			AgeAtLastRefurbish__BackingField = value;
		}
	}

	public bool EverBeenRefurbished
	{
		get
		{
			return EverBeenRefurbished__BackingField;
		}
		set
		{
			EverBeenRefurbished__BackingField = value;
		}
	}

	public bool IsFake
	{
		get
		{
			return IsFake__BackingField;
		}
		set
		{
			IsFake__BackingField = value;
		}
	}

	public bool HeroKnowsAuthenticity
	{
		get
		{
			return HeroKnowsAuthenticity__BackingField;
		}
		set
		{
			HeroKnowsAuthenticity__BackingField = value;
		}
	}

	public bool IsInMuseum
	{
		get
		{
			return IsInMuseum__BackingField;
		}
		set
		{
			IsInMuseum__BackingField = value;
		}
	}

	public int AuctionOrigin
	{
		get
		{
			return AuctionOrigin__BackingField;
		}
		set
		{
			AuctionOrigin__BackingField = value;
		}
	}

	public string AuctionOriginName
	{
		get
		{
			return AuctionOriginName__BackingField;
		}
		set
		{
			AuctionOriginName__BackingField = value;
		}
	}

	public DataBlackMarketBase Data => null;

	public double ValueAtAcquisition
	{
		get
		{
			return ValueAtAcquisition__BackingField;
		}
		set
		{
			ValueAtAcquisition__BackingField = value;
		}
	}

	public double AppraisalValue
	{
		get
		{
			return AppraisalValue__BackingField;
		}
		set
		{
			AppraisalValue__BackingField = value;
		}
	}

	public int YearsInHeroPossession
	{
		get
		{
			return YearsInHeroPossession__BackingField;
		}
		set
		{
			YearsInHeroPossession__BackingField = value;
		}
	}

	public int HeroAgeAtAcquisition
	{
		get
		{
			return HeroAgeAtAcquisition__BackingField;
		}
		set
		{
			HeroAgeAtAcquisition__BackingField = value;
		}
	}

	public int HeroAgeLastPlayWithItem
	{
		get
		{
			return HeroAgeLastPlayWithItem__BackingField;
		}
		set
		{
			HeroAgeLastPlayWithItem__BackingField = value;
		}
	}

	public float JewelCarat
	{
		get
		{
			return JewelCarat__BackingField;
		}
		set
		{
			JewelCarat__BackingField = value;
		}
	}

	public bool HeroConsumedSubstance
	{
		get
		{
			return HeroConsumedSubstance__BackingField;
		}
		set
		{
			HeroConsumedSubstance__BackingField = value;
		}
	}

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public SimZooAnimal ZooAnimal
	{
		get
		{
			return ZooAnimal__BackingField;
		}
		set
		{
			ZooAnimal__BackingField = value;
		}
	}

	public SimPet Pet
	{
		get
		{
			return Pet__BackingField;
		}
		set
		{
			Pet__BackingField = value;
		}
	}

	public string MetaHeroID
	{
		get
		{
			return MetaHeroID__BackingField;
		}
		set
		{
			MetaHeroID__BackingField = value;
		}
	}

	public float TaxidermyValue
	{
		get
		{
			return TaxidermyValue__BackingField;
		}
		set
		{
			TaxidermyValue__BackingField = value;
		}
	}

	public string DrugName
	{
		get
		{
			return DrugName__BackingField;
		}
		set
		{
			DrugName__BackingField = value;
		}
	}

	public bool IsMurderWeapon
	{
		get
		{
			return IsMurderWeapon__BackingField;
		}
		set
		{
			IsMurderWeapon__BackingField = value;
		}
	}

	public SimRaceCarDetails RaceCarDetails
	{
		get
		{
			return RaceCarDetails__BackingField;
		}
		set
		{
			RaceCarDetails__BackingField = value;
		}
	}

	public SimCarRepair ActiveRepair
	{
		get
		{
			return ActiveRepair__BackingField;
		}
		set
		{
			ActiveRepair__BackingField = value;
		}
	}

	public void IncrementAge()
	{
	}

	public static int GetMinScoreForCost(double cost)
	{
		return 0;
	}

	public string GetEmojiString()
	{
		return null;
	}

	public float GenerateNewPrice(float withMinimum = 0f)
	{
		return 0f;
	}

	public int OddsBasedRarityItem()
	{
		return 0;
	}

	private static List<SimAuctionItem> FindAuctionItemInAllPlaces()
	{
		return null;
	}

	public static SimAuctionItem GenerateNewAntiqueAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewWeaponAuctionItem(float reputation, bool mustBeRare)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewArtAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewJewelryAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewSportAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewHistoricalAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewFashionAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewAutoAuctionItem(MARKET_GENERATE_ITEM searchType, float reputation, bool mustBeRare, string auctionHouseName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewTaxidermyItem(SimPerson person, string taxidermistName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewTaxidermyItem(SimZooAnimal zooAnimal, string taxidermistName)
	{
		return null;
	}

	public static SimAuctionItem GenerateNewTaxidermyItem(SimPet pet, string taxidermistName)
	{
		return null;
	}

	public string GetRacingClass()
	{
		return null;
	}

	public double GetEstimatedValue()
	{
		return 0.0;
	}

	public double GenerateNewPriceIfFakeAndCondition()
	{
		return 0.0;
	}

	public int GetYearsOwned()
	{
		return 0;
	}

	public string GetYearsOwnedString()
	{
		return null;
	}

	public void ImpactConditionByAmount(int amount)
	{
	}

	public void RefurbishItem()
	{
	}

	public double RefurbishCost()
	{
		return 0.0;
	}

	public List<int> GetChemistryEffectsImpacts()
	{
		return null;
	}

	public string GetHighestImpactTypeString()
	{
		return null;
	}

	public ArrayList ItemChemistryImpactHero(bool cut)
	{
		return null;
	}

	public int GetRiskValueForChemistry()
	{
		return 0;
	}

	public bool IsUnique()
	{
		return false;
	}

	public string GetPersonOrAnimalRelationshipString()
	{
		return null;
	}

	public int GetPersonOrAnimalAge()
	{
		return 0;
	}

	public double GetPersonOrAnimalNetWorth()
	{
		return 0.0;
	}

	public string GetPersonOrAnimalPronounString(int type)
	{
		return null;
	}

	public string GetPersonOrAnimalEmojiString()
	{
		return null;
	}

	public bool IsPerson()
	{
		return false;
	}

	public bool IsAnimal()
	{
		return false;
	}

	public string GetTaxidermyTitleName()
	{
		return null;
	}

	public string GetPersonOrAnimalGenericRelationshipString()
	{
		return null;
	}

	public int GetCommonnessScore()
	{
		return 0;
	}

	public int GetAppealScore()
	{
		return 0;
	}

	public bool IsPossessiveName()
	{
		return false;
	}

	public bool CanAcceptHaggleValue(float haggleValue, double currentPrice)
	{
		return false;
	}

	public bool IsPlural()
	{
		return false;
	}

	public string GetItemPossessiveName()
	{
		return null;
	}

	public string GetItemTypeName()
	{
		return null;
	}

	public string GetItemAuthenticityString()
	{
		return null;
	}

	public string GetItemShortName()
	{
		return null;
	}

	public string GetItemName(bool canShowFakeName = true)
	{
		return null;
	}

	public string GetFakeItemName()
	{
		return null;
	}

	public string GetItemNameByRaf(bool overrideArticle = false)
	{
		return null;
	}

	public string GetOriginString()
	{
		return null;
	}
}
