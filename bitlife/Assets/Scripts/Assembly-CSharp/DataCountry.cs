using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class DataCountry
{
	private string Name__BackingField;

	private string Continent__BackingField;

	private string Code__BackingField;

	private bool DefiniteArticle__BackingField;

	private string Demonym__BackingField;

	private string Adjectival__BackingField;

	private string Language__BackingField;

	private string Currency__BackingField;

	private bool ReversesNames__BackingField;

	public List<string> Cities;

	private string Religion__BackingField;

	public List<string> Disasters;

	private int TravelAppeal__BackingField;

	private int Volatility__BackingField;

	private int Strictness__BackingField;

	private int Corruption__BackingField;

	private int WarZoneDanger__BackingField;

	private long Population__BackingField;

	private float PriceOfCoke__BackingField;

	private float RentIndex__BackingField;

	private float Hdi__BackingField;

	private int WorkWeek__BackingField;

	private float TopTaxRate__BackingField;

	private float EstateTaxRate__BackingField;

	private int LifeExpectancyMale__BackingField;

	private int LifeExpectancyFemale__BackingField;

	private int RetirementAgeMale__BackingField;

	private int RetirementAgeFemale__BackingField;

	private int DrivingAge__BackingField;

	private int ConsentAge__BackingField;

	private int DrinkingAge__BackingField;

	private string SchoolTier1Name__BackingField;

	private string SchoolTier2Name__BackingField;

	private string SchoolTier3Name__BackingField;

	private int SchoolTier1StartAge__BackingField;

	private int SchoolTier2StartAge__BackingField;

	private int SchoolTier3StartAge__BackingField;

	private int SchoolGraduationAge__BackingField;

	private int SchoolLegalDropoutAge__BackingField;

	public List<string> DeathPenaltyMethods;

	private bool Prostitution__BackingField;

	private bool Gambling__BackingField;

	private bool GaySex__BackingField;

	private bool GayMarriage__BackingField;

	private bool FreeCollege__BackingField;

	private bool Alcohol__BackingField;

	private bool Abortion__BackingField;

	private bool ConjugalVisits__BackingField;

	private bool ArrangedMarriage__BackingField;

	private bool FreeHealthcare__BackingField;

	private bool SocialMedia__BackingField;

	private bool Surrogacy__BackingField;

	private int SkinColor1__BackingField;

	private int SkinColor2__BackingField;

	private int SkinColor3__BackingField;

	private bool NameChangingMale__BackingField;

	private bool NameChangingFemale__BackingField;

	private bool NameChangingHyphenation__BackingField;

	public List<string> ImmigrantCountries;

	public List<DataName> MaleNames;

	public List<DataName> FemaleNames;

	public List<DataName> LastNames;

	public string Name
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

	public string Continent
	{
		get
		{
			return Continent__BackingField;
		}
		set
		{
			Continent__BackingField = value;
		}
	}

	public string Code
	{
		get
		{
			return Code__BackingField;
		}
		set
		{
			Code__BackingField = value;
		}
	}

	public bool DefiniteArticle
	{
		get
		{
			return DefiniteArticle__BackingField;
		}
		set
		{
			DefiniteArticle__BackingField = value;
		}
	}

	public string Demonym
	{
		get
		{
			return Demonym__BackingField;
		}
		set
		{
			Demonym__BackingField = value;
		}
	}

	public string Adjectival
	{
		get
		{
			return Adjectival__BackingField;
		}
		set
		{
			Adjectival__BackingField = value;
		}
	}

	public string Language
	{
		get
		{
			return Language__BackingField;
		}
		set
		{
			Language__BackingField = value;
		}
	}

	public string Currency
	{
		get
		{
			return Currency__BackingField;
		}
		set
		{
			Currency__BackingField = value;
		}
	}

	public bool ReversesNames
	{
		get
		{
			return ReversesNames__BackingField;
		}
		set
		{
			ReversesNames__BackingField = value;
		}
	}

	public string Religion
	{
		get
		{
			return Religion__BackingField;
		}
		set
		{
			Religion__BackingField = value;
		}
	}

	public int TravelAppeal
	{
		get
		{
			return TravelAppeal__BackingField;
		}
		set
		{
			TravelAppeal__BackingField = value;
		}
	}

	public int Volatility
	{
		get
		{
			return Volatility__BackingField;
		}
		set
		{
			Volatility__BackingField = value;
		}
	}

	public int Strictness
	{
		get
		{
			return Strictness__BackingField;
		}
		set
		{
			Strictness__BackingField = value;
		}
	}

	public int Corruption
	{
		get
		{
			return Corruption__BackingField;
		}
		set
		{
			Corruption__BackingField = value;
		}
	}

	public int WarZoneDanger
	{
		get
		{
			return WarZoneDanger__BackingField;
		}
		set
		{
			WarZoneDanger__BackingField = value;
		}
	}

	public long Population
	{
		get
		{
			return Population__BackingField;
		}
		set
		{
			Population__BackingField = value;
		}
	}

	public float PriceOfCoke
	{
		get
		{
			return PriceOfCoke__BackingField;
		}
		set
		{
			PriceOfCoke__BackingField = value;
		}
	}

	public float RentIndex
	{
		get
		{
			return RentIndex__BackingField;
		}
		set
		{
			RentIndex__BackingField = value;
		}
	}

	public float Hdi
	{
		get
		{
			return Hdi__BackingField;
		}
		set
		{
			Hdi__BackingField = value;
		}
	}

	public int WorkWeek
	{
		get
		{
			return WorkWeek__BackingField;
		}
		set
		{
			WorkWeek__BackingField = value;
		}
	}

	public float TopTaxRate
	{
		get
		{
			return TopTaxRate__BackingField;
		}
		set
		{
			TopTaxRate__BackingField = value;
		}
	}

	public float EstateTaxRate
	{
		get
		{
			return EstateTaxRate__BackingField;
		}
		set
		{
			EstateTaxRate__BackingField = value;
		}
	}

	public int LifeExpectancyMale
	{
		get
		{
			return LifeExpectancyMale__BackingField;
		}
		set
		{
			LifeExpectancyMale__BackingField = value;
		}
	}

	public int LifeExpectancyFemale
	{
		get
		{
			return LifeExpectancyFemale__BackingField;
		}
		set
		{
			LifeExpectancyFemale__BackingField = value;
		}
	}

	public int RetirementAgeMale
	{
		get
		{
			return RetirementAgeMale__BackingField;
		}
		set
		{
			RetirementAgeMale__BackingField = value;
		}
	}

	public int RetirementAgeFemale
	{
		get
		{
			return RetirementAgeFemale__BackingField;
		}
		set
		{
			RetirementAgeFemale__BackingField = value;
		}
	}

	public int DrivingAge
	{
		get
		{
			return DrivingAge__BackingField;
		}
		set
		{
			DrivingAge__BackingField = value;
		}
	}

	public int ConsentAge
	{
		get
		{
			return ConsentAge__BackingField;
		}
		set
		{
			ConsentAge__BackingField = value;
		}
	}

	public int DrinkingAge
	{
		get
		{
			return DrinkingAge__BackingField;
		}
		set
		{
			DrinkingAge__BackingField = value;
		}
	}

	public string SchoolTier1Name
	{
		get
		{
			return SchoolTier1Name__BackingField;
		}
		set
		{
			SchoolTier1Name__BackingField = value;
		}
	}

	public string SchoolTier2Name
	{
		get
		{
			return SchoolTier2Name__BackingField;
		}
		set
		{
			SchoolTier2Name__BackingField = value;
		}
	}

	public string SchoolTier3Name
	{
		get
		{
			return SchoolTier3Name__BackingField;
		}
		set
		{
			SchoolTier3Name__BackingField = value;
		}
	}

	public int SchoolTier1StartAge
	{
		get
		{
			return SchoolTier1StartAge__BackingField;
		}
		set
		{
			SchoolTier1StartAge__BackingField = value;
		}
	}

	public int SchoolTier2StartAge
	{
		get
		{
			return SchoolTier2StartAge__BackingField;
		}
		set
		{
			SchoolTier2StartAge__BackingField = value;
		}
	}

	public int SchoolTier3StartAge
	{
		get
		{
			return SchoolTier3StartAge__BackingField;
		}
		set
		{
			SchoolTier3StartAge__BackingField = value;
		}
	}

	public int SchoolGraduationAge
	{
		get
		{
			return SchoolGraduationAge__BackingField;
		}
		set
		{
			SchoolGraduationAge__BackingField = value;
		}
	}

	public int SchoolLegalDropoutAge
	{
		get
		{
			return SchoolLegalDropoutAge__BackingField;
		}
		set
		{
			SchoolLegalDropoutAge__BackingField = value;
		}
	}

	public bool Prostitution
	{
		get
		{
			return Prostitution__BackingField;
		}
		set
		{
			Prostitution__BackingField = value;
		}
	}

	public bool Gambling
	{
		get
		{
			return Gambling__BackingField;
		}
		set
		{
			Gambling__BackingField = value;
		}
	}

	public bool GaySex
	{
		get
		{
			return GaySex__BackingField;
		}
		set
		{
			GaySex__BackingField = value;
		}
	}

	public bool GayMarriage
	{
		get
		{
			return GayMarriage__BackingField;
		}
		set
		{
			GayMarriage__BackingField = value;
		}
	}

	public bool FreeCollege
	{
		get
		{
			return FreeCollege__BackingField;
		}
		set
		{
			FreeCollege__BackingField = value;
		}
	}

	public bool Alcohol
	{
		get
		{
			return Alcohol__BackingField;
		}
		set
		{
			Alcohol__BackingField = value;
		}
	}

	public bool Abortion
	{
		get
		{
			return Abortion__BackingField;
		}
		set
		{
			Abortion__BackingField = value;
		}
	}

	public bool ConjugalVisits
	{
		get
		{
			return ConjugalVisits__BackingField;
		}
		set
		{
			ConjugalVisits__BackingField = value;
		}
	}

	public bool ArrangedMarriage
	{
		get
		{
			return ArrangedMarriage__BackingField;
		}
		set
		{
			ArrangedMarriage__BackingField = value;
		}
	}

	public bool FreeHealthcare
	{
		get
		{
			return FreeHealthcare__BackingField;
		}
		set
		{
			FreeHealthcare__BackingField = value;
		}
	}

	public bool SocialMedia
	{
		get
		{
			return SocialMedia__BackingField;
		}
		set
		{
			SocialMedia__BackingField = value;
		}
	}

	public bool Surrogacy
	{
		get
		{
			return Surrogacy__BackingField;
		}
		set
		{
			Surrogacy__BackingField = value;
		}
	}

	public int SkinColor1
	{
		get
		{
			return SkinColor1__BackingField;
		}
		set
		{
			SkinColor1__BackingField = value;
		}
	}

	public int SkinColor2
	{
		get
		{
			return SkinColor2__BackingField;
		}
		set
		{
			SkinColor2__BackingField = value;
		}
	}

	public int SkinColor3
	{
		get
		{
			return SkinColor3__BackingField;
		}
		set
		{
			SkinColor3__BackingField = value;
		}
	}

	public bool NameChangingMale
	{
		get
		{
			return NameChangingMale__BackingField;
		}
		set
		{
			NameChangingMale__BackingField = value;
		}
	}

	public bool NameChangingFemale
	{
		get
		{
			return NameChangingFemale__BackingField;
		}
		set
		{
			NameChangingFemale__BackingField = value;
		}
	}

	public bool NameChangingHyphenation
	{
		get
		{
			return NameChangingHyphenation__BackingField;
		}
		set
		{
			NameChangingHyphenation__BackingField = value;
		}
	}

	public string Description()
	{
		return null;
	}

	public string FullName()
	{
		return null;
	}

	public string GetRandomFirstNameForGender(int gender)
	{
		return null;
	}

	public string GetRandomFirstNameForGenderAndTryToStartWithSameLetterAsName(int gender, string otherName)
	{
		return null;
	}

	public string GetRandomLastNameAndCanBeCustom(bool canBeCustom)
	{
		return null;
	}

	public SimPersonName GetRandomFullPersonNameOfGenderAndCanBeCustomAndBlockFromFutureUse(int gender, bool canBeCustom, bool block)
	{
		return null;
	}

	public SimPersonName CreateCelebDatingAppSimLoverName(string firstName, string lastName)
	{
		return null;
	}

	public bool CountryHasHyphenatedNames()
	{
		return false;
	}

	public ArrayList GetMaleNamesStringArray()
	{
		return null;
	}

	public ArrayList GetFemaleNamesStringArray()
	{
		return null;
	}

	public ArrayList GetLastNamesStringArray()
	{
		return null;
	}

	public ArrayList GetCitiesArray()
	{
		return null;
	}

	public string GetRandomCity()
	{
		return null;
	}

	public string GetRandomTopTierCity()
	{
		return null;
	}

	public string GetRandomBottomTierCity()
	{
		return null;
	}

	public bool HasDeathPenalty()
	{
		return false;
	}

	public int GetLifeExpectancyForGender(int gender)
	{
		return 0;
	}

	public int GetRetirementAgeForGender(int gender)
	{
		return 0;
	}

	public int GetARandomSkinColor()
	{
		return 0;
	}

	public int GetASkinColorForNameAndGender(SimPersonName name, int gender)
	{
		return 0;
	}

	public int PercentOfPopulationToOverrideToBeBlack()
	{
		return 0;
	}

	public bool MiddleSchoolExists()
	{
		return false;
	}

	public int SchoolTierForAge(int age)
	{
		return 0;
	}

	public int YearsInSchoolTier(int tier)
	{
		return 0;
	}

	public int YearsLeftInSchoolTierForAge(int tier, int age)
	{
		return 0;
	}

	public int AdjustForCostOfLiving(double amount)
	{
		return 0;
	}

	public float AdjustFloatForCostOfLiving(float amount)
	{
		return 0f;
	}

	public float GetLocalFloatCostForAmount(float amount, float minCost, int roundTo)
	{
		return 0f;
	}

	public int GetLocalCostForAmountWithMinimumCostAndRoundTo(float amount, int minCost, int roundTo)
	{
		return 0;
	}

	public int GetLocalRealEstateCostForAmountWithMinimumCostAndRoundTo(float amount, int minCost, int roundTo)
	{
		return 0;
	}

	public int GetWarZoneAnnualDeathOdds()
	{
		return 0;
	}
}
