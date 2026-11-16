using System.Collections.Generic;

public class DataMartialArt
{
	private string Name__BackingField;

	private List<string> BeltHierarchy__BackingField;

	private DataMartialArtMove Move1__BackingField;

	private DataMartialArtMove Move2__BackingField;

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

	public List<string> BeltHierarchy
	{
		get
		{
			return BeltHierarchy__BackingField;
		}
		set
		{
			BeltHierarchy__BackingField = value;
		}
	}

	public DataMartialArtMove Move1
	{
		get
		{
			return Move1__BackingField;
		}
		set
		{
			Move1__BackingField = value;
		}
	}

	public DataMartialArtMove Move2
	{
		get
		{
			return Move2__BackingField;
		}
		set
		{
			Move2__BackingField = value;
		}
	}
}
