using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CandywriterPolicy
{
	public class PrivacyPolicyWindow : MonoBehaviour
	{
		private TextMeshProUGUI title;

		private TextMeshProUGUI message;

		private RectTransform content;

		private Button button;

		private Action localCallback;

		private void OnDestroy()
		{
		}

		public void SetupWindow(string title, string message, Action callback)
		{
		}

		private void DoCallback()
		{
		}
	}
}
