using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Utils;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIJournal : MonoBehaviour
	{
		private sealed class _003CFadeOutJournalAnimation_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIJournal _003C_003E4__this;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeOutJournalAnimation_003Ed__23(int _003C_003E1__state)
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

		private sealed class _003CRevealJournalAnimation_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			private Vector2 _003Ctop_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealJournalAnimation_003Ed__22(int _003C_003E1__state)
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

		private const float WIDTH_SPACE = 60f;

		private const float REVEAL_DURATION_PART_A = 0.2f;

		private const float REVEAL_DURATION_PART_B = 0.1f;

		private const float HIDE_DURATION = 0.15f;

		private RectTransform rect;

		private CandyText journalLabel;

		private CandyText nameLabel;

		private CandyButton dismissButton;

		private CandyScroller logScroller;

		private RectTransform contentRect;

		private GameObject log;

		private CanvasGroup canvasGroup;

		private FadeCandyImage journalShade;

		private List<string> ages;

		private List<GameObject> entries;

		private int currentAge;

		private Grave _grave;

		private void OnDisable()
		{
		}

		public void Setup(Grave grave)
		{
		}

		public void ConstructLog(ArrayList eventArray, ArrayList intraYearArray, int startAge = 0)
		{
		}

		private void Dismiss()
		{
		}

		public void RevealJournal(Grave data)
		{
		}

		private IEnumerator RevealJournalAnimation(RectTransform rect)
		{
			return null;
		}

		private IEnumerator FadeOutJournalAnimation()
		{
			return null;
		}

		private void SetJournalActiveInHierarchy(bool isActive)
		{
		}

		private void SetCanvasGroupAlpha(float alpha)
		{
		}

		private void BuildAgeArray(int startAge, int eventCount, ArrayList intraYearArray)
		{
		}

		private void BuildLogEntries(int count, ArrayList eventArray)
		{
		}

		private int GetFontSize()
		{
			return 0;
		}

		private void AddEndSpace()
		{
		}

		private GameObject BuildLogEntryDisplayObject()
		{
			return null;
		}

		private void SetupLogScroller(GameObject content)
		{
		}
	}
}
