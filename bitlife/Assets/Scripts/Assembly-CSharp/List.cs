using System;
using System.Collections;

public class List
{
	public string Title;

	public string TranslatedTitle;

	public ArrayList ListItemArray;

	public string Breadcrumb;

	public int ListType;

	public int CustomType;

	public bool ExcludeNavigationButton;

	public bool RecordBreadCrumb;

	public ListMenuType MenuType;

	public bool OnlyCacheTranslation;

	public string TitleToShow => null;

	public List(string title, string item)
	{
	}

	public List(string title, ArrayList items)
	{
	}

	public List(string title, UILIST_TYPE type)
	{
	}

	public List Add(ListItem item)
	{
		return null;
	}

	public List Add(string title, Action<ListItem> configure = null)
	{
		return null;
	}

	public List Add(string title, string emoji, LIST_ITEM_SELECTION_ACTION action, Action<ListItem> configure = null)
	{
		return null;
	}

	public List Add(string title, string emoji, LIST_ITEM_SELECTION_ACTION action, int actionIndex, Action<ListItem> configure = null)
	{
		return null;
	}

	public List Add(string title, string emoji, LIST_ITEM_SELECTION_ACTION action, string secondLine, Action<ListItem> configure = null)
	{
		return null;
	}

	public List AddCustom(string resourceName, LIST_ITEM_SELECTION_ACTION action, int customHeight, object extraData)
	{
		return null;
	}
}
