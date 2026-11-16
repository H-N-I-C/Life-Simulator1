public class WrappedConfig
{
	public bool Control;

	public int Year;

	public bool UIActive;

	public int MinGraves;

	public int MinMinutes;

	public int RefreshMinutes;

	public bool HasYear()
	{
		return false;
	}

	public bool IsActive()
	{
		return false;
	}

	public bool IsUIActive()
	{
		return false;
	}
}
