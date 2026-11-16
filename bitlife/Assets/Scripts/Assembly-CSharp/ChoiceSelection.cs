using System.Collections.Generic;

public class ChoiceSelection
{
	private int ChoiceIndex__BackingField;

	private List<string> InputFields__BackingField;

	private List<int> DropDownIndexes__BackingField;

	private List<float> Sliders__BackingField;

	public int ChoiceIndex
	{
		get
		{
			return ChoiceIndex__BackingField;
		}
		set
		{
			ChoiceIndex__BackingField = value;
		}
	}

	public List<string> InputFields
	{
		get
		{
			return InputFields__BackingField;
		}
		set
		{
			InputFields__BackingField = value;
		}
	}

	public List<int> DropDownIndexes
	{
		get
		{
			return DropDownIndexes__BackingField;
		}
		set
		{
			DropDownIndexes__BackingField = value;
		}
	}

	public List<float> Sliders
	{
		get
		{
			return Sliders__BackingField;
		}
		set
		{
			Sliders__BackingField = value;
		}
	}
}
