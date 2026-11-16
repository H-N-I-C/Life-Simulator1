using System;

[Serializable]
public class SimDoctor
{
	private SimPersonName Name__BackingField;

	private int Gender__BackingField;

	private float Att_cost__BackingField;

	private float Att_competence__BackingField;

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

	public float Att_cost
	{
		get
		{
			return Att_cost__BackingField;
		}
		set
		{
			Att_cost__BackingField = value;
		}
	}

	public float Att_competence
	{
		get
		{
			return Att_competence__BackingField;
		}
		set
		{
			Att_competence__BackingField = value;
		}
	}

	private SimDoctor()
	{
	}

	public SimDoctor(DataCountry country, bool highQuality)
	{
	}

	public string NameString()
	{
		return null;
	}

	public float CostForTreatingDisease(SimDisease disease)
	{
		return 0f;
	}

	public bool CuresDisease(SimDisease disease)
	{
		return false;
	}

	public float CostForPerformingPlasticSurgery(DataPlasticSurgery plasticSurgery)
	{
		return 0f;
	}

	public bool SucceedsAtPlasticSurgery(DataPlasticSurgery plasticSurgery)
	{
		return false;
	}
}
