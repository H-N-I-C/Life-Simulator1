using System;
using System.Collections;
using System.Collections.Generic;
using Backend.Data;

namespace Backend.Service
{
	public class LifeDataService
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<RemoteSlotModel, DateTime> _003C_003E9__17_1;

			internal DateTime _003CCanUpdateSlot_003Eb__17_1(RemoteSlotModel t)
			{
				return default;
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_0
		{
			public string slot;

			public Action<ResultCode> result;

			internal void _003CLoadSlotRemoteData_003Eb__0(bool lifeRestored, string _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public SaveSlot slot;

			internal bool _003CAddSlot_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass17_0
		{
			public string slot;

			internal bool _003CCanUpdateSlot_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public SaveSlot slot;

			internal bool _003CSetSlotUpdated_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass21_0
		{
			public SaveSlot slot;

			internal bool _003CHasSlot_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public SaveSlot slot;

			internal bool _003CDeleteSlot_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_0
		{
			public SaveSlot slot;

			internal bool _003CIsSameSlotAndExists_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass26_0
		{
			public int index;

			internal bool _003CMarkSaveCurrentLife_003Eb__0(RemoteSlotModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public Action<ResultCode> result;

			public LifeDataService _003C_003E4__this;

			public string slot;

			public Action<bool> _003C_003E9__2;

			internal void _003CSaveRemoteSlot_003Eb__0(bool lifeSaved)
			{
			}

			internal void _003CSaveRemoteSlot_003Eb__1(bool r)
			{
			}

			internal void _003CSaveRemoteSlot_003Eb__2(bool lifeSaved)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public int currentSlot;

			public LifeDataService _003C_003E4__this;

			public Action<ResultCode> result;

			internal void _003CLoadFullDataAndUnzip_003Eb__0(ResultCode r, string filePath, string fileGrave)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_1
		{
			public string filePath;

			public string fileGrave;

			public _003C_003Ec__DisplayClass8_0 CS_0024_003C_003E8__locals1;

			public Action<ResultCode, List<string>> _003C_003E9__2;

			internal void _003CLoadFullDataAndUnzip_003Eb__1(bool lifeRestored, string fileSlotPath)
			{
			}

			internal void _003CLoadFullDataAndUnzip_003Eb__2(ResultCode unzipResult, List<string> returnList)
			{
			}
		}

		public List<RemoteSlotModel> Data;

		private readonly ConnectorService _service;

		private int _deltaMinutes;

		private int _deltaSaveLifeMinutes;

		private DateTime _lastUpdate;

		public LifeDataService(ConnectorService service)
		{
		}

		private static Dictionary<string, SaveSlot> GetListOfSavedLives()
		{
			return null;
		}

		public void SaveRemoteSlot(int index, Action<ResultCode> result)
		{
		}

		public void LoadFullDataAndUnzip(int currentSlot, Action<ResultCode> result)
		{
		}

		private void SyncSlotsAfterDownload(ArrayList oldSlots, int currentSlot)
		{
		}

		private void EnabledDownloadLives(bool active)
		{
		}

		public void UnzipList(List<string> files, Action<ResultCode, List<string>> result)
		{
		}

		public void LoadSlotRemoteData(string slot, Action<ResultCode> result)
		{
		}

		public void UpdateSlotsData(Action<bool> result)
		{
		}

		public void RemoveLifeSlot(SaveSlot slot)
		{
		}

		public void SetDeltaInterval(ConfigDataModel data)
		{
		}

		private void AddSlot(SaveSlot slot, string lastPlayed)
		{
		}

		public bool CanUpdateSlot(SaveSlot saveSlot)
		{
			return false;
		}

		public void SetSlotUpdated(SaveSlot slot)
		{
		}

		public void SetSlotsData(PlayerData data)
		{
		}

		public void AddSlot(int index, string lastPlayed)
		{
		}

		public bool HasSlot(SaveSlot slot)
		{
			return false;
		}

		public void DeleteSlot(SaveSlot slot)
		{
		}

		public bool CanSaveLife()
		{
			return false;
		}

		public void ResetTimer()
		{
		}

		public bool IsSameSlotAndExists(SaveSlot slot)
		{
			return false;
		}

		public void MarkSaveCurrentLife()
		{
		}
	}
}
