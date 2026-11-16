using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TMProUGUIHyperlinks : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	private Color32 hoveredColor;

	private Color32 pressedColor;

	private Color32 usedColor;

	private Color32 usedHoveredColor;

	private Color32 usedPressedColor;

	private List<Color32[]> startColors;

	private TextMeshProUGUI textMeshPro;

	private Dictionary<int, bool> usedLinks;

	private int hoveredLinkIndex;

	private int pressedLinkIndex;

	private Camera mainCamera;

	private void Awake()
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	private void LateUpdate()
	{
	}

	private int GetLinkIndex()
	{
		return 0;
	}

	private List<Color32[]> SetLinkColor(int linkIndex, Color32 color)
	{
		return null;
	}

	private void ResetLinkColor(int linkIndex, List<Color32[]> startColors)
	{
	}

	private void _003COnPointerUp_003Eb__13_0(Color32[] c)
	{
	}
}
