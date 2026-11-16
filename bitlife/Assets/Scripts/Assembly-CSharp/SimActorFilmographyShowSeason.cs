using System;

[Serializable]
public class SimActorFilmographyShowSeason
{
	private string ShowTitle__BackingField;

	private int Genre__BackingField;

	private string RoleTitle__BackingField;

	private int RoleType__BackingField;

	private string TelevisionCompanyName__BackingField;

	private int Season__BackingField;

	private int NumEpisodes__BackingField;

	private float Att_performance__BackingField;

	private float Att_popularity__BackingField;

	private float Att_reception__BackingField;

	private float CompensationPerEpisode__BackingField;

	private float AgentFee__BackingField;

	private int HeroAgeAtRelease__BackingField;

	private SimActorAward Award__BackingField;

	public string ShowTitle
	{
		get
		{
			return ShowTitle__BackingField;
		}
		set
		{
			ShowTitle__BackingField = value;
		}
	}

	public int Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public string RoleTitle
	{
		get
		{
			return RoleTitle__BackingField;
		}
		set
		{
			RoleTitle__BackingField = value;
		}
	}

	public int RoleType
	{
		get
		{
			return RoleType__BackingField;
		}
		set
		{
			RoleType__BackingField = value;
		}
	}

	public string TelevisionCompanyName
	{
		get
		{
			return TelevisionCompanyName__BackingField;
		}
		set
		{
			TelevisionCompanyName__BackingField = value;
		}
	}

	public int Season
	{
		get
		{
			return Season__BackingField;
		}
		set
		{
			Season__BackingField = value;
		}
	}

	public int NumEpisodes
	{
		get
		{
			return NumEpisodes__BackingField;
		}
		set
		{
			NumEpisodes__BackingField = value;
		}
	}

	public float Att_performance
	{
		get
		{
			return Att_performance__BackingField;
		}
		set
		{
			Att_performance__BackingField = value;
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

	public float Att_reception
	{
		get
		{
			return Att_reception__BackingField;
		}
		set
		{
			Att_reception__BackingField = value;
		}
	}

	public float CompensationPerEpisode
	{
		get
		{
			return CompensationPerEpisode__BackingField;
		}
		set
		{
			CompensationPerEpisode__BackingField = value;
		}
	}

	public float AgentFee
	{
		get
		{
			return AgentFee__BackingField;
		}
		set
		{
			AgentFee__BackingField = value;
		}
	}

	public int HeroAgeAtRelease
	{
		get
		{
			return HeroAgeAtRelease__BackingField;
		}
		set
		{
			HeroAgeAtRelease__BackingField = value;
		}
	}

	public SimActorAward Award
	{
		get
		{
			return Award__BackingField;
		}
		set
		{
			Award__BackingField = value;
		}
	}

	public float GetTotalEarnings()
	{
		return 0f;
	}
}
