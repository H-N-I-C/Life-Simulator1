using System;
using System.Collections;

[Serializable]
public class SimHustleStreet
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_1;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_2;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_3;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_4;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_5;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_6;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_7;

		public static Func<SimStreetPerson, bool> _003C_003E9__62_8;

		public static Func<SimStreetPerson, string> _003C_003E9__62_0;

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_1(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_2(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_3(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_4(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_5(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_6(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_7(SimStreetPerson x)
		{
			return false;
		}

		internal bool _003CRefreshAvailablePeopleOnStreet_003Eb__62_8(SimStreetPerson x)
		{
			return false;
		}

		internal string _003CRefreshAvailablePeopleOnStreet_003Eb__62_0(SimStreetPerson x)
		{
			return null;
		}
	}

	private SimStreetName StreetName__BackingField;

	private int Traffic__BackingField;

	private int Crime__BackingField;

	private int Police__BackingField;

	private int YearsOnStreet__BackingField;

	private int AgeOfStreet__BackingField;

	private float TotalEarnings__BackingField;

	private bool CrimeTrendingUp__BackingField;

	private bool PoliceTrendingUp__BackingField;

	private string ProstituteString__BackingField;

	private int AgeAtLastStreetPeopleRefresh__BackingField;

	private ArrayList _StreetPeopleArray__BackingField;

	private ArrayList _ChainPasserbyArray__BackingField;

	public SimStreetName StreetName
	{
		get
		{
			return StreetName__BackingField;
		}
		set
		{
			StreetName__BackingField = value;
		}
	}

	public int Traffic
	{
		get
		{
			return Traffic__BackingField;
		}
		set
		{
			Traffic__BackingField = value;
		}
	}

	public int Crime
	{
		get
		{
			return Crime__BackingField;
		}
		set
		{
			Crime__BackingField = value;
		}
	}

	public int Police
	{
		get
		{
			return Police__BackingField;
		}
		set
		{
			Police__BackingField = value;
		}
	}

	public int YearsOnStreet
	{
		get
		{
			return YearsOnStreet__BackingField;
		}
		set
		{
			YearsOnStreet__BackingField = value;
		}
	}

	public int AgeOfStreet
	{
		get
		{
			return AgeOfStreet__BackingField;
		}
		set
		{
			AgeOfStreet__BackingField = value;
		}
	}

	public float TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	public bool CrimeTrendingUp
	{
		get
		{
			return CrimeTrendingUp__BackingField;
		}
		set
		{
			CrimeTrendingUp__BackingField = value;
		}
	}

	public bool PoliceTrendingUp
	{
		get
		{
			return PoliceTrendingUp__BackingField;
		}
		set
		{
			PoliceTrendingUp__BackingField = value;
		}
	}

	public string ProstituteString
	{
		get
		{
			return ProstituteString__BackingField;
		}
		set
		{
			ProstituteString__BackingField = value;
		}
	}

	public int AgeAtLastStreetPeopleRefresh
	{
		get
		{
			return AgeAtLastStreetPeopleRefresh__BackingField;
		}
		set
		{
			AgeAtLastStreetPeopleRefresh__BackingField = value;
		}
	}

	private ArrayList _StreetPeopleArray
	{
		get
		{
			return _StreetPeopleArray__BackingField;
		}
		set
		{
			_StreetPeopleArray__BackingField = value;
		}
	}

	public ArrayList PeopleArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _ChainPasserbyArray
	{
		get
		{
			return _ChainPasserbyArray__BackingField;
		}
		set
		{
			_ChainPasserbyArray__BackingField = value;
		}
	}

	public ArrayList ChainPasserbyArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimHustleStreet(SimStreetName theStreetName, int theTraffic)
	{
	}

	public string GetNameAndCapitalize(bool capitalize = true)
	{
		return null;
	}

	public void IncrementAge()
	{
	}

	public void AdjustTotalEarningsOnStreet(float earnings)
	{
	}

	public void RefreshAvailablePeopleOnStreet()
	{
	}

	private SimStreetPerson CreateNewProstitute()
	{
		return null;
	}

	private static SimStreetPerson CreateNewPersonInNeed()
	{
		return null;
	}

	public SimStreetPerson CreateNewPoliceOfficer(int gender = 2, bool isRegular = true)
	{
		return null;
	}
}
