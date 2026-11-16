using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiniGameFightingBetting
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static TweenCallback _003C_003E9__29_0;

		internal void _003CReactWithDodge_003Eb__29_0()
		{
		}
	}

	private sealed class _003CDelayedBettingFinish_003Ed__35 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public MiniGameFightingBetting _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedBettingFinish_003Ed__35(int _003C_003E1__state)
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

	private sealed class _003CDelayedPlayNextTurn_003Ed__34 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public MiniGameFightingBetting _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedPlayNextTurn_003Ed__34(int _003C_003E1__state)
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

	private sealed class _003CDelayedTextThrob_003Ed__36 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float delay;

		public MiniGameFightingBetting _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CDelayedTextThrob_003Ed__36(int _003C_003E1__state)
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

	public const float BettingStartingRadius = 412f;

	public const float BettingFighter1StartDegrees = 157.5f;

	public const float BettingFighter2StartDegrees = 337.5f;

	private readonly MiniGameFighting _miniGame;

	private readonly MiniGameFightingFighter _fighter1;

	private readonly MiniGameFightingFighter _fighter2;

	private MiniGameFightingFighter _sourceFighter;

	private MiniGameFightingFighter _destFighter;

	private readonly List<MiniGameFightingBettingTurn> _bettingFightScript;

	private int _bettingScriptIndex;

	private MiniGameFightingBettingTurn _bettingCurrentTurn;

	private float _bettingTurnRunningDuration;

	private MiniGameFightingFighter _bettingWinningFighter;

	private float _bettingAttackDuration;

	private readonly SimFightClubBettingMatch _match;

	private readonly MiniGameFightingTextDisplay _textDisplay;

	private SOUND _lastPainSound;

	private const int BettingFightingRadius = 240;

	private const float BettingFighterScale = 0.475f;

	public MiniGameFightingBetting(MiniGameFighting minigame, MiniGameFightingFighter fighter1, MiniGameFightingFighter fighter2, MiniGameFightingTextDisplay textDisplay)
	{
	}

	private void PlayAnimationFighterRotateDegrees(MiniGameFightingFighter fighter, int addedDegrees, float duration)
	{
	}

	public void MoveFightersToCenter()
	{
	}

	private void Attack()
	{
	}

	private bool IsLastTurn()
	{
		return false;
	}

	private void ReactToAttack()
	{
	}

	private void Knockout()
	{
	}

	private void Finish()
	{
	}

	private void ReactToHit()
	{
	}

	private void ReactWithBlock()
	{
	}

	private void ReactWithDodge()
	{
	}

	private void SwivelFighters()
	{
	}

	private void PlayNextTurn()
	{
	}

	private void BuildTurnScript()
	{
	}

	private void AssignHpToTurns(int fighter1HitCount, int fighter2HitCount)
	{
	}

	private IEnumerator DelayedPlayNextTurn(float delay)
	{
		return null;
	}

	private IEnumerator DelayedBettingFinish(float delay)
	{
		return null;
	}

	private IEnumerator DelayedTextThrob(float delay)
	{
		return null;
	}

	private SOUND GetPainSoundForGender(GENDER gender)
	{
		return SOUND.NONE;
	}

	public static Vector2 DegreesToPoint(Vector2 center, float radius, float degrees)
	{
		return default;
	}

	private static float PointToDegrees(Vector2 center, Vector2 point)
	{
		return 0f;
	}

	private static Vector2 GetPointPastLine(Vector2 pointA, Vector2 pointB, float distance)
	{
		return default;
	}

	private static float DistanceBetweenRects(Rect a, Rect b)
	{
		return 0f;
	}

	private void _003CMoveFightersToCenter_003Eb__21_0()
	{
	}

	private void _003CKnockout_003Eb__25_0()
	{
	}

	private void _003CReactToHit_003Eb__27_0()
	{
	}
}
