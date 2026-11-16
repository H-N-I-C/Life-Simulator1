using System;
using System.Collections.Generic;

[Serializable]
public class AdvancedLogicThen
{
	private List<RouteOption> RouteOptions__BackingField;

	public List<RouteOption> RouteOptions
	{
		get
		{
			return RouteOptions__BackingField;
		}
		set
		{
			RouteOptions__BackingField = value;
		}
	}
}
