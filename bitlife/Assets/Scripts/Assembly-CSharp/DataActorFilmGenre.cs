using System.Collections;

public class DataActorFilmGenre
{
	private int Genre__BackingField;

	private string Name__BackingField;

	private bool CanUseStandalone__BackingField;

	private int BudgetScore__BackingField;

	private int CommonnessScore__BackingField;

	private ArrayList CountryArray__BackingField;

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

	public bool CanUseStandalone
	{
		get
		{
			return CanUseStandalone__BackingField;
		}
		set
		{
			CanUseStandalone__BackingField = value;
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

	public ArrayList CountryArray
	{
		get
		{
			return CountryArray__BackingField;
		}
		set
		{
			CountryArray__BackingField = value;
		}
	}
}
