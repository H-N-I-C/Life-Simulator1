using System;

public class DataStreetName
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__28_0;

		internal string _003CParseTypeString_003Eb__28_0(string p)
		{
			return null;
		}
	}

	private string Name__BackingField;

	private bool AddStreetType__BackingField;

	private int AffluenceScore__BackingField;

	private int CommonnessScore__BackingField;

	private bool CanBeResidential__BackingField;

	private bool CanBeRural__BackingField;

	private bool CanBeCity__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public bool AddStreetType
	{
		get
		{
			return AddStreetType__BackingField;
		}
		set
		{
			AddStreetType__BackingField = value;
		}
	}

	public int AffluenceScore
	{
		get
		{
			return AffluenceScore__BackingField;
		}
		set
		{
			AffluenceScore__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool CanBeResidential
	{
		get
		{
			return CanBeResidential__BackingField;
		}
		set
		{
			CanBeResidential__BackingField = value;
		}
	}

	public bool CanBeRural
	{
		get
		{
			return CanBeRural__BackingField;
		}
		set
		{
			CanBeRural__BackingField = value;
		}
	}

	public bool CanBeCity
	{
		get
		{
			return CanBeCity__BackingField;
		}
		set
		{
			CanBeCity__BackingField = value;
		}
	}

	public void ParseTypeString(string typeString)
	{
	}

	public bool CanBeType(int theType)
	{
		return false;
	}
}
