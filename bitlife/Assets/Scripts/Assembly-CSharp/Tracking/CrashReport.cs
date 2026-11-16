using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tracking
{
	public static class CrashReport
	{
		private class CoroutineRunner : MonoBehaviour
		{
		}

		private sealed class _003C_003Ec__DisplayClass4_0
		{
			public Exception e;

			public Dictionary<string, string> trackingParams;

			internal void _003CFirebaseTracking_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public string logMessage;

			internal void _003CFirebaseLog_003Eb__0()
			{
			}
		}

		private sealed class _003CDequeueLogsAsync_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float timeBetweenLogs;

			private WaitForSeconds _003CwaitForSeconds_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDequeueLogsAsync_003Ed__6(int _003C_003E1__state)
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

		private static readonly Queue<Action> AsyncLogs;

		private static CoroutineRunner LogCoroutineHolder;

		private const float TIME_BETWEEN_LOGS = 3f;

		public static void FirebaseTracking(Exception e, Dictionary<string, string> extraParams = null)
		{
		}

		private static void LogCrashException(Exception e, Dictionary<string, string> trackingParams)
		{
		}

		private static IEnumerator DequeueLogsAsync(float timeBetweenLogs)
		{
			return null;
		}

		public static void FirebaseLog(string logMessage)
		{
		}
	}
}
