using System;

namespace Backend.Code.Models
{
	public class UseTokenItem : IMyStuffUseCheck
	{
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
