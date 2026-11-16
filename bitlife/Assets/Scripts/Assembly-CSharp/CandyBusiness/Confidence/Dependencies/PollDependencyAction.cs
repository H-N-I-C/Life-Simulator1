using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace CandyBusiness.Confidence.Dependencies
{
	public sealed class PollDependencyAction : ConfidenceDependencyAction
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CWaitAsync_003Ed__5 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationToken token;

			public PollDependencyAction _003C_003E4__this;

			private double _003CwakeAt_003E5__2;

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

		private readonly Func<bool> _predicate;

		private readonly int _pollMilliseconds;

		public override bool IsComplete => false;

		public PollDependencyAction(Func<bool> predicate, float pollSeconds = 0.25f)
		{
		}

		public override Awaitable WaitAsync(CancellationToken token)
		{
			return null;
		}
	}
}
