public class ScenarioChoice
{
	private string Title__BackingField;

	private bool IsDefaultStyle__BackingField;

	private bool IsGreenStyle__BackingField;

	private string ConfirmationString__BackingField;

	private bool CollapseAnyOpenListsWhenSelected__BackingField;

	private int Icon__BackingField;

	public string IapPriceString;

	private int SpecialIcon__BackingField;

	private string TranslatedTitle__BackingField;

	private string AccessibilityLabel__BackingField;

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

	public bool IsDefaultStyle
	{
		get
		{
			return IsDefaultStyle__BackingField;
		}
		set
		{
			IsDefaultStyle__BackingField = value;
		}
	}

	public bool IsGreenStyle
	{
		get
		{
			return IsGreenStyle__BackingField;
		}
		set
		{
			IsGreenStyle__BackingField = value;
		}
	}

	public string ConfirmationString
	{
		get
		{
			return ConfirmationString__BackingField;
		}
		set
		{
			ConfirmationString__BackingField = value;
		}
	}

	public bool CollapseAnyOpenListsWhenSelected
	{
		get
		{
			return CollapseAnyOpenListsWhenSelected__BackingField;
		}
		set
		{
			CollapseAnyOpenListsWhenSelected__BackingField = value;
		}
	}

	public int Icon
	{
		get
		{
			return Icon__BackingField;
		}
		set
		{
			Icon__BackingField = value;
		}
	}

	public int SpecialIcon
	{
		get
		{
			return SpecialIcon__BackingField;
		}
		set
		{
			SpecialIcon__BackingField = value;
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

	public string AccessibilityLabel
	{
		get
		{
			return AccessibilityLabel__BackingField;
		}
		set
		{
			AccessibilityLabel__BackingField = value;
		}
	}

	public ScenarioChoice()
	{
	}

	public ScenarioChoice(string theTitle, bool isDefaultStyle = false, bool collapseList = true)
	{
	}

	public ScenarioChoice(string theTitle, string theConfirmation, bool isDefaultStyle)
	{
	}

	public ScenarioChoice(string title, bool isDefaultStyle, bool collapseAnyOpenListWhenSelected, int iconStyle, string IAP)
	{
	}

	public bool RequiresConfirmation()
	{
		return false;
	}

	public string GetTitle()
	{
		return null;
	}
}
