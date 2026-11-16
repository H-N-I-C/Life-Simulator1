using System;
using UnityEngine;
using UnityEngine.Events;

namespace BitLife.UI.CandyObjects
{
	public class CandyButtonNoEventSystem : MonoBehaviour
	{
		private Action onClickMethod;

		private UnityAction<int> onClickMethodIndexed;

		private int choiceIndex;

		private Func<bool> CanClick__BackingField;

		public Func<bool> CanClick
		{
			get
			{
				return CanClick__BackingField;
			}
			set
			{
				CanClick__BackingField = value;
			}
		}

		private void Update()
		{
		}

		private void RunOnClickLogic()
		{
		}

		public void SetOnClickDelegateMethod(Action method)
		{
		}

		public void SetOnClickDelegateMethod(UnityAction<int> method)
		{
		}

		public void SetOnClickDelegateMethod(UnityAction<int> method, int index = -1)
		{
		}

		public void SetOnClickDelegateMethodChoiceIndex(int index)
		{
		}
	}
}
