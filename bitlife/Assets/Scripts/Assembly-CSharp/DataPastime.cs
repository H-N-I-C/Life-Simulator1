public class DataPastime
{
	private string Pastime__BackingField;

	private bool Holiday__BackingField;

	public string Pastime
	{
		get
		{
			return Pastime__BackingField;
		}
		set
		{
			Pastime__BackingField = value;
		}
	}

	public bool Holiday
	{
		get
		{
			return Holiday__BackingField;
		}
		set
		{
			Holiday__BackingField = value;
		}
	}
}
