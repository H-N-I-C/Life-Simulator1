using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyImage : MonoBehaviour, IguiObject
	{
		private RectTransform rect;

		private Image image;

		private Vector4 border;

		private Vector2 size;

		private Color color;

		private Texture2D texture;

		private bool isSeenAsGrayscale;

		public CandyImageOptions options;

		private bool colorIsSet;

		private bool sizeHasBeenSet;

		public RectTransform Rect => null;

		private void OnDestroy()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public bool IsMouseOnImage()
		{
			return false;
		}

		public void TurnOffImage()
		{
		}

		public void TurnOnImage()
		{
		}

		public void SetAlphaThreshold(float value)
		{
		}

		public void AddOffset(float heightOffset, float anchoredPositionOffset)
		{
		}

		public float GetPosX()
		{
			return 0f;
		}

		public Color GetColor()
		{
			return default;
		}

		public Color GetImageColor()
		{
			return default;
		}

		public Texture2D GetTexture()
		{
			return null;
		}

		public Sprite GetSprite()
		{
			return null;
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public bool HasTexture()
		{
			return false;
		}

		public void SetHorizontalPos(float posX)
		{
		}

		public void SetVerticalPos(float posY)
		{
		}

		public void SetPos(float posX, float posY)
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void SetColor(Color color)
		{
		}

		public void SetSprite(Sprite sprite)
		{
		}

		public void SetAlpha(float alpha)
		{
		}

		public void SetImageType(Image.Type imageType)
		{
		}

		public void SetTexture(Texture2D texture)
		{
		}

		public void SetBorder(Vector4 border)
		{
		}

		public void SetIsSeenAsGrayscale(bool isSeenAsGrayscale)
		{
		}

		public void ApplyChanges()
		{
		}

		private Color ConvertToGrayscale(Color color)
		{
			return default;
		}

		public void SetHeightConstraint()
		{
		}
	}
}
