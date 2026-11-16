using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RouletteBetItem : MonoBehaviour
{
	private RectTransform _rectTransform;

	private Button _button;

	private Image _borderImage;

	private Image _backgroundImage;

	private TMP_Text _text;

	private RouletteBet Bet__BackingField;

	public Action<RouletteBetItem> OnClicked;

	public RectTransform RectTransform => null;

	public RouletteBet Bet
	{
		get
		{
			return Bet__BackingField;
		}
		private set
		{
			Bet__BackingField = value;
		}
	}

	public void ButtonDown()
	{
	}

	public void ButtonUp()
	{
	}

	public void Init(RouletteBet bet, float borderSize, string overrideDescription = "")
	{
	}

	public void SetSelected(bool selected)
	{
	}
}
