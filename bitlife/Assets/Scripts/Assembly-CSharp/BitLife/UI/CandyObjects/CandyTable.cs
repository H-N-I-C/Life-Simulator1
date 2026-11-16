using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.CandyObjects
{
	public class CandyTable : MonoBehaviour, IguiObject
	{
		private sealed class _003CTimer_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyTable _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTimer_003Ed__19(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private RectTransform rect;

		private VerticalLayoutGroup verticalLayoutGroup;

		private List<GameObject> entries;

		private KeyValuePair<string, string> firstPair;

		private CandyText countdownEntry;

		private bool isCountdown;

		private bool hasStarted;

		private int count;

		private Action<int> delegateCallbackMethodForInt;

		private Action delegateCallbackMethod;

		private CandyTableOptions cachedOptions;

		private Coroutine timer;

		public void UpdateWithoutAlignmentShift(IguiOptions boxedOptions)
		{
		}

		private bool TryExtractHexColor(string text, out string hexColor)
		{
			hexColor = null;
			return false;
		}

		private void ColorizeString(KeyValuePair<string, string> pair, bool recognizeNegativeCurrency, bool useValueColorForKeyColor, out string valueString, out string keyString, out bool wasRecognizedAsSignedCurrency)
		{
			valueString = null;
			keyString = null;
			wasRecognizedAsSignedCurrency = default;
		}

		private void Start()
		{
		}

		private void OnDisable()
		{
		}

		private void StopTimer()
		{
		}

		private void LaunchTimer()
		{
		}

		private IEnumerator Timer()
		{
			return null;
		}

		public void Constructor(IguiOptions boxedOptions)
		{
		}

		public void RebuildLayout()
		{
		}

		public Vector2 GetActualSize()
		{
			return default;
		}

		public void SetWidth(float width)
		{
		}

		public void SetInsets(int horizontalInset)
		{
		}

		public void SetHeightConstraint()
		{
		}

		public void SetSize(Vector2 size)
		{
		}

		public void UpdateValues(Dictionary<string, string> tableEntryPairs)
		{
		}
	}
}
