using BitLife.UI.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameRoulette : MonoBehaviour, IguiObject
{
	private const int SPEED_UP_BUTTON_INDEX = 0;

	private const int BET_AGAIN_BUTTON_INDEX = 1;

	private const int REPEAT_SAME_BET_BUTTON_INDEX = 2;

	private const int BET_EARNINGS_BUTTON_INDEX = 3;

	private const float START_SPIN_DELAY_SECONDS = 0.25f;

	private const float TABLE_SPIN_ACCELERATION_SECONDS = 0.5f;

	private const float BALL_SPIN_START = 1f;

	private const float BALL_SPIN_SECONDS = 2f;

	private const float BALL_SPIN_SECONDS_INSIDE = 2f;

	private const float BALL_HEIGHT_ON_EDGE = 280f;

	private const float BALL_HEIGHT_INSIDE = 220f;

	private const float SPIN_SPEED = 200f;

	private const float TABLE_SPIN_DECELERATION_SECONDS = 6f;

	private RectTransform _rect;

	private GameObject _spinnerView;

	private Image _spinnerImage;

	private Image _ballImage;

	private TextMeshProUGUI _playerLabel;

	private RouletteBetItem _betItem;

	private GameObject _speedUpButton;

	private GameObject _repeatSameBetButton;

	private SimCasinoSession _session;

	private ScenarioGameRoulette _scenario;

	private float _betItemBorderSize;

	private int _initialBet;

	private bool _initialized;

	private float _ballSpinDegrees;

	private int _resultNumber;

	private bool _buttonsEnabled;

	private bool _buttonJustPressed;

	private float _spinSpeed;

	private ROULETTE_COLOR ResultColor => ROULETTE_COLOR.NONE;

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

	public Vector2 GetActualSize()
	{
		return default;
	}

	public void SetHeightConstraint()
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	private void ShowRepeatSameBetButton(bool show)
	{
	}

	private void ShowSpeedUpButton(bool show)
	{
	}

	public virtual void OnButtonDown(int buttonIndex)
	{
	}

	public virtual void OnButtonUp(int buttonIndex)
	{
	}

	private void OnButtonUpLogic(int buttonIndex)
	{
	}

	protected void BetEarnings()
	{
	}

	protected void ResetButtonJustPressed()
	{
	}

	private void ExitGame()
	{
	}

	private void StartSpin()
	{
	}

	private void KillSpinAnimation()
	{
	}

	private void StartSpinAnimation()
	{
	}

	private static float GetBallSpinDegrees(int number)
	{
		return 0f;
	}

	private void OnSpinDone()
	{
	}

	private void SlowdownSpinAnimation()
	{
	}

	public int CalculateEarnings()
	{
		return 0;
	}

	private void _003CStartSpinAnimation_003Eb__50_0()
	{
	}

	private void _003CStartSpinAnimation_003Eb__50_1(float t)
	{
	}

	private void _003CStartSpinAnimation_003Eb__50_2()
	{
	}

	private void _003CStartSpinAnimation_003Eb__50_3(float t)
	{
	}

	private void _003CStartSpinAnimation_003Eb__50_4(float y)
	{
	}

	private void _003CStartSpinAnimation_003Eb__50_5()
	{
	}

	private void _003CSlowdownSpinAnimation_003Eb__53_0(float t)
	{
	}
}
