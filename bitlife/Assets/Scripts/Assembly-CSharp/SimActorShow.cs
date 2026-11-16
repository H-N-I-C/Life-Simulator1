using System;
using System.Collections;

[Serializable]
public class SimActorShow
{
	private string Title__BackingField;

	private bool IsEnsemble__BackingField;

	private string _RawTitle;

	private ArrayList RoleArray__BackingField;

	private SimCompany TelevisionCompany__BackingField;

	private int YearsRunning__BackingField;

	private float CachedAverageRelationshipStrength__BackingField;

	private float Att_quality__BackingField;

	private float Att_budget__BackingField;

	private float Att_popularity__BackingField;

	private float Att_potential__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public bool IsEnsemble
	{
		get
		{
			return IsEnsemble__BackingField;
		}
		set
		{
			IsEnsemble__BackingField = value;
		}
	}

	public DataActorShow Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ArrayList RoleArray
	{
		get
		{
			return RoleArray__BackingField;
		}
		set
		{
			RoleArray__BackingField = value;
		}
	}

	public SimCompany TelevisionCompany
	{
		get
		{
			return TelevisionCompany__BackingField;
		}
		set
		{
			TelevisionCompany__BackingField = value;
		}
	}

	public int YearsRunning
	{
		get
		{
			return YearsRunning__BackingField;
		}
		set
		{
			YearsRunning__BackingField = value;
		}
	}

	public float CachedAverageRelationshipStrength
	{
		get
		{
			return CachedAverageRelationshipStrength__BackingField;
		}
		set
		{
			CachedAverageRelationshipStrength__BackingField = value;
		}
	}

	public float Att_quality
	{
		get
		{
			return Att_quality__BackingField;
		}
		set
		{
			Att_quality__BackingField = value;
		}
	}

	public float Att_budget
	{
		get
		{
			return Att_budget__BackingField;
		}
		set
		{
			Att_budget__BackingField = value;
		}
	}

	public float Att_popularity
	{
		get
		{
			return Att_popularity__BackingField;
		}
		set
		{
			Att_popularity__BackingField = value;
		}
	}

	public float Att_potential
	{
		get
		{
			return Att_potential__BackingField;
		}
		set
		{
			Att_potential__BackingField = value;
		}
	}

	public string GetTitleWithQuotes()
	{
		return null;
	}

	public void ImpactPopularity(int impactScore)
	{
	}
}
