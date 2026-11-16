using System.Collections;

public class DataDumbScenarioActor : DataDumbScenario
{
	private bool HeroHasLover__BackingField;

	private int LoverGender__BackingField;

	private int MinBudget__BackingField;

	private int MaxBudget__BackingField;

	private int ActorType__BackingField;

	private int ActorDumbType__BackingField;

	private bool WorksWithAllGenres__BackingField;

	private ArrayList SpecificGenreArray__BackingField;

	private bool MustBeMajorRole__BackingField;

	private bool MustHaveAgent__BackingField;

	public bool HeroHasLover
	{
		get
		{
			return HeroHasLover__BackingField;
		}
		set
		{
			HeroHasLover__BackingField = value;
		}
	}

	public int LoverGender
	{
		get
		{
			return LoverGender__BackingField;
		}
		set
		{
			LoverGender__BackingField = value;
		}
	}

	public int MinBudget
	{
		get
		{
			return MinBudget__BackingField;
		}
		set
		{
			MinBudget__BackingField = value;
		}
	}

	public int MaxBudget
	{
		get
		{
			return MaxBudget__BackingField;
		}
		set
		{
			MaxBudget__BackingField = value;
		}
	}

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

	public int ActorDumbType
	{
		get
		{
			return ActorDumbType__BackingField;
		}
		set
		{
			ActorDumbType__BackingField = value;
		}
	}

	public bool WorksWithAllGenres
	{
		get
		{
			return WorksWithAllGenres__BackingField;
		}
		set
		{
			WorksWithAllGenres__BackingField = value;
		}
	}

	public ArrayList SpecificGenreArray
	{
		get
		{
			return SpecificGenreArray__BackingField;
		}
		set
		{
			SpecificGenreArray__BackingField = value;
		}
	}

	public bool MustBeMajorRole
	{
		get
		{
			return MustBeMajorRole__BackingField;
		}
		set
		{
			MustBeMajorRole__BackingField = value;
		}
	}

	public bool MustHaveAgent
	{
		get
		{
			return MustHaveAgent__BackingField;
		}
		set
		{
			MustHaveAgent__BackingField = value;
		}
	}
}
