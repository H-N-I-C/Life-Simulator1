using System;
using System.Collections;
using System.Collections.Generic;
using Backend;
using BitLife.UI.Utils;
using Game;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIStartSequence : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<bool> _003C_003E9__22_1;

			public static Action<MYSTUFF_REDEEM> _003C_003E9__22_5;

			public static Action _003C_003E9__22_2;

			public static Func<bool> _003C_003E9__26_0;

			internal void _003CCheckForDeepLinkReward_003Eb__22_1(bool _)
			{
			}

			internal void _003CCheckForDeepLinkReward_003Eb__22_2()
			{
			}

			internal void _003CCheckForDeepLinkReward_003Eb__22_5(MYSTUFF_REDEEM success)
			{
			}

			internal bool _003CFinishUp_003Eb__26_0()
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public int count;

			internal void _003CCheckForDeepLinkReward_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_1
		{
			public DataMyStuffCatalog item;

			public bool shouldStartNewLife;

			public int holdUntilRedeem;

			public Action<bool> _003C_003E9__4;

			internal void _003CCheckForDeepLinkReward_003Eb__3(MYSTUFF_REDEEM success)
			{
			}

			internal void _003CCheckForDeepLinkReward_003Eb__4(bool loadedLife)
			{
			}
		}

		private sealed class _003CAnimateGiantSperm_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStartSequence _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAnimateGiantSperm_003Ed__25(int _003C_003E1__state)
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

		private sealed class _003CCheckForDeepLinkReward_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private _003C_003Ec__DisplayClass22_0 _003C_003E8__1;

			public UIStartSequence _003C_003E4__this;

			private _003C_003Ec__DisplayClass22_1 _003C_003E8__2;

			private bool _003CisDataAvailable_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCheckForDeepLinkReward_003Ed__22(int _003C_003E1__state)
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

		private sealed class _003CFinishUp_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStartSequence _003C_003E4__this;

			private string _003ClanguageForTranslation_003E5__2;

			private int _003Ci_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFinishUp_003Ed__26(int _003C_003E1__state)
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

		private sealed class _003CWelcome_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWelcome_003Ed__23(int _003C_003E1__state)
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

		public static Action OnFinishStartSequence;

		public static UIStartSequence Inst;

		private const float START_FADE_OUT_DELAY = 2.1f;

		private const float FADE_OUT_TIME_ELOTED = 0.3f;

		private const float SPERM_ANIM_DURATION = 1.1f;

		private const float PAUSE_DURATION = 0.1f;

		private const float NOTICE_DURATION = 2f;

		private const float FADE_DURATION = 0.3f;

		private CanvasGroup startSequenceCanvasGroup;

		private FadeCandyImage colorBackground;

		private FadeCandyImage startImage;

		private RectTransform giantSpermRect;

		private AnimationImageSequence animControler;

		private FadeCandyText noticeText;

		private FadeCandyImage redBackground;

		private bool hasStarted;

		private bool shouldPlayVideo;

		private Coroutine swim;

		private bool _waitingStart;

		private void Awake()
		{
		}

		private void OnDisable()
		{
		}

		private void Start()
		{
		}

		private IEnumerator CheckForDeepLinkReward()
		{
			return null;
		}

		private IEnumerator Welcome()
		{
			return null;
		}

		private void FixedUpdate()
		{
		}

		private IEnumerator AnimateGiantSperm()
		{
			return null;
		}

		private IEnumerator FinishUp()
		{
			return null;
		}
	}
}
