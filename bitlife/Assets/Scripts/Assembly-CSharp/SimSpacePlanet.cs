using System;
using System.Collections.Generic;

[Serializable]
public class SimSpacePlanet : SimDiscovery
{
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public string message;

		internal bool _003CGetHint_003Eb__0(KeyValuePair<char, string> x)
		{
			return false;
		}
	}

	private double Age__BackingField;

	private int NumberOfMoons__BackingField;

	private bool HasRings__BackingField;

	private float HabitabilityScore__BackingField;

	private Dictionary<char, string> LanguageMapping__BackingField;

	private int EmojiPlanetType__BackingField;

	public double Age
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

	public int NumberOfMoons
	{
		get
		{
			return NumberOfMoons__BackingField;
		}
		set
		{
			NumberOfMoons__BackingField = value;
		}
	}

	public bool HasRings
	{
		get
		{
			return HasRings__BackingField;
		}
		set
		{
			HasRings__BackingField = value;
		}
	}

	public float HabitabilityScore
	{
		get
		{
			return HabitabilityScore__BackingField;
		}
		set
		{
			HabitabilityScore__BackingField = value;
		}
	}

	public Dictionary<char, string> LanguageMapping
	{
		get
		{
			return LanguageMapping__BackingField;
		}
		set
		{
			LanguageMapping__BackingField = value;
		}
	}

	public int EmojiPlanetType
	{
		get
		{
			return EmojiPlanetType__BackingField;
		}
		set
		{
			EmojiPlanetType__BackingField = value;
		}
	}

	public SimSpacePlanet()
	{
	}

	public SimSpacePlanet(bool generateName)
	{
	}

	private void GenerateRandomLanguageMapping()
	{
	}

	public string EncryptCharacter(char c)
	{
		return null;
	}

	public string GetHint(string message)
	{
		return null;
	}
}
