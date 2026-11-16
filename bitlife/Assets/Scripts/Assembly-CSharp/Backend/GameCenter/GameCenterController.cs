using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Apple.Core.Runtime;
using Apple.GameKit;

namespace Backend.GameCenter
{
	public class GameCenterController
	{
		private sealed class _003C_003Ec__DisplayClass5_0
		{
			public string bitLifeAchievementId;

			internal void _003CCompleteAchievementByBitLifeId_003Eb__0()
			{
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CEnsureAuthenticated_003Ed__8 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public Action onAuthenticated;

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

		private static GameCenterAchievements _achievements;

		private static GKLocalPlayer _localPlayer;

		private static bool _authInProgress;

		private static Action _onAuthenticated;

		private static bool _hasHookedAuthenticateEvents;

		public static void CompleteAchievementByBitLifeId(string bitLifeAchievementId)
		{
		}

		public static bool ToggleIsDebug()
		{
			return false;
		}

		private static void Init()
		{
		}

		private static void EnsureAuthenticated(Action onAuthenticated)
		{
		}

		private static void OnAuthenticateUpdate(GKLocalPlayer localPlayer)
		{
		}

		private static void OnAuthenticateError(NSError error)
		{
		}

		private static void ProcessAuthentication(bool success)
		{
		}

		public static void OpenGameCenterAchievements()
		{
		}

		private static void ShowAchievementsUI()
		{
		}

		public static void Log(string message)
		{
		}
	}
}
