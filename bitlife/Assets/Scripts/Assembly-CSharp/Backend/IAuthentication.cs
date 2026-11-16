using System;
using Backend.Authentication;
using Backend.Data;
using Backend.Service;

namespace Backend
{
	public interface IAuthentication
	{
		Action<GetResponseData> OnLoginSuccess { get; set; }

		Action OnLoginFailed { get; set; }

		Action OnLoginExpired { get; set; }

		Action OnLogout { get; set; }

		Action OnLoginAtStart { get; set; }

		string GetUserId();

		string GetUserGuid();

		void Init();

		void Login(bool isFirstTime, AuthDataModel token, ILoginMethod loginMethod);

		void Logout();

		ConfigDataModel GetAwsConfig();

		LoginStatus GetLoginState();

		void UpdateSlotsData(ILoginMethod loginMethod, PlayerData data, Action<bool> callback);

		void FailedLogin();

		void ResetCredentials();

		void GetUserProfileData(ILoginMethod loginMethod, Action<GetResponseData> callback);

		void SetLoginAtStart(bool b);

		bool IsBanned();
	}
}
