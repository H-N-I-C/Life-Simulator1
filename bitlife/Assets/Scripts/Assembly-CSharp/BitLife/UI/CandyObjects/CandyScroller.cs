using System;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyScroller : MonoBehaviour, IguiObject, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
	{
		private RectTransform rect;

		private ScrollRect scrollRect;

		private Image frameBorderImage;

		private Scrollbar verticalScrollBar;

		private Action onScrollReleaseMethod;

		private bool isScrolling;

		private Image _blocker;

		private bool _overrideDisableBloker;

		private void Start()
		{
		}

		private bool IsBlockerEnabled()
		{
			return false;
		}

		private void SetupBlocker()
		{
		}

		private void OnValueChangeListener(Vector2 pos)
		{
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public ScrollRect GetScrollRect()
		{
			return null;
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnDrag(PointerEventData eventData)
		{
		}

		public void OnEndDrag(PointerEventData eventData)
		{
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		private void DelayDisable()
		{
		}

		private void SetBlocker(bool active)
		{
		}

		public void SetScrollReleaseDelegateMethod(Action method)
		{
		}

		public void SetBackgroundColor(Color color)
		{
		}

		public float GetVerticalScrollBarPos()
		{
			return 0f;
		}

		public Scrollbar GetVerticalScrollBar()
		{
			return null;
		}

		public void SetScrollBarPos(float value)
		{
		}

		public bool IsScrolling()
		{
			return false;
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetScrollingAllowedHorizontal(bool isAllowed)
		{
		}

		public void SetScrollingAllowedVertical(bool isAllowed)
		{
		}

		public void SetIsShowingScrollbars(bool isShowing)
		{
		}

		public void SetContent(GameObject content)
		{
		}

		public void SetContentAnchoredPosition(Vector2 pos)
		{
		}

		public void SetHeightConstraint()
		{
		}
	}
}
