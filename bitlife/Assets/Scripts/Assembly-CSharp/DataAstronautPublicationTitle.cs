using System.Collections;

public class DataAstronautPublicationTitle
{
	private string Title__BackingField;

	private ArrayList DiscoveryType__BackingField;

	private int CommonnessScore__BackingField;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public ArrayList DiscoveryType
	{
		get
		{
			return DiscoveryType__BackingField;
		}
		set
		{
			DiscoveryType__BackingField = value;
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
}
