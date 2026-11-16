using UnityEngine;
using UnityEngine.Events;

public class UIToggleGameObject : MonoBehaviour, IToggle
{
	private UnityAction<int> callback;

	private int index;

	public void Select()
	{
	}

	public void SetButtonSound(SOUND sound)
	{
	}

	public void SetOnClickDelegateMethod(UnityAction<int> callback)
	{
	}

	public void SetOnClickDelegateMethodChoiceIndex(int index)
	{
	}

	public void Setup()
	{
	}

	public void Unselect()
	{
	}
}
