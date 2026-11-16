using System.Collections;

public class DataPropertyManagementCompanyName
{
	private ArrayList Format__BackingField;

	private ArrayList Prefix__BackingField;

	private ArrayList Suffix__BackingField;

	public ArrayList Format
	{
		get
		{
			return Format__BackingField;
		}
		set
		{
			Format__BackingField = value;
		}
	}

	public ArrayList Prefix
	{
		get
		{
			return Prefix__BackingField;
		}
		set
		{
			Prefix__BackingField = value;
		}
	}

	public ArrayList Suffix
	{
		get
		{
			return Suffix__BackingField;
		}
		set
		{
			Suffix__BackingField = value;
		}
	}
}
