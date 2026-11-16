using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimModelingAgencyClient
{
	private sealed class _003C_003Ec__DisplayClass37_0
	{
		public MODEL_CAREER_TYPE type;

		internal bool _003CHasModelTypeGig_003Eb__0(DataModelCategory t)
		{
			return false;
		}
	}

	private string ClientName__BackingField;

	private MODEL_INDUSTRY ClientIndustry__BackingField;

	private float ClientRelationship__BackingField;

	private float Profile__BackingField;

	private float Reputation__BackingField;

	private int HeroAgeAtLastInteract__BackingField;

	private int HeroAgeAtLastGoSee__BackingField;

	private GENDER TypicalGender__BackingField;

	private List<string> MainCategoriesList__BackingField;

	public string ClientName
	{
		get
		{
			return ClientName__BackingField;
		}
		set
		{
			ClientName__BackingField = value;
		}
	}

	public MODEL_INDUSTRY ClientIndustry
	{
		get
		{
			return ClientIndustry__BackingField;
		}
		set
		{
			ClientIndustry__BackingField = value;
		}
	}

	public float ClientRelationship
	{
		get
		{
			return ClientRelationship__BackingField;
		}
		set
		{
			ClientRelationship__BackingField = value;
		}
	}

	public float Profile
	{
		get
		{
			return Profile__BackingField;
		}
		set
		{
			Profile__BackingField = value;
		}
	}

	public float Reputation
	{
		get
		{
			return Reputation__BackingField;
		}
		set
		{
			Reputation__BackingField = value;
		}
	}

	public int HeroAgeAtLastInteract
	{
		get
		{
			return HeroAgeAtLastInteract__BackingField;
		}
		set
		{
			HeroAgeAtLastInteract__BackingField = value;
		}
	}

	public int HeroAgeAtLastGoSee
	{
		get
		{
			return HeroAgeAtLastGoSee__BackingField;
		}
		set
		{
			HeroAgeAtLastGoSee__BackingField = value;
		}
	}

	public GENDER TypicalGender
	{
		get
		{
			return TypicalGender__BackingField;
		}
		set
		{
			TypicalGender__BackingField = value;
		}
	}

	public List<string> MainCategoriesList
	{
		get
		{
			return MainCategoriesList__BackingField;
		}
		set
		{
			MainCategoriesList__BackingField = value;
		}
	}

	public void ImpactRelationship(int impactScore)
	{
	}

	public bool HasModelTypeGig(MODEL_CAREER_TYPE type)
	{
		return false;
	}
}
