using System;

namespace Backend.Code.Models
{
	public class CustomLife : IMyStuffUseCheck
	{
		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public Action<bool> callback;

			internal void _003CPreDownload_003Eb__0(bool resultDownload)
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

		private void _003CExecutePowerUp_003Eb__8_0(bool resultDownload)
		{
		}
	}
}
