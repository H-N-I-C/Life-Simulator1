using System;

[Serializable]
public class SimOccupation
{
	private int OccupationType__BackingField;

	private float Salary__BackingField;

	private SimWorkplace _WorkPlace__BackingField;

	public int OccupationType
	{
		get
		{
			return OccupationType__BackingField;
		}
		set
		{
			OccupationType__BackingField = value;
		}
	}

	public float Salary
	{
		get
		{
			return Salary__BackingField;
		}
		set
		{
			Salary__BackingField = value;
		}
	}

	private SimWorkplace _WorkPlace
	{
		get
		{
			return _WorkPlace__BackingField;
		}
		set
		{
			_WorkPlace__BackingField = value;
		}
	}

	public SimWorkplace Workplace
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimOccupation()
	{
	}

	public SimOccupation(int type, float salary)
	{
	}

	public bool HasWorkPlace()
	{
		return false;
	}
}
