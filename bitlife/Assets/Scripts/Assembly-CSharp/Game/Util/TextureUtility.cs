using UnityEngine;

namespace Game.Util
{
	public static class TextureUtility
	{
		public const int WARN_BASE64_LENGTH = 1000000;

		public static string ToBase64Png(Texture2D texture, int maxDimension = 0)
		{
			return null;
		}

		public static string ToBase64Jpg(Texture2D texture, int quality = 75, int maxDimension = 0)
		{
			return null;
		}

		private static string Encode(Texture2D texture, bool isJpg, int jpgQuality, int maxDimension, string label)
		{
			return null;
		}

		public static bool FromBase64Png(string base64, out Texture2D texture)
		{
			texture = null;
			return false;
		}

		private static string SanitizeBase64(string raw)
		{
			return null;
		}

		private static Texture2D GetEncodableTexture(Texture2D texture, int maxDimension, out bool madeTemp)
		{
			madeTemp = default;
			return null;
		}
	}
}
