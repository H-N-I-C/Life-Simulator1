using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAlertViewEntry : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private Image background;

	private TextMeshProUGUI text;

	public ScrollRect scrollRect;

	private Action<int> callback;

	private bool pointerClicked;

	public void SetBackgroundColor(Color color)
	{
	}

	public string GetText()
	{
		return null;
	}

	public GameObject GetTextGameObject()
	{
		return null;
	}

	public void SetText(string title)
	{
	}

	public void SetCallback(Action<int> callback)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void ForceClick()
	{
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
	}

	public void OnDrag(PointerEventData eventData)
	{
	}

	public void OnEndDrag(PointerEventData eventData)
	{
	}
}
