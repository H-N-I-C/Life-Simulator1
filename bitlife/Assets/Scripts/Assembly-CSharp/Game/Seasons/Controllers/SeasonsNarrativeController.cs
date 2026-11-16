using System;
using Backend.Authentication;
using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public class SeasonsNarrativeController
	{
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public string narrativeId;

			public Action<LoadNarrativeState> callback;

			public SeasonsNarrativeController _003C_003E4__this;

			public Action<string, Scenario> _003C_003E9__2;

			internal bool _003CLoadNarrative_003Eb__0(SeasonNarrativeCompletionModel t)
			{
				return false;
			}

			internal void _003CLoadNarrative_003Eb__1((string, bool) result)
			{
			}

			internal void _003CLoadNarrative_003Eb__2(string narrativeResult, Scenario scenario)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public string narrativeId;

			internal bool _003CProcessFeedback_003Eb__0(SeasonNarrativeCompletionModel t)
			{
				return false;
			}
		}

		private SeasonsSettings _settings;

		public void LoadNarrative(string narrativeId, bool watchedAd, Action<LoadNarrativeState> callback)
		{
		}

		private void ProcessFeedback(string command, string narrativeId, Scenario scenario)
		{
		}

		public void Init(SeasonsSettings settings)
		{
		}
	}
}
