using DG.Tweening;
using UnityEngine;

public class MiniGameFightingFighterStats : MonoBehaviour
{
	private sealed class _003C_003Ec__DisplayClass32_0
	{
		public int newValue;

		public int unit;

		public MiniGameFightingFighterStats _003C_003E4__this;

		public TweenCallback _003C_003E9__0;

		internal void _003CAdjustStatValueRemainingText_003Eb__0()
		{
		}
	}

	private GameObject _nameObject;

	private GameObject _levelObject;

	private GameObject _statBarObject;

	private GameObject _statBarValueRemainingObject;

	private GameObject _pointerObject;

	private Vector2 _size;

	private int _statValue;

	private int _maxStatValue;

	private float _width;

	private float _height;

	private bool _pointsRight;

	private string _statBarLabel;

	private string _name;

	private SimFightClubFighterStats _stats;

	private MiniGameFighting _miniGame;

	private const float Spacer = 3f;

	private const int NameFontSize = 55;

	private const int StatsFontSize = 50;

	private const int Width = 400;

	private const int PointerHeight = 25;

	private const int StatBarHeight = 35;

	private void OnDestroy()
	{
	}

	public bool InitWithParameters(MiniGameFighting minigame, string fighterName, SimFightClubFighterStats stats, string statBarLabel, int statBarValue, int maxStatBarValue, bool pointsRight)
	{
		return false;
	}

	private bool BuildComponents()
	{
		return false;
	}

	private bool SetSize()
	{
		return false;
	}

	private bool BuildNameLabel()
	{
		return false;
	}

	private bool BuildLevelLabel()
	{
		return false;
	}

	private bool BuildStatBar()
	{
		return false;
	}

	private bool BuildStatBarValueRemainingLabel()
	{
		return false;
	}

	private bool BuildPointer()
	{
		return false;
	}

	private bool PositionComponents()
	{
		return false;
	}

	private void SetStatValueRemainingText(int value)
	{
	}

	private void AdjustStatValueRemainingText(int originalValue, int amount, float duration)
	{
	}

	public void AdjustStatValue(int amount, float duration)
	{
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	public Vector2 GetSize()
	{
		return default;
	}
}
