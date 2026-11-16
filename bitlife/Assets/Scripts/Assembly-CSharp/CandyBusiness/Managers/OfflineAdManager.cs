using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace CandyBusiness.Managers
{
	public class OfflineAdManager : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<VideoClip, bool> _003C_003E9__29_0;

			internal bool _003CPlayLocalAD_003Eb__29_0(VideoClip x)
			{
				return false;
			}
		}

		private sealed class _003CPlayLocalAD_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OfflineAdManager _003C_003E4__this;

			public int adType;

			private VideoClip _003CselectedClip_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPlayLocalAD_003Ed__29(int _003C_003E1__state)
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

		private sealed class _003CSkipButtonWait_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OfflineAdManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSkipButtonWait_003Ed__28(int _003C_003E1__state)
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

		public static OfflineAdManager Inst;

		private GameObject Background;

		private GameObject SkipButtonObj;

		private GameObject ProgressMeterObj;

		private VideoPlayer OfflineAdPlayer;

		private VideoClip[] AvailableOfflineAds;

		private Image ProgressBarFill;

		private Button SkipButton;

		private bool IsPlayingAD;

		private float ElapsedTime;

		private double AdDuration;

		private float PercComplete;

		private bool IsRewarded;

		public static Scenario NextScenario;

		public static string LogString;

		public static int Origin;

		public static Action Callback;

		public void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void Update()
		{
		}

		public void ShowInterstitial()
		{
		}

		public void ShowRewarded(Scenario postPlayScenario, string postPlayLog, int origin, Action callback)
		{
		}

		public void ShowRewarded(Scenario postPlayScenario, string postPlayLog, Action callback)
		{
		}

		private void DeactivateDisplay()
		{
		}

		private void OnAdComplete()
		{
		}

		private void UpdateElapsedTime()
		{
		}

		private void OnAdSkipped()
		{
		}

		private IEnumerator SkipButtonWait()
		{
			return null;
		}

		private IEnumerator PlayLocalAD(int adType)
		{
			return null;
		}

		private void EndReached(VideoPlayer pVP)
		{
		}
	}
}
