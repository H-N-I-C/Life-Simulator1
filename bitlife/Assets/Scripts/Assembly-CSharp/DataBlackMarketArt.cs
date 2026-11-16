public class DataBlackMarketArt : DataBlackMarketBase
{
	private string Title__BackingField;

	private string Artist__BackingField;

	private string Genre__BackingField;

	private bool CanBeFake__BackingField;

	private bool StorageUnitOnly__BackingField;

	private bool BlackMarketOnly__BackingField;

	private int AppealScore__BackingField;

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

	public string Artist
	{
		get
		{
			return Artist__BackingField;
		}
		set
		{
			Artist__BackingField = value;
		}
	}

	public string Genre
	{
		get
		{
			return Genre__BackingField;
		}
		set
		{
			Genre__BackingField = value;
		}
	}

	public bool CanBeFake
	{
		get
		{
			return CanBeFake__BackingField;
		}
		set
		{
			CanBeFake__BackingField = value;
		}
	}

	public bool StorageUnitOnly
	{
		get
		{
			return StorageUnitOnly__BackingField;
		}
		set
		{
			StorageUnitOnly__BackingField = value;
		}
	}

	public bool BlackMarketOnly
	{
		get
		{
			return BlackMarketOnly__BackingField;
		}
		set
		{
			BlackMarketOnly__BackingField = value;
		}
	}

	public int AppealScore
	{
		get
		{
			return AppealScore__BackingField;
		}
		set
		{
			AppealScore__BackingField = value;
		}
	}
}
