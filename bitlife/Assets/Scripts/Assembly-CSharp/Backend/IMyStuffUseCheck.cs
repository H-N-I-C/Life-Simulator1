using System;

namespace Backend
{
	public interface IMyStuffUseCheck
	{
		StuffItem Item { get; set; }

		bool RequiresScenario { get; }

		bool CanExecute();

		void PreDownload(Action<bool> callback);

		void ExecutePowerUp();

		int GetLastIntResult();

		string GetErrorMessage();
	}
}
