using System;

[Serializable]
public class SimPrisonGang
{
	public int _GangIndexNI;

	private int YearsInGang__BackingField;

	public DataPrisonGang GangData
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int YearsInGang
	{
		get
		{
			return YearsInGang__BackingField;
		}
		set
		{
			YearsInGang__BackingField = value;
		}
	}
}
