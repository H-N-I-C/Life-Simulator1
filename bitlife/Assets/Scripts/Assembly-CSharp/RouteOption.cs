using System;
using System.Collections.Generic;

[Serializable]
public class RouteOption
{
	private string RouteTo__BackingField;

	private string ScenarioFile__BackingField;

	private int Weight__BackingField;

	private List<RouteOption> RoutePool__BackingField;

	private string WithoutReplacementKey__BackingField;

	private string Impacts__BackingField;

	private List<string> Methods__BackingField;

	private List<string> PostMethods__BackingField;

	public string RouteTo
	{
		get
		{
			return RouteTo__BackingField;
		}
		set
		{
			RouteTo__BackingField = value;
		}
	}

	public string ScenarioFile
	{
		get
		{
			return ScenarioFile__BackingField;
		}
		set
		{
			ScenarioFile__BackingField = value;
		}
	}

	public int Weight
	{
		get
		{
			return Weight__BackingField;
		}
		set
		{
			Weight__BackingField = value;
		}
	}

	public List<RouteOption> RoutePool
	{
		get
		{
			return RoutePool__BackingField;
		}
		set
		{
			RoutePool__BackingField = value;
		}
	}

	public string WithoutReplacementKey
	{
		get
		{
			return WithoutReplacementKey__BackingField;
		}
		set
		{
			WithoutReplacementKey__BackingField = value;
		}
	}

	public string Impacts
	{
		get
		{
			return Impacts__BackingField;
		}
		set
		{
			Impacts__BackingField = value;
		}
	}

	public List<string> Methods
	{
		get
		{
			return Methods__BackingField;
		}
		set
		{
			Methods__BackingField = value;
		}
	}

	public List<string> PostMethods
	{
		get
		{
			return PostMethods__BackingField;
		}
		set
		{
			PostMethods__BackingField = value;
		}
	}
}
