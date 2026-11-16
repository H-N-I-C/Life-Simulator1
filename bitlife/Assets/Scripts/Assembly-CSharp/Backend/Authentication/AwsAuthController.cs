using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Aws;
using Backend.Code.Models;
using Backend.Data;
using Backend.Service;

namespace Backend.Authentication
{
	public class AwsAuthController : IAuthentication
	{
		private sealed class _003C_003Ec__DisplayClass35_0
		{
			public AwsAuthController _003C_003E4__this;

			public AuthDataModel dataModel;

			public ILoginMethod loginMethod;

			internal void _003CLogin_003Eb__0(LoginResponseModel loginDataResponse)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass36_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRecoverAndUpdateEmail_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass36_0 _003C_003E4__this;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

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

			public AwsAuthController _003C_003E4__this;

			public AuthDataModel dataModel;

			internal Task _003CRecoverAndUpdateEmail_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass44_0
		{
			public Action<bool> callback;

			internal void _003CUpdateSlotsData_003Eb__0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass46_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CClientSavePlayerData_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass46_0 _003C_003E4__this;

				private bool _003CsuccessResponse_003E5__2;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

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

			public PostSlotsRequest requestData;

			public AwsAuthController _003C_003E4__this;

			public Action<bool> callback;

			public Action _003C_003E9__1;

			public Action _003C_003E9__2;

			internal Task _003CClientSavePlayerData_003Eb__0()
			{
				return null;
			}

			internal void _003CClientSavePlayerData_003Eb__1()
			{
			}

			internal void _003CClientSavePlayerData_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass47_0
		{
			public AwsAuthController _003C_003E4__this;

			public Action<GetResponseData> callback;

			internal void _003CGetUserProfileData_003Eb__0(LoginResponse loginResponse, string _, string jsonSlots)
			{
			}
		}

		private static Action OnSessionExpired__BackingField;

		private Action<GetResponseData> OnLoginSuccess__BackingField;

		private Action OnLoginFailed__BackingField;

		private Action OnLoginExpired__BackingField;

		private Action OnLogout__BackingField;

		private Action OnLoginAtStart__BackingField;

		private bool _initialized;

		private string _awsPlayerId;

		private string _sessionTicket;

		private ConfigDataModel _aws;

		private bool _inLoginTransit;

		private bool _isLoggedIn;

		private string _userGuid;

		private bool _isBanned;

		public static Action OnSessionExpired
		{
			get
			{
				return OnSessionExpired__BackingField;
			}
			set
			{
				OnSessionExpired__BackingField = value;
			}
		}

		public Action<GetResponseData> OnLoginSuccess
		{
			get
			{
				return OnLoginSuccess__BackingField;
			}
			set
			{
				OnLoginSuccess__BackingField = value;
			}
		}

		public Action OnLoginFailed
		{
			get
			{
				return OnLoginFailed__BackingField;
			}
			set
			{
				OnLoginFailed__BackingField = value;
			}
		}

		public Action OnLoginExpired
		{
			get
			{
				return OnLoginExpired__BackingField;
			}
			set
			{
				OnLoginExpired__BackingField = value;
			}
		}

		public Action OnLogout
		{
			get
			{
				return OnLogout__BackingField;
			}
			set
			{
				OnLogout__BackingField = value;
			}
		}

		public Action OnLoginAtStart
		{
			get
			{
				return OnLoginAtStart__BackingField;
			}
			set
			{
				OnLoginAtStart__BackingField = value;
			}
		}

		public string GetUserId()
		{
			return null;
		}

		public string GetUserGuid()
		{
			return null;
		}

		public void Init()
		{
		}

		public void Login(bool isFirstTime, AuthDataModel dataModel, ILoginMethod loginMethod)
		{
		}

		private void RecoverAndUpdateEmail(AuthDataModel dataModel, GetResponseData response)
		{
		}

		public void Logout()
		{
		}

		public void FailedLogin()
		{
		}

		public void ResetCredentials()
		{
		}

		private void DeleteMonetizationData()
		{
		}

		public LoginStatus GetLoginState()
		{
			return LoginStatus.LoggedOut;
		}

		public void SetLoginAtStart(bool active)
		{
		}

		public bool IsBanned()
		{
			return false;
		}

		public void UpdateSlotsData(ILoginMethod loginMethod, PlayerData data, Action<bool> callback)
		{
		}

		public ConfigDataModel GetAwsConfig()
		{
			return null;
		}

		private void ClientSavePlayerData(ILoginMethod loginMethod, PlayerData data, Action<bool> callback)
		{
		}

		public void GetUserProfileData(ILoginMethod loginMethod, Action<GetResponseData> callback)
		{
		}

		private void _003CInit_003Eb__34_0()
		{
		}
	}
}
