using System;
using System.Collections.Generic;
using Backend.Authentication;

namespace Game.Seasons.Controllers
{
	public class SeasonsBarMessageController
	{
		public static Action<string> OnNextMessage;

		private static bool _hasStarted;

		private const string _defaultMessage = "Welcome to Seasons!";

		private int _count;

		private SeasonsSettings _settings;

		private int _currentMessage;

		private string _nextMessage;

		public void Init(SeasonsSettings seasonsSettings)
		{
		}

		private void SetData(SeasonTimeInfo _)
		{
		}

		private void ReportCurrentMessage()
		{
		}

		private void GenerateNextMessage()
		{
		}

		private List<string> GetListForState()
		{
			return null;
		}

		private void OnGameSettingsAvailable()
		{
		}

		public void Reset()
		{
		}
	}
}
