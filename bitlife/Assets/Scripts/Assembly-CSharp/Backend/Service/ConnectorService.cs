using System;
using System.Collections;
using System.Collections.Generic;
using Backend.Authentication;
using Backend.Data;
using UnityEngine.Purchasing;

namespace Backend.Service
{
	public class ConnectorService : ConnectorServiceBase
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public ConnectorService _003C_003E4__this;

			public string gameType;

			public Action _003C_003E9__7;

			public Action _003C_003E9__9;

			public Action _003C_003E9__10;

			public Action _003C_003E9__11;

			internal void _003C_002Ector_003Eb__0(GetResponseData data)
			{
			}

			internal void _003C_002Ector_003Eb__7()
			{
			}

			internal void _003C_002Ector_003Eb__1()
			{
			}

			internal void _003C_002Ector_003Eb__9()
			{
			}

			internal void _003C_002Ector_003Eb__2()
			{
			}

			internal void _003C_002Ector_003Eb__3()
			{
			}

			internal void _003C_002Ector_003Eb__10()
			{
			}

			internal void _003C_002Ector_003Eb__11()
			{
			}

			internal void _003C_002Ector_003Eb__4()
			{
			}

			internal void _003C_002Ector_003Eb__5()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass0_1
		{
			public GetResponseData data;

			public bool isNewAccount;

			public _003C_003Ec__DisplayClass0_0 CS_0024_003C_003E8__locals1;

			public Action<ResultCode> _003C_003E9__8;

			internal void _003C_002Ector_003Eb__6(int index)
			{
			}

			internal void _003C_002Ector_003Eb__8(ResultCode r)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass14_0
		{
			public Action<List<Product>> action;

			internal void _003CSaveAllPurchases_003Eb__0(List<Product> saveResult)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass15_0
		{
			public Action<List<TransactionItemModel>> action;

			internal void _003CRestorePurchases_003Eb__0(List<TransactionItemModel> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public SaveSlot slot;

			internal bool _003CHasNewServerData_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public SaveSlot slot;

			internal bool _003CCheckForNewDataUpdate_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public SaveSlot slot;

			internal bool _003CRemoveAllFoldersExceptCurrent_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			public Action<bool> reloaded;

			public ConnectorService _003C_003E4__this;

			internal void _003CReloadData_003Eb__0(GetResponseData data)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_1
		{
			public GetResponseData data;

			public _003C_003Ec__DisplayClass28_0 CS_0024_003C_003E8__locals1;

			internal void _003CReloadData_003Eb__1(ResultCode r)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass29_0
		{
			public ConnectorService _003C_003E4__this;

			public SaveSlot slot;

			internal void _003CDeleteSaveFile_003Eb__0(bool result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass31_0
		{
			public SaveSlot slot;

			public ConnectorService _003C_003E4__this;

			public Action<bool> callback;

			public Action<bool> _003C_003E9__1;

			internal void _003CSaveCurrentLife_003Eb__0(ResultCode result)
			{
			}

			internal void _003CSaveCurrentLife_003Eb__1(bool _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass32_0
		{
			public ConnectorService _003C_003E4__this;

			public Action<int> callback;

			internal void _003CRestorePurchasesAndConsume_003Eb__0(List<TransactionItemModel> result)
			{
			}
		}

		private sealed class _003CResetReceiptSendProcess_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public ConnectorService _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CResetReceiptSendProcess_003Ed__26(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		public ConnectorService(IAuthentication authentication, string gameType)
		{
		}

		public void GetNextLifeToDownload()
		{
		}

		public void Login(AuthDataModel dataModel, ILoginMethod method, bool setRetry = false)
		{
		}

		public string GetUserId()
		{
			return null;
		}

		public string GetUserGuid()
		{
			return null;
		}

		public void Enqueue(QueueDataModel dataModel)
		{
		}

		public void RestoreSlot(string slot, bool unzip, Action<bool, string> result)
		{
		}

		public void RestoreData(bool unzip, Action<ResultCode, string, string> result)
		{
		}

		public void SetUpdateMode(bool active)
		{
		}

		public void Logout()
		{
		}

		public void UpdateSlotsData(Action<bool> result)
		{
		}

		public (bool, List<string>) UnzipData(string destinationFolder, string sourceZipFile, bool deleteAtEnd = true)
		{
			return default;
		}

		public void EnableDownloadLives(bool active)
		{
		}

		public bool SlotIsAvailable(int lLifeIndexZi)
		{
			return false;
		}

		public void SaveAllPurchases(Action<List<Product>> action)
		{
		}

		public void RestorePurchases(Action<List<TransactionItemModel>> action)
		{
		}

		public void SaveReceipt(Product aProduct)
		{
		}

		public void Init(GetConfigResponse serverConfigData)
		{
		}

		public void EnableUpdatesAfterNewDataWarning()
		{
		}

		public bool HasNewServerData()
		{
			return false;
		}

		private void CheckForNewDataUpdate(PlayerData data, Action callback)
		{
		}

		private void RemoveAllFoldersExceptCurrent(int currentSlot, PlayerData data)
		{
		}

		private void SetFirstTimeLogin()
		{
		}

		public bool IsFirstTimeLogin()
		{
			return false;
		}

		private void RemoveFirstTimeLogin()
		{
		}

		private void ProcessReceipts()
		{
		}

		private IEnumerator ResetReceiptSendProcess()
		{
			return null;
		}

		public void GetUserProfileData(Action<GetResponseData> callback)
		{
		}

		public void ReloadData(Action<bool> reloaded)
		{
		}

		public void DeleteSaveFile(SaveSlot slot)
		{
		}

		public bool CanSaveLife()
		{
			return false;
		}

		public void SaveCurrentLife(Action<bool> callback)
		{
		}

		public void RestorePurchasesAndConsume(Action<int> callback)
		{
		}

		public void SetLoginAtStart(bool b)
		{
		}

		public void MergeLogout()
		{
		}

		public void ResetCredentials()
		{
		}

		public bool IsPlayerBanned()
		{
			return false;
		}

		private void _003CProcessReceipts_003Eb__25_0(List<Product> resultReceipts)
		{
		}
	}
}
