using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;

public class MiniGameEarthDefense : MonoBehaviour, IguiObject
{
	public enum EARTH_DEFENSE_RESULT
	{
		WIN = 0,
		LOSE = 1
	}

	private sealed class _003CDamageEffectThenExit_003Ed__69 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public MiniGameEarthDefense _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDamageEffectThenExit_003Ed__69(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
		}
	}

	private RectTransform rect;

	private bool initialized;

	private int successfulHitCount;

	private int requiredHits;

	private bool gameOver;

	private GameObject dimmedOverlayObject;

	private GameObject maskObject;

	private GameObject shipObject;

	private GameObject earthObject;

	private GameObject timerObject;

	private GameObject asteroidObject;

	private Scenario scenario;

	private float theWidth;

	private float theHeight;

	private float rightEdge;

	private float topEdge;

	private float bottomEdge;

	private ArrayList bulletPool;

	private ArrayList firedBullets;

	private ArrayList bulletsToRemove;

	private float bulletPixelSteps;

	private float shipPixelSteps;

	private float asteroidPixelSteps;

	private float targetFrameRate;

	private float multiplier;

	private float scale;

	private float ratio;

	private MiniGameDataEarthDefense gameData;

	private int obstacleClearedSoundIndex;

	private int lastObstaclePositionType;

	private int lastObstacleType;

	private int obstaclesSinceLastCenterPosition;

	private bool wonGame;

	private float shipPixels;

	private int asteroidPhase;

	private int hitRequirement1;

	private int hitRequirement2;

	private int hitRequirement3;

	private int hitRequirement4;

	private int hitRequirement5;

	private int totalHitRequirement;

	private bool allowMovement;

	private int currentHitRequirement;

	private float physicsRatio;

	private float tabletPhysicsRatio;

	private Rigidbody2D shipBody;

	private float power;

	private float thrustMult;

	private float targetGravity;

	private double timer;

	private double startTime;

	private Vector2 savedTimerPosition;

	private void Update()
	{
	}

	public void Constructor(IguiOptions boxedOptions)
	{
	}

	public void InitWithParameters(float desiredWidth, Scenario theScenario)
	{
	}

	private void IncrementAsteroidPhase()
	{
	}

	private void PlayerSpinOut()
	{
	}

	private void UpdateTimer()
	{
	}

	private void StartGame()
	{
	}

	public void ShipHit()
	{
	}

	public void HitDetected(GameObject bullet)
	{
	}

	private void ReturnBulletToPool(GameObject bullet)
	{
	}

	private void FireBullet()
	{
	}

	private void DelayedInitialized()
	{
	}

	private void WinGame()
	{
	}

	private void LoseGame()
	{
	}

	private void Cleanup()
	{
	}

	private void DelayedExitGame()
	{
	}

	private IEnumerator DamageEffectThenExit()
	{
		return null;
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
