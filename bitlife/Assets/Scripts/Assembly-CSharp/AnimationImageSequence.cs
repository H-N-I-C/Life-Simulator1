using System;
using UnityEngine;
using UnityEngine.UI;

public class AnimationImageSequence : MonoBehaviour
{
	private string spritePrefix;

	private int frameCount;

	private Image image;

	private float duration;

	private bool doAnimation;

	private bool loopOnlyOnce;

	private bool clearSpriteOnDisable;

	private float timeElapsed;

	private int currentFrame;

	private Action callback;

	public string SpritePrefix
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool ClearSpriteOnDisable
	{
		get
		{
			return false;
		}
		set
		{
		}
	}

	private void OnDisable()
	{
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void ResetAnimationState()
	{
	}

	private bool SetFrame(int frame)
	{
		return false;
	}

	private void GoToNextFrame()
	{
	}

	public void SetCallback(Action callback)
	{
	}

	public void StartAnimation()
	{
	}

	public void StartAnimationFromFirstFrame(string spritePrefix)
	{
	}

	public void StopAnimation()
	{
	}
}
