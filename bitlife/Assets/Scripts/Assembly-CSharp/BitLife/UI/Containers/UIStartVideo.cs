using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.Video;

namespace BitLife.UI.Containers
{
	public class UIStartVideo : MonoBehaviour
	{
		private sealed class _003CTimer_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStartVideo _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTimer_003Ed__14(int _003C_003E1__state)
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

		private sealed class _003CVideoSequence_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIStartVideo _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CVideoSequence_003Ed__15(int _003C_003E1__state)
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

		public static UIStartVideo Inst;

		private const float FADE_DURATION = 1f;

		private CanvasGroup canvasGroup;

		private VideoPlayer videoPlayer;

		private CandyButton dismissButton;

		private CandyButton videoPlayerButton;

		private Coroutine videoPlayback;

		private float timeAllowedBeforeVideoClick;

		private float timePassed;

		private bool videoClickIsAllowed;

		public bool ShouldPlayValidation()
		{
			return false;
		}

		private void Awake()
		{
		}

		private void OnDisable()
		{
		}

		public void Launch()
		{
		}

		private IEnumerator Timer()
		{
			return null;
		}

		private IEnumerator VideoSequence()
		{
			return null;
		}

		private void DismissButtonPress()
		{
		}

		private void VideoClick()
		{
		}

		public void TurnOff()
		{
		}

		public void FinishUp()
		{
		}
	}
}
