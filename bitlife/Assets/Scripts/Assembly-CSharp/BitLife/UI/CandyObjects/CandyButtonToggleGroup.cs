using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI.CandyObjects
{
	public class CandyButtonToggleGroup : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public bool found;

			public CandyButtonToggle toggle;

			internal void _003CNotifyToggleOn_003Eb__0(CandyButtonToggle i)
			{
			}
		}

		private List<CandyButtonToggle> toggles;

		public bool NotifyToggleOn(CandyButtonToggle toggle)
		{
			return false;
		}
	}
}
