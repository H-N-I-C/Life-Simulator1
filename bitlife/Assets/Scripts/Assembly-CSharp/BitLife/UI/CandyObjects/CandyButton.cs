using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyButton : MonoBehaviour, IguiObject, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
	{
		public delegate void ScenarioDelegate(Scenario scenario);

		public delegate void ListDelegate(List list);

		private sealed class _003CClickLogic_003Ed__47 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyButton _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CClickLogic_003Ed__47(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private sealed class _003CPassOnBeginDragEvent_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyButton _003C_003E4__this;

			public PointerEventData eventData;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPassOnBeginDragEvent_003Ed__50(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private sealed class _003CPassOnClickEvent_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyButton _003C_003E4__this;

			public PointerEventData eventData;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPassOnClickEvent_003Ed__49(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private Material shadeMat;

		private RectTransform rect;

		private Image image;

		private GameObject displayObjectPressed;

		private GameObject displayObjectNormal;

		public AccessibleCandyButton uapButton;

		private CandyImage singleCandyImageDisplayObject;

		private CandyImage singleCandyImageDisplayObjectPressed;

		private CandyImage singleCandyImageFrameDisplayObject;

		private CandyImage singleCandyImageFrameDisplayObjectPressed;

		private CandyText singleCandyTextDisplayObject;

		private CandyText singleCandyTextDisplayObjectPressed;

		private int choiceIndex;

		public SOUND sound;

		private bool ignoreNoDelegateWarning;

		private bool disabled;

		private ScrollRect parentScrollRect;

		private Action onPointerDownMethod;

		private Action onClickMethod;

		private UnityAction<int> onClickMethodIndexed;

		private ScenarioDelegate onClickMethodScenario;

		private ListDelegate onClickMethodList;

		private Scenario onClickScenario;

		private List onClickList;

		private bool amIPressed;

		private float _lastClickTime;

		private const float CLICK_COOLDOWN = 0.3f;

		public Image Image
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public GameObject DisplayObjectPressed => null;

		public GameObject DisplayObjectNormal => null;

		public bool Disabled
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void ToggleUAPHelpText()
		{
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void SetSound(SOUND sound)
		{
		}

		public void SetOnPointerDownDelegateMethod(Action delegateMethod)
		{
		}

		public void OnPointerDown(PointerEventData eventData)
		{
		}

		public void OnPointerUp(PointerEventData eventData)
		{
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
		}

		public void OnPointerExit(PointerEventData eventData)
		{
		}

		private IEnumerator ClickLogic()
		{
			return null;
		}

		private void TrackFirebaseClick(Exception e)
		{
		}

		private IEnumerator PassOnClickEvent(PointerEventData eventData)
		{
			return null;
		}

		private IEnumerator PassOnBeginDragEvent(PointerEventData eventData)
		{
			return null;
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

		public void OnClick()
		{
		}

		public string GetSingleCandyTextString()
		{
			return null;
		}

		public float GetPosOfSingleCandyTextRightEdge()
		{
			return 0f;
		}

		public GameObject GetSingleCandyTextGameObject()
		{
			return null;
		}

		public GameObject GetSingleCandyImage()
		{
			return null;
		}

		public void SetScrollParent(ScrollRect scroller)
		{
		}

		public void SetOnClickScenario(Scenario scenario)
		{
		}

		public void SetOnClickList(List list)
		{
		}

		public void SetSingleCandyImageAlphaThreshold(float value)
		{
		}

		public void SetSingleCandyImage(Texture2D texture)
		{
		}

		public void SetSingleCandyImageColor(Color color)
		{
		}

		public void SetSingleCandyImageFrameColor(Color color)
		{
		}

		public void SetSingleCandyTextText(string text, int fontType = -1)
		{
		}

		public void SetSingleCandyTextTextFont(int fontType)
		{
		}

		public void SetSingleCandyTextSize(float size)
		{
		}

		public void SetSingleCandyTextColor(Color color, Color pressedColor)
		{
		}

		public void SetIsUnderlined(bool isUnderlined)
		{
		}

		public void SetupText(bool isSelected)
		{
		}

		public void SetOverlayState(bool isActive)
		{
		}

		public void ResetThisButton()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetColor(Color color)
		{
		}

		public void CreateCandyImageDisplayObject(Texture2D texture, Vector2 size)
		{
		}

		public void SetDisplayObject(GameObject displayObject, bool createShadow = true)
		{
		}

		public void SetDisplayObjectPressed(GameObject displayObject)
		{
		}

		public void SetOnClickDelegateMethod(Action method)
		{
		}

		public void SetOnClickDelegateMethod(UnityAction<int> method)
		{
		}

		public void SetOnClickDelegateMethod(UnityAction<int> method, int index = -1)
		{
		}

		public void SetOnClickDelegateMethodChoiceIndex(int index)
		{
		}

		public void SetOnClickDelegateMethodScenario(ScenarioDelegate method)
		{
		}

		public void SetOnClickDelegateMethodList(ListDelegate method)
		{
		}

		public void SetHeightConstraint()
		{
		}
	}
}
