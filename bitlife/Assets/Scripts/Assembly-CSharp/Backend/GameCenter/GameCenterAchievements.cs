using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Backend.GameCenter
{
	public class GameCenterAchievements
	{
		[StructLayout((LayoutKind)3)]
		private struct _003CCompleteByBitLifeId_003Ed__7 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public GameCenterAchievements _003C_003E4__this;

			public string bitLifeAchievementId;

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

		private const string GAME_CENTER_ID_PREFIX = "com.candywriter.bitlife.achievement.early.";

		private const float COMPLETE_PROGRESS = 100f;

		private bool _isDebug;

		private HashSet<string> _bitLifeAchievementIds;

		public bool ToggleIsDebug()
		{
			return false;
		}

		public bool ContainsBitLifeId(string bitLifeAchievementId)
		{
			return false;
		}

		public void CompleteByBitLifeId(string bitLifeAchievementId)
		{
		}

		private string FormatGameCenterIdByBitLifeId(string bitLifeAchievementId)
		{
			return null;
		}
	}
}
