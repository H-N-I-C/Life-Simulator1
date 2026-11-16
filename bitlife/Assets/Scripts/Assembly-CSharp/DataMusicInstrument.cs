using System.Collections;

public class DataMusicInstrument
{
	private string Name__BackingField;

	private string SingularName__BackingField;

	private string Category__BackingField;

	private ArrayList Finishes__BackingField;

	private int MinCost__BackingField;

	private int MaxCost__BackingField;

	private bool CanBuy__BackingField;

	private bool CanBusk__BackingField;

	private int BuskScore__BackingField;

	private int ParentLessonsScore__BackingField;

	private int CostScore__BackingField;

	private int DifficultyScore__BackingField;

	private int CommonnessScore__BackingField;

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

	public string SingularName
	{
		get
		{
			return SingularName__BackingField;
		}
		set
		{
			SingularName__BackingField = value;
		}
	}

	public string Category
	{
		get
		{
			return Category__BackingField;
		}
		set
		{
			Category__BackingField = value;
		}
	}

	public ArrayList Finishes
	{
		get
		{
			return Finishes__BackingField;
		}
		set
		{
			Finishes__BackingField = value;
		}
	}

	public int MinCost
	{
		get
		{
			return MinCost__BackingField;
		}
		set
		{
			MinCost__BackingField = value;
		}
	}

	public int MaxCost
	{
		get
		{
			return MaxCost__BackingField;
		}
		set
		{
			MaxCost__BackingField = value;
		}
	}

	public bool CanBuy
	{
		get
		{
			return CanBuy__BackingField;
		}
		set
		{
			CanBuy__BackingField = value;
		}
	}

	public bool CanBusk
	{
		get
		{
			return CanBusk__BackingField;
		}
		set
		{
			CanBusk__BackingField = value;
		}
	}

	public int BuskScore
	{
		get
		{
			return BuskScore__BackingField;
		}
		set
		{
			BuskScore__BackingField = value;
		}
	}

	public int ParentLessonsScore
	{
		get
		{
			return ParentLessonsScore__BackingField;
		}
		set
		{
			ParentLessonsScore__BackingField = value;
		}
	}

	public int CostScore
	{
		get
		{
			return CostScore__BackingField;
		}
		set
		{
			CostScore__BackingField = value;
		}
	}

	public int DifficultyScore
	{
		get
		{
			return DifficultyScore__BackingField;
		}
		set
		{
			DifficultyScore__BackingField = value;
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

	public bool IsVoice()
	{
		return false;
	}

	public bool IsPlural()
	{
		return false;
	}
}
