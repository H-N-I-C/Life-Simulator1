using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIScenarioCard : MonoBehaviour
	{
		private sealed class _003CActivateAfterTime_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float timeInSeconds;

			public GameObject gameObject;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CActivateAfterTime_003Ed__27(int _003C_003E1__state)
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

		private sealed class _003CCardAnimation_003Ed__33 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioCard _003C_003E4__this;

			public float duration;

			public int passedInDamage;

			private Vector2 _003CstartPos_003E5__2;

			private Vector2 _003CendPos_003E5__3;

			private int _003Ci_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCardAnimation_003Ed__33(int _003C_003E1__state)
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

		private sealed class _003CReveal_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioCard _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CReveal_003Ed__29(int _003C_003E1__state)
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

		private sealed class _003CRevealFeedbackAnimation_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioCard _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealFeedbackAnimation_003Ed__32(int _003C_003E1__state)
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

		private sealed class _003CRevealNormalAnimation_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioCard _003C_003E4__this;

			private Vector2 _003Ctop_003E5__2;

			private Vector2 _003Cbottom_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealNormalAnimation_003Ed__31(int _003C_003E1__state)
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

		private const float TAP_TEXT_ACTIVATE_DELAY = 5f;

		private const float REVEAL_DROP_DURATION = 0.22f;

		private const float REVEAL_DROP_BACK_DURATION = 0.14f;

		private const float REVEAL_DROP_TO_POS = -40f;

		private const float REVEAL_SCALE_DURATION = 0.16f;

		private const float REVEAL_SCALE_BACK_DURATION = 0.14f;

		private const float REVEAL_SCALE_UP_TO = 1.15f;

		private const string TAP_ANYWHERE_TEXT = "Tap anywhere to continue";

		private RectTransform rect;

		private CandyButton dismissButton;

		private CandyImage background;

		private CandyButton fullScreenButton;

		private GameObject TapAnywhereText;

		private bool isFeedbackStyle;

		public int damage;

		private bool isShowingFullScreenDismissButton;

		public IguiObject[] guiObjects;

		public Scenario scenario;

		public List<CandyInputField> inputFields;

		public List<CandyDropDown> dropDowns;

		public List<CandySlider> sliders;

		private void OnDisable()
		{
		}

		public void Constructor(UIScenarioCardOptions options, IguiObject[] guiObjects, Scenario scenario = null)
		{
		}

		public void SetFullScreenButtonSizeAndScale()
		{
		}

		public void SetFullScreenButtonSizeDriver(RectTransform rect)
		{
		}

		public void SetDismissButtonVisibility(bool isShowingDimissButton)
		{
		}

		public void SetFullScreenDismissVisibility(bool isShowingFullScreenDismiss)
		{
		}

		private IEnumerator ActivateAfterTime(GameObject gameObject, float timeInSeconds)
		{
			return null;
		}

		public void SetDismissButtonMethod(Action delegateMethod)
		{
		}

		public IEnumerator Reveal()
		{
			return null;
		}

		public void SetToTop()
		{
		}

		private IEnumerator RevealNormalAnimation()
		{
			return null;
		}

		private IEnumerator RevealFeedbackAnimation()
		{
			return null;
		}

		public IEnumerator CardAnimation(float duration, int passedInDamage)
		{
			return null;
		}
	}
}
