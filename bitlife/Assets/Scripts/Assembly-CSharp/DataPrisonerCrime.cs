public class DataPrisonerCrime
{
	private string Crime__BackingField;

	private int Seriousness__BackingField;

	private bool IsViolent__BackingField;

	private int CommonnessScore__BackingField;

	public string Crime
	{
		get
		{
			return Crime__BackingField;
		}
		set
		{
			Crime__BackingField = value;
		}
	}

	public int Seriousness
	{
		get
		{
			return Seriousness__BackingField;
		}
		set
		{
			Seriousness__BackingField = value;
		}
	}

	public bool IsViolent
	{
		get
		{
			return IsViolent__BackingField;
		}
		set
		{
			IsViolent__BackingField = value;
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
