using System.Collections;

public class DataAccessoryHeadwear : DataAccessory
{
	private bool Brim__BackingField;

	private ArrayList CountryArray__BackingField;

	public bool Brim
	{
		get
		{
			return Brim__BackingField;
		}
		set
		{
			Brim__BackingField = value;
		}
	}

	public ArrayList CountryArray
	{
		get
		{
			return CountryArray__BackingField;
		}
		set
		{
			CountryArray__BackingField = value;
		}
	}

	public bool IsRoyal()
	{
		return false;
	}

	public static bool IsRoyalForIndex(int pIndex)
	{
		return false;
	}

	public static int GetIndexForRoyalRankGenderRoyalByBirthCountryCode(int pRank, int pGender, bool proyalByBirth, string pCountryCode)
	{
		return 0;
	}
}
