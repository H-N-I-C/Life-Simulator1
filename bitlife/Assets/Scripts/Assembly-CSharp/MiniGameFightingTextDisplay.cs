using DG.Tweening;
using UnityEngine;

public class MiniGameFightingTextDisplay : MonoBehaviour
{
	private string _text;

	private float _perCharacterAnimationDuration;

	private int _currentCharacterIndex;

	private float _normalFontSize;

	private float _maxFontSize;

	private float _normalWidth;

	private int _durationDivisor;

	private bool _useCenterAlignment;

	private Sequence _throbSequence;

	private const int DefaultDurationDivisor = 30;

	private const int NormalFontSize = 75;

	private const float SmallFontMultiplier = 0.9f;

	private const float LargeFontMultiplier = 1.2f;

	private const int LargeFontBoxWidth = 950;

	private const int MinFontSize = 10;

	private void OnDestroy()
	{
	}

	public void InitWithParameters(Vector2 size)
	{
	}

	private void SetFontSize(FIGHTING_DISPLAY_FONT_SIZE fontSize)
	{
	}

	public void LoadText(string text)
	{
	}

	public float GetAnimationDuration()
	{
		return 0f;
	}

	public void DisplayText(FIGHTING_DISPLAY_FONT_SIZE fontSize, bool useCenterAlignment = false, int durationDivisor = -1, bool animated = true)
	{
	}

	private void AnimateText()
	{
	}

	private void UpdateText(string text)
	{
	}

	public void Clear()
	{
	}

	private void Show()
	{
	}

	public void Hide()
	{
	}

	public void Throb()
	{
	}

	private void _003CDisplayText_003Eb__20_0()
	{
	}
}
