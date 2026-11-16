using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAnalyticsSDK.Events
{
	public class GA_SpecialEvents : MonoBehaviour
	{
		private sealed class _003CCheckCriticalFPSRoutine_003Ed__12 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GA_SpecialEvents _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCheckCriticalFPSRoutine_003Ed__12(int _003C_003E1__state)
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

		private sealed class _003CSubmitFPSRoutine_003Ed__11 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSubmitFPSRoutine_003Ed__11(int _003C_003E1__state)
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

		private static int _frameCountAvg;

		private static float _lastUpdateAvg;

		private int _frameCountCrit;

		private float _lastUpdateCrit;

		private static int _criticalFpsCount;

		private static int _fpsWaitTimeMultiplier;

		private static float _lastPauseStartTime;

		private static float _pauseDurationAvg;

		private static float _pauseDurationCrit;

		public void Start()
		{
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}

		private IEnumerator SubmitFPSRoutine()
		{
			return null;
		}

		private IEnumerator CheckCriticalFPSRoutine()
		{
			return null;
		}

		public void Update()
		{
		}

		public static void SubmitFPS()
		{
		}

		public void CheckCriticalFPS()
		{
		}
	}
}
