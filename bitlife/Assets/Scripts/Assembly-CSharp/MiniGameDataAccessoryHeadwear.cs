using System.Collections;

public class MiniGameDataAccessoryHeadwear
{
	private ArrayList HeadwearArray__BackingField;

	private int SelectedIndex__BackingField;

	public ArrayList HeadwearArray
	{
		get
		{
			return HeadwearArray__BackingField;
		}
		set
		{
			HeadwearArray__BackingField = value;
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
