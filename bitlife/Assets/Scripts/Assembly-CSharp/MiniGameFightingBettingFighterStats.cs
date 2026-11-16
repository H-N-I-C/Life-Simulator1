using UnityEngine;

public class MiniGameFightingBettingFighterStats : MonoBehaviour
{
	private GameObject _nameObject;

	private GameObject _statBarObject;

	private Vector2 _size;

	private int _statValue;

	private int _maxStatValue;

	private float _width;

	private float _height;

	private bool _leftSide;

	private string _name;

	private MiniGameFighting _miniGame;

	private float _barHeight;

	private readonly float _spacer;

	private void OnDestroy()
	{
	}

	public bool InitWithParameters(MiniGameFighting minigame, string fighterName, int statBarValue, int maxStatBarValue, bool leftSide)
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

	private bool BuildStatBar()
	{
		return false;
	}

	private bool PositionComponents()
	{
		return false;
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
