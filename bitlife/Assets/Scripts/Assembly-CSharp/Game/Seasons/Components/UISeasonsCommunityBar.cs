using System.Collections.Generic;
using Game.Seasons.Models;
using TMPro;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsCommunityBar : UISeasonsBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public UISeasonsCommunityBar _003C_003E4__this;

			public string message;

			internal void _003COnLanguageChanged_003Eb__0(List<string> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_0
		{
			public UISeasonsCommunityBar _003C_003E4__this;

			public string message;

			internal void _003COnNextMessage_003Eb__0(List<string> result)
			{
			}
		}

		private Image background;

		private TextMeshProUGUI label;

		private SeasonTimeInfo _timeData;

		private string _seasonsCommunityGoalTextTemplate;

		private string _seasonsCommunityGoalTextTemplateTranslated;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		protected override void ApplyTheme()
		{
		}

		private void TimeData(SeasonTimeInfo timeData)
		{
		}

		private void OnGameSettingsAvailable()
		{
		}

		private void OnLanguageChanged(string value)
		{
		}

		private void OnNextMessage(string message)
		{
		}

		public void SetData(SeasonModel data)
		{
		}

		private string GetParsedUniversalString(string text, Dictionary<string, string> dict)
		{
			return null;
		}
	}
}
