using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Authentication;
using Backend.Data;
using Backend.Service;
using UnityEngine.Purchasing;

namespace Backend
{
	public class AccountServerController : AccountServerControllerBase
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CStartAccountDeletion_003Eb__72_0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				private _003C_003Ec__DisplayClass72_0 _003C_003E8__1;

				private TaskAwaiter<bool> _003C_003Eu__1;

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

			public static Action _003C_003E9__26_1;

			public static Action _003C_003E9__26_2;

			public static Action<GetConfigResponse> _003C_003E9__26_0;

			public static Action<AuthDataModel> _003C_003E9__32_0;

			public static Func<RemoteSlotModel, DateTime> _003C_003E9__59_0;

			public static Func<SaveSlot, string> _003C_003E9__59_1;

			public static Action<int> _003C_003E9__63_0;

			public static Func<Task> _003C_003E9__72_0;

			internal void _003CInitLive_003Eb__26_0(GetConfigResponse data)
			{
			}

			internal void _003CInitLive_003Eb__26_1()
			{
			}

			internal void _003CInitLive_003Eb__26_2()
			{
			}

			internal void _003CGuestLogin_003Eb__32_0(AuthDataModel credential)
			{
			}

			internal DateTime _003CCheckForRemoteSlots_003Eb__59_0(RemoteSlotModel t)
			{
				return default;
			}

			internal string _003CCheckForRemoteSlots_003Eb__59_1(SaveSlot t)
			{
				return null;
			}

			internal void _003CLiveServiceOnLoginFailed_003Eb__63_0(int index)
			{
			}

			internal Task _003CStartAccountDeletion_003Eb__72_0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass30_0
		{
			public bool mergeCurrentGuest;

			public bool? isMarketingChecked;

			internal void _003CAppleLogin_003Eb__0(AuthDataModel credential)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public bool mergeCurrentGuest;

			public bool? isMarketingChecked;

			internal void _003CGoogleLogin_003Eb__0(AuthDataModel credential)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass52_0
		{
			public SaveSlot theSlot;

			public Action<ResultCode> _003C_003E9__1;

			internal void _003CLoadRemoteLife_003Eb__0(int index)
			{
			}

			internal void _003CLoadRemoteLife_003Eb__1(ResultCode result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass59_0
		{
			public RemoteSlotModel remote;

			internal bool _003CCheckForRemoteSlots_003Eb__2(SaveSlot t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass62_0
		{
			public AuthDataModel authData;

			public ILoginMethod loginMethod;

			internal void _003CLiveServiceOnLoginAtStart_003Eb__0(string isConnected)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass64_0
		{
			public Action callback;

			internal void _003CCanDoGuestLogin_003Eb__0(string deviceId)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass72_0
		{
			public bool result;

			internal void _003CStartAccountDeletion_003Eb__1()
			{
			}
		}

		private static bool _isServerActive;

		private static bool _accountIsDeleted;

		private static Action<GetConfigResponse> OnGameSettingsAvailable__BackingField;

		private static Action OnGameIsReady__BackingField;

		private static Action OnLoginSuccess__BackingField;

		private static bool InvalidGuestAccount__BackingField;

		private static int ForceBreakAccount__BackingField;

		public static Action<GetConfigResponse> OnGameSettingsAvailable
		{
			get
			{
				return OnGameSettingsAvailable__BackingField;
			}
			set
			{
				OnGameSettingsAvailable__BackingField = value;
			}
		}

		public static Action OnGameIsReady
		{
			get
			{
				return OnGameIsReady__BackingField;
			}
			set
			{
				OnGameIsReady__BackingField = value;
			}
		}

		public static Action OnLoginSuccess
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

		public static Action OnUnityUpdateTimer
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public static bool InvalidGuestAccount
		{
			get
			{
				return InvalidGuestAccount__BackingField;
			}
			set
			{
				InvalidGuestAccount__BackingField = value;
			}
		}

		public static int ForceBreakAccount
		{
			get
			{
				return ForceBreakAccount__BackingField;
			}
			set
			{
				ForceBreakAccount__BackingField = value;
			}
		}

		public static bool TryGetUserId(out string userId)
		{
			userId = null;
			return false;
		}

		public static void InitLive()
		{
		}

		private static void LeveServiceOnGameIsReady()
		{
		}

		public static bool IsActive()
		{
			return false;
		}

		public static void AddNewDataButton()
		{
		}

		public static void AppleLogin(bool? isMarketingChecked)
		{
		}

		public static void GoogleLogin(bool? isMarketingChecked)
		{
		}

		public static void GuestLogin()
		{
		}

		public static void SetVisibleUI(bool visible)
		{
		}

		public static void ShowProfileScenario()
		{
		}

		public new static LoginStatus GetLoginState()
		{
			return LoginStatus.LoggedOut;
		}

		public static bool IsAccountDeleted()
		{
			return false;
		}

		public static List<RemoteSlotModel> GetUserDataServiceData()
		{
			return null;
		}

		public static string GetUserGuid()
		{
			return null;
		}

		public static void Logout()
		{
		}

		private static void MergeLogout()
		{
		}

		public static void SaveAllPurchases(Action<List<Product>> callback)
		{
		}

		public static void RestorePurchasesAndConsume(Action<int> callback)
		{
		}

		public static void RestorePurchases(Action<List<TransactionItemModel>> callback)
		{
		}

		public static void SaveReceipt(Product product)
		{
		}

		public static bool SlotIsAvailable(int index)
		{
			return false;
		}

		public static Scenario ShowGoogleLoginCancelButton(bool show)
		{
			return null;
		}

		public static string GetLastProviderName()
		{
			return null;
		}

		public static string GetProviderName(string fallback = "")
		{
			return null;
		}

		public static bool IsRemoteSlot(SaveSlot lLife)
		{
			return false;
		}

		public static bool IsRemoteSlotData(SaveSlot lLife)
		{
			return false;
		}

		public static bool IsRemoteSlotAvailable(SaveSlot lLife)
		{
			return false;
		}

		public static void LoadRemoteLife(SaveSlot theSlot)
		{
		}

		public static void DeleteSaveFile(SaveSlot theSlot)
		{
		}

		public static void SaveCurrentLife(Action<bool> action)
		{
		}

		public static bool CanSaveLife()
		{
			return false;
		}

		public static void EnableUpdatesAfterNewDataWarning()
		{
		}

		public static bool HasNewServerData()
		{
			return false;
		}

		public static void ReloadData(Action<bool> action)
		{
		}

		public static ArrayList CheckForRemoteSlots(ArrayList lives)
		{
			return null;
		}

		private static void LiveServiceOnLoginSuccess(bool isNewAccount)
		{
		}

		public static Dictionary<string, string> GetBackendTrackInformation()
		{
			return null;
		}

		private static void LiveServiceOnLoginAtStart(AuthDataModel authData)
		{
		}

		private static void LiveServiceOnLoginFailed(LoginErrorState state)
		{
		}

		private static void CanDoGuestLogin(Action callback)
		{
		}

		public static bool IsSystemBlockedForUpdate()
		{
			return false;
		}

		public static string GetUserCountry()
		{
			return null;
		}

		public static string GetUserEmail()
		{
			return null;
		}

		public static void MarkSaveCurrentLife()
		{
		}

		public static bool IsGuestAccount()
		{
			return false;
		}

		public static void ForceDisconnect()
		{
		}

		public static void ResetCredentials()
		{
		}

		public static void StartAccountDeletion()
		{
		}

		public static bool ShowDeleteAccountButton()
		{
			return false;
		}

		public static GameSettings GetSettings()
		{
			return null;
		}

		public static Scenario DefaultErrorScenario()
		{
			return null;
		}

		public static bool IsReceiptValidationDisabled()
		{
			return false;
		}

		public static string GetValidSeasonId(string activeSeasonId)
		{
			return null;
		}
	}
}
