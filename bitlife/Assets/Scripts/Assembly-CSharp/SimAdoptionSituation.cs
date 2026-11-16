public class SimAdoptionSituation
{
	private SimChild Child__BackingField;

	private DataAdoptionReason Reason__BackingField;

	public SimChild Child
	{
		get
		{
			return Child__BackingField;
		}
		set
		{
			Child__BackingField = value;
		}
	}

	public DataAdoptionReason Reason
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
}
