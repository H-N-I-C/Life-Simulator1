using System;
using System.Collections;
using System.Collections.Generic;
using Game;

public class VampireHuntActivity
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Comparison<DataVampireSoloHuntLocation> _003C_003E9__17_0;

		public static Comparison<DataVampireAssaultMethod> _003C_003E9__22_0;

		public static Comparison<DataVampireEnticeMethod> _003C_003E9__25_0;

		public static Comparison<HuntAppearanceByLikelihood> _003C_003E9__38_0;

		internal int _003CFormatLocationSentences_003Eb__17_0(DataVampireSoloHuntLocation left, DataVampireSoloHuntLocation right)
		{
			return 0;
		}

		internal int _003CFormatAssaultMethods_003Eb__22_0(DataVampireAssaultMethod left, DataVampireAssaultMethod right)
		{
			return 0;
		}

		internal int _003CFormatEnticeMethods_003Eb__25_0(DataVampireEnticeMethod left, DataVampireEnticeMethod right)
		{
			return 0;
		}

		internal int _003CGetHuntAppearanceByLikelihood_003Eb__38_0(HuntAppearanceByLikelihood left, HuntAppearanceByLikelihood right)
		{
			return 0;
		}
	}

	private const string InLocationTag = "<in-location>";

	private const string AnimalTag = "<animal>";

	private const string AnimalTitleTag = "<animal-title>";

	private const string AnimalIndefiniteTag = "<animal-indefinite>";

	private const string AnimalAttackedTag = "<attack-string>";

	private const string PersonTag = "<person>";

	private const string VictimTag = "<victim>";

	private const string VictimPossessiveTag = "<victim-possessive>";

	private const string VampireTag = "<vampire>";

	private readonly Dictionary<string, DataVampireSoloHuntLocation> _locationsBySentence;

	private readonly Dictionary<string, DataVampireAssaultMethod> _assaultMethodsBySentence;

	private readonly Dictionary<string, DataVampireEnticeMethod> _enticeMethodsBySentence;

	private readonly Dictionary<string, string> _bracketDictionary;

	private readonly BracketParser _parser;

	private SimPerson _victim;

	private SimPerson _vampire;

	public ArrayList FormatLocationSentences()
	{
		return null;
	}

	public Scenario SelectScenarioByLocationSentence(string locationSentence)
	{
		return null;
	}

	public DataAnimal GetRandomAnimal()
	{
		return null;
	}

	public SimPerson GetVictim()
	{
		return null;
	}

	public void SetVictim(SimPerson victim)
	{
	}

	public ArrayList FormatAssaultMethods()
	{
		return null;
	}

	public DataVampireAssaultMethod SelectAssaultMethodBySentence(string selectedSentence)
	{
		return null;
	}

	public bool IsEffective(int effectivenessScore)
	{
		return false;
	}

	public ArrayList FormatEnticeMethods()
	{
		return null;
	}

	public DataVampireEnticeMethod SelectEnticeMethodBySentence(string selectedSentence)
	{
		return null;
	}

	public SimPerson GenerateVampire()
	{
		return null;
	}

	public SimPerson GetVampire()
	{
		return null;
	}

	public string GetEncounterColorTemplate()
	{
		return null;
	}

	public string ParseBody(string bodyTemplate)
	{
		return null;
	}

	public void LogNarrative(string narrativeTemplate)
	{
	}

	public static bool IsVampireHunterAppearing()
	{
		return false;
	}

	public static bool IsVampireHunterAppearing(int maxPercent)
	{
		return false;
	}

	private DataAnimal GetRandomAnimalToEncounter()
	{
		return null;
	}

	private void FormatAnimal(DataAnimal animal)
	{
	}

	private DataVampireSoloHuntLocation SelectLocationBySentence(string selectedSentence)
	{
		return null;
	}

	private Scenario SelectScenarioByLocation(DataVampireSoloHuntLocation location)
	{
		return null;
	}

	private HuntAppearance GetHuntAppearanceByLikelihood(HuntAppearanceByLikelihood[] appearances)
	{
		return HuntAppearance.None;
	}

	private Scenario GetScenarioByHuntAppearance(HuntAppearance huntAppearance)
	{
		return null;
	}

	private void PrepareRandomBracketInFailureBySuccess(string successFeedbackScenario)
	{
	}
}
