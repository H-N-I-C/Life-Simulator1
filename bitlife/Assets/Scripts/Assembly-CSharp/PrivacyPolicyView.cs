using System;
using CandywriterPolicy;
using UnityEngine;

public class PrivacyPolicyView : MonoBehaviour
{
	private const float ANIM_DUR = 0.24f;

	public static PrivacyPolicyView Inst;

	private PrivacyPolicyWindow window;

	private PrivacyPolicyFadeImage shade;

	private TextAsset androidText;

	private TextAsset iOSText;

	public PrivacyPolicyWindow Window => null;

	private void Awake()
	{
	}

	public void Show(string title, string message, Action callback = null)
	{
	}

	public void Show(Action callback = null)
	{
	}

	public void Reveal()
	{
	}

	public void Remove()
	{
	}
}
