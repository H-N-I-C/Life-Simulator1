using System.Collections.Generic;

public class DataDiseaseType
{
	private string Name__BackingField;

	private bool UsesIndefiniteArticle__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	private List<string> PrimarySymptomArray__BackingField;

	private List<string> SecondarySymptomArray__BackingField;

	private bool ImpactsBodyPart__BackingField;

	private bool FighterOnly__BackingField;

	private int Gender__BackingField;

	private bool SingleYearDisease__BackingField;

	private int MinAge__BackingField;

	private int MaxAge__BackingField;

	private bool CanBeFatal__BackingField;

	private bool NoCure__BackingField;

	private int ContractsOrganicallyScore__BackingField;

	private int CuresOrganicallyScore__BackingField;

	private int TreatmentEffectivenessScore__BackingField;

	private int HealthImpactScore__BackingField;

	private int HappinessImpactScore__BackingField;

	private int AppearanceImpactScore__BackingField;

	private int IntelligenceImpactScore__BackingField;

	private int CostScore__BackingField;

	private int SeriousnessScore__BackingField;

	private bool TreatedByChiropractor__BackingField;

	private bool TreatedByAcupuncture__BackingField;

	private bool ThirdPersonsContract__BackingField;

	private bool IsFunny__BackingField;

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

	public bool UsesIndefiniteArticle
	{
		get
		{
			return UsesIndefiniteArticle__BackingField;
		}
		set
		{
			UsesIndefiniteArticle__BackingField = value;
		}
	}

	public bool UsesDefiniteArticle
	{
		get
		{
			return UsesDefiniteArticle__BackingField;
		}
		set
		{
			UsesDefiniteArticle__BackingField = value;
		}
	}

	public List<string> PrimarySymptomArray
	{
		get
		{
			return PrimarySymptomArray__BackingField;
		}
		set
		{
			PrimarySymptomArray__BackingField = value;
		}
	}

	public List<string> SecondarySymptomArray
	{
		get
		{
			return SecondarySymptomArray__BackingField;
		}
		set
		{
			SecondarySymptomArray__BackingField = value;
		}
	}

	public bool ImpactsBodyPart
	{
		get
		{
			return ImpactsBodyPart__BackingField;
		}
		set
		{
			ImpactsBodyPart__BackingField = value;
		}
	}

	public bool FighterOnly
	{
		get
		{
			return FighterOnly__BackingField;
		}
		set
		{
			FighterOnly__BackingField = value;
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

	public bool SingleYearDisease
	{
		get
		{
			return SingleYearDisease__BackingField;
		}
		set
		{
			SingleYearDisease__BackingField = value;
		}
	}

	public int MinAge
	{
		get
		{
			return MinAge__BackingField;
		}
		set
		{
			MinAge__BackingField = value;
		}
	}

	public int MaxAge
	{
		get
		{
			return MaxAge__BackingField;
		}
		set
		{
			MaxAge__BackingField = value;
		}
	}

	public bool CanBeFatal
	{
		get
		{
			return CanBeFatal__BackingField;
		}
		set
		{
			CanBeFatal__BackingField = value;
		}
	}

	public bool NoCure
	{
		get
		{
			return NoCure__BackingField;
		}
		set
		{
			NoCure__BackingField = value;
		}
	}

	public int ContractsOrganicallyScore
	{
		get
		{
			return ContractsOrganicallyScore__BackingField;
		}
		set
		{
			ContractsOrganicallyScore__BackingField = value;
		}
	}

	public int CuresOrganicallyScore
	{
		get
		{
			return CuresOrganicallyScore__BackingField;
		}
		set
		{
			CuresOrganicallyScore__BackingField = value;
		}
	}

	public int TreatmentEffectivenessScore
	{
		get
		{
			return TreatmentEffectivenessScore__BackingField;
		}
		set
		{
			TreatmentEffectivenessScore__BackingField = value;
		}
	}

	public int HealthImpactScore
	{
		get
		{
			return HealthImpactScore__BackingField;
		}
		set
		{
			HealthImpactScore__BackingField = value;
		}
	}

	public int HappinessImpactScore
	{
		get
		{
			return HappinessImpactScore__BackingField;
		}
		set
		{
			HappinessImpactScore__BackingField = value;
		}
	}

	public int AppearanceImpactScore
	{
		get
		{
			return AppearanceImpactScore__BackingField;
		}
		set
		{
			AppearanceImpactScore__BackingField = value;
		}
	}

	public int IntelligenceImpactScore
	{
		get
		{
			return IntelligenceImpactScore__BackingField;
		}
		set
		{
			IntelligenceImpactScore__BackingField = value;
		}
	}

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public int SeriousnessScore
	{
		get
		{
			return SeriousnessScore__BackingField;
		}
		set
		{
			SeriousnessScore__BackingField = value;
		}
	}

	public bool TreatedByChiropractor
	{
		get
		{
			return TreatedByChiropractor__BackingField;
		}
		set
		{
			TreatedByChiropractor__BackingField = value;
		}
	}

	public bool TreatedByAcupuncture
	{
		get
		{
			return TreatedByAcupuncture__BackingField;
		}
		set
		{
			TreatedByAcupuncture__BackingField = value;
		}
	}

	public bool ThirdPersonsContract
	{
		get
		{
			return ThirdPersonsContract__BackingField;
		}
		set
		{
			ThirdPersonsContract__BackingField = value;
		}
	}

	public bool IsFunny
	{
		get
		{
			return IsFunny__BackingField;
		}
		set
		{
			IsFunny__BackingField = value;
		}
	}

	public bool IsSTD()
	{
		return false;
	}

	public bool IsBloodTransfusionDisease()
	{
		return false;
	}

	public bool IsDepression()
	{
		return false;
	}

	public bool IsAnxiety()
	{
		return false;
	}

	public bool IsHighBloodPressure()
	{
		return false;
	}

	public bool IsPTSD()
	{
		return false;
	}

	public bool IsGenderDysphoria()
	{
		return false;
	}

	public bool CanBeTreatedByMedicalDoctor()
	{
		return false;
	}

	public int ThirdPersonCureScore()
	{
		return 0;
	}
}
