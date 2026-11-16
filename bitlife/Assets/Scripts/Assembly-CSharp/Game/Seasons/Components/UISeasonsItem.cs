using BitLife.UI.CandyObjects;
using Game.Seasons.Controllers;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsItem : UISeasonsBehaviour
	{
		private const string NoSeasonProgressLabel = "Complete One Life to Unlock Seasonal Events!";

		private SeasonsItemType itemType;

		private UISeasonsBar seasonsBar;

		private CandyButton candybutton;

		private GameObject progressBar;

		private Image progressBarAmount;

		private TextMeshProUGUI progressLabel;

		private SeasonsAudioAssetTypes audioName;

		private int complete;

		private int required;

		private bool AnimationBlocked__BackingField;

		public SeasonsItemType ItemType => SeasonsItemType.Tasks;

		public bool AnimationBlocked
		{
			get
			{
				return AnimationBlocked__BackingField;
			}
			set
			{
				AnimationBlocked__BackingField = value;
			}
		}

		public void Enable(bool enabled)
		{
		}

		protected override void ApplyTheme()
		{
		}

		protected override void Awake()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void RefreshData()
		{
		}

		public void SetAnimationProgress(float f)
		{
		}

		private void SetData(SeasonTimeInfo data)
		{
		}

		private void SetProgress(SeasonProgressModel result)
		{
		}

		private void SetProgressLabelWithoutTranslation(string text)
		{
		}

		private void SetProgressLabel(string text)
		{
		}

		private void SetProgressAmount(float value)
		{
		}

		private bool HasClaimItems()
		{
			return false;
		}

		private void _003CAwake_003Eb__18_0()
		{
		}
	}
}
