using System;
using Backend.GoogleSdk;

namespace Backend.Authentication
{
	public class GoogleAuthService : IAuthManager
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public Action<string> result;

			internal void _003CGetCredentialState_003Eb__0(GoogleAuthResponse state, int _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public Action<AuthDataModel> response;

			internal void _003CNativeLogin_003Eb__0(GoogleAuthResponse awsCredentials, int _)
			{
			}
		}

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
