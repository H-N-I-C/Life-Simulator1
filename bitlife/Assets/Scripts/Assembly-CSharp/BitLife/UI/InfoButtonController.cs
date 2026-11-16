using System;
using System.Collections.Generic;
using UnityEngine;

namespace BitLife.UI
{
	public class InfoButtonController : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public string name;

			internal bool _003CRemoveButton_003Eb__0(GameObject t)
			{
				return false;
			}

			internal bool _003CRemoveButton_003Eb__1(GameObject t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass11_0
		{
			public string seasonsWelcome;

			internal bool _003CIsButtonOnScreen_003Eb__0(GameObject t)
			{
				return false;
			}
		}

		private static InfoButtonController _instance;

		public static Action InfoButtonControllerReady;

		private Transform buttonParent;

		private List<GameObject> _buttons;

		public static bool IsReady => false;

		private static void Init()
		{
		}

		private void Start()
		{
		}

		public static void AddButton(string name)
		{
		}

		public static void AddButton(string name, string resourcePath)
		{
		}

		public static void RemoveButton(string name)
		{
		}

		public static bool IsButtonOnScreen(string seasonsWelcome)
		{
			return false;
		}
	}
}
