using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyImageOptions : IguiOptions
	{
		private const float BORDER_INSET = 0f;

		private const bool IS_SEEN_AS_GRAYSCALE = false;

		private static readonly Color COLOR;

		public Texture2D texture;

		public Color color;

		public Vector2 size;

		public Vector4 border;

		public bool isSeenAsGrayscale;

		public CandyImageOptions(Texture2D texture)
		{
		}

		public CandyImageOptions(Texture2D texture, Vector2 size)
		{
		}

		public CandyImageOptions(Texture2D texture, Vector2 size, Color color, Vector4 border, bool isSeenAsGrayscale = false)
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
