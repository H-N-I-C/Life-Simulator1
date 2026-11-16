using System;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandySlider : MonoBehaviour, IguiObject
	{
		private RectTransform rect;

		private Slider slider;

		private CandyImage thumbHandle;

		private CandyText title;

		private CandyText value;

		private Image line;

		private ScrollRect parentScrollRect;

		private CandySliderOptions.ReturnStringFromFloat delegateMethod_StringFromValue;

		private Action<float> delegateMethod_SetValueLabelUsingValue;

		private Action<CandySlider> poolCallback;

		private bool isPercentage;

		private bool hideValue;

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public string GetTitleText()
		{
			return null;
		}

		public void SetTitleText(string text)
		{
		}

		public void SetValueText(string text)
		{
		}

		public void SetTitleEmoji(Texture2D texture)
		{
		}

		public void SetOnSlideDelegateMethod(Action<float> callback)
		{
		}

		public void SetOnSlideDelegateMethod(CandySliderOptions.ReturnStringFromFloat callback)
		{
		}

		public void SetPoolCallback(Action<CandySlider> callback)
		{
		}

		public void SetThumbHandleScale()
		{
		}

		private float RecursivelyGetTotalScaleApplied(Transform child)
		{
			return 0f;
		}

		public void OnSlide(float sliderValue)
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

		public float SetSliderValue(float value)
		{
			return 0f;
		}

		public void SetHeightConstraint()
		{
		}

		public void SetWidth(float width)
		{
		}

		public void SetSize(Vector2 size)
		{
		}
	}
}
