using System;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class MiniGameCarRaceVehicle : MonoBehaviour
{
	private bool IsHeroCar__BackingField;

	private bool IsCop__BackingField;

	private int CurrentLap__BackingField;

	private SimRaceCar RaceCar__BackingField;

	private bool Crashed__BackingField;

	private bool CaughtByPolice__BackingField;

	private bool HadBreakdown__BackingField;

	private bool CopOnDuty__BackingField;

	private bool FinishedRace__BackingField;

	private bool VictoryLapInProgress__BackingField;

	private Stopwatch LapTimer__BackingField;

	private TimeSpan RaceTime__BackingField;

	private bool MarkedForCollision__BackingField;

	private MiniGameCarRaceVehicle CollisionPartner__BackingField;

	private int DurabilityPoints__BackingField;

	private int DemolitionKills__BackingField;

	private int _demolitionHitsTaken;

	private AudioController _sound;

	private bool _breakdownInProgress;

	private int _numWarningsAllowed;

	private int _riskEventCount;

	private float _driverSkill;

	private float _duration;

	private float _currentTime;

	private float _riskEventCountResetTime;

	private bool _riskEventResetTimerActive;

	private int _beginningOfCurrentRiskAreaIndex;

	private int _beginningOfNextRiskAreaIndex;

	private int _riskEventWarningIndex;

	private Vector2 _startPos;

	private Vector2 _endPos;

	private MiniGameCarRace _miniGame;

	private List<MiniGameCarRaceTrackPointData> _trackPointsData;

	private int _currentTrackIndex;

	private float _currentSpeed;

	private int _maxLaps;

	private float _laneSpeedMult;

	private float _testLaneDistance;

	private float _laneDistance;

	private float _trackMaxSpeedDuration;

	private bool _gasPedalDown;

	private List<GameObject> _radarMarkers;

	private float _speedRequestAlteration;

	private float _npcSpeedDecreaseRequestIncrement;

	private float _npcSpeedDecreaseRequestTargetSpeed;

	private bool _npcSpeedDecreaseRequestActive;

	private float _npcSpeedIncreaseRequestTargetSpeed;

	private bool _npcSpeedIncreaseRequestActive;

	private float _heroSpeedDecreaseIncrement;

	private float _heroSpeedRequestTargetSpeed;

	private const float _minimumSpeed = 0f;

	private const int _maxRadarDistance = 300;

	private const float _risk0AdvisedSpeed = 100f;

	private const float _risk1AdvisedSpeed = 75f;

	private const float _risk2AdvisedSpeed = 65f;

	private const float _risk3AdvisedSpeed = 55f;

	private const float _risk4AdvisedSpeed = 40f;

	private const float _npcNonLinearAccelerationTimerDelay = 0.1f;

	private const float _npcLinearAccelerationTimerDelay = 0.2f;

	private const float _heroDecelerationIncrement = 4f;

	private const float _heroAccelerationTimerDelay = 0.1f;

	private const float _heroDecelerationTimerDelay = 0.1f;

	private float _referenceDistanceReciprocal;

	private const float _heroLogTimeSegment = 3.3333333f;

	private const float _npcLogTimeSegment = 3.3333333f;

	private readonly float _maxLogVal;

	private readonly float _npcAcceleration;

	private float _targetFrameRate;

	private Sequence _throbSequence;

	private MiniGameCarRaceTrack _track;

	private bool _active;

	private int _lane;

	private int _potentialRiskEventCountdown;

	private bool _potentialRiskEventCountdownEnabled;

	private float _statMaxSpeed;

	private float _statAcceleration;

	private float _statHandling;

	private float _statDurability;

	private float _statCondition;

	private int _breakdownIndex;

	private float _breakdownDecelerationIncrement;

	private bool _breakdownAnimationPlaying;

	private float _angle;

	private bool _useLinearNPCAcceleration;

	private Rigidbody2D _rigidBody;

	private BoxCollider2D _boxCollider;

	private PolygonCollider2D _polyCollider;

	private Vector2 _crashVector;

	private GameObject _demolitionPointsObject;

	private readonly int _defaultSortOrder;

	private readonly bool _balancingMode;

	private float _startingCondition;

	private readonly int _maxDurabilityPoints;

	private Vector3 _defaultScale;

	private Sequence _advertiseCarSequence;

	private bool _temporaryCrashAlreadyHappened;

	private bool _temporarySpinOutActive;

	private bool _isWinningBetDriver;

	private float _originalLaneSpeedMult;

	private bool _betCrash;

	private int _betCrashCountdown;

	private float _moveSpeed;

	public bool IsHeroCar
	{
		get
		{
			return IsHeroCar__BackingField;
		}
		set
		{
			IsHeroCar__BackingField = value;
		}
	}

	public bool IsCop
	{
		get
		{
			return IsCop__BackingField;
		}
		set
		{
			IsCop__BackingField = value;
		}
	}

	public int CurrentLap
	{
		get
		{
			return CurrentLap__BackingField;
		}
		set
		{
			CurrentLap__BackingField = value;
		}
	}

	public SimRaceCar RaceCar
	{
		get
		{
			return RaceCar__BackingField;
		}
		set
		{
			RaceCar__BackingField = value;
		}
	}

	public bool Crashed
	{
		get
		{
			return Crashed__BackingField;
		}
		set
		{
			Crashed__BackingField = value;
		}
	}

	public bool CaughtByPolice
	{
		get
		{
			return CaughtByPolice__BackingField;
		}
		set
		{
			CaughtByPolice__BackingField = value;
		}
	}

	public bool HadBreakdown
	{
		get
		{
			return HadBreakdown__BackingField;
		}
		set
		{
			HadBreakdown__BackingField = value;
		}
	}

	public bool CopOnDuty
	{
		get
		{
			return CopOnDuty__BackingField;
		}
		set
		{
			CopOnDuty__BackingField = value;
		}
	}

	public bool FinishedRace
	{
		get
		{
			return FinishedRace__BackingField;
		}
		set
		{
			FinishedRace__BackingField = value;
		}
	}

	public bool VictoryLapInProgress
	{
		get
		{
			return VictoryLapInProgress__BackingField;
		}
		set
		{
			VictoryLapInProgress__BackingField = value;
		}
	}

	public Stopwatch LapTimer
	{
		get
		{
			return LapTimer__BackingField;
		}
		set
		{
			LapTimer__BackingField = value;
		}
	}

	public TimeSpan RaceTime
	{
		get
		{
			return RaceTime__BackingField;
		}
		set
		{
			RaceTime__BackingField = value;
		}
	}

	public bool MarkedForCollision
	{
		get
		{
			return MarkedForCollision__BackingField;
		}
		set
		{
			MarkedForCollision__BackingField = value;
		}
	}

	public MiniGameCarRaceVehicle CollisionPartner
	{
		get
		{
			return CollisionPartner__BackingField;
		}
		set
		{
			CollisionPartner__BackingField = value;
		}
	}

	public int DurabilityPoints
	{
		get
		{
			return DurabilityPoints__BackingField;
		}
		set
		{
			DurabilityPoints__BackingField = value;
		}
	}

	public int DemolitionKills
	{
		get
		{
			return DemolitionKills__BackingField;
		}
		set
		{
			DemolitionKills__BackingField = value;
		}
	}

	private void Update()
	{
	}

	private void OnDestroy()
	{
	}

	public void InitWithParameters(List<MiniGameCarRaceTrackPointData> trackPointsData, bool heroControlled, SimRaceCar raceCar, float driverSkill, int lane, MiniGameCarRace miniGame, bool isWinningBetDriver = false, bool copCar = false)
	{
	}

	private string GetHexColorForColor(string color)
	{
		return null;
	}

	private Sprite GetImageForRaceCar(SimRaceCar car)
	{
		return null;
	}

	private Color32 ConvertColorToColor32(Color pColor)
	{
		return default;
	}

	private void CombineTextures(Color32[] pData1, Color32[] pData2, Color32 pColor, bool pChangeColor)
	{
	}

	private int DurabilityRemaining()
	{
		return 0;
	}

	public float DurabilityPercentage()
	{
		return 0f;
	}

	private float ConvertStat(float stat)
	{
		return 0f;
	}

	private void SetAnchoredPosition(Vector2 anchoredPosition)
	{
	}

	private Vector2 GetAnchoredPosition()
	{
		return default;
	}

	public int GetCurrentIndex()
	{
		return 0;
	}

	public int GetFirstIndex()
	{
		return 0;
	}

	public int GetLastIndex()
	{
		return 0;
	}

	public float GetPercentageComplete()
	{
		return 0f;
	}

	private void UpdateMove()
	{
	}

	private int GetClosestIndexForPosition(Vector2 localPosition)
	{
		return 0;
	}

	private void PredictBreakdown()
	{
	}

	public bool DetectBreakdown()
	{
		return false;
	}

	public float GetCondition()
	{
		return 0f;
	}

	public float GetConditionChange()
	{
		return 0f;
	}

	private void ImpactCondition()
	{
	}

	public bool ShouldNotMove()
	{
		return false;
	}

	public void Crash()
	{
	}

	private void SpinOutAnimation()
	{
	}

	private void BreakdownAnimation()
	{
	}

	public void Breakdown()
	{
	}

	private void AdjustBreakdownSpeed()
	{
	}

	private float GetAdvisedSpeedForRisk(int risk)
	{
		return 0f;
	}

	private void Position(Vector2 position, float duration)
	{
	}

	private bool IndexOutOfRange(int index)
	{
		return false;
	}

	private MiniGameCarRaceTrackPointData GetDataForIndexOrNull(int index)
	{
		return null;
	}

	private Vector2 GetPositionForIndex(int index)
	{
		return default;
	}

	private int GetRiskForIndex(int index)
	{
		return 0;
	}

	public void MoveToNextPosition()
	{
	}

	private void SetCurrentSpeed(float newSpeed)
	{
	}

	public float GetCurrentSpeed()
	{
		return 0f;
	}

	private Vector2 GetNextPosition()
	{
		return default;
	}

	private Vector2 GetPreviousPosition()
	{
		return default;
	}

	public int GetNextIndex()
	{
		return 0;
	}

	private int GetPreviousIndex()
	{
		return 0;
	}

	private int GetRiskForNextPosition()
	{
		return 0;
	}

	private int GetRiskForCurrentPosition()
	{
		return 0;
	}

	private int GetIndexForDistanceAhead(float distance)
	{
		return 0;
	}

	public int GetIndexForDistanceBehind(float distance)
	{
		return 0;
	}

	private bool IsIndexBetweenTwoIndexesInclusive(int targetIndex, int startingIndex, int endingIndex)
	{
		return false;
	}

	public float GetDistanceBetweenIndexes(int startIndex, int endIndex)
	{
		return 0f;
	}

	public int GetRiskForFutureIndex(int futureIndex)
	{
		return 0;
	}

	private int GetNextIndexAfter(int index)
	{
		return 0;
	}

	private int GetPreviousIndexBefore(int index)
	{
		return 0;
	}

	private void SetAngle(Vector2 startPt, Vector2 endPt, float duration)
	{
	}

	private float GetDuration(Vector2 pointA, Vector2 pointB, float speed)
	{
		return 0f;
	}

	private float CalculateAngle(Vector2 pointA, Vector2 pointB)
	{
		return 0f;
	}

	private int GetNumberOfIndexesBetweenIndexes(int startIndex, int endIndex)
	{
		return 0;
	}

	public void ResetCop()
	{
	}

	public void RelaunchCop()
	{
	}

	private bool EqualSpeeds(float speed1, float speed2)
	{
		return false;
	}

	private void CopStop(MiniGameCarRaceVehicle stoppedCar)
	{
	}

	private void DemolitionSpinOutSequence()
	{
	}

	private void RiskRelatedTemporaryCrashSpinOutSequence()
	{
	}

	private void ResumeFromTemporaryCrash()
	{
	}

	private void Flash(bool endDifferent, float duration)
	{
	}

	private void AddInvulnerability(float duration)
	{
	}

	private void RemoveInvulnerability()
	{
	}

	private void ResetDemolitionDamageState()
	{
	}

	private void RadarCheck(ref int nextRisk, ref int indexOfRisk, ref float distanceToRisk, bool lookAheadOveride = false)
	{
	}

	private void DrawFullRadar()
	{
	}

	private void DemolitionCrash()
	{
	}

	private void ReduceSaturation()
	{
	}

	public void ImpactDurability()
	{
	}

	public void CollisionSpinOut()
	{
	}

	private void UpdateDemolitionPointsRing()
	{
	}

	private void DrawRadarToIndex(int endIndex, Color color)
	{
	}

	public float GetDurability()
	{
		return 0f;
	}

	private void AddRadarMarker(Vector2 pos, Color color)
	{
	}

	private void RemoveAllRadarMarkers()
	{
	}

	public bool DetectRiskEvent()
	{
		return false;
	}

	public void PostRiskAreaCheck()
	{
	}

	public void Advertise(bool advertise)
	{
	}

	public void NPCRequestSpeedIncrease(float accelerationOveride = 0f)
	{
	}

	public void NPCIncreaseSpeedTimer()
	{
	}

	private void NPCRequestSpeedDecrease(float newSpeed, int destinationIndex)
	{
	}

	private void NPCSpeedAdjustment()
	{
	}

	public void NPCMaxSpeedOverride(float newSpeed)
	{
	}

	public void HeroPressGasPedal(float power)
	{
	}

	public void HeroAdjustSpeedTimer()
	{
	}

	private float GetSpeedForLogArgument(float logArgument)
	{
		return 0f;
	}

	private float GetLogArgumentForSpeed(float speed)
	{
		return 0f;
	}

	private void InitSound()
	{
	}

	private void KillSound()
	{
	}

	public void StartSound(float speed = 1f)
	{
	}

	private void StopSound()
	{
	}

	private void SetSoundSpeed(float speed)
	{
	}

	public void SetSoundVolume(float volume)
	{
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
	}

	private void OnCollisionStay2D(Collision2D collision)
	{
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
	}

	private void DisableCollisions()
	{
	}

	private void EnableCollisions()
	{
	}
}
