using System.Collections;
using System.Collections.Generic;

public abstract class AbstractScenario : Scenario
{
	private static readonly Dictionary<string, string> DEFAULT_TABLE_DICTIONARY;

	private static readonly List<ScenarioChoiceHandler> DEFAULT_CHOICES_LIST;

	private static readonly List<string> DEFAULT_TABLE_ORDER_LIST;

	private static readonly List<ScenarioInputField> DEFAULT_INPUT_LIST;

	private static readonly List<ScenarioDropDown> DEFAULT_DROPDOWN_LIST;

	private static readonly List<ScenarioSlider> DEFAULT_SLIDER_LIST;

	private static readonly List<ScenarioBarGraph> DEFAULT_BAR_GRAPH_LIST;

	private static readonly ArrayList EMPTY_LIST;

	private bool _initialized;

	private readonly List<ScenarioChoiceHandler> _choiceHandlers;

	public sealed override void RefreshScenario()
	{
	}

	public sealed override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public sealed override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	protected abstract void OneTimeSetup();

	protected abstract string CreateExteriorTitle();

	protected abstract string CreateTitle();

	protected abstract string CreateBody();

	protected virtual List<ScenarioChoiceHandler> CreateChoiceHandlerArray()
	{
		return null;
	}

	protected virtual List<ScenarioDropDown> CreateDropDownArray()
	{
		return null;
	}

	protected virtual Dictionary<string, string> CreateTableDictionary()
	{
		return null;
	}

	protected virtual List<string> CreateTableOrder()
	{
		return null;
	}

	protected virtual List<ScenarioBarGraph> CreateBarGraphArray()
	{
		return null;
	}

	protected virtual List<ScenarioSlider> CreateSliderArray()
	{
		return null;
	}

	protected virtual List<ScenarioInputField> CreateInputFieldArray()
	{
		return null;
	}

	protected virtual string GetImageString()
	{
		return null;
	}

	protected void LogNarrative(string narrative)
	{
	}

	private Dictionary<K, V> ConvertToDictionary<K, V>(ArrayList widgetSource, ArrayList widgetValue)
	{
		return null;
	}

	private ArrayList CreateChoices(List<ScenarioChoiceHandler> handlers)
	{
		return null;
	}
}
