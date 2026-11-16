using System.Collections;

public class MiniGameDataAccessorySunglasses
{
	private ArrayList SunglassesArray__BackingField;

	private int SelectedIndex__BackingField;

	public ArrayList SunglassesArray
	{
		get
		{
			return SunglassesArray__BackingField;
		}
		set
		{
			SunglassesArray__BackingField = value;
		}
	}

	public int SelectedIndex
	{
		get
		{
			return SelectedIndex__BackingField;
		}
		set
		{
			SelectedIndex__BackingField = value;
		}
	}
}
