using System;

[Serializable]
public class SimPrenuptialAgreement
{
	private bool RequestedByHero__BackingField;

	private double HeroNetWorthAtTimeOfMarriage__BackingField;

	private double LoverNetWorthAtTimeOfMarriage__BackingField;

	private double HeroHardAssetsValueAtTimeOfMarriage__BackingField;

	private double LoverHardAssetsValueAtTimeOfMarriage__BackingField;

	public bool RequestedByHero
	{
		get
		{
			return RequestedByHero__BackingField;
		}
		set
		{
			RequestedByHero__BackingField = value;
		}
	}

	public double HeroNetWorthAtTimeOfMarriage
	{
		get
		{
			return HeroNetWorthAtTimeOfMarriage__BackingField;
		}
		set
		{
			HeroNetWorthAtTimeOfMarriage__BackingField = value;
		}
	}

	public double LoverNetWorthAtTimeOfMarriage
	{
		get
		{
			return LoverNetWorthAtTimeOfMarriage__BackingField;
		}
		set
		{
			LoverNetWorthAtTimeOfMarriage__BackingField = value;
		}
	}

	public double HeroHardAssetsValueAtTimeOfMarriage
	{
		get
		{
			return HeroHardAssetsValueAtTimeOfMarriage__BackingField;
		}
		set
		{
			HeroHardAssetsValueAtTimeOfMarriage__BackingField = value;
		}
	}

	public double LoverHardAssetsValueAtTimeOfMarriage
	{
		get
		{
			return LoverHardAssetsValueAtTimeOfMarriage__BackingField;
		}
		set
		{
			LoverHardAssetsValueAtTimeOfMarriage__BackingField = value;
		}
	}
}
