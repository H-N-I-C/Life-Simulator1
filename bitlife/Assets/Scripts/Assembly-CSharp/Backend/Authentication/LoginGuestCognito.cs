using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentity.Model;
using Amazon.Runtime;
using Backend.Code.Models;

namespace Backend.Authentication
{
	public class LoginGuestCognito : ILoginMethod
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CLogin_003Eb__13_0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

			private _003C_003Ec__DisplayClass13_0 _003C_003E8__1;

			private TaskAwaiter<bool> _003C_003Eu__1;

			private TaskAwaiter<(bool, string)> _003C_003Eu__2;

			private TaskAwaiter _003C_003Eu__3;

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

		private sealed class _003C_003Ec__DisplayClass13_0
		{
			public string jsonString;

			public LoginGuestCognito _003C_003E4__this;

			internal void _003CLogin_003Eb__5()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetCredentials_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

			private TaskAwaiter<GetCredentialsForIdentityResponse> _003C_003Eu__1;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__2;

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
		private struct _003CLoginGuest_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(bool, string)> _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

			private TaskAwaiter<GetCredentialsForIdentityResponse> _003C_003Eu__1;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__2;

			private TaskAwaiter<(string, bool)> _003C_003Eu__3;

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
		private struct _003CRefreshToken_003Ed__14 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private bool _003CloginSuccess_003E5__2;

			private int _003Ci_003E5__3;

			private TaskAwaiter<bool> _003C_003Eu__2;

			private TaskAwaiter _003C_003Eu__3;

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
		private struct _003CRefreshTokenWithServer_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CTryRegenerateCredential_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginGuestCognito _003C_003E4__this;

			private TaskAwaiter<GetCredentialsForIdentityResponse> _003C_003Eu__1;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__2;

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

		private Action<LoginResponseModel> _callback;

		private string _userId;

		private DateTime _sessionStartTime;

		private AmazonCognitoIdentityClient _cognitoCredentials;

		private bool _isFirstTime;

		private AuthDataModel _data;

		private readonly string _deviceId;

		private ImmutableCredentials _credentials;

		private int _refreshCount;

		private bool _deviceIdReady;

		private bool _isLoginProcess;

		private string _userName;

		public void Login(bool isFirstTime, string userName, AuthDataModel data, Action<LoginResponseModel> callback)
		{
		}

		public Task<ImmutableCredentials> RefreshToken()
		{
			return null;
		}

		private Task<ImmutableCredentials> TryRegenerateCredential()
		{
			return null;
		}

		private bool ShouldRefresh()
		{
			return false;
		}

		private bool HasValidCredentials()
		{
			return false;
		}

		public Task<ImmutableCredentials> GetCredentials()
		{
			return null;
		}

		private Task<(bool, string)> LoginGuest()
		{
			return null;
		}

		public bool HasValidLoginData()
		{
			return false;
		}

		public void GetUserProfile(Action<LoginResponse, string, string> callback)
		{
		}

		public string GetLoginMethodName()
		{
			return null;
		}

		private Task<bool> RefreshTokenWithServer()
		{
			return null;
		}

		private bool IsValidToken(string dataJwtToken, string userId)
		{
			return false;
		}

		private Task _003CLogin_003Eb__13_0()
		{
			return null;
		}

		private void _003CLogin_003Eb__13_1()
		{
		}

		private void _003CLogin_003Eb__13_2()
		{
		}

		private void _003CLogin_003Eb__13_3()
		{
		}

		private void _003CLogin_003Eb__13_4()
		{
		}
	}
}
