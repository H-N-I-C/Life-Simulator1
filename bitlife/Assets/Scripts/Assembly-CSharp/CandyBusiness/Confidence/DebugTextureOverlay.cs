using UnityEngine;
using UnityEngine.UI;

namespace CandyBusiness.Confidence
{
	public class DebugTextureOverlay : MonoBehaviour
	{
		private const string GO_NAME = "[DebugTextureOverlay]";

		private static DebugTextureOverlay _instance;

		private RawImage _rawImage;

		private Text _headerLabel;

		private Texture2D _ownedTexture;

		public static void Show(Texture2D texture, string header = null, bool takeOwnership = true)
		{
		}

		public static void Hide()
		{
		}

		private static void EnsureInstance()
		{
		}

		private void Build()
		{
		}

		private void Display(Texture2D texture, string header, bool takeOwnership)
		{
		}

		private void OnDestroy()
		{
		}

		private static RectTransform CreateChild(string name, Transform parent)
		{
			return null;
		}

		private static void StretchFull(RectTransform rect)
		{
		}
	}
}
