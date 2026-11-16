public class MiniGameDataLicenseCar
{
	private bool Passed__BackingField;

	private DataLicenseCarQuestion Question__BackingField;

	public bool Passed
	{
		get
		{
			return Passed__BackingField;
		}
		set
		{
			Passed__BackingField = value;
		}
	}

	public DataLicenseCarQuestion Question
	{
		get
		{
			return Question__BackingField;
		}
		set
		{
			Question__BackingField = value;
		}
	}

	public MiniGameDataLicenseCar(int signIndex = 0)
	{
	}
}
