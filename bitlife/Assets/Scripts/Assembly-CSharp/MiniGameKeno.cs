using System.Collections.Generic;
using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameKeno : MonoBehaviour, IguiObject
{
	private RectTransform _rect;

	private bool _initialized;

	private bool _touchInPlay;

	private bool _gameOver;

	private float _width;

	private float _gridSpotWidth;

	private float _gridStartVerticalPos;

	private Vector2 _gridSize;

	private Scenario _scenario;

	private List<MiniGameKenoGridSpot> _gridSpots;

	private GameObject _boardObject;

	private MiniGameKenoGridSpot _activeSpot;

	private SimCasinoSession _miniGameData;

	private SimCasinoGameKeno _gameData;

	private GameObject _buttonObject;

	private GameObject _resultsLabelObject;

	private bool _pickingPhase;

	private bool _buttonJustPressed;

	private List<MiniGameKenoBall> _kenoBalls;

	private MiniGameKenoBall _ballToReveal;

	private List<Sequence> _ballSequences;

	private List<int> _playerPickedNumbers;

	private int _matchedCount;

	private int _kenoBallSoundIndex;

	private ScenarioMinigameTextRegistry mTextRegistry;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float width, Scenario scenario)
	{
	}

	private float BuildGrid()
	{
		return 0f;
	}

	private int IndexForCoordinate(Vector2 coordinate)
	{
		return 0;
	}

	private Vector2 CoordinateForIndex(int index)
	{
		return default;
	}

	private MiniGameKenoGridSpot GridSpotForIndex(int index)
	{
		return null;
	}

	private Sequence ThrobObject(GameObject theObject)
	{
		return null;
	}

	private void KillThrobs()
	{
	}

	private void ExitGame()
	{
	}

	private string GroupedStringForNumbers(List<int> numbers)
	{
		return null;
	}

	private MiniGameKenoGridSpot GetGridSpotForLocation(Vector3 touchLocation)
	{
		return null;
	}

	private MiniGameKenoGridSpot GetGridSpotForCoordinate(Vector2 coordinate)
	{
		return null;
	}

	private bool AllNumbersPicked()
	{
		return false;
	}

	private void PickKenoBalls()
	{
	}

	private bool KenoBallExists(int number)
	{
		return false;
	}

	private void ShowKenoBalls()
	{
	}

	private void RevealBall(MiniGameKenoBall ball)
	{
	}

	private void PlayBallSound()
	{
	}

	private void RevealBall1()
	{
	}

	private void RevealBall2()
	{
	}

	private void RevealBall3()
	{
	}

	private void RevealBall4()
	{
	}

	private void RevealBall5()
	{
	}

	private void RevealBall6()
	{
	}

	private void PostGameFeedback()
	{
	}

	private void ShowButton()
	{
	}

	private void HideButton()
	{
	}

	private void HandleTouchDown(Vector3 touchLocation)
	{
	}

	private void HandleTouchMove(Vector3 touchLocation)
	{
	}

	private void HandleTouchUp(Vector3 touchLocation)
	{
	}

	public void ButtonUp()
	{
	}

	public void ButtonDown()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	private bool ButtonUpVerification()
	{
		return false;
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}
}
