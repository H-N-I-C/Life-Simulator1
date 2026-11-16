using System;
using System.Collections;
using System.Collections.Generic;
using Backend.Authentication;
using UnityEngine;

namespace CandyBusiness.AgeApi
{
	public class AgeRanceCheckComponent : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass6_0
		{
			public AgeRangeResult result;

			public AgeRanceCheckComponent _003C_003E4__this;

			internal void _003CHandleIOSData_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public bool hasStartSequence;

			public AgeRanceCheckComponent _003C_003E4__this;

			public bool hasSettings;

			internal void _003CStart_003Eb__0()
			{
			}

			internal void _003CStart_003Eb__1(GetConfigResponse settings)
			{
			}
		}

		private sealed class _003CDelayApiCall_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public AgeRanceCheckComponent _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDelayApiCall_003Ed__7(int _003C_003E1__state)
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

		private AndroidJavaObject _activity;

		private bool _canCallApiAgain;

		private GetConfigResponse _settings;

		private static void Init()
		{
		}

		private void CallAndroidApi()
		{
		}

		private void CallIOSApi()
		{
		}

		private void HandleIOSData(AgeRangeResult result)
		{
		}

		private IEnumerator DelayApiCall()
		{
			return null;
		}

		private void Start()
		{
		}

		private void HandleStartCheck(bool start)
		{
		}

		private static string GetStatusLogLabel(AgeStatus status)
		{
			return null;
		}

		public void CheckAgeSignal(Action<AgeStatus, int?, int?, string> callback)
		{
		}

		private void _003CCallIOSApi_003Eb__5_0(AgeRangeResult result)
		{
		}
	}
}
