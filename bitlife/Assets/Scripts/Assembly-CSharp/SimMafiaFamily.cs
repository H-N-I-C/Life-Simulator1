using System;
using System.Collections;

[Serializable]
public class SimMafiaFamily
{
	private string Name__BackingField;

	[NonSerialized]
	public DataMafiaMob _Mob;

	private string MobName__BackingField;

	private string CallingCardTableForm__BackingField;

	private string CallingCardWithArticle__BackingField;

	private string OperatingCountryCode__BackingField;

	private ArrayList _MobsterArray__BackingField;

	private int AgeAtLastMobsterRefresh__BackingField;

	private string RivalFamilyName__BackingField;

	private ArrayList _RivalFamilyMobsterArray__BackingField;

	private bool KnowsHowToFindHero__BackingField;

	private bool HeroSharesMobHeritage__BackingField;

	private int AgeAtLastTriedToJoin__BackingField;

	private float Att_notoriety__BackingField;

	private float Att_money__BackingField;

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

	public DataMafiaMob Mob
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string MobName
	{
		get
		{
			return MobName__BackingField;
		}
		set
		{
			MobName__BackingField = value;
		}
	}

	public string CallingCardTableForm
	{
		get
		{
			return CallingCardTableForm__BackingField;
		}
		set
		{
			CallingCardTableForm__BackingField = value;
		}
	}

	public string CallingCardWithArticle
	{
		get
		{
			return CallingCardWithArticle__BackingField;
		}
		set
		{
			CallingCardWithArticle__BackingField = value;
		}
	}

	public string OperatingCountryCode
	{
		get
		{
			return OperatingCountryCode__BackingField;
		}
		set
		{
			OperatingCountryCode__BackingField = value;
		}
	}

	private ArrayList _MobsterArray
	{
		get
		{
			return _MobsterArray__BackingField;
		}
		set
		{
			_MobsterArray__BackingField = value;
		}
	}

	public ArrayList MobsterArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int AgeAtLastMobsterRefresh
	{
		get
		{
			return AgeAtLastMobsterRefresh__BackingField;
		}
		set
		{
			AgeAtLastMobsterRefresh__BackingField = value;
		}
	}

	public string RivalFamilyName
	{
		get
		{
			return RivalFamilyName__BackingField;
		}
		set
		{
			RivalFamilyName__BackingField = value;
		}
	}

	private ArrayList _RivalFamilyMobsterArray
	{
		get
		{
			return _RivalFamilyMobsterArray__BackingField;
		}
		set
		{
			_RivalFamilyMobsterArray__BackingField = value;
		}
	}

	public ArrayList RivalFamilyMobsterArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool KnowsHowToFindHero
	{
		get
		{
			return KnowsHowToFindHero__BackingField;
		}
		set
		{
			KnowsHowToFindHero__BackingField = value;
		}
	}

	public bool HeroSharesMobHeritage
	{
		get
		{
			return HeroSharesMobHeritage__BackingField;
		}
		set
		{
			HeroSharesMobHeritage__BackingField = value;
		}
	}

	public int AgeAtLastTriedToJoin
	{
		get
		{
			return AgeAtLastTriedToJoin__BackingField;
		}
		set
		{
			AgeAtLastTriedToJoin__BackingField = value;
		}
	}

	public float Att_notoriety
	{
		get
		{
			return Att_notoriety__BackingField;
		}
		set
		{
			Att_notoriety__BackingField = value;
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

	public SimMafiaFamily(DataMafiaMob theMob)
	{
	}

	public string GetNameWithFamilyAndArticle(bool family, bool article)
	{
		return null;
	}

	public string GetRivalFamilyNameWithFamilyAndArticle(bool family, bool article)
	{
		return null;
	}

	public string GetNicknameForHero()
	{
		return null;
	}

	public void SetupMobsterArrays()
	{
	}

	public void IncrementYearsInFamily()
	{
	}

	public void RefreshMobsters()
	{
	}

	public void HandlePromotionsUpToRank(int rankNI)
	{
	}

	public void LookToPromoteMobstersFromRankToRank(int fromRankNI, int toRankNI)
	{
	}

	public void GenerateMobstersToFillInRank(int rankNI)
	{
	}

	public int GetHeroRank()
	{
		return 0;
	}

	public SimSpecialJobMafia GetSpecialJob()
	{
		return null;
	}

	public SimMobster GetGodfather()
	{
		return null;
	}

	public SimMobster GetHeroBoss()
	{
		return null;
	}

	public SimMobster GetRandomFamilyMobster()
	{
		return null;
	}

	public SimMobster GetRandomRivalFamilyMobster()
	{
		return null;
	}

	public SimMobster GetSnitch()
	{
		return null;
	}

	public SimMobster GetSpendTimeMobster()
	{
		return null;
	}

	public ArrayList GetAllMobstersUnderHero()
	{
		return null;
	}

	public float GetAverageRelationshipStrengthWithFamily()
	{
		return 0f;
	}

	public void ImpactAllRelationshipStrengths(int impactScore)
	{
	}

	public static int GetNumberOfPositionsForRank(int rankNI)
	{
		return 0;
	}
}
