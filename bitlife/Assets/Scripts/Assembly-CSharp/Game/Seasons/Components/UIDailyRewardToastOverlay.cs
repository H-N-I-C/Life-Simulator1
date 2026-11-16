using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using TMPro;
using UnityEngine;

namespace Game.Seasons.Components
{
	public sealed class UIDailyRewardToastOverlay : MonoBehaviour
	{
		private sealed class _003CPresentationRoutine_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDailyRewardToastOverlay _003C_003E4__this;

			private float _003Celapsed_003E5__2;

			private float _003CremainingSeconds_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPresentationRoutine_003Ed__16(int _003C_003E1__state)
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

		private const string ResourcePath = "daily-reward/DailyRewardToastOverlay";

		private CanvasGroup canvasGroup;

		private TextMeshProUGUI amountLabel;

		private CandyButton dismissButton;

		private Animator coinAnimator;

		private float coinDuration;

		private static UIDailyRewardToastOverlay activeOverlay;

		private Coroutine presentation;

		private bool skipPresentation;

		private bool accessiblePresentation;

		private Action completed;

		private Action coinFinished;

		private float followingAnimationDuration;

		public static bool Show(int amount, Action onCompleted = null, Action onCoinFinished = null, float followingAnimationSeconds = 0f)
		{
			return false;
		}

		private void Awake()
		{
		}

		private void Present(int amount)
		{
		}

		private IEnumerator PresentationRoutine()
		{
			return null;
		}

		private void SkipPresentation()
		{
		}

		private void Update()
		{
		}

		private void OnDestroy()
		{
		}
	}
}
