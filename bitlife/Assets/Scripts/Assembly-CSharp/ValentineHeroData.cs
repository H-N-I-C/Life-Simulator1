using System;
using System.Collections.Generic;

public class ValentineHeroData
{
	public class Job
	{
		private string CountryCode__BackingField;

		private string CareerName__BackingField;

		private int SeniorityTrackPositionZI__BackingField;

		private SimCompany Company__BackingField;

		public string CountryCode
		{
			get
			{
				return CountryCode__BackingField;
			}
			set
			{
				CountryCode__BackingField = value;
			}
		}

		public string CareerName
		{
			get
			{
				return CareerName__BackingField;
			}
			set
			{
				CareerName__BackingField = value;
			}
		}

		public int SeniorityTrackPositionZI
		{
			get
			{
				return SeniorityTrackPositionZI__BackingField;
			}
			set
			{
				SeniorityTrackPositionZI__BackingField = value;
			}
		}

		public SimCompany Company
		{
			get
			{
				return Company__BackingField;
			}
			set
			{
				Company__BackingField = value;
			}
		}

		public Job()
		{
		}

		public Job(SimJob job)
		{
		}

		public SimJob CreateJob(DataCareer career)
		{
			return null;
		}
	}

	public class Person
	{
		private SimPersonName Name__BackingField;

		private SimPersonName NameAtBirth__BackingField;

		private PersonEmojiScript EmojiScript__BackingField;

		private int Gender__BackingField;

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

		private string PlaceOfBirthString__BackingField;

		private int GenderAtBirth__BackingField;

		private int SkinColor__BackingField;

		private int LgbtqGenderInclination__BackingField;

		private bool IsVampire__BackingField;

		private int Age__BackingField;

		private int Sexuality__BackingField;

		private int LgbtqGenderIdentity__BackingField;

		private bool OnBirthControl__BackingField;

		private int AccessoryHeadwearIndex__BackingField;

		private int AccessorySunglassesIndex__BackingField;

		private int AccessoryMaskIndex__BackingField;

		private int AccessoryForeheadTattooIndex__BackingField;

		private int AccessoryLeftCheekTattooIndex__BackingField;

		private int AccessoryRightCheekTattooIndex__BackingField;

		private bool IsFamous__BackingField;

		private bool IsRoyal__BackingField;

		private int HighestCompletedSchoolType__BackingField;

		private List<string> Diseases__BackingField;

		private Vampire Vampire__BackingField;

		private Fame Fame__BackingField;

		private Royalty Royalty__BackingField;

		private Occupation Occupation__BackingField;

		private double NetWorth__BackingField;

		private bool OverrideNetWorth__BackingField;

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

		public string PlaceOfBirthString
		{
			get
			{
				return PlaceOfBirthString__BackingField;
			}
			set
			{
				PlaceOfBirthString__BackingField = value;
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

		public bool IsVampire
		{
			get
			{
				return IsVampire__BackingField;
			}
			set
			{
				IsVampire__BackingField = value;
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

		public bool IsFamous
		{
			get
			{
				return IsFamous__BackingField;
			}
			set
			{
				IsFamous__BackingField = value;
			}
		}

		public bool IsRoyal
		{
			get
			{
				return IsRoyal__BackingField;
			}
			set
			{
				IsRoyal__BackingField = value;
			}
		}

		public int HighestCompletedSchoolType
		{
			get
			{
				return HighestCompletedSchoolType__BackingField;
			}
			set
			{
				HighestCompletedSchoolType__BackingField = value;
			}
		}

		public List<string> Diseases
		{
			get
			{
				return Diseases__BackingField;
			}
			set
			{
				Diseases__BackingField = value;
			}
		}

		public Vampire Vampire
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

		public Fame Fame
		{
			get
			{
				return Fame__BackingField;
			}
			set
			{
				Fame__BackingField = value;
			}
		}

		public Royalty Royalty
		{
			get
			{
				return Royalty__BackingField;
			}
			set
			{
				Royalty__BackingField = value;
			}
		}

		public Occupation Occupation
		{
			get
			{
				return Occupation__BackingField;
			}
			set
			{
				Occupation__BackingField = value;
			}
		}

		public double NetWorth
		{
			get
			{
				return NetWorth__BackingField;
			}
			set
			{
				NetWorth__BackingField = value;
			}
		}

		public bool OverrideNetWorth
		{
			get
			{
				return OverrideNetWorth__BackingField;
			}
			set
			{
				OverrideNetWorth__BackingField = value;
			}
		}

		public Person(SimHero person)
		{
		}

		public SimPerson GetAsSimPerson()
		{
			return null;
		}

		public Person()
		{
		}

		private static string GetFameStringFromFameType(FAME_TYPE fameType)
		{
			return null;
		}
	}

	public class Fame
	{
		private FAME_TYPE CelebrityFameType__BackingField;

		private int CelebrityGenre__BackingField;

		public FAME_TYPE CelebrityFameType
		{
			get
			{
				return CelebrityFameType__BackingField;
			}
			set
			{
				CelebrityFameType__BackingField = value;
			}
		}

		public int CelebrityGenre
		{
			get
			{
				return CelebrityGenre__BackingField;
			}
			set
			{
				CelebrityGenre__BackingField = value;
			}
		}

		public Fame(Life life, SimHero hero)
		{
		}

		public Fame()
		{
		}
	}

	public class Royalty
	{
		private int RoyalRank__BackingField;

		private string RoyalSeat__BackingField;

		private bool RoyalByBirth__BackingField;

		private bool RoyalDowager__BackingField;

		public int RoyalRank
		{
			get
			{
				return RoyalRank__BackingField;
			}
			set
			{
				RoyalRank__BackingField = value;
			}
		}

		public string RoyalSeat
		{
			get
			{
				return RoyalSeat__BackingField;
			}
			set
			{
				RoyalSeat__BackingField = value;
			}
		}

		public bool RoyalByBirth
		{
			get
			{
				return RoyalByBirth__BackingField;
			}
			set
			{
				RoyalByBirth__BackingField = value;
			}
		}

		public bool RoyalDowager
		{
			get
			{
				return RoyalDowager__BackingField;
			}
			set
			{
				RoyalDowager__BackingField = value;
			}
		}
	}

	public class Military
	{
		private int MilitaryBranch__BackingField;

		private int MilitaryStatus__BackingField;

		private int MilitaryRank__BackingField;

		private int AgeAtMilitaryStart__BackingField;

		private int MilitaryPromotions__BackingField;

		public int MilitaryBranch
		{
			get
			{
				return MilitaryBranch__BackingField;
			}
			set
			{
				MilitaryBranch__BackingField = value;
			}
		}

		public int MilitaryStatus
		{
			get
			{
				return MilitaryStatus__BackingField;
			}
			set
			{
				MilitaryStatus__BackingField = value;
			}
		}

		public int MilitaryRank
		{
			get
			{
				return MilitaryRank__BackingField;
			}
			set
			{
				MilitaryRank__BackingField = value;
			}
		}

		public int AgeAtMilitaryStart
		{
			get
			{
				return AgeAtMilitaryStart__BackingField;
			}
			set
			{
				AgeAtMilitaryStart__BackingField = value;
			}
		}

		public int MilitaryPromotions
		{
			get
			{
				return MilitaryPromotions__BackingField;
			}
			set
			{
				MilitaryPromotions__BackingField = value;
			}
		}

		public Military(SimOccupationMilitary simOccupationMilitary, int age)
		{
		}

		public Military()
		{
		}
	}

	public class Occupation
	{
		private OCCUPATION_TYPE OccupationType__BackingField;

		private int SchoolType__BackingField;

		private string CareerName__BackingField;

		private Job Job__BackingField;

		private SimCompany Company__BackingField;

		private int AgeAtLastJobStart__BackingField;

		private int AgeAtLastPromotion__BackingField;

		private int AgeAtRetirement__BackingField;

		private float Pension__BackingField;

		private Military Military__BackingField;

		public OCCUPATION_TYPE OccupationType
		{
			get
			{
				return OccupationType__BackingField;
			}
			set
			{
				OccupationType__BackingField = value;
			}
		}

		public int SchoolType
		{
			get
			{
				return SchoolType__BackingField;
			}
			set
			{
				SchoolType__BackingField = value;
			}
		}

		public string CareerName
		{
			get
			{
				return CareerName__BackingField;
			}
			set
			{
				CareerName__BackingField = value;
			}
		}

		public Job Job
		{
			get
			{
				return Job__BackingField;
			}
			set
			{
				Job__BackingField = value;
			}
		}

		public SimCompany Company
		{
			get
			{
				return Company__BackingField;
			}
			set
			{
				Company__BackingField = value;
			}
		}

		public int AgeAtLastJobStart
		{
			get
			{
				return AgeAtLastJobStart__BackingField;
			}
			set
			{
				AgeAtLastJobStart__BackingField = value;
			}
		}

		public int AgeAtLastPromotion
		{
			get
			{
				return AgeAtLastPromotion__BackingField;
			}
			set
			{
				AgeAtLastPromotion__BackingField = value;
			}
		}

		public int AgeAtRetirement
		{
			get
			{
				return AgeAtRetirement__BackingField;
			}
			set
			{
				AgeAtRetirement__BackingField = value;
			}
		}

		public float Pension
		{
			get
			{
				return Pension__BackingField;
			}
			set
			{
				Pension__BackingField = value;
			}
		}

		public Military Military
		{
			get
			{
				return Military__BackingField;
			}
			set
			{
				Military__BackingField = value;
			}
		}

		public Occupation(Life life, int age, bool isRoyal, bool isCelebrity)
		{
		}

		public Occupation()
		{
		}
	}

	public class Vampire
	{
		private SimVampireHonorific VampireHonorific__BackingField;

		private VAMPIRE_FAVORITE_BLOOD VampireFavoriteBlood__BackingField;

		private string VampireBloodRestrictionType__BackingField;

		private VAMPIRE_BLOOD_RESTRICTION VampireBloodRestriction__BackingField;

		private VAMPIRE_BLOOD_CURSE VampireBloodCurse__BackingField;

		private float VampireEssence__BackingField;

		private int VampireInfectedAge__BackingField;

		private string VampireAnimalForm__BackingField;

		public SimVampireHonorific VampireHonorific
		{
			get
			{
				return VampireHonorific__BackingField;
			}
			set
			{
				VampireHonorific__BackingField = value;
			}
		}

		public VAMPIRE_FAVORITE_BLOOD VampireFavoriteBlood
		{
			get
			{
				return VampireFavoriteBlood__BackingField;
			}
			set
			{
				VampireFavoriteBlood__BackingField = value;
			}
		}

		public string VampireBloodRestrictionType
		{
			get
			{
				return VampireBloodRestrictionType__BackingField;
			}
			set
			{
				VampireBloodRestrictionType__BackingField = value;
			}
		}

		public VAMPIRE_BLOOD_RESTRICTION VampireBloodRestriction
		{
			get
			{
				return VampireBloodRestriction__BackingField;
			}
			set
			{
				VampireBloodRestriction__BackingField = value;
			}
		}

		public VAMPIRE_BLOOD_CURSE VampireBloodCurse
		{
			get
			{
				return VampireBloodCurse__BackingField;
			}
			set
			{
				VampireBloodCurse__BackingField = value;
			}
		}

		public float VampireEssence
		{
			get
			{
				return VampireEssence__BackingField;
			}
			set
			{
				VampireEssence__BackingField = value;
			}
		}

		public int VampireInfectedAge
		{
			get
			{
				return VampireInfectedAge__BackingField;
			}
			set
			{
				VampireInfectedAge__BackingField = value;
			}
		}

		public string VampireAnimalForm
		{
			get
			{
				return VampireAnimalForm__BackingField;
			}
			set
			{
				VampireAnimalForm__BackingField = value;
			}
		}

		public Vampire(SimVampire simVampire)
		{
		}

		public Vampire()
		{
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<object, string> _003C_003E9__12_0;

		internal string _003CGetSerializedHero_003Eb__12_0(object q)
		{
			return null;
		}
	}

	public string LifeId;

	private Person Hero__BackingField;

	public Person Hero
	{
		get
		{
			return Hero__BackingField;
		}
		set
		{
			Hero__BackingField = value;
		}
	}

	public static string GetSerializedHero()
	{
		return null;
	}

	public static SimPerson GetDeserializedFriend(string json)
	{
		return null;
	}
}
