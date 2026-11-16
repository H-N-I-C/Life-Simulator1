using System.Collections;

public class DataMusicAlbumRejection
{
	private string Reason__BackingField;

	private bool Solo__BackingField;

	private bool Band__BackingField;

	private bool WorksWithAllAlbumTypes__BackingField;

	private ArrayList SpecificAlbumTypeArray__BackingField;

	public string Reason
	{
		get
		{
			return Reason__BackingField;
		}
		set
		{
			Reason__BackingField = value;
		}
	}

	public bool Solo
	{
		get
		{
			return Solo__BackingField;
		}
		set
		{
			Solo__BackingField = value;
		}
	}

	public bool Band
	{
		get
		{
			return Band__BackingField;
		}
		set
		{
			Band__BackingField = value;
		}
	}

	public bool WorksWithAllAlbumTypes
	{
		get
		{
			return WorksWithAllAlbumTypes__BackingField;
		}
		set
		{
			WorksWithAllAlbumTypes__BackingField = value;
		}
	}

	public ArrayList SpecificAlbumTypeArray
	{
		get
		{
			return SpecificAlbumTypeArray__BackingField;
		}
		set
		{
			SpecificAlbumTypeArray__BackingField = value;
		}
	}
}
