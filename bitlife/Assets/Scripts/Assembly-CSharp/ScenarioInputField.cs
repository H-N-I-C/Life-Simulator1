using System.Collections;

public class ScenarioInputField
{
	private string Title__BackingField;

	private string Placeholder__BackingField;

	private string StartingContents__BackingField;

	private ArrayList RandomContentsArray__BackingField;

	private string PickRandomTitle__BackingField;

	private string TranslatedTitle__BackingField;

	private string TranslatedPlaceholder__BackingField;

	private string TranslatedStartingContents__BackingField;

	private ArrayList TranslatedRandomContentsArray__BackingField;

	private string TranslatedPickRandomTitle__BackingField;

	private float Y_Offset__BackingField;

	public bool ShouldTranslateStartingContent;

	public int OverrideMaxSpaces;

	public int OverrideMaxCharacters;

	public bool OverrideCapitalizationRules;

	public bool OverrideNumberRules;

	public bool OverrideNumberAtBeginning;

	public bool AllowRawInput;

	public string Title
	{
		get
		{
			return Title__BackingField;
		}
		set
		{
			Title__BackingField = value;
		}
	}

	public string Placeholder
	{
		get
		{
			return Placeholder__BackingField;
		}
		set
		{
			Placeholder__BackingField = value;
		}
	}

	public string StartingContents
	{
		get
		{
			return StartingContents__BackingField;
		}
		set
		{
			StartingContents__BackingField = value;
		}
	}

	public ArrayList RandomContentsArray
	{
		get
		{
			return RandomContentsArray__BackingField;
		}
		set
		{
			RandomContentsArray__BackingField = value;
		}
	}

	public string PickRandomTitle
	{
		get
		{
			return PickRandomTitle__BackingField;
		}
		set
		{
			PickRandomTitle__BackingField = value;
		}
	}

	public string TranslatedTitle
	{
		get
		{
			return TranslatedTitle__BackingField;
		}
		set
		{
			TranslatedTitle__BackingField = value;
		}
	}

	public string TranslatedPlaceholder
	{
		get
		{
			return TranslatedPlaceholder__BackingField;
		}
		set
		{
			TranslatedPlaceholder__BackingField = value;
		}
	}

	public string TranslatedStartingContents
	{
		get
		{
			return TranslatedStartingContents__BackingField;
		}
		set
		{
			TranslatedStartingContents__BackingField = value;
		}
	}

	public ArrayList TranslatedRandomContentsArray
	{
		get
		{
			return TranslatedRandomContentsArray__BackingField;
		}
		set
		{
			TranslatedRandomContentsArray__BackingField = value;
		}
	}

	public string TranslatedPickRandomTitle
	{
		get
		{
			return TranslatedPickRandomTitle__BackingField;
		}
		set
		{
			TranslatedPickRandomTitle__BackingField = value;
		}
	}

	public float Y_Offset
	{
		get
		{
			return Y_Offset__BackingField;
		}
		set
		{
			Y_Offset__BackingField = value;
		}
	}

	public ScenarioInputField()
	{
	}

	public ScenarioInputField(string title, string startingContents, string[] randomContents, bool shouldTranslateStartingContent = true)
	{
	}

	public string GetTitle()
	{
		return null;
	}

	public string GetPlaceHolder()
	{
		return null;
	}

	public string GetStartingContents()
	{
		return null;
	}

	public ArrayList GetRandomContentsArray()
	{
		return null;
	}
}
