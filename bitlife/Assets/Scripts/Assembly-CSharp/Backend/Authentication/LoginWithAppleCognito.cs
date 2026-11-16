using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Backend.Code.Models;

namespace Backend.Authentication
{
	public class LoginWithAppleCognito : ILoginMethod
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CLogin_003Eb__18_0_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithAppleCognito _003C_003E4__this;

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

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public Exception e;

			public LoginWithAppleCognito _003C_003E4__this;

			internal void _003CLogin_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public Exception e;

			public LoginWithAppleCognito _003C_003E4__this;

			internal void _003CRefreshToken_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public string jsonString;

			public LoginWithAppleCognito _003C_003E4__this;

			internal void _003CLoginWithApple_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_1
		{
			public Exception e;

			public LoginWithAppleCognito _003C_003E4__this;

			internal void _003CLoginWithApple_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			public Exception e;

			public LoginWithAppleCognito _003C_003E4__this;

			internal void _003CRefreshApple_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetUserProfile_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass25_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass25_1 _003C_003E8__1;

				private TaskAwaiter<(string, bool)> _003C_003Eu__1;

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

			public LoginWithAppleCognito _003C_003E4__this;

			public Action<LoginResponse, string, string> callback;

			public Action _003C_003E9__1;

			internal Task _003CGetUserProfile_003Eb__0()
			{
				return null;
			}

			internal void _003CGetUserProfile_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_1
		{
			public string jsonString;

			public _003C_003Ec__DisplayClass25_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetUserProfile_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_2
		{
			public Exception e;

			public _003C_003Ec__DisplayClass25_0 CS_0024_003C_003E8__locals2;

			internal void _003CGetUserProfile_003Eb__3()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetCredentials_003Ed__21 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginWithAppleCognito _003C_003E4__this;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

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
		private struct _003CLoginWithApple_003Ed__22 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithAppleCognito _003C_003E4__this;

			private _003C_003Ec__DisplayClass22_0 _003C_003E8__1;

			private ImmutableCredentials _003CawsCredentials_003E5__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<string> _003C_003Eu__2;

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
		private struct _003CRefreshApple_003Ed__23 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<bool> _003C_003Et__builder;

			public GetRefreshTokenResponse data;

			public LoginWithAppleCognito _003C_003E4__this;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__1;

			private TaskAwaiter<string> _003C_003Eu__2;

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
		private struct _003CRefreshToken_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginWithAppleCognito _003C_003E4__this;

			private GetRefreshTokenResponse _003CrefreshObj_003E5__2;

			private TaskAwaiter<string> _003C_003Eu__1;

			private TaskAwaiter<bool> _003C_003Eu__2;

			private TaskAwaiter<ImmutableCredentials> _003C_003Eu__3;

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

		private ImmutableCredentials _credentials;

		private string _userId;

		private DateTime _sessionStartTime;

		public static string appleJwtToken;

		private CognitoAWSCredentials _cognitoCredentials;

		private readonly string _identityPool;

		private string _refreshToken;

		private bool _isFirstTime;

		private AuthDataModel _data;

		private readonly bool? _isMarketingChecked;

		private readonly bool _mergeCurrentGuest;

		private readonly string _guestUserGuid;

		private readonly string _guestUserId;

		private DateTime? _dateLastLogin;

		private string _userName;

		public LoginWithAppleCognito(bool? isMarketingChecked)
		{
		}

		public LoginWithAppleCognito(bool? isMarketingChecked, bool mergeCurrentGuest, string guestUserGuid, string guestUserId)
		{
		}

		public void Login(bool isFirstTime, string userName, AuthDataModel data, Action<LoginResponseModel> callback)
		{
		}

		public Task<ImmutableCredentials> RefreshToken()
		{
			return null;
		}

		private bool ShouldRefresh()
		{
			return false;
		}

		public Task<ImmutableCredentials> GetCredentials()
		{
			return null;
		}

		private Task LoginWithApple()
		{
			return null;
		}

		private Task<bool> RefreshApple(GetRefreshTokenResponse data)
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

		private Task _003CLogin_003Eb__18_0()
		{
			return null;
		}

		private void _003CRefreshToken_003Eb__19_0()
		{
		}

		private void _003CRefreshToken_003Eb__19_1()
		{
		}

		private void _003CLoginWithApple_003Eb__22_0()
		{
		}

		private void _003CLoginWithApple_003Eb__22_2()
		{
		}

		private void _003CLoginWithApple_003Eb__22_3()
		{
		}

		private void _003CRefreshApple_003Eb__23_0()
		{
		}

		private void _003CRefreshApple_003Eb__23_1()
		{
		}
	}
}
