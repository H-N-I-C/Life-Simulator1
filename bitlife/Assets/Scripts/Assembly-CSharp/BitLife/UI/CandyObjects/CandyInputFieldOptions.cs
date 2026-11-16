using BitLife.UI.Interfaces;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyInputFieldOptions : IguiOptions
	{
		public Vector2 size;

		public CandyTextOptions startingTextOptions;

		public CandyTextOptions placeholderTextOptions;

		public CandyTextOptions titleOptions;

		public Color backgroundColor;

		public Color frameColor;

		public bool acceptsInput;

		public Delegates.InputFieldEndEditDelegate delegateMethod;

		public Delegates.InputFieldEndEditDelegateIndexed delegateMethodIndexed;

		public Delegates.InputFieldSumbitEditDelegate delegateMethodSubmit;

		public string[] randomContent;

		public CandyTextOptions randomTitleOptions;

		public bool isPlaceholder;

		public int listIndex;

		public ScenarioInputField scenarioInputField;

		public CandyInputFieldOptions(Vector2 size, CandyTextOptions startingText, CandyTextOptions placeholderTextOptions, CandyTextOptions titleOptions, Color backgroundColor, Color frameColor, string[] randomContent = null, CandyTextOptions randomTitleOptions = null, bool acceptsInput = false, Delegates.InputFieldEndEditDelegate delegateMethod = null, bool isPlaceholder = false, int listIndex = -1, Delegates.InputFieldEndEditDelegateIndexed delegateMethodIndexed = null, ScenarioInputField scenarioInputField = null, Delegates.InputFieldSumbitEditDelegate delegateMethodSubmit = null)
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
