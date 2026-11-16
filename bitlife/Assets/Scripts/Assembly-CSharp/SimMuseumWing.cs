using System;
using System.Collections.Generic;

[Serializable]
public class SimMuseumWing
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimDisplayItem, bool> _003C_003E9__23_0;

		public static Func<SimDisplayItem, double> _003C_003E9__23_1;

		public static Func<SimDisplayItem, bool> _003C_003E9__24_0;

		public static Func<SimDisplayItem, bool> _003C_003E9__24_1;

		public static Func<SimDisplayItem, int> _003C_003E9__24_2;

		public static Func<SimDisplayItem, bool> _003C_003E9__26_0;

		public static Func<SimDisplayItem, bool> _003C_003E9__27_0;

		public static Func<SimDisplayItem, bool> _003C_003E9__27_1;

		public static Func<SimDisplayItem, double> _003C_003E9__27_2;

		internal bool _003CGetMostExpensiveExhibit_003Eb__23_0(SimDisplayItem t)
		{
			return false;
		}

		internal double _003CGetMostExpensiveExhibit_003Eb__23_1(SimDisplayItem x)
		{
			return 0.0;
		}

		internal bool _003CGetAverageAppealForWing_003Eb__24_0(SimDisplayItem t)
		{
			return false;
		}

		internal bool _003CGetAverageAppealForWing_003Eb__24_1(SimDisplayItem t)
		{
			return false;
		}

		internal int _003CGetAverageAppealForWing_003Eb__24_2(SimDisplayItem x)
		{
			return 0;
		}

		internal bool _003CGetNumberOfWingExhibits_003Eb__26_0(SimDisplayItem t)
		{
			return false;
		}

		internal bool _003CGetWingEmoji_003Eb__27_0(SimDisplayItem t)
		{
			return false;
		}

		internal bool _003CGetWingEmoji_003Eb__27_1(SimDisplayItem t)
		{
			return false;
		}

		internal double _003CGetWingEmoji_003Eb__27_2(SimDisplayItem x)
		{
			return 0.0;
		}
	}

	private int WingType__BackingField;

	private string WingName__BackingField;

	private List<SimDisplayItem> _DisplayItems__BackingField;

	private bool IsOpen__BackingField;

	public int WingType
	{
		get
		{
			return WingType__BackingField;
		}
		set
		{
			WingType__BackingField = value;
		}
	}

	public string WingName
	{
		get
		{
			return WingName__BackingField;
		}
		set
		{
			WingName__BackingField = value;
		}
	}

	private List<SimDisplayItem> _DisplayItems
	{
		get
		{
			return _DisplayItems__BackingField;
		}
		set
		{
			_DisplayItems__BackingField = value;
		}
	}

	public List<SimDisplayItem> DisplayItems
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool IsOpen
	{
		get
		{
			return IsOpen__BackingField;
		}
		set
		{
			IsOpen__BackingField = value;
		}
	}

	public SimMuseumWing(int wing)
	{
	}

	public double MaxExhibits()
	{
		return 0.0;
	}

	public string GetWingName(bool capitalize = false)
	{
		return null;
	}

	public double GetWingExhibitValue(int museumAge)
	{
		return 0.0;
	}

	public string GetMostExpensiveExhibit()
	{
		return null;
	}

	public float GetAverageAppealForWing()
	{
		return 0f;
	}

	public void SetWingName(string name = "")
	{
	}

	public int GetNumberOfWingExhibits()
	{
		return 0;
	}

	public string GetWingEmoji()
	{
		return null;
	}
}
