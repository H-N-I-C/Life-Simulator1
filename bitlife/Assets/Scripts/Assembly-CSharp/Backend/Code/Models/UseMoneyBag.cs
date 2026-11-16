using System;

namespace Backend.Code.Models
{
	public class UseMoneyBag : IMyStuffUseCheck
	{
		private int _bagValue;

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

		public UseMoneyBag(int functionIntValue)
		{
		}

		public UseMoneyBag()
		{
		}

		public void PreDownload(Action<bool> callback)
		{
		}

		public bool CanExecute()
		{
			return false;
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
