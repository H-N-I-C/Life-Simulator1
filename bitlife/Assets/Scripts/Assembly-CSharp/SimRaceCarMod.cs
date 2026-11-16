using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimRaceCarMod
{
	private string Name__BackingField;

	private bool Plural__BackingField;

	private string ShortName__BackingField;

	private string DescriptiveString__BackingField;

	private int HandlingValueInfluence__BackingField;

	private int SpeedValueInfluence__BackingField;

	private int AccelerationValueInfluence__BackingField;

	private int DurabilityInfluence__BackingField;

	private int YearsInstalled__BackingField;

	private double BaseCost__BackingField;

	private string MostInfluencedStat__BackingField;

	private float _condition;

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

	public bool Plural
	{
		get
		{
			return Plural__BackingField;
		}
		set
		{
			Plural__BackingField = value;
		}
	}

	public string ShortName
	{
		get
		{
			return ShortName__BackingField;
		}
		set
		{
			ShortName__BackingField = value;
		}
	}

	public string DescriptiveString
	{
		get
		{
			return DescriptiveString__BackingField;
		}
		set
		{
			DescriptiveString__BackingField = value;
		}
	}

	public int HandlingValueInfluence
	{
		get
		{
			return HandlingValueInfluence__BackingField;
		}
		set
		{
			HandlingValueInfluence__BackingField = value;
		}
	}

	public int SpeedValueInfluence
	{
		get
		{
			return SpeedValueInfluence__BackingField;
		}
		set
		{
			SpeedValueInfluence__BackingField = value;
		}
	}

	public int AccelerationValueInfluence
	{
		get
		{
			return AccelerationValueInfluence__BackingField;
		}
		set
		{
			AccelerationValueInfluence__BackingField = value;
		}
	}

	public int DurabilityInfluence
	{
		get
		{
			return DurabilityInfluence__BackingField;
		}
		set
		{
			DurabilityInfluence__BackingField = value;
		}
	}

	public int YearsInstalled
	{
		get
		{
			return YearsInstalled__BackingField;
		}
		set
		{
			YearsInstalled__BackingField = value;
		}
	}

	public double BaseCost
	{
		get
		{
			return BaseCost__BackingField;
		}
		set
		{
			BaseCost__BackingField = value;
		}
	}

	public string MostInfluencedStat
	{
		get
		{
			return MostInfluencedStat__BackingField;
		}
		set
		{
			MostInfluencedStat__BackingField = value;
		}
	}

	public SimRaceCarMod(DataRacingRaceCarMod data)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetCondition()
	{
		return 0f;
	}

	public void SetCondition(float value)
	{
	}

	public void ImpactCondition(int impact)
	{
	}

	public string DescriptionWithArticle()
	{
		return null;
	}

	public List<string> NegativelyImpactedStats()
	{
		return null;
	}

	public List<string> PositivelyImpactedStats()
	{
		return null;
	}
}
