using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAlertViewWindow : MonoBehaviour
{
	private const float ENTRY_HEIGHT = 150f;

	private UIAlertViewEntry entryTemplate;

	private Transform contentParent;

	private TextMeshProUGUI title;

	private TextMeshProUGUI message;

	private RectTransform titleBlockParent;

	private ScrollRect scrollView;

	private List<string> buttonTitles;

	public int cancelIndex;

	public void SetupWindow(string title, string message, string cancelButtonTitle = null, string[] otherButtonTitles = null, Action<int> callback = null, bool useCallbackOnCancel = true, bool translate = true)
	{
	}

	public string ButtonTitleAtIndex(int index)
	{
		return null;
	}

	public bool ButtonTitleAtIndexContains(int index, string word)
	{
		return false;
	}
}
