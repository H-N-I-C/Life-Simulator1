using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

namespace DG.Tweening
{
	public static class DOTweenModuleUI
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public CanvasGroup target;

			internal float _003CDOFade_003Eb__0()
			{
				return 0f;
			}

			internal void _003CDOFade_003Eb__1(float x)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public Graphic target;

			internal Color _003CDOFade_003Eb__0()
			{
				return default;
			}

			internal void _003CDOFade_003Eb__1(Color x)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass3_0
		{
			public Image target;

			internal Color _003CDOColor_003Eb__0()
			{
				return default;
			}

			internal void _003CDOColor_003Eb__1(Color x)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_0
		{
			public Image target;

			internal Color _003CDOFade_003Eb__0()
			{
				return default;
			}

			internal void _003CDOFade_003Eb__1(Color x)
			{
			}
		}

		public static TweenerCore<float, float, FloatOptions> DOFade(CanvasGroup target, float endValue, float duration)
		{
			return null;
		}

		public static TweenerCore<Color, Color, ColorOptions> DOFade(Graphic target, float endValue, float duration)
		{
			return null;
		}

		public static TweenerCore<Color, Color, ColorOptions> DOColor(Image target, Color endValue, float duration)
		{
			return null;
		}

		public static TweenerCore<Color, Color, ColorOptions> DOFade(Image target, float endValue, float duration)
		{
			return null;
		}
	}
}
