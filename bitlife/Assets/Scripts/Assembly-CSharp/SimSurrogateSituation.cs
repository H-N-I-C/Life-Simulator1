using System;

[Serializable]
public class SimSurrogateSituation
{
	[NonSerialized]
	public SimPerson Host;

	private float Cost__BackingField;

	private int EggDonor__BackingField;

	private SimLover EggDonorLover__BackingField;

	private int SpermDonor__BackingField;

	private SimLover SpermDonorLover__BackingField;

	private bool HasRejected__BackingField;

	public float Cost
	{
		get
		{
			return Cost__BackingField;
		}
		set
		{
			Cost__BackingField = value;
		}
	}

	public int EggDonor
	{
		get
		{
			return EggDonor__BackingField;
		}
		set
		{
			EggDonor__BackingField = value;
		}
	}

	public SimLover EggDonorLover
	{
		get
		{
			return EggDonorLover__BackingField;
		}
		set
		{
			EggDonorLover__BackingField = value;
		}
	}

	public int SpermDonor
	{
		get
		{
			return SpermDonor__BackingField;
		}
		set
		{
			SpermDonor__BackingField = value;
		}
	}

	public SimLover SpermDonorLover
	{
		get
		{
			return SpermDonorLover__BackingField;
		}
		set
		{
			SpermDonorLover__BackingField = value;
		}
	}

	public bool HasRejected
	{
		get
		{
			return HasRejected__BackingField;
		}
		set
		{
			HasRejected__BackingField = value;
		}
	}

	private SimSurrogateSituation()
	{
	}

	public SimSurrogateSituation(SimPerson thePerson)
	{
	}

	public static bool IsSurrogacySituationPossible()
	{
		return false;
	}
}
