using System;
using UnityEngine;

public class ListItem : IComparable
{
	public string Title;

	public string TranslatedTitle;

	public string Subtitle;

	public string TranslatedSubtitle;

	public string TranslatedSecondLine;

	public string TranslatedBarGraphTitle;

	public string SecondLine;

	public string EmojiString;

	public bool PrisonBarsOnEmoji;

	public Sprite RightHandImage;

	public bool ButtonStyling;

	public List SelectionList;

	public DeferredSelectionList SelectionListProvider;

	public List SelectionTakeoverList;

	public Scenario SelectionScenario;

	public int SelectionAction;

	public string BarGraphTitle;

	public float BarGraphValue;

	public int BarGraphColorScheme;

	public int BarGraphBubbles;

	public bool BarGraphTwoLine;

	public BarGraphSourceType BarGraphSource;

	public bool SeparatorItem;

	public string ConfirmationString;

	public int PaywallIAPOrigin;

	public int NestedIAPOrigin;

	public int GodModePaywallIAPOrigin;

	public int SpecialCareerIAPSpecialJobType;

	public bool GodModeIcon;

	public int SortableVar;

	public bool Disabled;

	public bool ApplySpecialCareersTheme;

	public bool LaunchesRankLifeCrossPromo;

	public int SpecialCareerRVSpecialJobType;

	public int BackgroundColor;

	public object ExtraData;

	public bool HideEllipsis;

	public SOUND CustomSound;

	public Action OnClickAction;

	public bool IsFeatured;

	public int TimeInSeconds;

	public bool UserGenContent;

	private string ResourceName__BackingField;

	private bool IsResource__BackingField;

	private int CustomHeight__BackingField;

	private float HeightScale__BackingField;

	private CustomTheme CustomTheme__BackingField;

	private int PinnedDepth__BackingField;

	private bool IsNativeAd__BackingField;

	public Func<int> DynamicHeight;

	public string ResourceName
	{
		get
		{
			return ResourceName__BackingField;
		}
		set
		{
			ResourceName__BackingField = value;
		}
	}

	public bool IsResource
	{
		get
		{
			return IsResource__BackingField;
		}
		set
		{
			IsResource__BackingField = value;
		}
	}

	public int CustomHeight
	{
		get
		{
			return CustomHeight__BackingField;
		}
		set
		{
			CustomHeight__BackingField = value;
		}
	}

	public float HeightScale
	{
		get
		{
			return HeightScale__BackingField;
		}
		set
		{
			HeightScale__BackingField = value;
		}
	}

	public CustomTheme CustomTheme
	{
		get
		{
			return CustomTheme__BackingField;
		}
		set
		{
			CustomTheme__BackingField = value;
		}
	}

	public int PinnedDepth
	{
		get
		{
			return PinnedDepth__BackingField;
		}
		set
		{
			PinnedDepth__BackingField = value;
		}
	}

	public bool IsNativeAd
	{
		get
		{
			return IsNativeAd__BackingField;
		}
		set
		{
			IsNativeAd__BackingField = value;
		}
	}

	public bool HasSelectionListTarget => false;

	public ListItem()
	{
	}

	public ListItem(bool isBlank)
	{
	}

	public ListItem(string theTitle, List theList)
	{
	}

	public ListItem(string theTitle, List theList, bool useButtonStyling)
	{
	}

	public ListItem(string theTitle, string theEmoji, Scenario theScenario, string theSubtitle, string theSecondLine, bool useButtonStyling)
	{
	}

	public ListItem(string theTitle, string theEmoji, int theAction, string theSubtitle, Sprite rightImage, bool useButtonStyling, string theSecondLine = null)
	{
	}

	public ListItem(string resourceName, int theAction, int customHeight, int depth)
	{
	}

	public ListItem(string theTitle, string theEmoji, int theAction, string theSubtitle, Sprite rightImage, bool useButtonStyling, bool isInPrison)
	{
	}

	public ListItem(string theTitle)
	{
	}

	public ListItem SetBarGraph(string title, float value, int numBubbles, bool twoLine = true)
	{
		return null;
	}

	public ListItem SetBarGraphWithColor(string title, float value, int numBubbles, bool twoLine = true, BAR_GRAPH_COLOR_SCHEME color = BAR_GRAPH_COLOR_SCHEME.ALL_GREEN)
	{
		return null;
	}

	public bool RequiresConfirmation()
	{
		return false;
	}

	public bool TryResolveSelectionList(out List list)
	{
		list = null;
		return false;
	}

	public bool WillBeDisabled()
	{
		return false;
	}

	public void SetRelationshipBarGraph<T>(T target, bool isLover) where T : SimPerson
	{
	}

	public void SetRelationshipBarGraph<T>(T target) where T : SimPerson
	{
	}

	public void SetRelationshipBarGraph(float barGraphValue, BarGraphSourceType sourceType)
	{
	}

	public int CompareTo(object obj)
	{
		return 0;
	}

	public string GetTitle()
	{
		return null;
	}

	public string GetSubTitle()
	{
		return null;
	}

	public string GetSecondLine()
	{
		return null;
	}

	public string GetBarGraphTitle()
	{
		return null;
	}
}
