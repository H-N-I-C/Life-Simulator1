using System.Collections;

public class MiniGameDataAccessoryMask
{
	private ArrayList MaskArray__BackingField;

	private int SelectedIndex__BackingField;

	public ArrayList MaskArray
	{
		get
		{
			return MaskArray__BackingField;
		}
		set
		{
			MaskArray__BackingField = value;
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
