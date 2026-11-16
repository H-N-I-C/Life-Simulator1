using System;
using System.Collections.Generic;

[Serializable]
public class SimMuseumFloor
{
	private List<SimMuseumWing> _Wings__BackingField;

	private List<SimMuseumWing> _Wings
	{
		get
		{
			return _Wings__BackingField;
		}
		set
		{
			_Wings__BackingField = value;
		}
	}

	public List<SimMuseumWing> Wings
	{
		get
		{
			return null;
		}
		set
		{
		}
	}
}
