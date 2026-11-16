using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine.Networking;

namespace Backend.Code.Services
{
	public static class ServerChecker
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CCheckReachable_003Ed__0 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public string url;

			public int timeoutSeconds;

			private UnityWebRequest _003Crequest_003E5__2;

			private UnityWebRequestAsyncOperation _003Coperation_003E5__3;

			private TaskAwaiter _003C_003Eu__1;

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

		public static Task<bool> CheckReachable(string url, int timeoutSeconds = 5)
		{
			return null;
		}
	}
}
