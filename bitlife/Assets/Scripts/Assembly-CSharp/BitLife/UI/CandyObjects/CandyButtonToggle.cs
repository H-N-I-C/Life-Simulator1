using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyButtonToggle : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass12_0
		{
			public bool isOn;

			internal void _003CUpdateToggleIsOn_003Eb__0(GameObject i)
			{
			}

			internal void _003CUpdateToggleIsOn_003Eb__1(GameObject i)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass15_0
		{
			public string text;

			internal void _003CSetText_003Eb__0(GameObject i)
			{
			}
		}

		private bool isOn;

		private List<GameObject> toggleTargetsOff;

		private List<GameObject> toggleTargetsOn;

		private CandyButtonToggleGroup toggleGroup;

		public Action<bool> OnValueChanged;

		private CandyButton buttonTarget => null;

		public bool IsOn
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		private void Awake()
		{
		}

		public void SetIsOnWithoutNotify(bool isOn)
		{
		}

		public void UpdateToggleIsOn(bool isOn)
		{
		}

		public void Toggle()
		{
		}

		public void SetText(string text)
		{
		}

		private void SetText(List<GameObject> parents, string text)
		{
		}

		private void Set(bool isOn, bool notifyValueChanged)
		{
		}
	}
}
