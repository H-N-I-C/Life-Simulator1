using System.Collections;

public class ScenarioDropDown
{
	public bool ShouldTranslate;

	private string Title__BackingField;

	private ArrayList ContentsArray__BackingField;

	private int DefaultSelectedIndexZI__BackingField;

	private string TranslatedTitle__BackingField;

	private ArrayList _translatedContentsArray;

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

	public ArrayList ContentsArray
	{
		get
		{
			return ContentsArray__BackingField;
		}
		set
		{
			ContentsArray__BackingField = value;
		}
	}

	public int DefaultSelectedIndexZI
	{
		get
		{
			return DefaultSelectedIndexZI__BackingField;
		}
		set
		{
			DefaultSelectedIndexZI__BackingField = value;
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

	public ArrayList TranslatedContentsArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ScenarioDropDown()
	{
	}

	public ScenarioDropDown(string title, ArrayList contentsArray, bool shouldTranslate = true)
	{
	}

	public string GetTitle()
	{
		return null;
	}

	public ArrayList GetContentsArray()
	{
		return null;
	}
}
