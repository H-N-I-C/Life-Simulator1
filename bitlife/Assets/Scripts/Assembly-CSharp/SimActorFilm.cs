using System;
using System.Collections;

[Serializable]
public class SimActorFilm
{
	private string Title__BackingField;

	private string _RawTitle;

	private ArrayList RoleArray__BackingField;

	private SimActorDirectorMeta DirectorMeta__BackingField;

	private SimCompany FilmStudioCompany__BackingField;

	private float Budget__BackingField;

	private int BudgetScore__BackingField;

	private int RatingTier__BackingField;

	private float Att_quality__BackingField;

	private float Att_budget__BackingField;

	private float Att_success__BackingField;

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

	public DataActorFilm Data
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

	public SimActorDirectorMeta DirectorMeta
	{
		get
		{
			return DirectorMeta__BackingField;
		}
		set
		{
			DirectorMeta__BackingField = value;
		}
	}

	public SimCompany FilmStudioCompany
	{
		get
		{
			return FilmStudioCompany__BackingField;
		}
		set
		{
			FilmStudioCompany__BackingField = value;
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

	public int BudgetScore
	{
		get
		{
			return BudgetScore__BackingField;
		}
		set
		{
			BudgetScore__BackingField = value;
		}
	}

	public int RatingTier
	{
		get
		{
			return RatingTier__BackingField;
		}
		set
		{
			RatingTier__BackingField = value;
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

	public void SetTheBudget()
	{
	}

	public float GetBoxOfficeReceipts()
	{
		return 0f;
	}

	public string GetTitleWithQuotes()
	{
		return null;
	}

	public string GetRatingString()
	{
		return null;
	}

	public void ImpactSuccess(int impactScore)
	{
	}
}
