using System.Collections;
using System.Collections.Generic;

public static class ExList
{
	public static void Shuffle<T>(IList<T> list)
	{
	}

	public static List<T> Repeated<T>(T value, int count)
	{
		return null;
	}

	public static T GetOrDefault<T>(IList<T> list, int index, T @default = default(T))
	{
		return default;
	}

	public static T GetRandom<T>(IList<T> list)
	{
		return default;
	}

	public static bool TryGetRandomAndRemove<T>(IList<T> list, out T value)
	{
		value = default;
		return false;
	}

	public static T GetRandom<T>(IList list)
	{
		return default;
	}
}
