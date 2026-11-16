using System.Collections;

public class DataCompanyNameStructure
{
	private int CompanyType__BackingField;

	private ArrayList FormatArray__BackingField;

	private ArrayList PrefixArray__BackingField;

	private ArrayList SuffixArray__BackingField;

	private ArrayList PredefinedArray__BackingField;

	public int CompanyType
	{
		get
		{
			return CompanyType__BackingField;
		}
		set
		{
			CompanyType__BackingField = value;
		}
	}

	public ArrayList FormatArray
	{
		get
		{
			return FormatArray__BackingField;
		}
		set
		{
			FormatArray__BackingField = value;
		}
	}

	public ArrayList PrefixArray
	{
		get
		{
			return PrefixArray__BackingField;
		}
		set
		{
			PrefixArray__BackingField = value;
		}
	}

	public ArrayList SuffixArray
	{
		get
		{
			return SuffixArray__BackingField;
		}
		set
		{
			SuffixArray__BackingField = value;
		}
	}

	public ArrayList PredefinedArray
	{
		get
		{
			return PredefinedArray__BackingField;
		}
		set
		{
			PredefinedArray__BackingField = value;
		}
	}
}
