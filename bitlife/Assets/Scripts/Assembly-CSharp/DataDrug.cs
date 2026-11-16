using System.Collections;

public class DataDrug
{
	private string Name__BackingField;

	private ArrayList NicknameArray__BackingField;

	private int AddictivenessScore__BackingField;

	private int OverdoseLikelinessScore__BackingField;

	private int CommonnessScore__BackingField;

	private int HealthDamageScore__BackingField;

	private int EnlightenmentLikelinessScore__BackingField;

	private bool SusceptibleToDrugTests__BackingField;

	private bool Illegal__BackingField;

	private bool ClubDrug__BackingField;

	private bool VipClubDrug__BackingField;

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

	public ArrayList NicknameArray
	{
		get
		{
			return NicknameArray__BackingField;
		}
		set
		{
			NicknameArray__BackingField = value;
		}
	}

	public int AddictivenessScore
	{
		get
		{
			return AddictivenessScore__BackingField;
		}
		set
		{
			AddictivenessScore__BackingField = value;
		}
	}

	public int OverdoseLikelinessScore
	{
		get
		{
			return OverdoseLikelinessScore__BackingField;
		}
		set
		{
			OverdoseLikelinessScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public int HealthDamageScore
	{
		get
		{
			return HealthDamageScore__BackingField;
		}
		set
		{
			HealthDamageScore__BackingField = value;
		}
	}

	public int EnlightenmentLikelinessScore
	{
		get
		{
			return EnlightenmentLikelinessScore__BackingField;
		}
		set
		{
			EnlightenmentLikelinessScore__BackingField = value;
		}
	}

	public bool SusceptibleToDrugTests
	{
		get
		{
			return SusceptibleToDrugTests__BackingField;
		}
		set
		{
			SusceptibleToDrugTests__BackingField = value;
		}
	}

	public bool Illegal
	{
		get
		{
			return Illegal__BackingField;
		}
		set
		{
			Illegal__BackingField = value;
		}
	}

	public bool ClubDrug
	{
		get
		{
			return ClubDrug__BackingField;
		}
		set
		{
			ClubDrug__BackingField = value;
		}
	}

	public bool VipClubDrug
	{
		get
		{
			return VipClubDrug__BackingField;
		}
		set
		{
			VipClubDrug__BackingField = value;
		}
	}

	public string GetNameOrNickname()
	{
		return null;
	}
}
