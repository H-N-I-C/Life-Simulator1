using System;
using System.Collections.Generic;
using Game;

[Serializable]
public class SimCultPlot
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimLoan, bool> _003C_003E9__83_0;

		public static Func<SimCommunard, float> _003C_003E9__96_0;

		public static Func<SimCommunard, float> _003C_003E9__97_0;

		internal bool _003CMortgageRejectionReason_003Eb__83_0(SimLoan t)
		{
			return false;
		}

		internal float _003CGetAverageCommunardLoyaltyScore_003Eb__96_0(SimCommunard t)
		{
			return 0f;
		}

		internal float _003CGetAverageLoyaltyScore_003Eb__97_0(SimCommunard t)
		{
			return 0f;
		}
	}

	private string Address__BackingField;

	private int Age__BackingField;

	private int Size__BackingField;

	private int YearsOwned__BackingField;

	private float ValueAdj__BackingField;

	private float Condition__BackingField;

	private float AppealScore__BackingField;

	private List<SimLoan> _Mortgages__BackingField;

	private string PlaceString__BackingField;

	private bool HasReceivedBankruptcyWarning__BackingField;

	private string ChosenDescriptor__BackingField;

	private CULT_PROPERTY_TYPE PropertyType__BackingField;

	private int Dwellings__BackingField;

	private int Buildings__BackingField;

	public List<SimCommunard> CommunardsList;

	private int AgeAtLastCommunardRefresh__BackingField;

	private List<string> _structures__BackingField;

	private float FollowerLoyalty__BackingField;

	public string Address
	{
		get
		{
			return Address__BackingField;
		}
		set
		{
			Address__BackingField = value;
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

	public int Size
	{
		get
		{
			return Size__BackingField;
		}
		set
		{
			Size__BackingField = value;
		}
	}

	public int YearsOwned
	{
		get
		{
			return YearsOwned__BackingField;
		}
		set
		{
			YearsOwned__BackingField = value;
		}
	}

	public float ValueAdj
	{
		get
		{
			return ValueAdj__BackingField;
		}
		set
		{
			ValueAdj__BackingField = value;
		}
	}

	public float Condition
	{
		get
		{
			return Condition__BackingField;
		}
		set
		{
			Condition__BackingField = value;
		}
	}

	public float AppealScore
	{
		get
		{
			return AppealScore__BackingField;
		}
		set
		{
			AppealScore__BackingField = value;
		}
	}

	private List<SimLoan> _Mortgages
	{
		get
		{
			return _Mortgages__BackingField;
		}
		set
		{
			_Mortgages__BackingField = value;
		}
	}

	public List<SimLoan> Mortgages
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string PlaceString
	{
		get
		{
			return PlaceString__BackingField;
		}
		set
		{
			PlaceString__BackingField = value;
		}
	}

	public bool HasReceivedBankruptcyWarning
	{
		get
		{
			return HasReceivedBankruptcyWarning__BackingField;
		}
		set
		{
			HasReceivedBankruptcyWarning__BackingField = value;
		}
	}

	public DataCultCommunePropertyType Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string ChosenDescriptor
	{
		get
		{
			return ChosenDescriptor__BackingField;
		}
		set
		{
			ChosenDescriptor__BackingField = value;
		}
	}

	public CULT_PROPERTY_TYPE PropertyType
	{
		get
		{
			return PropertyType__BackingField;
		}
		set
		{
			PropertyType__BackingField = value;
		}
	}

	public int Dwellings
	{
		get
		{
			return Dwellings__BackingField;
		}
		set
		{
			Dwellings__BackingField = value;
		}
	}

	public int Buildings
	{
		get
		{
			return Buildings__BackingField;
		}
		set
		{
			Buildings__BackingField = value;
		}
	}

	public int AgeAtLastCommunardRefresh
	{
		get
		{
			return AgeAtLastCommunardRefresh__BackingField;
		}
		set
		{
			AgeAtLastCommunardRefresh__BackingField = value;
		}
	}

	private List<string> _structures
	{
		get
		{
			return _structures__BackingField;
		}
		set
		{
			_structures__BackingField = value;
		}
	}

	public List<string> Structures
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float FollowerLoyalty
	{
		get
		{
			return FollowerLoyalty__BackingField;
		}
		set
		{
			FollowerLoyalty__BackingField = value;
		}
	}

	public SimCultPlot(CULT_PROPERTY_TYPE plotType)
	{
	}

	public void GeneratePlotStats(out int dwellings, out int buildings, out int size)
	{
		dwellings = default;
		buildings = default;
		size = default;
	}

	public static SimCultPlot GenerateRandomPlot(CULT_PROPERTY_TYPE type)
	{
		return null;
	}

	public bool IncrementAge()
	{
		return false;
	}

	public SimLoan OriginateMortgage(float loanAmount)
	{
		return null;
	}

	public LOAN_REJECTION_REASON MortgageRejectionReason(double cost = 0.0)
	{
		return LOAN_REJECTION_REASON.NONE;
	}

	private bool ProcessLoans(List<SimLoan> mortgages)
	{
		return false;
	}

	public string GetPlotString(CULT_PROPERTY_TYPE plotType)
	{
		return null;
	}

	public CULT_PROPERTY_TYPE GetPlotTypeFromString(string plotName)
	{
		return CULT_PROPERTY_TYPE.NONE;
	}

	public string DescriptionWithDescriptorAndAddressIsCapitalized(bool descriptor, bool address, bool capitalized)
	{
		return null;
	}

	public int AvailableDwellings()
	{
		return 0;
	}

	public int AvailableBuildings()
	{
		return 0;
	}

	public float PropertyEstimatedValue()
	{
		return 0f;
	}

	public double PropertyValue()
	{
		return 0.0;
	}

	public float MonthlyExpense()
	{
		return 0f;
	}

	public void AdjustCommunards(SimCommunard communard, bool add = true)
	{
	}

	public void RemoveExCommunard(SimCommunard exCommunard)
	{
	}

	public float GetAppealScore()
	{
		return 0f;
	}

	public float GetAverageCommunardLoyaltyScore()
	{
		return 0f;
	}

	public float GetAverageLoyaltyScore()
	{
		return 0f;
	}

	public void RefreshCommunards()
	{
	}

	public SimCommunard GetRandomCommunard()
	{
		return null;
	}

	public void ImpactEntireCommuneLoyalty(int loyaltyImpact)
	{
	}

	private void ImpactFollowerLoyalty(int impactScore)
	{
	}

	public void ImpactAppeal(int impactScore)
	{
	}
}
