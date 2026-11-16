using System.Collections.Generic;
using TMPro;
using Tracking;
using UnityEngine;

public class UIVoteScenario : MonoBehaviour, IUIVoteButtonGroup
{
	private sealed class _003C_003Ec__DisplayClass10_0
	{
		public UIVoteButton selected;

		internal void _003CVoteSelected_003Eb__0(UIVoteButton i)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public bool show;

		internal void _003CShowVoteButtons_003Eb__0(UIVoteButton i)
		{
		}
	}

	private List<UIVoteButton> voteButtons;

	private TextMeshProUGUI textFinished;

	private string thanksMessage;

	private BitLife_ScenarioVote telemetry;

	private static bool ScenarioVotingEnabled;

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}

	private void OnDisable()
	{
	}

	private void OnRevealScenario(Scenario scenario)
	{
	}

	private bool ShowVoteButtons(bool show)
	{
		return false;
	}

	public void VoteSelected(UIVoteButton selected)
	{
	}

	public void VoteSelectionComplete()
	{
	}

	private void _003CAwake_003Eb__5_0(UIVoteButton i)
	{
	}
}
