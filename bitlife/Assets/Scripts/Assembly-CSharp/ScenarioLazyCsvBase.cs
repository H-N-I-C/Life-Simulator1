using System.Collections;
using System.Collections.Generic;

public class ScenarioLazyCsvBase : ScenarioBase
{
	public delegate void ChoiceCallback(ChoiceSelection selection);

	public delegate void DismissCallback();

	private readonly ActivityLazyCsvBase _activityBase;

	private readonly ChoiceCallback _choiceCallback;

	private readonly DismissCallback _onDismissSelected;

	protected string _csvName;

	protected string _scenarioID;

	public ScenarioLazyCsvBase(ActivityLazyCsvBase activity)
		: base(null)
	{
	}

	public ScenarioLazyCsvBase(ActivityLazyCsvBase activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	public ScenarioLazyCsvBase(ActivityLazyCsvBase activity, ChoiceCallback choiceCallback, DismissCallback onDismissSelected)
		: base(null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
	{
	}

	public ActivityLazyCsvBase GetBaseActivity()
	{
		return null;
	}

	protected virtual void PrepareParser(BracketParser parser)
	{
	}

	private void CreateCsvBracketDictionary()
	{
	}

	private void LoadCsv()
	{
	}

	private List<int> ToContentIndexList(ArrayList dropDownStrings)
	{
		return null;
	}

	public override void DismissSelected()
	{
	}

	private static List<T> ToList<T>(ArrayList arrayList)
	{
		return null;
	}
}
