using System;
using System.Collections.Generic;
using Backend.Authentication;
using Backend.Service;
using UnityEngine;

namespace Backend
{
	public interface IOnlineSaveMono
	{
		Action OnUpdateRequest { get; set; }

		Action OnDownloadRequest { get; set; }

		Action OnUnityUpdate { get; set; }

		Action OnUnityQuit { get; set; }

		void EnableAws(string gameTypeString);

		void SetUpdateMode(bool active);

		void Enqueue(QueueDataModel dataModel);

		void ResetQueue();

		void RestoreSlot(string slot, string userId, bool unzip, Action<bool, string> result);

		void RestoreData(string userId, bool unzip, Action<ResultCode, string, string> result);

		(bool, List<string>) UnzipData(string destinationFolder, string sourceZipFile, bool deleteAtEnd = true);

		void EnableDownloadLives(bool active);

		void CallDelayedFunction(Action action);

		void InvokeWhenHeroReady(Action action);

		void InvokeWhenServerReady(Action action);

		void InvokeWhenGameReady(Action action);

		GameObject GetGameObject();

		void ShowReloadPopup(Action<bool> callback);

		bool HasOperationInQueue();

		void Setup(GetConfigResponse serverConfigData);
	}
}
