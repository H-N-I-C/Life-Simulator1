using System;
using Backend.Authentication;

namespace Backend
{
	public interface IAuthManager
	{
		void GetCredentialState(string userId, Action<string> result);

		void UpdateAuth();

		void Init();

		void NativeLogin(Action<AuthDataModel> response);
	}
}
