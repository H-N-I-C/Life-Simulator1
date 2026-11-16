using System.Collections.Generic;
using UnityEngine;

public class AccessibleUIGroupRoot : MonoBehaviour
{
	public enum EUIElement
	{
		EUndefined = 0,
		EButton = 1,
		ELabel = 2,
		EToggle = 3,
		ESlider = 4,
		ETextEdit = 5,
		EDropDown = 6
	}

	public class Accessible_UIElement
	{
		public EUIElement m_Type;

		public UAP_BaseElement m_Object;

		public Vector2 m_Pos;

		public int m_PositionOrder;

		public int m_SecondaryOrder;

		public bool AllowsVoiceOver()
		{
			return false;
		}

		public bool ReadType()
		{
			return false;
		}

		public void CalculatePositionOrder(UAP_BaseElement uiElement, int backupIndex)
		{
		}

		public Accessible_UIElement(UAP_BaseElement item, EUIElement type, int index)
		{
		}
	}

	public bool m_PopUp;

	public bool m_IsSilentPopUp;

	public bool m_AllowExternalJoining;

	public bool m_AutoRead;

	public int m_Priority;

	public string m_ContainerName;

	public bool m_IsLocalizationKey;

	public bool m_2DNavigation;

	public bool m_ConstrainToContainerUp;

	public bool m_ConstrainToContainerDown;

	public bool m_ConstrainToContainerLeft;

	public bool m_ConstrainToContainerRight;

	public bool m_AllowTouchExplore;

	private bool m_HasStarted;

	private bool m_RefreshNextFrame;

	private bool m_ActivateContainerNextFrame;

	public bool m_DoubleCheckUIElementsPositions;

	private bool m_NeedsRefreshBeforeActivation;

	public List<GameObject> separatorObjects;

	private int currentSeparatorIndex;

	private List<Accessible_UIElement> m_AllElements;

	private UAP_BaseElement m_CurrentStartItem;

	private int m_CurrentItemIndex;

	private bool hasBeenForcedCleared;

	public int CurrentSeparatorIndex => 0;

	public void SetCurrentSeparatorIndex(int index)
	{
	}

	public void DecrementCurrentSeparatorIndex()
	{
	}

	public void IncrementCurrentSeparatorIndex()
	{
	}

	public bool IsConstrainedToContainer(UAP_AccessibilityManager.ESDirection direction)
	{
		return false;
	}

	public static void GetAbsoluteAnchors(RectTransform t, out Vector2 anchorMin, out Vector2 anchorMax, out Vector2 centerPos, bool stopAtScrollView = false)
	{
		anchorMin = default;
		anchorMax = default;
		centerPos = default;
	}

	public void CheckForRegister(UAP_BaseElement item)
	{
	}

	public void SetAsStartItem(UAP_BaseElement item)
	{
	}

	public void UnRegister(UAP_BaseElement item)
	{
	}

	public void ForceClearItems()
	{
	}

	public void RefreshContainer()
	{
	}

	private void ActivateContainer_Internal()
	{
	}

	private void Register_Item(UAP_BaseElement item)
	{
	}

	public void ForceAddItem(UAP_BaseElement item)
	{
	}

	private void OnEnable()
	{
	}

	public void ResetToStart()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	public Accessible_UIElement GetCurrentElement(bool rollOverAllowed)
	{
		return null;
	}

	public Accessible_UIElement GetElementByIndex(int index)
	{
		return null;
	}

	public int GetAllElementsCount()
	{
		return 0;
	}

	public void SelectFirstItem()
	{
	}

	private int FindFirstActiveItemIndex(int startIndex, bool rollOverAllowed)
	{
		return 0;
	}

	private int FindPreviousActiveItemIndex(int startIndex, bool rollOverAllowed)
	{
		return 0;
	}

	public bool IncrementCurrentItem(bool rollOverAllowed)
	{
		return false;
	}

	public bool DecrementCurrentItem(bool rollOverAllowed)
	{
		return false;
	}

	public bool MoveFocus2D(UAP_AccessibilityManager.ESDirection direction)
	{
		return false;
	}

	private void Update()
	{
	}

	public void RefreshNextUpdate()
	{
	}

	public void JumpToFirst(bool skipFirstLine = false)
	{
	}

	public bool JumpToLast()
	{
		return false;
	}

	public void SetActiveElementIndex(int index, bool rollOverAllowed)
	{
	}

	public int GetCurrentElementIndex()
	{
		return 0;
	}

	public List<Accessible_UIElement> GetElements()
	{
		return null;
	}

	public bool SelectItem(UAP_BaseElement element, bool forceRepeatItem = false)
	{
		return false;
	}

	public void SelectNextItem()
	{
	}

	public void SelectPreviousItem()
	{
	}

	public void SelectNearestSeparator(bool goingUp)
	{
	}

	public int GetNearestSeparatorFromIndex(int elementIndex, bool goingUp)
	{
		return 0;
	}

	public int GetElementIndex(UAP_BaseElement element)
	{
		return 0;
	}

	public string GetContainerName(bool useGameObjectNameIfNone = false)
	{
		return null;
	}

	public bool IsNameLocalizationKey()
	{
		return false;
	}
}
