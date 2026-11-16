using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameNumberlink : MonoBehaviour, IguiObject
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

	private MiniGameDataNumberlink _data;

	private List<MiniGameNumberlinkGridSpot> _gridSpots;

	private GameObject _boardObject;

	private GameObject _tutorialObject;

	private float _reductionMult;

	private GameObject _timerLabelObject;

	private int _timeRemaining;

	private int _timeLimit;

	private MiniGameNumberlinkGridSpot _activeSpot;

	private List<List<MiniGameNumberlinkGridSpot>> _chains;

	private List<MiniGameNumberlinkGridSpot> _activeChain;

	private float timer;

	private List<int> _homeBaseIndexes;

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

	private void StartGame()
	{
	}

	private int GetNewHomeBaseIndex(int spotIndex, bool mirrorForHorizontal)
	{
		return 0;
	}

	private void MirrorHomeBasesHorizontal(bool horizontal)
	{
	}

	private void BuildTutorialBoard()
	{
	}

	private void BuildGameBoard()
	{
	}

	private float BuildGrid()
	{
		return 0f;
	}

	private void UpdateCountdown()
	{
	}

	private void TimeIsUp()
	{
	}

	private int IndexForCoordinate(Vector2 coordinate)
	{
		return 0;
	}

	private Vector2 CoordinateForIndex(int index)
	{
		return default;
	}

	private MiniGameNumberlinkGridSpot GridSpotForIndex(int index)
	{
		return null;
	}

	private void SetEndPointAtSpot(Vector2 coordinate, NUMBERLINK_COLOR_TYPE colorTypeType, int id)
	{
	}

	private void ExitGame()
	{
	}

	private void GameOverWithType(int type)
	{
	}

	private bool BoardCompleted()
	{
		return false;
	}

	private MiniGameNumberlinkGridSpot SpotInDirectionOfSpot(MiniGameNumberlinkGridSpot spot, NUMBERLINK_DIRECTION direction)
	{
		return null;
	}

	private NUMBERLINK_LINE_ENTRY_DIRECTION AdjacentDirectionBetweenSpots(MiniGameNumberlinkGridSpot newSpot, MiniGameNumberlinkGridSpot previousSpot)
	{
		return NUMBERLINK_LINE_ENTRY_DIRECTION.NONE;
	}

	private NUMBERLINK_LINE_ENTRY_DIRECTION DiagonalDirectionBetweenSpots(MiniGameNumberlinkGridSpot newSpot, MiniGameNumberlinkGridSpot previousSpot)
	{
		return NUMBERLINK_LINE_ENTRY_DIRECTION.NONE;
	}

	private MiniGameNumberlinkGridSpot GetGridSpotForLocation(Vector3 touchLocation)
	{
		return null;
	}

	private MiniGameNumberlinkGridSpot GetGridSpotForCoordinate(Vector2 coordinate)
	{
		return null;
	}

	private void AddSpotToChain(MiniGameNumberlinkGridSpot spot, List<MiniGameNumberlinkGridSpot> chain)
	{
	}

	private void CropChainBeforeSpot(MiniGameNumberlinkGridSpot spot, List<MiniGameNumberlinkGridSpot> chain)
	{
	}

	private bool SpotExistsInChain(MiniGameNumberlinkGridSpot spot, List<MiniGameNumberlinkGridSpot> chain)
	{
		return false;
	}

	private int SpotIndexinChain(MiniGameNumberlinkGridSpot theSpot, List<MiniGameNumberlinkGridSpot> chain)
	{
		return 0;
	}

	private List<MiniGameNumberlinkGridSpot> ChainForSpot(MiniGameNumberlinkGridSpot spot)
	{
		return null;
	}

	private void AddChain(List<MiniGameNumberlinkGridSpot> chain)
	{
	}

	private void RemoveChain(List<MiniGameNumberlinkGridSpot> chain)
	{
	}

	private void DrawChain(List<MiniGameNumberlinkGridSpot> chain)
	{
	}

	private bool TestDiagonalChainWithProposedSpot(MiniGameNumberlinkGridSpot currentSpot, MiniGameNumberlinkGridSpot fillerSpot, List<MiniGameNumberlinkGridSpot> chain)
	{
		return false;
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

	private List<int> GetRandomBoard(int forceBoard = -1)
	{
		return null;
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
