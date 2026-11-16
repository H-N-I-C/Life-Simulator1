using System;
using System.Collections.Generic;

public struct BracketVariableParameters<T>
{
	private string BracketName__BackingField;

	private ValueSource<T> ValueSource__BackingField;

	private Dictionary<string, string> BracketDictionary__BackingField;

	private Func<T, string> BracketUpdateFunction__BackingField;

	public string BracketName
	{
		get
		{
			return BracketName__BackingField;
		}
		set
		{
			BracketName__BackingField = value;
		}
	}

	public ValueSource<T> ValueSource
	{
		get
		{
			return ValueSource__BackingField;
		}
		set
		{
			ValueSource__BackingField = value;
		}
	}

	public Dictionary<string, string> BracketDictionary
	{
		get
		{
			return BracketDictionary__BackingField;
		}
		set
		{
			BracketDictionary__BackingField = value;
		}
	}

	public Func<T, string> BracketUpdateFunction
	{
		get
		{
			return BracketUpdateFunction__BackingField;
		}
		set
		{
			BracketUpdateFunction__BackingField = value;
		}
	}
}
