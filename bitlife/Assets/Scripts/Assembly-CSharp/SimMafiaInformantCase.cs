using System;
using System.Collections;

[Serializable]
public class SimMafiaInformantCase
{
	private int YearsCompleted__BackingField;

	private int YearsLeft__BackingField;

	private ArrayList _PendingCrimeArray__BackingField;

	private float Att_evidence__BackingField;

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public int YearsLeft
	{
		get
		{
			return YearsLeft__BackingField;
		}
		set
		{
			YearsLeft__BackingField = value;
		}
	}

	private ArrayList _PendingCrimeArray
	{
		get
		{
			return _PendingCrimeArray__BackingField;
		}
		set
		{
			_PendingCrimeArray__BackingField = value;
		}
	}

	public ArrayList PendingCrimeArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float Att_evidence
	{
		get
		{
			return Att_evidence__BackingField;
		}
		set
		{
			Att_evidence__BackingField = value;
		}
	}

	public SimMafiaInformantCase(int theMaxYears, ArrayList theCrimeArray)
	{
	}

	public void IncrementYears()
	{
	}

	public void ImpactEvidence(int impactScore)
	{
	}
}
