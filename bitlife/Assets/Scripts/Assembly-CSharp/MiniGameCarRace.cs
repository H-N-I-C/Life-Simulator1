using System;
using System.Collections.Generic;
using System.Diagnostics;
using BitLife.UI.Interfaces;
using DG.Tweening;
using UnityEngine;

public class MiniGameCarRace : MonoBehaviour, IguiObject
{
	private MiniGameCarRaceTrack Track__BackingField;

	private MiniGameDataCarRace Data__BackingField;

	private List<string> DesiredCarColors__BackingField;

	private bool GameOver__BackingField;

	private bool IsSingleCarRace__BackingField;

	private RectTransform rect;

	private static MiniGamePowerBar.SetPowerDelegate PowerDelegate__BackingField;

	private float _width;

	private bool _initialized;

	private bool _raceStarted;

	private bool _useMPH;

	private Scenario _scenario;

	private GameObject _centerLabelObject;

	private GameObject _leftLabelObject;

	private GameObject _rightLabelObject;

	private GameObject _trackObject;

	private GameObject _vehiclePrefabObject;

	private GameObject _car1Object;

	private GameObject _car2Object;

	private GameObject _car3Object;

	private GameObject _car4Object;

	private GameObject _car5Object;

	private GameObject _car6Object;

	private GameObject _copCar1Object;

	private GameObject _copCar2Object;

	private GameObject _copCar3Object;

	private GameObject _copCar4Object;

	private GameObject _powerBarObject;

	private GameObject _overlayObject;

	private GameObject _tapToContinueObject;

	private GameObject _continueButtonObject;

	private GameObject _surrenderButtonObject;

	private GameObject _carLabelObject;

	private GameObject _carDisplayObject;

	private MiniGameCarRaceVehicle _car1;

	private MiniGameCarRaceVehicle _car2;

	private MiniGameCarRaceVehicle _car3;

	private MiniGameCarRaceVehicle _car4;

	private MiniGameCarRaceVehicle _car5;

	private MiniGameCarRaceVehicle _car6;

	private MiniGameCarRaceVehicle _heroCar;

	private MiniGameCarRaceVehicle _copCar1;

	private MiniGameCarRaceVehicle _copCar2;

	private MiniGameCarRaceVehicle _copCar3;

	private MiniGameCarRaceVehicle _copCar4;

	private MiniGamePowerBar _powerBar;

	private bool _touchInPlay;

	private float _speedMultiplier;

	private List<MiniGameCarRaceVehicle> _finishedCars;

	private List<MiniGameCarRaceVehicle> _deadCars;

	private List<MiniGameCarRaceVehicle> _cars;

	private bool _npcRiskEventsAllowed;

	private SimRacing _racing;

	private bool _touchesEnabled;

	private Sequence _overlayThrobSequence;

	private readonly float _countdownDelay;

	private readonly float _countdownAnimationDuration;

	private bool _buttonJustPressed;

	private Color _defaultLabelColor;

	private Stopwatch _stopwatch;

	private TimeSpan _failsafeTime;

	private TimeSpan _surrenderTime;

	private MiniGameCarRaceVehicle _currentCopCar;

	private List<MiniGameCarRaceVehicle> _cops;

	private bool _delayedPowerDeliveryActive;

	private float _delayedPowerLevel;

	private int _heroLane;

	private bool _hasSurrendered;

	private float _preRaceRevPower;

	private bool _rightHandedSideDriving;

	private static MusicController _music;

	private bool _revActive;

	private ScenarioMinigameTextRegistry mTextRegistry;

	public MiniGameCarRaceTrack Track
	{
		get
		{
			return Track__BackingField;
		}
		set
		{
			Track__BackingField = value;
		}
	}

	public MiniGameDataCarRace Data
	{
		get
		{
			return Data__BackingField;
		}
		set
		{
			Data__BackingField = value;
		}
	}

	public List<string> DesiredCarColors
	{
		get
		{
			return DesiredCarColors__BackingField;
		}
		set
		{
			DesiredCarColors__BackingField = value;
		}
	}

	public bool GameOver
	{
		get
		{
			return GameOver__BackingField;
		}
		set
		{
			GameOver__BackingField = value;
		}
	}

	public bool IsSingleCarRace
	{
		get
		{
			return IsSingleCarRace__BackingField;
		}
		set
		{
			IsSingleCarRace__BackingField = value;
		}
	}

	private static MiniGamePowerBar.SetPowerDelegate PowerDelegate
	{
		get
		{
			return PowerDelegate__BackingField;
		}
		set
		{
			PowerDelegate__BackingField = value;
		}
	}

	private void OnDestroy()
	{
	}

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	private void InitWithParameters(float width, Scenario scenario)
	{
	}

	private void UpdateStatDisplay()
	{
	}

	private void FixCarColors()
	{
	}

	private string AdjustCarColor(string color, string otherColor, List<int> familyBlackList)
	{
		return null;
	}

	private int GetColorFamilyForColor(string color)
	{
		return 0;
	}

	private string GetPreferredCarColorForFamily(int family)
	{
		return null;
	}

	private void SetGasPower(float power)
	{
	}

	private void ResetRevTimer()
	{
	}

	private void DelayPowerDelivery()
	{
	}

	private void ShowTapToStart()
	{
	}

	private void DisplayRaceResult()
	{
	}

	private void StartCountdown()
	{
	}

	private void Countdown2()
	{
	}

	private void Countdown1()
	{
	}

	private void CountdownGo()
	{
	}

	private void CountdownDone()
	{
	}

	private Sequence ThrobOverlay(GameObject theObject, float duration, int repeatCount = 1000)
	{
		return null;
	}

	private void GrowOverlay(GameObject theObject, float duration)
	{
	}

	private void KillOverlayThrob(Sequence theSequence, GameObject theObject)
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

	private void BeginRace()
	{
	}

	private void DelayStartCar1()
	{
	}

	private void DelayStartCar2()
	{
	}

	private void DelayStartCar3()
	{
	}

	private void DelayStartCar4()
	{
	}

	private void DelayStartCar5()
	{
	}

	private void DelayStartCar6()
	{
	}

	private SimRaceCarDriver GetFastestNPC(List<SimRaceCarDriver> drivers)
	{
		return null;
	}

	private float GetHeroSkillForLicense()
	{
		return 0f;
	}

	private void EnableNPCCrashes()
	{
	}

	private void ResetSpeedColor()
	{
	}

	private void TestMarkers()
	{
	}

	private void AddMarker(Vector2 pos, Color color)
	{
	}

	private Rect RectTransformToWorldSpace(RectTransform transform)
	{
		return default;
	}

	private Vector2 MouseToPosition(Vector3 touchLocation)
	{
		return default;
	}

	public Vector2 GetLocalTrackPointForPoint(Vector2 point)
	{
		return default;
	}

	public Vector2 GetPointForLocalPointTrackPoint(Vector2 localPoint)
	{
		return default;
	}

	public Vector2 GetActualSize()
	{
		return default;
	}

	private void SetWidth(float width)
	{
	}

	public void SetSize(Vector2 size)
	{
	}

	public void SetHeightConstraint()
	{
	}

	private void ShowContinueButton()
	{
	}

	private void ShowSurrenderButton()
	{
	}

	public void ResetButtonJustPressed()
	{
	}

	public void ButtonUpContinue()
	{
	}

	public void ButtonDownContinue()
	{
	}

	public void ButtonUpSurrender()
	{
	}

	public void ButtonDownSurrender()
	{
	}

	private void RaceCompleteSequence()
	{
	}

	private void ExitGame()
	{
	}

	private void AdjustHeroCarCondition()
	{
	}

	private string GetTrackingStringForRaceStyle()
	{
		return null;
	}

	public void SpeedWarningGiven()
	{
	}

	public void ActivateBreakdown(MiniGameCarRaceVehicle vehicle)
	{
	}

	public void ActivateCrash(MiniGameCarRaceVehicle vehicle)
	{
	}

	public void MoveVehicleCompletionHandler(MiniGameCarRaceVehicle vehicle)
	{
	}

	public void TriggerNextCop()
	{
	}

	public void TriggerRandomCop()
	{
	}

	public void CaughtByCop()
	{
	}

	private void InitMusic()
	{
	}

	public void StartMusic()
	{
	}

	private void StopMusic()
	{
	}

	public void SetMusicVolume(float volume)
	{
	}
}
