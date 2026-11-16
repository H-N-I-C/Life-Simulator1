using System;

[Serializable]
public class SimCostar : SimLover, IComparable
{
	private int ActorType__BackingField;

	private int RoleType__BackingField;

	private string RoleTitle__BackingField;

	private float Compensation__BackingField;

	private int Genre__BackingField;

	private int HeroAgeAtLastRehearsed__BackingField;

	private int HeroAgeAtLastRehearseRejection__BackingField;

	private int HeroAgeAtLastDiss__BackingField;

	private float CurrentPerformance__BackingField;

	public int ActorType
	{
		get
		{
			return ActorType__BackingField;
		}
		set
		{
			ActorType__BackingField = value;
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

	public int HeroAgeAtLastRehearsed
	{
		get
		{
			return HeroAgeAtLastRehearsed__BackingField;
		}
		set
		{
			HeroAgeAtLastRehearsed__BackingField = value;
		}
	}

	public int HeroAgeAtLastRehearseRejection
	{
		get
		{
			return HeroAgeAtLastRehearseRejection__BackingField;
		}
		set
		{
			HeroAgeAtLastRehearseRejection__BackingField = value;
		}
	}

	public int HeroAgeAtLastDiss
	{
		get
		{
			return HeroAgeAtLastDiss__BackingField;
		}
		set
		{
			HeroAgeAtLastDiss__BackingField = value;
		}
	}

	public float CurrentPerformance
	{
		get
		{
			return CurrentPerformance__BackingField;
		}
		set
		{
			CurrentPerformance__BackingField = value;
		}
	}

	public SimCostar(bool initHere)
		: base(initHere: false)
	{
	}

	public void SetupForFilmRole(SimActorFilmRole role)
	{
	}

	public void SetupForShowRole(SimActorShowRole role)
	{
	}

	public void SetupAsExtra(int actorType)
	{
	}

	public void SetupAsDirector(SimActorFilm film)
	{
	}

	public override void IncrementAge()
	{
	}

	public new string RelationshipStatusString()
	{
		return null;
	}

	public int PropensityScoreToPlayFilmRole(SimActorFilmRole role)
	{
		return 0;
	}

	public int PropensityScoreToPlayShowRole(SimActorShowRole role)
	{
		return 0;
	}

	public string GetRoleTypeString(bool useArticle, bool useRole, bool capitalize)
	{
		return null;
	}

	public string GetHighLevelDescriptionString()
	{
		return null;
	}

	public bool IsDirector()
	{
		return false;
	}

	public bool IsExtra()
	{
		return false;
	}

	public bool IsBit()
	{
		return false;
	}

	public bool IsNormalActor()
	{
		return false;
	}

	public bool IsSmug()
	{
		return false;
	}

	public int RolePoints()
	{
		return 0;
	}

	public new int CompareTo(object obj)
	{
		return 0;
	}
}
