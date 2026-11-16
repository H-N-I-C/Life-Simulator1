using System;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleSetManager_Career : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass7_0
	{
		public IAP_TYPE iapType;

		internal bool _003CRemoveToggle_003Eb__0(UIAreaToggle_Career toggle)
		{
			return false;
		}
	}

	public List<UIAreaToggle_Career> toggles;

	private int selectedToggleIndex;

	public Action callback;

	public int SelectedToggleIndex => 0;

	public void Start()
	{
	}

	public void SetButtonSound(SOUND sound)
	{
	}

	public void RemoveToggle(IAP_TYPE iapType)
	{
	}

	public void SelectToggle(int index)
	{
	}
}
