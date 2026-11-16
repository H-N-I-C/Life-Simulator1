using System;
using System.Collections;
using System.Collections.Generic;

namespace Tracking
{
	public sealed class GameTelemetryPreReadyEventDrain
	{
		private sealed class _003CDrain_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Action<IGameTelemetryEvent> dispatchEvent;

			public GameTelemetryPreReadyEventDrain _003C_003E4__this;

			public Func<IEnumerator> waitOneSecond;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDrain_003Ed__7(int _003C_003E1__state)
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

		private readonly Queue<IGameTelemetryEvent> _pendingEvents;

		private bool _drainStarted;

		public int PendingCount => 0;

		public void Enqueue(IGameTelemetryEvent telemetryEvent)
		{
		}

		public bool TryBeginDrain()
		{
			return false;
		}

		public void DispatchReadyEvent(IGameTelemetryEvent telemetryEvent, Action<IGameTelemetryEvent> dispatchEvent)
		{
		}

		public IEnumerator Drain(Action<IGameTelemetryEvent> dispatchEvent, Func<IEnumerator> waitOneSecond)
		{
			return null;
		}
	}
}
