using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<KeyValuePair<ScenarioChoice, Action>, ScenarioChoice> _003C_003E9__91_0;

		internal ScenarioChoice _003CSetChoiceActionDictionary_003Eb__91_0(KeyValuePair<ScenarioChoice, Action> x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass68_0
	{
		public string name;

		internal bool _003CSetIsVotable_003Eb__0(string i)
		{
			return false;
		}
	}

	public Dictionary<Type, ScenarioComponentEvent> BodyUIEvents;

	private Dictionary<ScenarioChoice, int> _ChoiceToOriginalIndex;

	private Dictionary<ScenarioChoice, Action> _ChoiceToAction;

	public bool HasDismissButton;

	public bool OnlyCacheTranslation;

	public string ExteriorTitle;

	public string Title;

	public string ImageString;

	public string Body;

	public Sprite ImageSprite;

	public int ImageSpriteNumVerticalSpacing;

	public SimPerson FocusPerson;

	public SimPet FocusPet;

	public SimGhost FocusGhost;

	public SimStreetPerson FocusStreetPerson;

	public SimZooAnimal FocusZooAnimal;

	public string FocusCustomString;

	public string FocusCustomEmojiString;

	public string FocusCustomName;

	public Dictionary<string, string> TableDictionary;

	public ArrayList TableOrderArray;

	public bool TableShowsPositiveCurrencyAsGreen;

	private bool NotifyTableDictionaryEntriesCreated__BackingField;

	public ArrayList BarGraphArray;

	public ArrayList InputFieldArray;

	public ArrayList DropDownArray;

	public ArrayList SliderArray;

	public Dictionary<string, string> CurrencyTableDictionary;

	public ArrayList CurrencyTableOrderArray;

	public Dictionary<string, string> CountdownTableDictionary;

	public int MiniGame;

	public object MiniGameData;

	public ScenarioMinigameTextRegistry MiniGameTextRegistry;

	public ArrayList ChoiceArray;

	public bool RandomizeChoices;

	public List<ScenarioChoice> PostChoiceTextList;

	public float PostChoiceTextListHeight;

	private Scenario NextScenario__BackingField;

	public bool DeferScenarioAdvancementOnClose;

	public bool PushScenario;

	public bool FeedbackStyleScenario;

	public bool CollapseAnyOpenListsOnDismiss;

	public int Damage;

	public bool DeathAnimation;

	public int ColorScheme;

	public string CustomSoundFileName;

	public SOUND CustomSound;

	public bool DisallowRandomSelection;

	public bool CancelScenario;

	public bool LightningOnDismiss;

	public bool DeferAfterChain;

	public bool ClickedRandomSelection;

	public int BackgroundEffect;

	public AudioClip CustomAudioClip;

	private string VotingCategory__BackingField;

	public bool IsPrisonThemable;

	public bool IsInvalid;

	[NonSerialized]
	public string InvalidReason;

	public bool NotifyTableDictionaryEntriesCreated
	{
		get
		{
			return NotifyTableDictionaryEntriesCreated__BackingField;
		}
		protected set
		{
			NotifyTableDictionaryEntriesCreated__BackingField = value;
		}
	}

	public Scenario NextScenario
	{
		get
		{
			return NextScenario__BackingField;
		}
		set
		{
			NextScenario__BackingField = value;
		}
	}

	public bool IsVotable => false;

	public string VotingCategory
	{
		get
		{
			return VotingCategory__BackingField;
		}
		protected set
		{
			VotingCategory__BackingField = value;
		}
	}

	public virtual void OnScenarioComponentEvent(IScenarioComponentEvent sender)
	{
	}

	protected void AddComponentEvent<T>(string key, string value, ref Dictionary<Type, ScenarioComponentEvent> target) where T : Component
	{
	}

	public void SetIsVotable(string votingCategory)
	{
	}

	public void ClearIsVotable()
	{
	}

	public Scenario(bool instantiateOnLoad = false)
	{
	}

	public Scenario(string theTitle, string theBody, Scenario theNextScenario = null)
	{
	}

	public virtual void RefreshScenario()
	{
	}

	public virtual void DismissSelected()
	{
	}

	public virtual void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public virtual void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public virtual void MiniGameCompleted()
	{
	}

	public virtual void OnTableDictionaryEntriesCreated(List<CandyText> entries)
	{
	}

	public virtual void CountdownTimerUpdated(int currentTimerValue)
	{
	}

	public virtual void CountdownTimerExpired()
	{
	}

	public bool PreventRandomPushAfterScenario()
	{
		return false;
	}

	public virtual void Revealed()
	{
	}

	public virtual void Closed()
	{
	}

	public void Invalidate(string reason = null)
	{
	}

	protected void SetColorScheme(SCENARIO_COLOR_SCHEME scenarioColorScheme)
	{
	}

	protected int GetOriginalChoiceIndex(int scenarioChoice)
	{
		return 0;
	}

	protected int GetOriginalChoiceIndex(ScenarioChoice scenarioChoice)
	{
		return 0;
	}

	protected void BuildChoiceIndexDictionary(ArrayList choices)
	{
	}

	protected void SetChoiceActionDictionary(List<KeyValuePair<ScenarioChoice, Action>> actions)
	{
	}

	protected Action GetChoiceAction(int choiceSelectedAtIndex)
	{
		return null;
	}

	protected Action GetChoiceAction(ScenarioChoice scenarioChoice)
	{
		return null;
	}

	protected void InvokeAction(int choiceSelectedAtIndex)
	{
	}

	protected void InvokeAction(ScenarioChoice scenarioChoice)
	{
	}

	public void PushFront(Scenario scenario)
	{
	}

	public void PushBack(Scenario scenario)
	{
	}

	public static void LogNarrativeByParser(string narrativeTemplate, BracketParser parser)
	{
	}

	protected void LogNarrativeByBracket(string narrativeTemplate, Dictionary<string, string> bracketDictionary)
	{
	}

	public static void LogNarrativeString(string narrative)
	{
	}

	public static Scenario ChainScenariosReverseOrder(IReadOnlyList<Scenario> scenarios)
	{
		return null;
	}

	public static Scenario ChainScenarios(IReadOnlyList<Scenario> scenarios)
	{
		return null;
	}

	private static Scenario FindTail(Scenario scenario)
	{
		return null;
	}

	protected ScenarioInputField BuildInputField(string title, ArrayList randomChoices, int overrideMaxCharacters = 50, int overrideMaxSpaces = 7)
	{
		return null;
	}

	public virtual string EmojiString()
	{
		return null;
	}

	public static bool ScenarioIsAgeAppropriateForPush()
	{
		return false;
	}

	public static bool ScenarioIsEligibleForRandomPush()
	{
		return false;
	}

	public string FocusEmojiString()
	{
		return null;
	}

	public string FocusName()
	{
		return null;
	}

	public string FocusExteriorTitle()
	{
		return null;
	}

	private string GetLoverRelationshipStatusString(SimPerson person)
	{
		return null;
	}

	public virtual void OnTranslationFinished()
	{
	}
}
