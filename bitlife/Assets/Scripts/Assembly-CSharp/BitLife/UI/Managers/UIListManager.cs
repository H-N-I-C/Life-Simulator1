using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Containers;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIListManager : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public UIListManager _003C_003E4__this;

			public int owner;

			public Life life;

			public Func<bool> canPresent;

			public bool presentationPending;

			public IDisposable lease;

			public Coroutine presentationTimeout;

			public Action<bool> onPresentationCompleted;

			public Action<bool> completePresentation;

			public List data;

			public int parentListItemIndex;

			public int sortCategory;

			public bool isAnimated;

			public Func<bool> isCurrent;

			public Action _003C_003E9__4;

			public Func<bool> _003C_003E9__5;

			internal bool _003CShowList_003Eb__0()
			{
				return false;
			}

			internal void _003CShowList_003Eb__1(bool succeeded)
			{
			}

			internal void _003CShowList_003Eb__2()
			{
			}

			internal void _003CShowList_003Eb__3()
			{
			}

			internal void _003CShowList_003Eb__4()
			{
			}

			internal bool _003CShowList_003Eb__5()
			{
				return false;
			}
		}

		private sealed class _003CDismissAllListsAndShowListCoroutine_003Ed__59 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			public List list;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDismissAllListsAndShowListCoroutine_003Ed__59(int _003C_003E1__state)
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

		private sealed class _003CGoToMenuListAnimation_003Ed__60 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CGoToMenuListAnimation_003Ed__60(int _003C_003E1__state)
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

		private sealed class _003CGoToNormalListAnimation_003Ed__62 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CGoToNormalListAnimation_003Ed__62(int _003C_003E1__state)
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

		private sealed class _003CHideLowerPanel_003Ed__71 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			public float duration;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideLowerPanel_003Ed__71(int _003C_003E1__state)
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

		private sealed class _003CRemoveMenuListAnimation_003Ed__64 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveMenuListAnimation_003Ed__64(int _003C_003E1__state)
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

		private sealed class _003CRemoveMenuListNoAnimation_003Ed__67 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveMenuListNoAnimation_003Ed__67(int _003C_003E1__state)
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

		private sealed class _003CRemoveNormalListAnimation_003Ed__65 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveNormalListAnimation_003Ed__65(int _003C_003E1__state)
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

		private sealed class _003CRemoveNormalListAnimationNoReturn_003Ed__66 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveNormalListAnimationNoReturn_003Ed__66(int _003C_003E1__state)
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

		private sealed class _003CRemoveNormalListNoAnimation_003Ed__68 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveNormalListNoAnimation_003Ed__68(int _003C_003E1__state)
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

		private sealed class _003CRemoveNormalListNoAnimationNoReturn_003Ed__69 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRemoveNormalListNoAnimationNoReturn_003Ed__69(int _003C_003E1__state)
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

		private sealed class _003CResetLifeLogPosToBottom_003Ed__72 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CResetLifeLogPosToBottom_003Ed__72(int _003C_003E1__state)
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

		private sealed class _003CShowLowerPanel_003Ed__70 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListManager _003C_003E4__this;

			public float duration;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowLowerPanel_003Ed__70(int _003C_003E1__state)
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

		private sealed class _003CWaitForListPresentationTimeout_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Action onTimeout;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitForListPresentationTimeout_003Ed__41(int _003C_003E1__state)
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

		private const float HIDE_DURATION = 0.17f;

		private const float TOOLBAR_BIG_BUTTON_SPACE = 100f;

		private const float LIST_PRESENTATION_TIMEOUT_SECONDS = 60f;

		private static UIListManager Instance__BackingField;

		private RectTransform lowerPanelAnimationGroup;

		private CandyImage lowerPanelBackground;

		private UIListContainer normalListContainer;

		private UIListContainer menuListContainer;

		private CandyButton listDismissButton;

		private CandyImage safeBottom;

		private AccessibleUIGroupRoot menuListAccessibleUIGroupRoot;

		private AccessibleUIGroupRoot normalListAccessibleUIGroupRoot;

		private AccessibleUIGroupRoot tooblarUIGroupRoot;

		private AccessibleUIGroupRoot multiPurposeUIGroupRoot;

		private GameObject ageUpGameObject;

		private UIListContainer currentContainer;

		private bool isShowingList;

		private bool isShowingLowerPanel;

		private float heroBarHeight;

		private List<string> ListBreadcrumb__BackingField;

		private int translationGeneration;

		public static UIListManager Instance
		{
			get
			{
				return Instance__BackingField;
			}
			private set
			{
				Instance__BackingField = value;
			}
		}

		public UIListContainer CurrentContainer => null;

		public List<string> ListBreadcrumb
		{
			get
			{
				return ListBreadcrumb__BackingField;
			}
			private set
			{
				ListBreadcrumb__BackingField = value;
			}
		}

		private void OnDisable()
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void Init()
		{
		}

		public void SetThemeColors()
		{
		}

		public void SetDoStockMarketUpdate(bool flag)
		{
		}

		public void SetDoPortfolioUpdate(bool flag)
		{
		}

		private void DismissAllLists()
		{
		}

		public bool IsShowingList()
		{
			return false;
		}

		public bool IsListOnScreenAndIsThatListOfMenuType()
		{
			return false;
		}

		public void ShowList(List data, int parentListItemIndex = -1, int sortCategory = 0, bool isAnimated = true, Action<bool> onPresentationCompleted = null, Func<bool> canPresent = null)
		{
		}

		public void ReactivateCurrentContainerForAccessibility()
		{
		}

		private static IEnumerator WaitForListPresentationTimeout(Action onTimeout)
		{
			return null;
		}

		private static void RunListPresentationIfAllowed(Action presentation, Action<bool> onPresentationCompleted, Func<bool> canPresent)
		{
		}

		private static void RunListPresentation(Action presentation, Action<bool> onPresentationCompleted)
		{
		}

		public float GetLowerPanelHeight()
		{
			return 0f;
		}

		public string GetCurrentListTitle()
		{
			return null;
		}

		public int GetNumberOfListsInExistence()
		{
			return 0;
		}

		public List<UIList> GetAllLists()
		{
			return null;
		}

		public List GetCurrentList()
		{
			return null;
		}

		public int GetCurrentListSortState()
		{
			return 0;
		}

		public void UpdateList(List data, bool doParentListItemUpdate)
		{
		}

		public void UpdateListItemsForBitizenship()
		{
		}

		public void UpdateListsForListItemsContainingEmojiAndTitle(string emojiString, string titleString, float barValue = -1f)
		{
		}

		public void UpdateSubTitleForListsForListItemsContainingTitle(string titleString, string newSubTitle)
		{
		}

		public void ShowListAlertCustomPeople()
		{
		}

		public int GetCurrentListType()
		{
			return 0;
		}

		public void GoToList(int type, bool isAnimated = true)
		{
		}

		public void RemoveList(int type, bool isAnimated = false)
		{
		}

		public void DismissAllListsAndShowList(List list)
		{
		}

		private IEnumerator DismissAllListsAndShowListCoroutine(List list)
		{
			return null;
		}

		private IEnumerator GoToMenuListAnimation()
		{
			return null;
		}

		private void GoToMenuListImmediately()
		{
		}

		private IEnumerator GoToNormalListAnimation()
		{
			return null;
		}

		private void GoToNormalListImmediately()
		{
		}

		private IEnumerator RemoveMenuListAnimation()
		{
			return null;
		}

		private IEnumerator RemoveNormalListAnimation()
		{
			return null;
		}

		private IEnumerator RemoveNormalListAnimationNoReturn()
		{
			return null;
		}

		private IEnumerator RemoveMenuListNoAnimation()
		{
			return null;
		}

		private IEnumerator RemoveNormalListNoAnimation()
		{
			return null;
		}

		private IEnumerator RemoveNormalListNoAnimationNoReturn()
		{
			return null;
		}

		public IEnumerator ShowLowerPanel(float duration = 0.17f)
		{
			return null;
		}

		public IEnumerator HideLowerPanel(float duration = 0.17f)
		{
			return null;
		}

		private IEnumerator ResetLifeLogPosToBottom()
		{
			return null;
		}
	}
}
