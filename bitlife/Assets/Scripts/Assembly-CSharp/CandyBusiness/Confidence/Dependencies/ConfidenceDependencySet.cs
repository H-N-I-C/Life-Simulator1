using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace CandyBusiness.Confidence.Dependencies
{
	public sealed class ConfidenceDependencySet
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CWaitAllAsync_003Ed__6 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationToken token;

			public ConfidenceDependencySet _003C_003E4__this;

			private object _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			private TaskAwaiter _003C_003Eu__1;

			private MainThreadAwaitable _003C_003Eu__2;

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

		private readonly List<ConfidenceDependency> _dependencies;

		public IReadOnlyList<ConfidenceDependency> Dependencies => null;

		public bool AllComplete => false;

		public ConfidenceDependencySet Add(ConfidenceDependency dependency)
		{
			return null;
		}

		public Awaitable WaitAllAsync(CancellationToken token)
		{
			return null;
		}

		public void Dispose()
		{
		}
	}
}
