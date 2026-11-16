using System;
using UnityEngine;

public class PennyataClick : MonoBehaviour
{
	private Animator _animator;

	private AudioClip[] clickSFX;

	private AudioClip clickSFXEnd;

	private AudioSource audioSource;

	private const int _clicksRequired = 7;

	private int _clicks;

	private Action OnFinalClick__BackingField;

	public Action OnFinalClick
	{
		get
		{
			return OnFinalClick__BackingField;
		}
		set
		{
			OnFinalClick__BackingField = value;
		}
	}

	private void Start()
	{
	}

	public void Bounce()
	{
	}

	private void SwitchToAnotherAnimation()
	{
	}

	private void EndFlow()
	{
	}

	private void PlayClickSound()
	{
	}
}
