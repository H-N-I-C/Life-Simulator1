using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Backend.Authentication
{
	public class GuestAuthService : IAuthManager
	{
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public Action<string> result;

			internal void _003CGetCredentialState_003Eb__0(string deviceId)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_0
		{
			public Action<AuthDataModel> response;

			public GuestAuthService _003C_003E4__this;

			public Action _003C_003E9__1;

			public Action _003C_003E9__3;

			internal void _003CNativeLogin_003Eb__0(string deviceId)
			{
			}

			internal void _003CNativeLogin_003Eb__1()
			{
			}

			internal void _003CNativeLogin_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_1
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CNativeLogin_003Eb__2_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass4_1 _003C_003E4__this;

				private _003C_003Ec__DisplayClass4_2 _003C_003E8__1;

				private TaskAwaiter<string> _003C_003Eu__1;

				private TaskAwaiter _003C_003Eu__2;

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

			public string deviceId;

			public _003C_003Ec__DisplayClass4_0 CS_0024_003C_003E8__locals1;

			internal Task _003CNativeLogin_003Eb__2()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_2
		{
			public GetGuestTokenResponse refreshObj;

			public _003C_003Ec__DisplayClass4_1 CS_0024_003C_003E8__locals2;

			internal void _003CNativeLogin_003Eb__4()
			{
			}
		}

		private int _retryCount;

		public void GetCredentialState(string userId, Action<string> result)
		{
		}

		public void UpdateAuth()
		{
		}

		public void Init()
		{
		}

		public void NativeLogin(Action<AuthDataModel> response)
		{
		}
	}
}
