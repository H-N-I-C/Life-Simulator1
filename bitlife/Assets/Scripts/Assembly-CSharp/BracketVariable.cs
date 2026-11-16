using System;
using System.Collections.Generic;

public abstract class BracketVariable
{
	private Action<BracketVariable> m_OnValueChanged;

	private readonly HashSet<BracketVariable> _dependencies;

	private readonly string _bracketName;

	private bool _isRefreshing;

	private Dictionary<string, string> _boundDictionary;

	protected event Action<BracketVariable> OnValueChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	public BracketVariable(string bracketName, Dictionary<string, string> dict)
	{
	}

	public void BindDictionary(Dictionary<string, string> bracketDict)
	{
	}

	public void RefreshBracketValue()
	{
	}

	public void AddDependency(BracketVariable dependency)
	{
	}

	public void RemoveDependency(BracketVariable dependency)
	{
	}

	private void OnDependencyChanged(BracketVariable dependency)
	{
	}

	protected abstract string GetBracketValue();
}
public sealed class BracketVariable<T> : BracketVariable
{
	private Func<T, string> _bracketValueFunction;

	private ValueSource<T> _valueSource;

	public T Value
	{
		get
		{
			return default;
		}
		set
		{
		}
	}

	public BracketVariable(BracketVariableParameters<T> parameters)
		: base(null, null)
	{
	}

	public BracketVariable<T> RebindDictionary(Dictionary<string, string> bracketDict)
	{
		return null;
	}

	public BracketVariable<T> RebindBracketFunction(Func<T, string> bracketFunction)
	{
		return null;
	}

	public BracketVariable<T> RebindValueSource(ValueSource<T> valueSource)
	{
		return null;
	}

	public BracketVariable<T> DependsOn(BracketVariable dependency)
	{
		return null;
	}

	protected override string GetBracketValue()
	{
		return null;
	}
}
