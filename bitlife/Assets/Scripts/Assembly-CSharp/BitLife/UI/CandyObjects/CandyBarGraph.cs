using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyBarGraph : MonoBehaviour, IguiObject
	{
		private RectTransform rect;

		private Image image;

		private RectTransform valueRect;

		private Image valueImage;

		private Image valueImageBuff;

		private float value;

		private RectTransform divisionParent;

		private CandyImage divisionTemplate;

		private BarGraphSourceType sourceType;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public float GetValue()
		{
			return 0f;
		}

		public void SetBubbleDivisions(int bubbleCount, Color dividerColor)
		{
		}

		public void SetHorizontalPos(float posX)
		{
		}

		public void SetVerticalPos(float posY)
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void Setup(ListItem data, int bubbleCount, Color dividerColor)
		{
		}

		public void SetValue(float value)
		{
		}

		private bool ShowValue(Image image, float x1, float x2)
		{
			return false;
		}

		public void SetBackgroundColor(Color color)
		{
		}

		public void SetValueBarColor(CandyBarGraphOptions options)
		{
		}

		public void SetValueBarColor(float value, float lowValueMax, float mediumValueMax, Color lowColor, Color mediumColor, Color highColor)
		{
		}

		public void SetValueBarSprite(float value, float lowValueMax, float mediumValueMax, Sprite lowSprite, Sprite medSprite, Sprite highSprite)
		{
		}

		public void SetHeightConstraint()
		{
		}
	}
}
