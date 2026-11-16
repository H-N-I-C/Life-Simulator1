using System.Runtime.InteropServices;
using GGS.Util;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameCard : MonoBehaviour
{
	public enum CARD_HIGHLIGHT_MODE
	{
		NONE = 0,
		SELECTED_BY_PLAYER = 1,
		SELECTED_BY_DEALER = 2,
		SELECTED_BY_BOTH = 3
	}

	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Joker
	{
	}

	public struct NormalCard
	{
		public CARD_SUIT Suit;

		public CARD Type;
	}

	private sealed class _003C_003Ec__DisplayClass49_0
	{
		public MiniGameCard _003C_003E4__this;

		public bool revealed;

		internal void _003CSetRevealed_003Eb__0()
		{
		}
	}

	private bool Revealed__BackingField;

	private Either<NormalCard, Joker> Card__BackingField;

	private float Duration__BackingField;

	private float CurrentTime__BackingField;

	private CARD_HIGHLIGHT_MODE HighlightMode__BackingField;

	private GameObject _back;

	private MiniGameCardFront _front;

	private Image _highlight;

	private Sprite _cardSelectedByPlayer;

	private Sprite _cardSelectedByDealer;

	private Sprite _cardSelectedByBoth;

	private Vector3 _startPos;

	private Vector3 _endPos;

	private Vector3 _startScale;

	private Vector3 _endScale;

	private Vector3 _originalScale;

	private bool _moveActive;

	private bool _scaleActive;

	private bool _reverse;

	private bool _alphaActive;

	private float _startAlpha;

	private float _endAlpha;

	private RectTransform _rectTransform;

	private CanvasGroup _canvasGroup;

	public bool Revealed
	{
		get
		{
			return Revealed__BackingField;
		}
		protected set
		{
			Revealed__BackingField = value;
		}
	}

	public Either<NormalCard, Joker> Card
	{
		get
		{
			return Card__BackingField;
		}
		protected set
		{
			Card__BackingField = value;
		}
	}

	public float Duration
	{
		get
		{
			return Duration__BackingField;
		}
		protected set
		{
			Duration__BackingField = value;
		}
	}

	public float CurrentTime
	{
		get
		{
			return CurrentTime__BackingField;
		}
		protected set
		{
			CurrentTime__BackingField = value;
		}
	}

	public CARD_HIGHLIGHT_MODE HighlightMode
	{
		get
		{
			return HighlightMode__BackingField;
		}
		protected set
		{
			HighlightMode__BackingField = value;
		}
	}

	private void Awake()
	{
	}

	private void Update()
	{
	}

	public void InitWithParameters(Either<NormalCard, Joker> card)
	{
	}

	public void MoveTo(Vector2 position, float duration)
	{
	}

	public void ScaleTo(Vector3 scale, float duration, bool reverse)
	{
	}

	public void AlphaTo(float alpha, float duration)
	{
	}

	public void MakeVisible(bool revealed)
	{
	}

	public void SetRevealed(bool revealed, bool animated)
	{
	}

	public void SetHighlightMode(CARD_HIGHLIGHT_MODE highlightMode)
	{
	}

	protected void UpdateMove()
	{
	}

	protected void UpdateScale()
	{
	}

	protected void UpdateAlpha()
	{
	}
}
