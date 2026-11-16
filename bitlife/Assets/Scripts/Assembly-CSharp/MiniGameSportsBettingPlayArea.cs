using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MiniGameSportsBettingPlayArea : MonoBehaviour
{
	private bool _initialized;

	private GameObject _colorBarTeamAObject;

	private GameObject _colorBarTeamBObject;

	private GameObject _emojiTeamAObject;

	private GameObject _emojiTeamBObject;

	private SimCasinoGameSportsBetting _gameData;

	private int _updateMinSteps;

	private int _updateCurrentSteps;

	private bool _teamsHaveLockedUp;

	private List<float> _moves;

	private float _halfEmojiWidth;

	private int _currentMoveCount;

	private float _playAreaWidth;

	private bool _gameActive;

	private float _moveUnit;

	private MiniGameSportsBetting _parent;

	private bool _lastPlayedPositiveDirection;

	private Sequence _losingTeamSequence;

	private Sequence _winningTeamSequence;

	private Sequence _waitingTeamASequence;

	private Sequence _waitingTeamBSequence;

	private Sequence _collisionSequence;

	private int _collisionTypeIndex;

	private DateTime _timeSinceLastCollisionSound;

	private bool _pushSoundPlayed;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(SimCasinoGameSportsBetting gameData, MiniGameSportsBetting parent)
	{
	}

	private void PlayEndPushSound()
	{
	}

	private void AddMove(int percentage, int currentPercentage)
	{
	}

	public void StartGame()
	{
	}

	private void PlayCollisionSound()
	{
	}

	public void WaitingAnimations()
	{
	}

	public void LoserAnimation()
	{
	}

	public void WinnerAnimation()
	{
	}

	public void KillWaitingSequence()
	{
	}

	public void KillSeqences()
	{
	}
}
