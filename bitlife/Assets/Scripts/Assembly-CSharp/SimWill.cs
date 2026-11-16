using System;

[Serializable]
public class SimWill
{
	private int BeneficiaryType__BackingField;

	private string SoleBeneficiaryName__BackingField;

	public int BeneficiaryType
	{
		get
		{
			return BeneficiaryType__BackingField;
		}
		set
		{
			BeneficiaryType__BackingField = value;
		}
	}

	public string SoleBeneficiaryName
	{
		get
		{
			return SoleBeneficiaryName__BackingField;
		}
		set
		{
			SoleBeneficiaryName__BackingField = value;
		}
	}

	private SimWill()
	{
	}

	public SimWill(bool initHere)
	{
	}
}
