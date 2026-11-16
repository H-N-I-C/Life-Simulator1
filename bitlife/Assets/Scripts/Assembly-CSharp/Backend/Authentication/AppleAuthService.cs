using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AppleAuth;
using AppleAuth.Enums;
using AppleAuth.Interfaces;

namespace Backend.Authentication
{
	public class AppleAuthService : IAuthManager
	{
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public Action<string> result;

			internal void _003CGetCredentialState_003Eb__0(CredentialState state)
			{
			}

			internal void _003CGetCredentialState_003Eb__1(IAppleError error)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_0
		{
			public Action<AuthDataModel> response;

			internal void _003CNativeLogin_003Eb__0(ICredential credential)
			{
			}

			internal void _003CNativeLogin_003Eb__1(IAppleError error)
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

			public string authCode;

			public IAppleIDCredential data;

			public string decodedString;

			public string email;

			public _003C_003Ec__DisplayClass4_0 CS_0024_003C_003E8__locals1;

			internal Task _003CNativeLogin_003Eb__2()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_2
		{
			public GetRefreshTokenResponse refreshObj;

			public _003C_003Ec__DisplayClass4_1 CS_0024_003C_003E8__locals2;

			internal void _003CNativeLogin_003Eb__3()
			{
			}
		}

		private AppleAuthManager _appleAuthManager;

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
