using System;
using System.Collections.Generic;
using Genesis.Controllers;

public class ActivityLazyCsvBase
{
	private readonly List<BracketVariable> _bracketVariables;

	private readonly Dictionary<string, string> _bracketDictionary;

	private Scenario _entryScenario;

	private ScenarioController _Controller;

	protected readonly Life _life;

	protected Scenario _currentScenario;

	public Scenario EntryScenario
	{
		get
		{
			return null;
		}
		protected set
		{
		}
	}

	public Dictionary<string, string> BodyDictionary => null;

	public ScenarioController ScenarioController
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ActivityLazyCsvBase(Life life)
	{
	}

	protected void PushBack(Scenario scenario)
	{
	}

	protected void PushFront(Scenario scenario)
	{
	}

	public void AddBracketVariable(BracketVariable bracketVariable)
	{
	}

	public BracketVariable<T> CreateBracketVariable<T>(string bracketName, ValueSource<T> valueSource, Func<T, string> bracketValueFunction)
	{
		return null;
	}

	public void RefreshBrackets()
	{
	}
}
