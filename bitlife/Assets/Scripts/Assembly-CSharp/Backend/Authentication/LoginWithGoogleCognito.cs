using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Amazon.CognitoIdentity;
using Amazon.Runtime;
using Backend.Code.Models;
using Backend.GoogleSdk;

namespace Backend.Authentication
{
	public class LoginWithGoogleCognito : ILoginMethod
	{
		[StructLayout((LayoutKind)3)]
		private struct _003C_003CLogin_003Eb__16_1_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

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

		[StructLayout((LayoutKind)3)]
		private struct _003C_003CLogin_003Eb__16_4_003Ed : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

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

		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public LoginWithGoogleCognito _003C_003E4__this;

			public bool waitResponse;

			public Action _003C_003E9__1;

			internal void _003CRefreshToken_003Eb__0(GoogleAuthResponse response, int _)
			{
			}

			internal void _003CRefreshToken_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public string jsonString;

			public LoginWithGoogleCognito _003C_003E4__this;

			internal void _003CLoginWithGoogle_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetUserProfile_003Eb__1_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass22_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass22_2 _003C_003E8__1;

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

			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetUserProfile_003Eb__4_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass22_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass22_1 _003C_003E8__1;

				private bool _003CsuccessData_003E5__2;

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

			public Action<LoginResponse, string, string> callback;

			public LoginWithGoogleCognito _003C_003E4__this;

			public Action _003C_003E9__2;

			public Action _003C_003E9__3;

			public Action _003C_003E9__5;

			public Func<Task> _003C_003E9__4;

			public Action _003C_003E9__8;

			internal void _003CGetUserProfile_003Eb__0(GoogleAuthResponse response, int code)
			{
			}

			internal void _003CGetUserProfile_003Eb__2()
			{
			}

			internal void _003CGetUserProfile_003Eb__3()
			{
			}

			internal Task _003CGetUserProfile_003Eb__4()
			{
				return null;
			}

			internal void _003CGetUserProfile_003Eb__5()
			{
			}

			internal void _003CGetUserProfile_003Eb__7()
			{
			}

			internal Task _003CGetUserProfile_003Eb__1()
			{
				return null;
			}

			internal void _003CGetUserProfile_003Eb__8()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_1
		{
			public string jsonString;

			public _003C_003Ec__DisplayClass22_0 CS_0024_003C_003E8__locals1;

			internal void _003CGetUserProfile_003Eb__6()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_2
		{
			public string jsonString;

			public _003C_003Ec__DisplayClass22_0 CS_0024_003C_003E8__locals2;

			internal void _003CGetUserProfile_003Eb__9()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CGetCredentials_003Ed__18 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

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
		private struct _003CLoginWithGoogle_003Ed__19 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

			private _003C_003Ec__DisplayClass19_0 _003C_003E8__1;

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
		private struct _003CRefreshGoogleAwsLogin_003Ed__20 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

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
		private struct _003CRefreshToken_003Ed__17 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<ImmutableCredentials> _003C_003Et__builder;

			public LoginWithGoogleCognito _003C_003E4__this;

			private _003C_003Ec__DisplayClass17_0 _003C_003E8__1;

			private ImmutableCredentials _003Ccredentials_003E5__2;

			private TaskAwaiter _003C_003Eu__1;

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

		private string _googleJwtToken;

		private CognitoAWSCredentials _cognitoCredentials;

		private bool _isFirstTime;

		private ImmutableCredentials _tempCredentials;

		private readonly string _identityPool;

		private AuthDataModel _data;

		private readonly bool? _isMarketingChecked;

		private readonly bool _mergeCurrentGuest;

		private readonly string _guestUserGuid;

		private readonly string _guestUserId;

		private string _userName;

		public LoginWithGoogleCognito(bool? isMarketingChecked)
		{
		}

		public LoginWithGoogleCognito(bool? isMarketingChecked, bool mergeCurrentGuest, string guestUserGuid, string guestUserId)
		{
		}

		public void Login(bool isFirstTime, string userName, AuthDataModel data, Action<LoginResponseModel> callback)
		{
		}

		public Task<ImmutableCredentials> RefreshToken()
		{
			return null;
		}

		public Task<ImmutableCredentials> GetCredentials()
		{
			return null;
		}

		private Task LoginWithGoogle()
		{
			return null;
		}

		private Task RefreshGoogleAwsLogin()
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

		private void _003CLogin_003Eb__16_0(GoogleAuthResponse response, int code)
		{
		}

		private void _003CLogin_003Eb__16_2()
		{
		}

		private void _003CLogin_003Eb__16_3()
		{
		}

		private Task _003CLogin_003Eb__16_4()
		{
			return null;
		}

		private void _003CLogin_003Eb__16_5()
		{
		}

		private Task _003CLogin_003Eb__16_1()
		{
			return null;
		}

		private void _003CLoginWithGoogle_003Eb__19_0()
		{
		}

		private void _003CLoginWithGoogle_003Eb__19_2()
		{
		}

		private void _003CLoginWithGoogle_003Eb__19_3()
		{
		}

		private void _003CRefreshGoogleAwsLogin_003Eb__20_0()
		{
		}
	}
}
