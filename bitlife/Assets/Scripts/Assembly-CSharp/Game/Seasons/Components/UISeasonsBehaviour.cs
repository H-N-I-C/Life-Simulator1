using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class UISeasonsBehaviour : MonoBehaviour
	{
		[Serializable]
		public class ThemeData
		{
			public THEME theme;
		}

		[Serializable]
		public class ThemeSprite : ThemeData
		{
			public Sprite sprite;
		}

		[Serializable]
		public class ThemeColor : ThemeData
		{
			public Color color;
		}

		protected List<ThemeColor> themeColors;

		protected List<ThemeColor> themeTextColors;

		protected List<ThemeSprite> themeBackSprites;

		protected THEME theme;

		public void SetTheme(THEME theme)
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void OnDestroy()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void ApplyTheme()
		{
		}

		protected void ApplyTheme(CandyButton button)
		{
		}

		protected void ApplyTheme(Image image)
		{
		}

		protected void ApplyThemeTextColor(MaskableGraphic graphic)
		{
		}

		protected void ApplyThemeColor(MaskableGraphic graphic)
		{
		}

		private bool _003CApplyTheme_003Eb__10_0(ThemeSprite i)
		{
			return false;
		}

		private bool _003CApplyThemeTextColor_003Eb__11_0(ThemeColor i)
		{
			return false;
		}

		private bool _003CApplyThemeColor_003Eb__12_0(ThemeColor i)
		{
			return false;
		}
	}
}
