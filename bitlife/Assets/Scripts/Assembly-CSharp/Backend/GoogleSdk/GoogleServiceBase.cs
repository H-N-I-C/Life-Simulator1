using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Backend.GoogleSdk
{
	public class GoogleServiceBase
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRefreshLogin_003Eb__15_0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				private _003C_003Ec__DisplayClass15_0 _003C_003E8__1;

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

			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__12_2;

			public static Action _003C_003E9__12_3;

			public static Action _003C_003E9__15_2;

			public static Action _003C_003E9__15_3;

			public static Func<Task> _003C_003E9__15_0;

			internal void _003CAuthenticateCode_003Eb__12_2()
			{
			}

			internal void _003CAuthenticateCode_003Eb__12_3()
			{
			}

			internal Task _003CRefreshLogin_003Eb__15_0()
			{
				return null;
			}

			internal void _003CRefreshLogin_003Eb__15_2()
			{
			}

			internal void _003CRefreshLogin_003Eb__15_3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CAuthenticateCode_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass12_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass12_1 _003C_003E8__1;

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

			public string code;

			internal Task _003CAuthenticateCode_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_1
		{
			public GoogleAuthResponse jsonObject;

			internal void _003CAuthenticateCode_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass15_0
		{
			public GoogleAuthResponse jsonObject;

			internal void _003CRefreshLogin_003Eb__1()
			{
			}
		}

		public static Action<GoogleAuthResponse, int> GoogleLoginStatus;

		private const string TokenURL = "https://oauth2.googleapis.com/token";

		private static readonly string ClientID;

		private static readonly string RedirectUri;

		private const string AuthURL = "https://accounts.google.com/o/oauth2/v2/auth";

		private const string Scope = "openid profile email";

		private static readonly string AuthURLParams;

		private static GoogleServiceLifeCycle _lifeCycle;

		protected static readonly string AuthURLFull;

		protected static GoogleAuthResponse AuthResponse;

		public static bool IsLoggedIn()
		{
			return false;
		}

		protected static void Setup()
		{
		}

		public static void AuthenticateCode(string code)
		{
		}

		protected static void InternalLogin()
		{
		}

		protected static void InternalLogout()
		{
		}

		protected static void RefreshLogin()
		{
		}

		private static long GetTimestamp()
		{
			return 0L;
		}

		protected static bool InternalShouldRefresh()
		{
			return false;
		}
	}
}
