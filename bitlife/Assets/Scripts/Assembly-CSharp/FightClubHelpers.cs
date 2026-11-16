using System.Collections.Generic;

public static class FightClubHelpers
{
	public enum FamiliarType
	{
		CHILD = 0,
		FRIEND = 1,
		LOVER = 2,
		PARENT = 3
	}

	public class FightClubFamiliar
	{
		public FamiliarType type;

		public SimPerson person;
	}

	public static List<FightClubFamiliar> GetFamiliarList(Life life, int minChildAge)
	{
		return null;
	}

	public static bool HasValidFamiliar(Life life, int minChildAge)
	{
		return false;
	}
}
