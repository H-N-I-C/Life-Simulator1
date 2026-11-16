using System;

[Serializable]
public class SimSpecialJob
{
	private int Type__BackingField;

	private int WeeklyHours__BackingField;

	private int PowerScore__BackingField;

	private float Salary__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int WeeklyHours
	{
		get
		{
			return WeeklyHours__BackingField;
		}
		set
		{
			WeeklyHours__BackingField = value;
		}
	}

	public int PowerScore
	{
		get
		{
			return PowerScore__BackingField;
		}
		set
		{
			PowerScore__BackingField = value;
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

	public virtual void IncrementAge()
	{
	}

	public virtual string JobTitleWithArticle(bool article)
	{
		return null;
	}

	public virtual void OnJobEnded()
	{
	}

	public virtual bool ShouldBecomeFamous()
	{
		return false;
	}

	public virtual int GetMaxFame()
	{
		return 0;
	}

	public virtual int GetFameType()
	{
		return 0;
	}

	public virtual bool CanAttendRehab()
	{
		return false;
	}
}
