using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CandyBusiness.Confidence.Dependencies
{
	public sealed class EventDependencyAction : ConfidenceDependencyAction
	{
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public EventDependencyAction _003C_003E4__this;

			public CancellationToken token;

			internal void _003CWaitAsync_003Eb__0()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CWaitAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public EventDependencyAction _003C_003E4__this;

			public CancellationToken token;

			private CancellationTokenRegistration _003C_003E7__wrap1;

			private object _003C_003E7__wrap2;

			private int _003C_003E7__wrap3;

			private TaskAwaiter<bool> _003C_003Eu__1;

			private ValueTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private readonly Func<bool> _alreadyDone;

		private readonly Action<Action> _unsubscribe;

		private readonly Action _handler;

		private TaskCompletionSource<bool> _tcs;

		private bool _complete;

		public override bool IsComplete => false;

		public EventDependencyAction(Action<Action> subscribe, Action<Action> unsubscribe, Func<bool> alreadyDone = null)
		{
		}

		private void OnFired()
		{
		}

		public override Awaitable WaitAsync(CancellationToken token)
		{
			return null;
		}

		public override void Dispose()
		{
		}
	}
	public sealed class EventDependencyAction<T> : ConfidenceDependencyAction
	{
		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public EventDependencyAction<T> _003C_003E4__this;

			public CancellationToken token;

			internal void _003CWaitAsync_003Eb__0()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CWaitAsync_003Ed__9 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public EventDependencyAction<T> _003C_003E4__this;

			public CancellationToken token;

			private CancellationTokenRegistration _003C_003E7__wrap1;

			private object _003C_003E7__wrap2;

			private int _003C_003E7__wrap3;

			private TaskAwaiter<bool> _003C_003Eu__1;

			private ValueTaskAwaiter _003C_003Eu__2;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		private readonly Func<bool> _alreadyDone;

		private readonly Action<Action<T>> _unsubscribe;

		private readonly Action<T> _handler;

		private readonly TaskCompletionSource<bool> _tcs;

		private bool _complete;

		public override bool IsComplete => false;

		public EventDependencyAction(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe, Func<bool> alreadyDone = null)
		{
		}

		private void OnFired(T _)
		{
		}

		public override Awaitable WaitAsync(CancellationToken token)
		{
			return null;
		}

		public override void Dispose()
		{
		}
	}
}
