using BitLife.UI.CandyObjects;
using BitLife.UI.Containers;
using Game.Seasons.Models;
using TMPro;
using UnityEngine;

namespace Game.Seasons.Components
{
	public sealed class UIDailyLifeRewardButton : MonoBehaviour
	{
		private sealed class ButtonState
		{
			public readonly LabelState First;

			public readonly LabelState Second;

			public readonly IconState Claim;

			public readonly IconState Limit;

			public ButtonState(CandyText first, CandyText second, CandyImage claim, CandyImage limit)
			{
			}

			public void Restore()
			{
			}
		}

		private sealed class LabelState
		{
			public readonly TextMeshProUGUI Label;

			private readonly string _text;

			private readonly float _fontSize;

			private readonly Color _color;

			private readonly Vector2 _position;

			private readonly bool _active;

			private readonly bool _wrapping;

			private readonly bool _raycast;

			private readonly float _characterSpacing;

			public LabelState(TextMeshProUGUI label)
			{
			}

			public void Restore()
			{
			}
		}

		private sealed class IconState
		{
			public readonly CandyImage Icon;

			private readonly Sprite _sprite;

			private readonly Vector2 _size;

			private readonly Vector2 _position;

			private readonly Color _color;

			private readonly bool _active;

			private readonly bool _preserveAspect;

			private readonly bool _raycast;

			public IconState(CandyImage icon)
			{
			}

			public void Restore()
			{
			}
		}

		private UIDarklyButton _button;

		private ButtonState _normal;

		private ButtonState _pressed;

		private Sprite _currency;

		private bool _rendered;

		private bool _artApplied;

		private Vector2 _nativeButtonSize;

		public bool HasCustomLayout => false;

		public void Render(CandyButton button, DailyLifeCompletionStateModel state, bool showDailyProgress, bool canClaim, bool loading)
		{
		}

		private void RenderState(ButtonState view, DailyLifeCompletionStateModel state, bool claim, bool progress, bool loading)
		{
		}

		private static void SetLabel(TextMeshProUGUI label, float fontSize, float y, Color color)
		{
		}

		private void SetCurrencyLine(TextMeshProUGUI label, CandyImage icon, string prefix, string amount, float iconSize, float y)
		{
		}

		private void SetTrailingCurrencyLine(TextMeshProUGUI label, CandyImage icon, string text, float iconSize, float y)
		{
		}

		private void SetCurrencyIcon(CandyImage icon, float iconSize, float x, float y)
		{
		}
	}
}
