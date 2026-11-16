using System.Collections;

public class CustomPersonManager
{
	private static CustomPersonManager manager;

	private ArrayList customPersonArray;

	public static CustomPersonManager Manager => null;

	public ArrayList CustomPersonArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void LoadDefaults()
	{
	}

	public void LoadCustomPersons()
	{
	}

	public void SaveCustomPersons()
	{
	}

	public static bool ShouldTryToUsePartialCustomPerson()
	{
		return false;
	}

	public static bool ShouldTryToUseFullCustomPerson()
	{
		return false;
	}

	public static CustomPerson GetRandomCustomPerson()
	{
		return null;
	}

	public static int ProbableGenderForFirstName(string firstName)
	{
		return 0;
	}

	public static bool ShouldOverrideShareButtonToCustomPerson()
	{
		return false;
	}

	public void MigrateCustomPersons()
	{
	}
}
