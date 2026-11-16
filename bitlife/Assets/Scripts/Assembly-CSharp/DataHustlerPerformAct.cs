using System.Collections;

public class DataHustlerPerformAct
{
	private int Act__BackingField;

	private string ActPerson__BackingField;

	private int DifficultyScore__BackingField;

	private int MoneyScore__BackingField;

	private int DangerScore__BackingField;

	private ArrayList DangerBodyParts__BackingField;

	private ArrayList DangerVerbs__BackingField;

	public int Act
	{
		get
		{
			return Act__BackingField;
		}
		set
		{
			Act__BackingField = value;
		}
	}

	public string ActPerson
	{
		get
		{
			return ActPerson__BackingField;
		}
		set
		{
			ActPerson__BackingField = value;
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

	public int MoneyScore
	{
		get
		{
			return MoneyScore__BackingField;
		}
		set
		{
			MoneyScore__BackingField = value;
		}
	}

	public int DangerScore
	{
		get
		{
			return DangerScore__BackingField;
		}
		set
		{
			DangerScore__BackingField = value;
		}
	}

	public ArrayList DangerBodyParts
	{
		get
		{
			return DangerBodyParts__BackingField;
		}
		set
		{
			DangerBodyParts__BackingField = value;
		}
	}

	public ArrayList DangerVerbs
	{
		get
		{
			return DangerVerbs__BackingField;
		}
		set
		{
			DangerVerbs__BackingField = value;
		}
	}
}
