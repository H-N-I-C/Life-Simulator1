using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TranslatorIndicator : MonoBehaviour
{
	private sealed class Lease : IDisposable
	{
		private TranslatorIndicator owner;

		public Lease(TranslatorIndicator owner)
		{
		}

		public void Dispose()
		{
		}
	}

	public static TranslatorIndicator Inst;

	public static UnityEvent<bool> OnTranslationIndicatorChanged;

	private GameObject _babyElement;

	private GameObject _circleElement;

	private Image _blockImage;

	private Material _blurMaterial;

	private bool _enableBlur;

	private Color _lBabyColor;

	private Color _lCircleColor;

	private Color _lBlockColor;

	private Color _dBabyColor;

	private Color _dCircleColor;

	private Color _dBlockColor;

	private Color _gBabyColor;

	private Color _gCircleColor;

	private Color _gBlockColor;

	private static int _lockCount;

	private void Awake()
	{
	}

	public void UpdateTheme()
	{
	}

	public IDisposable Acquire()
	{
		return null;
	}

	public void Show()
	{
	}

	public void Hide()
	{
	}

	private void ThrottleGame(bool throttle)
	{
	}
}
