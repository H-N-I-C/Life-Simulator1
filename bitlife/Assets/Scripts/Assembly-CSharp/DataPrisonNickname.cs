public class DataPrisonNickname
{
	private string Nickname__BackingField;

	private int Gender__BackingField;

	private bool IsPrefix__BackingField;

	public string Nickname
	{
		get
		{
			return Nickname__BackingField;
		}
		set
		{
			Nickname__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public bool IsPrefix
	{
		get
		{
			return IsPrefix__BackingField;
		}
		set
		{
			IsPrefix__BackingField = value;
		}
	}
}
