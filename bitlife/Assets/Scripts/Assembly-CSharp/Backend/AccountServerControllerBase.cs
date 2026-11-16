using System;
using Backend.Authentication;
using Backend.Data;
using Backend.Service;

namespace Backend
{
	public class AccountServerControllerBase
	{
		private sealed class _003C_003Ec__DisplayClass14_0
		{
			public int index;

			internal bool _003CGetDataSlotForIndex_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		protected static IAuthManager _authManager;

		protected static bool _showUI;

		protected static ConnectorService _liveService;

		protected static GetConfigResponse _serverConfigData;

		protected static SettingsController _settings;

		protected static bool _systemWideDeactivated;

		private static Action OnUnityUpdateTimerBase__BackingField;

		protected static Action OnUnityUpdateTimerBase
		{
			get
			{
				return OnUnityUpdateTimerBase__BackingField;
			}
			set
			{
				OnUnityUpdateTimerBase__BackingField = value;
			}
		}

		protected static void RemoveWarningButtons()
		{
		}

		protected static void AddWarningButton()
		{
		}

		protected static void UnityUpdateCallback()
		{
		}

		protected static void UnityQuiteCallback()
		{
		}

		protected static RemoteSlotModel GetDataSlotForIndex(int index)
		{
			return null;
		}

		public static LoginStatus GetLoginState()
		{
			return LoginStatus.LoggedOut;
		}
	}
}
