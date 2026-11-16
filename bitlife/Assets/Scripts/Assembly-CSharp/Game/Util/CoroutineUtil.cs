using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Util
{
	public static class CoroutineUtil
	{
		private sealed class _003CDelayedAction_003Ed__0 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public Action action;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDelayedAction_003Ed__0(int _003C_003E1__state)
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

		private sealed class _003CFrameDelayedAction_003Ed__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int frameDelay;

			public Action action;

			private int _003Ci_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFrameDelayedAction_003Ed__1(int _003C_003E1__state)
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

		public static IEnumerator DelayedAction(Action action, float delay)
		{
			return null;
		}

		public static IEnumerator FrameDelayedAction(Action action, int frameDelay)
		{
			return null;
		}
	}
}
