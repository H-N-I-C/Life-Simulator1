using System.Collections;
using System.Collections.Generic;

public class ScenarioVampireHypnotize : Scenario
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public ScenarioVampireHypnotize _003C_003E4__this;

		public string taskString;

		internal Scenario _003CChoiceSelectedAtIndex_003Eb__0()
		{
			return null;
		}
	}

	private readonly BracketParser _parser;

	private readonly Dictionary<string, string> _bodyDictionary;

	private readonly Dictionary<string, string> _narrativeDictionary;

	private readonly SimPerson _person;

	private readonly SimHero _hero;

	public ScenarioVampireHypnotize(SimPerson person, string descriptionString = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	private Scenario ChoiceFeedback(string choice)
	{
		return null;
	}

	private void LeaveLover()
	{
	}

	private string LoversFirstName()
	{
		return null;
	}

	private string TransferAsset()
	{
		return null;
	}

	private static bool TryHypnotize(SimVampire heroVampire, SimPerson victim)
	{
		return false;
	}

	private static bool IsSuggestible(SimPerson victim, int maxPercent)
	{
		return false;
	}

	private static bool IsRelationshipWeak(SimPerson victim, int maxPercent)
	{
		return false;
	}

	private void SetCurrencyAmount(double amount)
	{
	}

	private string ParseBody(string bodyTemplate)
	{
		return null;
	}

	private string ParseNarrative(string narrativeTemplate)
	{
		return null;
	}

	private void LogNarrative(string narrativeTemplate)
	{
	}
}
