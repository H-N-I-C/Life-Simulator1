using System;
using System.Threading.Tasks;
using Amazon.Runtime;
using Backend.Authentication;
using Backend.Code.Models;

namespace Backend
{
	public interface ILoginMethod
	{
		Task<ImmutableCredentials> RefreshToken();

		Task<ImmutableCredentials> GetCredentials();

		void Login(bool getReceipts, string userName, AuthDataModel data, Action<LoginResponseModel> callback);

		bool HasValidLoginData();

		void GetUserProfile(Action<LoginResponse, string, string> callback);

		string GetLoginMethodName();
	}
}
