using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsStreakItem : MonoBehaviour
	{
		public enum FullAnimationState
		{
			CanStart = 0,
			Idle = 1,
			Play = 2,
			Ended = 3
		}

		private STREAK_STATUS streakStatus;

		private bool showProgressImageBar;

		private TextMeshProUGUI textStreak;

		private TextMeshProUGUI textProgress;

		private Image imageProgressAmount;

		private Animator animator;

		private SeasonTimeInfo _lastInfo;

		private FullAnimationState state;

		private void Awake()
		{
		}

		private void OnEnable()
		{
		}

		public void Show(SeasonTimeInfo info)
		{
		}

		public void EnableTextUpdate()
		{
		}

		private void SetLastStreakCount()
		{
		}

		private void _003CAwake_003Eb__9_0()
		{
		}
	}
}
