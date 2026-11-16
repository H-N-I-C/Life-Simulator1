using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyScrollerOptions : IguiOptions
	{
		private const bool HORIZONTAL_SCROLL_IS_ALLOWED = true;

		private const bool VERTICAL_SCROLL_IS_ALLOWED = true;

		private const bool IS_SHOWING_SCROLLBARS = true;

		public Vector2 size;

		public GameObject content;

		public Color backgroundColor;

		public bool horizontalScrollIsAllowed;

		public bool verticalScrollIsAllowed;

		public bool isShowingScrollbars;

		public CandyScrollerOptions(Vector2 size, GameObject content, Color backgroundColor, bool horizontalScrollIsAllowed = true, bool verticalScrollIsAllowed = true, bool isShowingScrollbars = true)
		{
		}

		public Vector2 GetSize()
		{
			return default;
		}

		public bool HasTexture()
		{
			return false;
		}
	}
}
