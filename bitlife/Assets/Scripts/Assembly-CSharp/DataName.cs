using System;

[Serializable]
public class DataName
{
	private string Name__BackingField;

	private string CountryCode__BackingField;

	private int Type__BackingField;

	private int SkinColor__BackingField;

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

	public string CountryCode
	{
		get
		{
			return CountryCode__BackingField;
		}
		set
		{
			CountryCode__BackingField = value;
		}
	}

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public int SkinColor
	{
		get
		{
			return SkinColor__BackingField;
		}
		set
		{
			SkinColor__BackingField = value;
		}
	}
}
