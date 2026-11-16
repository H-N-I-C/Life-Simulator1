using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Authentication;
using Backend.Service;
using UnityEngine;

namespace Backend
{
	public class OnlineSaveMono : MonoBehaviour, IOnlineSaveMono
	{
		private sealed class _003C_003Ec__DisplayClass43_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CUploadFile_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass43_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass43_1 _003C_003E8__1;

				private string _003CzipFileName_003E5__2;

				private string _003CpathZipName_003E5__3;

				private HttpClient _003ChttpClient_003E5__4;

				private FileStream _003CfileStream_003E5__5;

				private object _003C_003E7__wrap5;

				private int _003C_003E7__wrap6;

				private ValueTaskAwaiter _003C_003Eu__1;

				private TaskAwaiter<HttpResponseMessage> _003C_003Eu__2;

				private TaskAwaiter _003C_003Eu__3;

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

			public OnlineSaveMono _003C_003E4__this;

			public QueueDataModel dataModel;

			public Action _003C_003E9__1;

			public Action _003C_003E9__2;

			public Action _003C_003E9__4;

			internal Task _003CUploadFile_003Eb__0()
			{
				return null;
			}

			internal void _003CUploadFile_003Eb__1()
			{
			}

			internal void _003CUploadFile_003Eb__2()
			{
			}

			internal void _003CUploadFile_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass43_1
		{
			public bool sendResult;

			public _003C_003Ec__DisplayClass43_0 CS_0024_003C_003E8__locals1;

			internal void _003CUploadFile_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass44_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRestoreSlot_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass44_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass44_1 _003C_003E8__1;

				private string _003CdestinationFolder_003E5__2;

				private TaskAwaiter<(bool, bool)> _003C_003Eu__1;

				private TaskAwaiter _003C_003Eu__2;

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

			public string userId;

			public OnlineSaveMono _003C_003E4__this;

			public string slot;

			public bool unzip;

			public Action<bool, string> result;

			public Action _003C_003E9__1;

			public Action _003C_003E9__3;

			internal Task _003CRestoreSlot_003Eb__0()
			{
				return null;
			}

			internal void _003CRestoreSlot_003Eb__1()
			{
			}

			internal void _003CRestoreSlot_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass44_1
		{
			public string destinationPath;

			public _003C_003Ec__DisplayClass44_0 CS_0024_003C_003E8__locals1;

			internal void _003CRestoreSlot_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass45_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRestoreData_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass45_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass45_1 _003C_003E8__1;

				private string _003CdestinationFolder_003E5__2;

				private string _003CgraveName_003E5__3;

				private bool _003CdownloadSuccess_003E5__4;

				private TaskAwaiter<(bool, bool)> _003C_003Eu__1;

				private TaskAwaiter _003C_003Eu__2;

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

			public string userId;

			public OnlineSaveMono _003C_003E4__this;

			public bool unzip;

			public Action<ResultCode, string, string> result;

			public Action _003C_003E9__3;

			internal Task _003CRestoreData_003Eb__0()
			{
				return null;
			}

			internal void _003CRestoreData_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass45_1
		{
			public bool requestDenied;

			public string destinationPath;

			public string destinationPathGrave;

			public _003C_003Ec__DisplayClass45_0 CS_0024_003C_003E8__locals1;

			internal void _003CRestoreData_003Eb__1()
			{
			}

			internal void _003CRestoreData_003Eb__2()
			{
			}
		}

		private sealed class _003CCheckIsGameIsReady_003Ed__55 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Func<bool> condition;

			public Action action;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCheckIsGameIsReady_003Ed__55(int _003C_003E1__state)
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

		private sealed class _003CDelayedFunctionCo_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Action action;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDelayedFunctionCo_003Ed__50(int _003C_003E1__state)
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

		[StructLayout((LayoutKind)3)]
		private struct _003CDownloadFile_003Ed__47 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<(bool, bool)> _003C_003Et__builder;

			public string slotName;

			public string destinationPath;

			private HttpClient _003ChttpClient_003E5__2;

			private HttpResponseMessage _003Cresponse_003E5__3;

			private TaskAwaiter<HttpResponseMessage> _003C_003Eu__1;

			private FileStream _003CfileStream_003E5__4;

			private TaskAwaiter<string> _003C_003Eu__2;

			private object _003C_003E7__wrap4;

			private int _003C_003E7__wrap5;

			private (bool, bool) _003C_003E7__wrap6;

			private TaskAwaiter _003C_003Eu__3;

			private ValueTaskAwaiter _003C_003Eu__4;

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

		private sealed class _003CExecuteActionWithDelay_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float time;

			public Action action;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CExecuteActionWithDelay_003Ed__63(int _003C_003E1__state)
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

		private static OnlineSaveMono _instance;

		private Action OnUpdateRequest__BackingField;

		private Action OnDownloadRequest__BackingField;

		private Action OnUnityUpdate__BackingField;

		private Action OnUnityQuit__BackingField;

		private string _gameType;

		private ConcurrentQueue<QueueDataModel> _list;

		private readonly ConcurrentQueue<Action> _unityActions;

		private bool _activeLoading;

		private bool _isAutoUpdate;

		private bool _isAutoDownload;

		private string _dataPath;

		private GameObject _googleLogin;

		private GameObject _reloadWarning;

		private bool _activeSync;

		private Action<bool> _callbackReloadLogin;

		private bool _uploadGraves;

		public Action OnUpdateRequest
		{
			get
			{
				return OnUpdateRequest__BackingField;
			}
			set
			{
				OnUpdateRequest__BackingField = value;
			}
		}

		public Action OnDownloadRequest
		{
			get
			{
				return OnDownloadRequest__BackingField;
			}
			set
			{
				OnDownloadRequest__BackingField = value;
			}
		}

		public Action OnUnityUpdate
		{
			get
			{
				return OnUnityUpdate__BackingField;
			}
			set
			{
				OnUnityUpdate__BackingField = value;
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

		public void EnableAws(string gameTypeString)
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}

		private void OnApplicationFocus(bool pauseStatus)
		{
		}

		private void OnApplicationQuit()
		{
		}

		private void OnDestroy()
		{
		}

		public void SetUpdateMode(bool active)
		{
		}

		private void FileRequests()
		{
		}

		private void CheckQueue()
		{
		}

		private void CheckForAutoDownload()
		{
		}

		private void CheckForAutoUpdate()
		{
		}

		public void Enqueue(QueueDataModel dataModel)
		{
		}

		public void ResetQueue()
		{
		}

		private void UploadFile(QueueDataModel dataModel)
		{
		}

		public void RestoreSlot(string slot, string userId, bool unzip, Action<bool, string> result)
		{
		}

		public void RestoreData(string userId, bool unzip, Action<ResultCode, string, string> result)
		{
		}

		public (bool, List<string>) UnzipData(string destinationFolder, string sourceZipFile, bool deleteAtEnd = true)
		{
			return default;
		}

		private Task<(bool, bool)> DownloadFile(string slotName, string destinationPath)
		{
			return null;
		}

		public void EnableDownloadLives(bool active)
		{
		}

		public void CallDelayedFunction(Action action)
		{
		}

		private IEnumerator DelayedFunctionCo(Action action)
		{
			return null;
		}

		public void InvokeWhenHeroReady(Action action)
		{
		}

		public void InvokeWhenServerReady(Action action)
		{
		}

		public void InvokeWhenGameReady(Action action)
		{
		}

		public GameObject GetGameObject()
		{
			return null;
		}

		private IEnumerator CheckIsGameIsReady(Func<bool> condition, Action action)
		{
			return null;
		}

		public static void AddUnityAction(Action newAction)
		{
		}

		public void OnClickReload(bool doReload)
		{
		}

		public void ShowReloadPopup(Action<bool> callback)
		{
		}

		public bool HasOperationInQueue()
		{
			return false;
		}

		public void Setup(GetConfigResponse serverConfigData)
		{
		}

		public static void ExecuteDelayed(Action action)
		{
		}

		public static void ExecuteDelayed(float time, Action action)
		{
		}

		private IEnumerator ExecuteActionWithDelay(float time, Action action)
		{
			return null;
		}

		internal static bool _003CInvokeWhenHeroReady_003Eg__Condition_007C51_0()
		{
			return false;
		}

		internal static bool _003CInvokeWhenServerReady_003Eg__Condition_007C52_0()
		{
			return false;
		}

		internal static bool _003CInvokeWhenGameReady_003Eg__Condition_007C53_0()
		{
			return false;
		}
	}
}
