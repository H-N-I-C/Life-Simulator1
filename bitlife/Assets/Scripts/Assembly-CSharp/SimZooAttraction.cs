using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimZooAttraction
{
	private string Type__BackingField;

	private int Age__BackingField;

	private double CurrentTicketPrice__BackingField;

	private double InstallationCost__BackingField;

	private double AnnualUpkeepCost__BackingField;

	private int TicketsSoldLastYear__BackingField;

	private int TicketsSales__BackingField;

	private float CumulativeRevenue__BackingField;

	private float MinTicketCost__BackingField;

	private float MaxTicketCost__BackingField;

	private float TicketSales__BackingField;

	private float _engagementScore;

	public string Type
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

	public int Age
	{
		get
		{
			return Age__BackingField;
		}
		set
		{
			Age__BackingField = value;
		}
	}

	public double CurrentTicketPrice
	{
		get
		{
			return CurrentTicketPrice__BackingField;
		}
		set
		{
			CurrentTicketPrice__BackingField = value;
		}
	}

	public double InstallationCost
	{
		get
		{
			return InstallationCost__BackingField;
		}
		set
		{
			InstallationCost__BackingField = value;
		}
	}

	public double AnnualUpkeepCost
	{
		get
		{
			return AnnualUpkeepCost__BackingField;
		}
		set
		{
			AnnualUpkeepCost__BackingField = value;
		}
	}

	public int TicketsSoldLastYear
	{
		get
		{
			return TicketsSoldLastYear__BackingField;
		}
		set
		{
			TicketsSoldLastYear__BackingField = value;
		}
	}

	public int TicketsSales
	{
		get
		{
			return TicketsSales__BackingField;
		}
		set
		{
			TicketsSales__BackingField = value;
		}
	}

	public float CumulativeRevenue
	{
		get
		{
			return CumulativeRevenue__BackingField;
		}
		set
		{
			CumulativeRevenue__BackingField = value;
		}
	}

	public float MinTicketCost
	{
		get
		{
			return MinTicketCost__BackingField;
		}
		set
		{
			MinTicketCost__BackingField = value;
		}
	}

	public float MaxTicketCost
	{
		get
		{
			return MaxTicketCost__BackingField;
		}
		set
		{
			MaxTicketCost__BackingField = value;
		}
	}

	public float TicketSales
	{
		get
		{
			return TicketSales__BackingField;
		}
		set
		{
			TicketSales__BackingField = value;
		}
	}

	public DataZooAttraction AttractionData => null;

	public SimZooAttraction(bool startingZooAttraction, List<SimZooAttraction> exclusionList = null)
	{
	}

	public void IncrementAge()
	{
	}

	public float GetTrafficValue()
	{
		return 0f;
	}

	public float GetEngagement()
	{
		return 0f;
	}

	public string GetCategoryString()
	{
		return null;
	}

	public void ImpactEngagement(int impact)
	{
	}

	public void ImpactEngagementByAmount(int amount)
	{
	}

	public double GetAnnualRevenue()
	{
		return 0.0;
	}

	private bool _003C_002Ector_003Eb__47_0(SimZooAttraction a)
	{
		return false;
	}
}
