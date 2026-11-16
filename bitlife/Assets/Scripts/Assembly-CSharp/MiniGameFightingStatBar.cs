using DG.Tweening;
using UnityEngine;

public class MiniGameFightingStatBar : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass20_0
	{
		public MiniGameFightingStatBar _003C_003E4__this;

		public int unit;

		public TweenCallback _003C_003E9__0;

		internal void _003CAdjustValue_003Eb__0()
		{
		}
	}

	private GameObject _barContainer;

	private GameObject _foregroundObject;

	private GameObject _backgroundObject;

	private GameObject _shadowObject;

	private GameObject _labelObject;

	private float _value;

	private int _maxValue;

	private Vector2 _size;

	private string _labelString;

	private float _barWidth;

	private float _spacer;

	private MiniGameFighting _miniGame;

	private const int MinHeightCheck = 50;

	private const float LabelSizeMultiplier = 0.97f;

	private const int BarColorGreenMin = 50;

	private const int BarColorYellowMin = 25;

	private void OnDestroy()
	{
	}

	public bool InitWithParameters(MiniGameFighting minigame, Vector2 size, string labelString, int value, int maxValue)
	{
		return false;
	}

	private static Color GetBarColorForValue(int value)
	{
		return default;
	}

	public bool SetValue(float value)
	{
		return false;
	}

	public void AdjustValue(float amount, float duration)
	{
	}

	private bool BuildLabel()
	{
		return false;
	}

	private bool BuildBarContainer()
	{
		return false;
	}

	private bool BuildShadow()
	{
		return false;
	}

	private bool BuildForeground()
	{
		return false;
	}

	private bool BuildBackground()
	{
		return false;
	}

	private void Show()
	{
	}

	public Sequence Reveal(float duration, float delay)
	{
		return null;
	}

	public void Hide()
	{
	}

	public Vector2 GetSize()
	{
		return default;
	}
}
