using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CandyBusiness.Confidence.Dependencies
{
	public sealed class ConfidenceDependency : IDisposable
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CAsTask_003Ed__16 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Awaitable awaitable;

			private Awaitable.Awaiter _003C_003Eu__1;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CWaitAsync_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationToken token;

			public ConfidenceDependency _003C_003E4__this;

			private CancellationTokenSource _003Clinked_003E5__2;

			private Task _003CactionTask_003E5__3;

			private TaskAwaiter _003C_003Eu__1;

			private TaskAwaiter<Task> _003C_003Eu__2;

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

		private readonly ConfidenceDependencyAction _action;

		private readonly int _timeoutMilliseconds;

		private readonly string Name__BackingField;

		private readonly DependencyMode Mode__BackingField;

		private bool TimedOut__BackingField;

		public string Name => Name__BackingField;

		public DependencyMode Mode => Mode__BackingField;

		public bool TimedOut
		{
			get
			{
				return TimedOut__BackingField;
			}
			private set
			{
				TimedOut__BackingField = value;
			}
		}

		public bool IsComplete => false;

		public ConfidenceDependency(string name, ConfidenceDependencyAction action, float timeoutSeconds, DependencyMode mode = DependencyMode.Optional)
		{
		}

		public Awaitable WaitAsync(CancellationToken token)
		{
			return null;
		}

		private static Task AsTask(Awaitable awaitable)
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
