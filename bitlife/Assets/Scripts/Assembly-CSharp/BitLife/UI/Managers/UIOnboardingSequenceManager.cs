using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIOnboardingSequenceManager : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<GameObject, bool> _003C_003E9__10_0;

			internal bool _003Cget_IsShowing_003Eb__10_0(GameObject step)
			{
				return false;
			}
		}

		private sealed class _003CFinishRelease_003Ed__77 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			private UIOnboardingManager _003ConboardingManager_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFinishRelease_003Ed__77(int _003C_003E1__state)
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

		private sealed class _003CStep0FadeInSequence_003Ed__80 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStep0FadeInSequence_003Ed__80(int _003C_003E1__state)
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

		private sealed class _003CStep2FadeInSequence_003Ed__87 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStep2FadeInSequence_003Ed__87(int _003C_003E1__state)
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

		private sealed class _003CStep3FadeInSequence_003Ed__89 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStep3FadeInSequence_003Ed__89(int _003C_003E1__state)
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

		private sealed class _003CStep4FadeInSequence_003Ed__92 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStep4FadeInSequence_003Ed__92(int _003C_003E1__state)
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

		private sealed class _003CTransitionFromStep0ToStep1_003Ed__83 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIOnboardingSequenceManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTransitionFromStep0ToStep1_003Ed__83(int _003C_003E1__state)
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

		public static Action OnOnboardingFinished;

		private static bool onboardingFinishedDispatched;

		private static Action m_OnActivityChanged;

		private static UIOnboardingSequenceManager activeSequence;

		private const float STEP0_PARTIAL_FADEIN_DUR = 0.2f;

		private const float STEP1_ANIM_DUR = 0.35f;

		private const int STAT_THREASHOLD = 50;

		private const float ALPHA_THREASHOLD_FOR_CLICK_THROUGH = 0.001f;

		private const float TOP_BUFFER_HEIGHT = 179.16f;

		private const float BOT_BUFFER_HEIGHT = 60f;

		private const float BORDER_BUFFER_HEIGHT = 26.7f;

		private GameObject inputBlocker;

		private CandyImage bgShade;

		private GameObject bgShadeWideGroup;

		private CandyImage bgShadeWideLt;

		private CandyImage bgShadeWideRt;

		private GameObject bgShadeDynamicGroup;

		private GameObject[] stepGameObjects;

		private CandyImage step0Emoji;

		private CandyButton male;

		private CandyButton female;

		private CanvasGroup row1CanvasGroup;

		private CanvasGroup row2CanvasGroup;

		private CanvasGroup maleCanvasGroup;

		private CanvasGroup femaleCanvasGroup;

		private CandyButton person1;

		private CandyButton person2;

		private CandyButton person3;

		private CandyImage step2Shade;

		private CandyImage step2Emoji;

		private RectTransform step2HolePunchBorder;

		private CanvasGroup step2CanvasGroup;

		private CandyButtonNoEventSystem step2FullScreenButton;

		private CandyText step3Info;

		private CandyImage step3Emoji;

		private CandyButtonNoEventSystem step3FullScreenButton;

		private CandyImage step4Emoji;

		private CandyImage step4HolePunchBorderLtCI;

		private CandyImage step4HolePunchBorderRtCI;

		private CanvasGroup step4Row2CanvasGroup;

		private CanvasGroup step4Row3CanvasGroup;

		private CandyButtonNoEventSystem step4FullScreenButton;

		private CandyImage step5EmojiLt;

		private CandyImage step5EmojiRt;

		private CandyButtonNoEventSystem step5FullScreenButton;

		private bool step1IsReleased;

		private bool step2IsReleased;

		private bool step3IsReleased;

		private bool step4IsReleased;

		private List<GodModePersonConfig> personConfigs;

		private ArrayList candidates;

		private AGE_VARIANT _ageVariant;

		private List<string> candidateEmojis;

		private bool step5Requested;

		private bool step5Visible;

		private int step5ShownFrame;

		private Life step5Life;

		private UIOnboardingManager step5Owner;

		public static bool IsOnboardingActive => false;

		public bool IsShowing => false;

		public static event Action OnActivityChanged
		{
			add
			{
			}
			remove
			{
			}
		}

		private void SetOnboardingActive(bool active)
		{
		}

		public static void CompleteOnboarding()
		{
		}

		public static void DispatchOnboardingFinished()
		{
		}

		private void LateUpdate()
		{
		}

		private void OnDisable()
		{
		}

		private void Awake()
		{
		}

		private void CheckForConfidenceFlag()
		{
		}

		private void ShowStepGameObject(int step)
		{
		}

		private void ReleaseStep1()
		{
		}

		private void ReleaseStep2()
		{
		}

		private void ReleaseStep3()
		{
		}

		private void ReleaseStep4()
		{
		}

		private void ReleaseStep5()
		{
		}

		private IEnumerator FinishRelease()
		{
			return null;
		}

		private void HideStepGameObject(int step)
		{
		}

		public void Step0()
		{
		}

		private IEnumerator Step0FadeInSequence()
		{
			return null;
		}

		public void Step1(int gender)
		{
		}

		private void GetCandidatesFromData(AGE_VARIANT ageVariant, int gender)
		{
		}

		private IEnumerator TransitionFromStep0ToStep1()
		{
			return null;
		}

		private void CreateLifeWithPersonIndex(int index)
		{
		}

		private ArrayList GetCandidateArrayForGender(int gender)
		{
			return null;
		}

		public void Step2()
		{
		}

		private IEnumerator Step2FadeInSequence()
		{
			return null;
		}

		public void Step3()
		{
		}

		private IEnumerator Step3FadeInSequence()
		{
			return null;
		}

		private string GetStatsDescription()
		{
			return null;
		}

		public void Step4()
		{
		}

		private IEnumerator Step4FadeInSequence()
		{
			return null;
		}

		public void Step5()
		{
		}

		private bool IsStep5Eligible()
		{
			return false;
		}

		private bool CanPresentStep5()
		{
			return false;
		}

		private void UpdateStep5Presentation()
		{
		}

		private bool CanInteractWithStep5()
		{
			return false;
		}

		private void HideStep5Presentation()
		{
		}

		private void StopStep5()
		{
		}

		private void DismissStep5()
		{
		}

		private void ShowStep5Presentation()
		{
		}
	}
}
