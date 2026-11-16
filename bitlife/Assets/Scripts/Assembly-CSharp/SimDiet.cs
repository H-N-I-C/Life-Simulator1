using System;

[Serializable]
public class SimDiet
{
	public string DietName;

	private int YearsOnDiet__BackingField;

	public DataDiet Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int YearsOnDiet
	{
		get
		{
			return YearsOnDiet__BackingField;
		}
		set
		{
			YearsOnDiet__BackingField = value;
		}
	}
}
