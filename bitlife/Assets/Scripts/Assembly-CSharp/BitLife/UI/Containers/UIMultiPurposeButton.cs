using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIMultiPurposeButton : MonoBehaviour
	{
		private sealed class _003CHideAnimation_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public UIMultiPurposeButton _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideAnimation_003Ed__19(int _003C_003E1__state)
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

		private sealed class _003CShowAnimation_003Ed__17 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public UIMultiPurposeButton _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowAnimation_003Ed__17(int _003C_003E1__state)
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

		private sealed class _003CShowScenarioHeirloom_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float intervalSeconds;

			public int numRetries;

			public SimHeirloom heirloom;

			private float _003Celapsed_003E5__2;

			private float _003Cduration_003E5__3;

			private SimHeirloom _003CextraHeirloom_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowScenarioHeirloom_003Ed__14(int _003C_003E1__state)
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

		private const string BitLifeCommunityText = "BitLife Community...";

		private const string NewHeirloomText = "New Heirloom!";

		private const string PrayText = "Pray";

		private const string AddYourFriendsText = "Add your friends...";

		private const string NewPurchaseText = "New Purchase!";

		private CandyButton button;

		private CandyImage background;

		private CandyImage icon;

		private CandyText title;

		private bool isOnScreen;

		private int contentType;

		private void OnDisable()
		{
		}

		public void Setup(int contentType)
		{
		}

		private void ButtonPress()
		{
		}

		private IEnumerator ShowScenarioHeirloom(SimHeirloom heirloom, int numRetries, float intervalSeconds = 1f)
		{
			return null;
		}

		private void ShowScenarioHeirloomTextOnly(SimHeirloom heirloom)
		{
		}

		public void Show(float delay = 0f)
		{
		}

		private IEnumerator ShowAnimation(float delay = 0f)
		{
			return null;
		}

		public void Hide(float delay = 0f)
		{
		}

		private IEnumerator HideAnimation(float delay = 0f)
		{
			return null;
		}

		public string BuildRandomName()
		{
			return null;
		}

		public bool IsOnScreen()
		{
			return false;
		}
	}
}
