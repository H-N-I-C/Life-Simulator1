using System;

[Serializable]
public class SimActorFilmographyFilm
{
	private string FilmTitle__BackingField;

	private int Genre__BackingField;

	private string RoleTitle__BackingField;

	private int RoleType__BackingField;

	private string DirectorFullNameString__BackingField;

	private string FilmStudioName__BackingField;

	private float Budget__BackingField;

	private float BoxOfficeReceipts__BackingField;

	private float Att_success__BackingField;

	private float Att_reception__BackingField;

	private float Att_performance__BackingField;

	private float Compensation__BackingField;

	private float BoxOfficeBonus__BackingField;

	private float AgentFee__BackingField;

	private string ReceptionBlurb__BackingField;

	private int HeroAgeAtRelease__BackingField;

	private SimActorAward Award__BackingField;

	public string FilmTitle
	{
		get
		{
			return FilmTitle__BackingField;
		}
		set
		{
			FilmTitle__BackingField = value;
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

	public string DirectorFullNameString
	{
		get
		{
			return DirectorFullNameString__BackingField;
		}
		set
		{
			DirectorFullNameString__BackingField = value;
		}
	}

	public string FilmStudioName
	{
		get
		{
			return FilmStudioName__BackingField;
		}
		set
		{
			FilmStudioName__BackingField = value;
		}
	}

	public float Budget
	{
		get
		{
			return Budget__BackingField;
		}
		set
		{
			Budget__BackingField = value;
		}
	}

	public float BoxOfficeReceipts
	{
		get
		{
			return BoxOfficeReceipts__BackingField;
		}
		set
		{
			BoxOfficeReceipts__BackingField = value;
		}
	}

	public float Att_success
	{
		get
		{
			return Att_success__BackingField;
		}
		set
		{
			Att_success__BackingField = value;
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

	public float Compensation
	{
		get
		{
			return Compensation__BackingField;
		}
		set
		{
			Compensation__BackingField = value;
		}
	}

	public float BoxOfficeBonus
	{
		get
		{
			return BoxOfficeBonus__BackingField;
		}
		set
		{
			BoxOfficeBonus__BackingField = value;
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

	public string ReceptionBlurb
	{
		get
		{
			return ReceptionBlurb__BackingField;
		}
		set
		{
			ReceptionBlurb__BackingField = value;
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

	public string GetSuccessBlurb()
	{
		return null;
	}
}
