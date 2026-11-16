using System.Collections;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameShuttle : MonoBehaviour, IguiObject
{
	public enum SHUTTLE_RESULT
	{
		WIN = 0,
		LOSE = 1
	}

	public enum SHUTTLE_POSITION
	{
		BOTTOM = 0,
		LOWER_BOTTOM = 1,
		MIDDLE = 2,
		UPPER_TOP = 3,
		TOP = 4
	}

	public enum SHUTTLE_MOVE
	{
		LEFT = 0,
		RIGHT = 1,
		UP = 2,
		DOWN = 3
	}

	private RectTransform rect;

	public static ButtonActivatedDelegate buttonActivatedDelegate;

	private MiniGameSwipeNavigation navigationScript;

	public float power;

	public float targetGravity;

	public bool noLose;

	private bool initialized;

	private int timeRemaining;

	private int requiredTime;

	private bool gameOver;

	private GameObject dimmedOverlayObject;

	private GameObject touchpadOverlayObject;

	private GameObject maskObject;

	private GameObject backgroundAObject;

	private GameObject backgroundBObject;

	private GameObject backgroundCObject;

	private GameObject playerObject;

	private GameObject timerObject;

	private GameObject floorObject;

	private GameObject ceilingObject;

	private GameObject touchpadObject;

	private Rigidbody2D playerBody;

	private Scenario scenario;

	private float theWidth;

	private float theHeight;

	private float leftEdge;

	private ArrayList backgrounds;

	private ArrayList asteroids1;

	private ArrayList asteroids2;

	private ArrayList asteroids3;

	private ArrayList asteroids4;

	private ArrayList UFOs1;

	private ArrayList UFOs2;

	private ArrayList UFOs3;

	private ArrayList obstacles;

	private float backgroundPixelSteps;

	private float obstaclePixelSteps;

	private float timer;

	private float targetFrameRate;

	private float physicsRatio;

	private float tabletPhysicsRatio;

	private float multiplier;

	private float obstacleSpreadMult;

	private float scale;

	private Vector3 startPos;

	private float ratio;

	private MiniGameDataShuttle gameData;

	private int lastObstaclePositionType;

	private float thrustMult;

	private int lastObstacleType;

	private int obstaclesSinceLastCenterPosition;

	private bool wonGame;

	private bool movingUp;

	private float firstObstacleHorizontalPosition;

	private int numberOfUFOs;

	private bool ignoreSwipes;

	private bool UFOSoundPlaying;

	private Color asteroidColor;

	private float asteroidColorMult;

	private float previousAsteroidMult;

	private float touchTimer;

	private int lastDirection;

	private int thrustAdditions;

	private bool babyMode;

	private bool thrustSoundEnabled;

	private string secondsText;

	private string secondText;

	private string remainingText;

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float desiredWidth, Scenario theScenario)
	{
	}

	private void ResetUFOSound()
	{
	}

	private void ButtonActivated(int pButtonType)
	{
	}

	private void UpdateTimeRemainingText()
	{
	}

	private void UpdateCountdown()
	{
	}

	private void StartGame()
	{
	}

	public void HitDetected(int obstacleType)
	{
	}

	public void ColorizeAsteroid(GameObject obstacle, bool reset = false)
	{
	}

	private int MoveAngle()
	{
		return 0;
	}

	private void PlayerMoveUp(float force)
	{
	}

	private void PlayerMoveDown(float force)
	{
	}

	private void RestoreSwipes()
	{
	}

	private void PlayerSpinOut()
	{
	}

	private void ReturnObstacleToPool(GameObject obstacle)
	{
	}

	private void AddRandomObstacle()
	{
	}

	private float GetVerticalPosition(int newPositionType, int obstacleType)
	{
		return 0f;
	}

	private void DelayedInitialized()
	{
	}

	private GameObject PickRandomObstacle()
	{
		return null;
	}

	private void RemoveObstacles()
	{
	}

	private void WinGame()
	{
	}

	private void LoseGame(int obstacleType)
	{
	}

	private void CleanupPlayer()
	{
	}

	private void DelayedExitGame()
	{
	}

	private void ExitGame(int result)
	{
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
