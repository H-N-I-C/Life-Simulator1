using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UILifeLog : MonoBehaviour
	{
		private sealed class _003CAddOnScrollListener_003Ed__63 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UILifeLog _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAddOnScrollListener_003Ed__63(int _003C_003E1__state)
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

		private sealed class _003CAnimateToContentPosition_003Ed__74 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UILifeLog _003C_003E4__this;

			public bool isAnimated;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAnimateToContentPosition_003Ed__74(int _003C_003E1__state)
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

		private sealed class _003CStart_003Ed__57 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UILifeLog _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStart_003Ed__57(int _003C_003E1__state)
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

		private const float NEW_ENTRY_BOUNCE_DURATION = 0.2f;

		private const float AVENIR_FONT_TO_PIXEL_MULTIPLIER = 1.225f;

		private const float BOTTOM_SPACE_FOR_MULTIPURPOSE_BUTTON = 120f;

		private const float END_SPACE_LINE_HEIGHT_MULTIPLIER = 3f;

		private const float NAV_BAR_HEIGHT = 153f;

		private const float HERO_BAR_HEIGHT = 153f;

		private const float TOOL_BAR_HEIGHT = 252f;

		private const float STATS_AREA_HEIGHT = 520f;

		private const float AD_BANNER_HEIGHT = 190f;

		private const float CULL_LOG_WAIT_TIME_ALLOTED = 3f;

		private RectTransform rect;

		private UIScenarioManager candyObjectManager;

		private CandyScroller logCandyScroller;

		private RectTransform contentRect;

		private VerticalLayoutGroup contentLayoutGroup;

		private GameObject log;

		private RectTransform TopSpacer;

		private RectTransform BottomSpacer;

		private RectTransform EndSpace;

		private AccessibleUIGroupRoot uapGroup;

		private List<string> ages;

		private int currentAge;

		private float totalHeight;

		private float bottomSpacerHeight;

		private List<float> entryHeights;

		private List<UIPoolObject> poolObjects;

		private int eventIndex;

		private List<CandyTextOptions> entryOptionsList;

		private List<CandyText> poolObjectsCandyText;

		private List<AccessibleLabel> poolObjectsLabels;

		private readonly List<int> currentlyShowingLogEntryIndices;

		private readonly List<int> usedPoolObjectIndices;

		private readonly List<int> freePoolObjectIndices;

		private int topEntryIndexVisibleInView;

		private int bottomEntryIndexVisibleInView;

		private List<int> ageToEventLookUp;

		private float TopTop;

		private float TopBottom;

		private float BottomTop;

		private float BottomBottom;

		private float logWidth;

		private bool isInitiated;

		private Coroutine animateScroll;

		private int endOfBirthEventsIndex;

		private Coroutine cullingLifeLog;

		private bool forceCullEvenThoughLogWindowHeightDoesNotMatchExpected;

		private string ageKey;

		private string yearKey;

		private string yearsKey;

		private string monthKey;

		private string monthsKey;

		public AccessibleUIGroupRoot UAPGroup => null;

		public bool IsInitiated => false;

		private void OnDisable()
		{
		}

		public void Init()
		{
		}

		public IEnumerator Start()
		{
			return null;
		}

		public void ConstructLog(ArrayList eventArray, ArrayList intraYearArray, int startAge = 0)
		{
		}

		public string GetLastLogEntry()
		{
			return null;
		}

		public void SelectLastLogEntry()
		{
		}

		public void ScrollCheckForBlindUserControls(bool isScrollingUp)
		{
		}

		private int GetSiblingIndexOfLogEntryIndex(int logEntryIndex)
		{
			return 0;
		}

		private IEnumerator AddOnScrollListener()
		{
			return null;
		}

		public float GetLifeLogBirthEntriesHeight()
		{
			return 0f;
		}

		public void AddToCurrentYear(string newEvent)
		{
		}

		public void AddYearToLog(LifeLogIntraYear intraYear = null)
		{
		}

		private void AddYear(LifeLogIntraYear intraYear = null)
		{
		}

		public bool ShouldShowMultiPurposeButton()
		{
			return false;
		}

		public void UpdateLifeLogTheme()
		{
		}

		private void UpdateTheme()
		{
		}

		public void UpdateLifeLogTextSize()
		{
		}

		private void RecordAllNewEntryHeights()
		{
		}

		public void ScrollToBottom(bool isAnimated)
		{
		}

		private IEnumerator AnimateToContentPosition(bool isAnimated)
		{
			return null;
		}

		private void ScrollToPosition(float percentValue)
		{
		}

		private void AddToScrollPosition(float pixels)
		{
		}

		private void BuildAgeArray(int startAge, int eventCount, ArrayList intraYearArray)
		{
		}

		private void BuildLogEntryPool(ArrayList eventArray)
		{
		}

		private void FillLogEntryPool()
		{
		}

		private void AddPoolObject()
		{
		}

		private void RecordAllEntryHeightsAndStrings(ArrayList eventArray)
		{
		}

		private void StopScrolling()
		{
		}

		private bool CullLifeLog()
		{
			return false;
		}

		private void OnScroll(float value)
		{
		}

		private int GetFontSize()
		{
			return 0;
		}

		private void AddFreePoolObjectIndex(int i)
		{
		}

		private void RemoveFreePoolObjectIndex(int i)
		{
		}

		private void AddUsedPoolObjectIndex(int i, bool addToTop = false)
		{
		}

		private void RemoveUsedPoolObjectIndex(int i)
		{
		}

		private void AddIndexToCurrenltyShowingLogEntryIndices(int i, bool addToTop = false)
		{
		}

		private void RemoveIndexFromCurrenltyShowingLogEntryIndices(int i)
		{
		}

		private void ScrollTask_RemoveTop()
		{
		}

		private void ScrollTask_AddTop()
		{
		}

		private void ScrollTask_RemoveBottom()
		{
		}

		private void ScrollTask_AddBottom()
		{
		}
	}
}
