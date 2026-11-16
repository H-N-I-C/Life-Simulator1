using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class MiniGameFightingButton : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass21_0
	{
		public MiniGameFightingButton _003C_003E4__this;

		public float alpha;

		internal void _003CAlphaTo_003Eb__0()
		{
		}
	}

	private bool IsStartButton__BackingField;

	private bool IsShowingInfo__BackingField;

	private GameObject _textObject;

	private SimFightClubMove _move;

	private MiniGameFighting _minigame;

	private Sequence _throbSequence;

	private string _labelString;

	private const int InfoFontSize = 40;

	private const int NameFontSize = 50;

	public bool IsStartButton
	{
		get
		{
			return IsStartButton__BackingField;
		}
		private set
		{
			IsStartButton__BackingField = value;
		}
	}

	public bool IsShowingInfo
	{
		get
		{
			return IsShowingInfo__BackingField;
		}
		set
		{
			IsShowingInfo__BackingField = value;
		}
	}

	private void OnDestroy()
	{
	}

	public bool InitWithParameters(SimFightClubMove move, MiniGameFighting minigame, ScenarioMinigameTextRegistry registry)
	{
		return false;
	}

	public bool InitStartButtonWithParameters(MiniGameFighting minigame)
	{
		return false;
	}

	private bool BuildButton()
	{
		return false;
	}

	public SimFightClubMove GetMove()
	{
		return null;
	}

	public void BounceGrow()
	{
	}

	public void AlphaTo(float alpha, float duration)
	{
	}

	public void Throb()
	{
	}

	public void ToggleInfoReveal(float delay = 0f)
	{
	}

	private string GetInferiorFightingStyle(bool translated = true)
	{
		return null;
	}

	public void ResetInfoDisplay()
	{
	}

	private void _003CBuildButton_003Eb__18_0(BaseEventData data)
	{
	}

	private void _003CBuildButton_003Eb__18_1(BaseEventData data)
	{
	}

	private void _003CToggleInfoReveal_003Eb__23_0()
	{
	}

	private void _003CToggleInfoReveal_003Eb__23_1()
	{
	}
}
