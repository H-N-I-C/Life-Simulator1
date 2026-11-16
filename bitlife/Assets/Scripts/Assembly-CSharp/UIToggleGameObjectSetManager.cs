using System;
using System.Collections.Generic;
using UnityEngine;

public class UIToggleGameObjectSetManager : MonoBehaviour
{
	public List<GameObject> toggles;

	private int selectedToggleIndex;

	public Action callback;

	public int SelectedToggleIndex => 0;

	public void Start()
	{
	}

	public void SetButtonSound(SOUND sound)
	{
	}

	public void SelectToggle(int index)
	{
	}
}
