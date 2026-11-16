using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Authentication;

namespace Backend
{
	public class SettingsController
	{
		private enum ConnectState
		{
			Idle = 0,
			Connecting = 1,
			Connected = 2
		}

		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public string region;

			internal bool _003CProcessRegionAndReturn_003Eb__0(string t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass8_0
		{
			public SettingsController _003C_003E4__this;

			public string backendVersion;

			public string devMode;

			public Action<bool> _003C_003E9__1;

			internal void _003CInit_003Eb__0()
			{
			}

			internal void _003CInit_003Eb__1(bool _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass9_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CRunOnce_003Eb__1_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass9_0 _003C_003E4__this;

				private int _003Cretries_003E5__2;

				private TaskAwaiter<(string, bool)> _003C_003Eu__1;

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

			public string backendVersion;

			public string devMode;

			public SettingsController _003C_003E4__this;

			public Func<Task> _003C_003E9__1;

			internal void _003CRunOnce_003Eb__0(string _)
			{
			}

			internal Task _003CRunOnce_003Eb__1()
			{
				return null;
			}
		}

		public static Action<bool> OnApplicationPause;

		public static Action OnSemaphoreActive;

		private Action<GetConfigResponse> _callback;

		private ConnectState _initialized;

		private string _country;

		public string Country => null;

		public void Init(Action<GetConfigResponse> callback)
		{
		}

		private void RunOnce(string backendVersion, string devMode)
		{
		}

		private bool ProcessRegionAndReturn(GetConfigResponse requestData)
		{
			return false;
		}
	}
}
