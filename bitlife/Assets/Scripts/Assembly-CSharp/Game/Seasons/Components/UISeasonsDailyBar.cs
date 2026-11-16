using Game.Seasons.Models;
using TMPro;
using UnityEngine;

namespace Game.Seasons.Components
{
	public class UISeasonsDailyBar : MonoBehaviour
	{
		private GameObject pipIcon;

		private TextMeshProUGUI coinsLabel;

		private SeasonsDailyRewardModel Data__BackingField;

		public SeasonsDailyRewardModel Data
		{
			get
			{
				return Data__BackingField;
			}
			private set
			{
				Data__BackingField = value;
			}
		}

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void RefreshData()
		{
		}

		public void SetData(SeasonsDailyRewardModel data)
		{
		}

		public void RefreshPip()
		{
		}

		private void OnBarClicked()
		{
		}
	}
}
