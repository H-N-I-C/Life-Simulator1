using System;
using System.Collections;

[Serializable]
public class SimCrime
{
	private int CrimeType__BackingField;

	private int AgeAtTimeOfCrime__BackingField;

	public string PlaceOfCrimeString;

	public float DefenseCost;

	[NonSerialized]
	private SimPlace _PlaceOfCrime;

	public int CrimeType
	{
		get
		{
			return CrimeType__BackingField;
		}
		set
		{
			CrimeType__BackingField = value;
		}
	}

	public int AgeAtTimeOfCrime
	{
		get
		{
			return AgeAtTimeOfCrime__BackingField;
		}
		set
		{
			AgeAtTimeOfCrime__BackingField = value;
		}
	}

	public SimPlace PlaceOfCrime
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public SimCrime(int type, int age, SimPlace place)
	{
	}

	public SimCrime(CRIME_TYPE type, int age, SimPlace place)
	{
	}

	public int SentenceYears()
	{
		return 0;
	}

	public bool CarriesDeathPenalty()
	{
		return false;
	}

	public void ImpactHeroRelationshipsAfterConviction()
	{
	}

	public static int GetRandomFalseAccusationCrime()
	{
		return 0;
	}

	public static bool DoesCrimeArrayIncludeDeathPenalty(ArrayList crimeArray)
	{
		return false;
	}

	public static int GetTotalSentenceYearsForCrimeArray(ArrayList crimeArray)
	{
		return 0;
	}

	public static string GetStringForCrimeArray(ArrayList crimeArray)
	{
		return null;
	}

	public static int GetTotalDefenseCostForCrimeArray(ArrayList crimeArray)
	{
		return 0;
	}

	public static string GetStringForCrimeType(int crimeType)
	{
		return null;
	}

	public static int GetCrimeForCrimeString(string crimeString)
	{
		return 0;
	}
}
