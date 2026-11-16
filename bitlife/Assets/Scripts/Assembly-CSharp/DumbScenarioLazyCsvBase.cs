using System.Collections.Generic;

public abstract class DumbScenarioLazyCsvBase<TData> : Scenario where TData : IDumbScenarioData
{
	protected readonly BracketParser Parser;

	protected TData Data;

	private List<ScenarioChoice> _choices;

	private DumbScenarioLazyActivity<TData> Activity__BackingField;

	public DumbScenarioLazyActivity<TData> Activity
	{
		get
		{
			return Activity__BackingField;
		}
		set
		{
			Activity__BackingField = value;
		}
	}

	protected DumbScenarioLazyCsvBase(DumbScenarioLazyActivity<TData> activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override string EmojiString()
	{
		return null;
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	protected abstract void PrepareParser(BracketParser parser);

	protected abstract string GetExternalScenarioTitle();

	protected abstract string GetEmojiString();

	protected virtual Scenario OnFeedbackScenarioCreated(Scenario feedback)
	{
		return null;
	}

	private Scenario GetFeedbackScenario(IDumbChoice choice)
	{
		return null;
	}

	private ScenarioChoice _003CRefreshScenario_003Eb__8_0(IDumbChoice choice)
	{
		return null;
	}
}
