using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;

namespace CandyBusiness.Confidence
{
	public class ConfidenceFileAutoSaver : IDisposable
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CDebouncedSaveAsync_003Ed__29 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public ConfidenceFileAutoSaver _003C_003E4__this;

			public CancellationToken token;

			private MainThreadAwaitable _003C_003Eu__1;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CSaveOnceAsync_003Ed__32 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public CancellationTokenSource source;

			public ConfidenceFileAutoSaver _003C_003E4__this;

			private CancellationToken _003Ctoken_003E5__2;

			private object _003C_003E7__wrap2;

			private int _003C_003E7__wrap3;

			private string _003Cjson_003E5__5;

			private bool _003Cwritten_003E5__6;

			private BackgroundThreadAwaitable _003C_003Eu__1;

			private Awaitable<bool>.Awaiter _003C_003Eu__2;

			private MainThreadAwaitable _003C_003Eu__3;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CTickLoopAsync_003Ed__31 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder _003C_003Et__builder;

			public ConfidenceFileAutoSaver _003C_003E4__this;

			public CancellationToken token;

			private TaskAwaiter _003C_003Eu__1;

			private MainThreadAwaitable _003C_003Eu__2;

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

		[StructLayout((LayoutKind)3)]
		private struct _003CWriteWithRetryAsync_003Ed__33 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public Awaitable.AwaitableAsyncMethodBuilder<bool> _003C_003Et__builder;

			public ConfidenceFileAutoSaver _003C_003E4__this;

			public CancellationToken token;

			public string json;

			private string _003CtempPath_003E5__2;

			private string _003CbackupPath_003E5__3;

			private int _003Ci_003E5__4;

			private FileStream _003Cstream_003E5__5;

			private StreamWriter _003Cwriter_003E5__6;

			private TaskAwaiter _003C_003Eu__1;

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

		private string Path__BackingField;

		private readonly float saveIntervalMinutes;

		private readonly int debounceMs;

		private readonly int maxRetries;

		private readonly int retryDelayMs;

		private readonly bool isValidPath;

		private CancellationTokenSource cts;

		private ConfidenceSnapshot snapshot;

		private Action tick;

		private bool disposed;

		private bool dirty;

		private bool debouncePending;

		private bool writeInProgress;

		private bool pendingSave;

		public string Path
		{
			get
			{
				return Path__BackingField;
			}
			private set
			{
				Path__BackingField = value;
			}
		}

		public bool IsDirty => false;

		public ConfidenceFileAutoSaver(string path, float saveIntervalMinutes = 2f, float debounceSeconds = 1f, int maxRetries = 3, int retryDelayMs = 500)
		{
		}

		public void Dispose()
		{
		}

		public void Start(ConfidenceSnapshot toSave, bool saveNow, Action onTick = null)
		{
		}

		public void Stop()
		{
		}

		public void Save()
		{
		}

		public void QueueSave()
		{
		}

		private void OnAttributeValueChanged(ConfidenceAttribute attribute)
		{
		}

		private void OnApplicationFocusChanged(bool hasFocus)
		{
		}

		private void SubscribeAttributes()
		{
		}

		private void UnsubscribeAttributes()
		{
		}

		private Awaitable DebouncedSaveAsync(CancellationToken token)
		{
			return null;
		}

		private void RequestSave()
		{
		}

		private Awaitable TickLoopAsync(CancellationToken token)
		{
			return null;
		}

		private Awaitable SaveOnceAsync(CancellationTokenSource source)
		{
			return null;
		}

		private Awaitable<bool> WriteWithRetryAsync(string json, CancellationToken token)
		{
			return null;
		}

		private void FlushSynchronously()
		{
		}

		private void CommitTempFile(string tempPath, string backupPath)
		{
		}

		private static void CleanupTempFiles(string tempPath, string backupPath)
		{
		}
	}
}
