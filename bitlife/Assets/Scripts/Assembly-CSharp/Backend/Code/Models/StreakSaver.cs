using System;

namespace Backend.Code.Models
{
	public class StreakSaver : IMyStuffUseCheck
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<bool> _003C_003E9__8_0;

			internal void _003CExecutePowerUp_003Eb__8_0(bool _)
			{
			}
		}

		private StuffItem Item__BackingField;

		public bool RequiresScenario => false;

		public StuffItem Item
		{
			get
			{
				return Item__BackingField;
			}
			set
			{
				Item__BackingField = value;
			}
		}

		public bool CanExecute()
		{
			return false;
		}

		public void PreDownload(Action<bool> callback)
		{
		}

		public void ExecutePowerUp()
		{
		}

		public int GetLastIntResult()
		{
			return 0;
		}

		public string GetErrorMessage()
		{
			return null;
		}
	}
}
