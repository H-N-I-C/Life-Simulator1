using System;
using System.Collections.Generic;
using Backend.Authentication;
using Backend.Data;

namespace Backend.Service
{
	public class ConnectorServiceBase
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SaveSlot, DateTime> _003C_003E9__49_0;

			internal DateTime _003CGetNextLifeToSave_003Eb__49_0(SaveSlot t)
			{
				return default;
			}
		}

		private sealed class _003C_003Ec__DisplayClass49_0
		{
			public SaveSlot slot;

			public ConnectorServiceBase _003C_003E4__this;

			internal void _003CGetNextLifeToSave_003Eb__1(ResultCode result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass51_0
		{
			public SaveSlot slot;

			internal bool _003CUpdateLocalSlot_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private LifeDataService LifeDataService__BackingField;

		private IReceiptService ReceiptService__BackingField;

		private Action<bool> OnLoginSuccess__BackingField;

		private Action<LoginErrorState> OnLoginFailed__BackingField;

		private Action<AuthDataModel> OnLoginAtStart__BackingField;

		private Action OnUnityUpdateCallback__BackingField;

		private Action OnUnityQuit__BackingField;

		private Action OnGameIsReady__BackingField;

		protected IOnlineSaveMono _mono;

		protected AuthDataModel _authDataModel;

		protected IAuthentication _authentication;

		protected bool _activeSaving;

		protected bool _activeSavingReceipts;

		protected ILoginMethod _method;

		protected GetConfigResponse _serverConfigData;

		protected int _retriesReconnect;

		private static Action<List<TransactionItemModel>> m_OnConsumeRestoreItems;

		public LifeDataService LifeDataService
		{
			get
			{
				return LifeDataService__BackingField;
			}
			set
			{
				LifeDataService__BackingField = value;
			}
		}

		public IReceiptService ReceiptService
		{
			get
			{
				return ReceiptService__BackingField;
			}
			set
			{
				ReceiptService__BackingField = value;
			}
		}

		public Action<bool> OnLoginSuccess
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

		public Action<LoginErrorState> OnLoginFailed
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

		public Action<AuthDataModel> OnLoginAtStart
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

		public Action OnUnityUpdateCallback
		{
			get
			{
				return OnUnityUpdateCallback__BackingField;
			}
			set
			{
				OnUnityUpdateCallback__BackingField = value;
			}
		}

		public Action OnUnityQuit
		{
			get
			{
				return OnUnityQuit__BackingField;
			}
			set
			{
				OnUnityQuit__BackingField = value;
			}
		}

		public Action OnGameIsReady
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

		public static event Action<List<TransactionItemModel>> OnConsumeRestoreItems
		{
			add
			{
			}
			remove
			{
			}
		}

		~ConnectorServiceBase()
		{
		}

		protected void SetSaveReceipts()
		{
		}

		protected void RemoveSaveReceiptKey()
		{
		}

		protected void BlockSaveReceiptKey()
		{
		}

		protected bool ShouldSaveReceipt()
		{
			return false;
		}

		protected void FailedLogin()
		{
		}

		protected void UnityUpdateCallback()
		{
		}

		protected void UnityQuitCallback()
		{
		}

		protected void EnableUpdatesWithLoginSuccess(bool isNewAccount)
		{
		}

		protected void GetNextLifeToSave()
		{
		}

		public LoginStatus GetLoginState()
		{
			return LoginStatus.LoggedOut;
		}

		protected void UpdateLocalSlot(PlayerData data)
		{
		}

		protected void ConsumeRestoreItems(List<TransactionItemModel> result)
		{
		}

		private void GivePlatformConsumablePurchaseEventsById(string id, string createdAt)
		{
		}

		public bool IsGuestAccount()
		{
			return false;
		}

		public void ResetLoginState()
		{
		}

		private void _003CEnableUpdatesWithLoginSuccess_003Eb__48_0()
		{
		}

		private void _003CGetNextLifeToSave_003Eb__49_2(bool _)
		{
		}
	}
}
