using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIListContainer : MonoBehaviour
	{
		private sealed class _003CHideListPanel_003Ed__105 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			public float duration;

			public bool isHidingNavBar;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideListPanel_003Ed__105(int _003C_003E1__state)
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

		private sealed class _003CLerpColor_003Ed__61 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public CandyImage image;

			public Color fromColor;

			public Color toColor;

			public UIListContainer _003C_003E4__this;

			private float _003CtimePassed_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLerpColor_003Ed__61(int _003C_003E1__state)
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

		private sealed class _003CSetColorAfterDelay_003Ed__62 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public CandyImage image;

			public Color toColor;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetColorAfterDelay_003Ed__62(int _003C_003E1__state)
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

		private sealed class _003CShowListPanel_003Ed__103 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			public bool isHidingNavBar;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowListPanel_003Ed__103(int _003C_003E1__state)
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

		private sealed class _003CShuffleListBackAnimation_003Ed__98 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShuffleListBackAnimation_003Ed__98(int _003C_003E1__state)
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

		private sealed class _003CShuffleListForwardAnimation_003Ed__99 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShuffleListForwardAnimation_003Ed__99(int _003C_003E1__state)
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

		private sealed class _003CShuffleTitleBackAnimation_003Ed__96 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Color oldTitleColor;

			public UIListContainer _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShuffleTitleBackAnimation_003Ed__96(int _003C_003E1__state)
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

		private sealed class _003CShuffleTitleForwardAnimation_003Ed__95 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			private Color _003CshuffleColorSolid_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShuffleTitleForwardAnimation_003Ed__95(int _003C_003E1__state)
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

		private sealed class _003CUAP_SelectFirstItemAfterUnityUpdate_003Ed__101 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListContainer _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CUAP_SelectFirstItemAfterUnityUpdate_003Ed__101(int _003C_003E1__state)
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

		private const float SHOW_DURATION = 0.3f;

		private const float LIST_SHUFFLE_DURATION = 0.3f;

		private const float TITLE_FADE_OUT_DURATION = 0.2f;

		private const float TOOLBAR_BIG_BUTTON_SPACE = 100f;

		private const float NEW_TITLE_SPACE = 60f;

		private const float UNSCALED_LOCAL_HEIGHT_OF_LIST_TOPBAR = 153f;

		private RectTransform rect;

		private CandyText title;

		private CandyText shuffleTitle;

		private CandyImage titleBackground;

		private RectTransform titleAnimationGroup;

		private RectTransform shuffleTitleAnimationGroup;

		private CandyImage bottomBorder;

		private CandyButton navigationButton;

		private AccessibleCandyButton navigationButtonUAP;

		private CandyButton questionButton;

		private CandyImage background;

		private CandyText versionWatermark;

		private AccessibleUIGroupRoot uapGroupRoot;

		private UIHeroBar heroBar;

		private UINavBar navBar;

		private RectTransform listTopBar;

		private UIManager uiManager;

		private RectTransform listHorizontalGroup;

		private UIListManager listManager;

		private UIList listTemplate;

		private List<UIList> lists;

		private List<int> indexPath;

		private float bottomY;

		private RectTransform currentListRect;

		private bool isShowing;

		private int Type__BackingField;

		private bool isMenuList;

		private int theme;

		private Color titleTop;

		private Color titleBottom;

		private GameObject dailyRewardCatalogBalance;

		private Vector2 dailyRewardTitleOffsetMin;

		private Vector2 dailyRewardTitleOffsetMax;

		private bool dailyRewardTitleAdjusted;

		private int currentList;

		private GameObject seasonsShopButton;

		private Vector2 titleOffsetMin;

		private Vector2 titleOffsetMax;

		private Vector2 titleOffsetMinMod;

		private Vector2 titleOffsetMaxMod;

		public int Type
		{
			get
			{
				return Type__BackingField;
			}
			set
			{
				Type__BackingField = value;
			}
		}

		private void OnDisable()
		{
		}

		public List<UIList> GetLists()
		{
			return null;
		}

		public List GetCurrentList()
		{
			return null;
		}

		public void Init()
		{
		}

		public void SetDoStockMarketUpdate(bool flag)
		{
		}

		public void SetDoPortfolioUpdate(bool flag)
		{
		}

		public void UpdateScale()
		{
		}

		public void Reset()
		{
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

		public int GetCurrentListSortState()
		{
			return 0;
		}

		public void UpdateList(List data, bool doParentListItemUpdate = false)
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

		private IEnumerator LerpColor(CandyImage image, Color fromColor, Color toColor)
		{
			return null;
		}

		private IEnumerator SetColorAfterDelay(CandyImage image, Color toColor, float delay)
		{
			return null;
		}

		private void SetupHeaderAndColors(List data, Color headerColor, bool isAnimated = false)
		{
		}

		public void SetupListContainer(List data, Color headerColor, int parentListItemIndex = -1, int sortCategory = 0, bool isAnimated = true)
		{
		}

		public void ShowDailyRewardCatalogBalance(long balance)
		{
		}

		private void ApplyDailyRewardCatalogBalance(long balance)
		{
		}

		private void ResetDailyRewardCatalogHeader()
		{
		}

		public void ShowSeasonsShopButton(string resourceName, Vector2 titleOffsetMinMod, Vector2 titleOffsetMaxMod)
		{
		}

		private void SetupSeasonsShopButton(bool active)
		{
		}

		private void ShuffleListForwardHandler(List data, bool isAnimated)
		{
		}

		private void ShuffleListBackHandler()
		{
		}

		private void QuestionOnClick()
		{
		}

		public void ShowListAlertCustomPeople(Action<int> callback = null)
		{
		}

		private void ShowListAlertCareers(Action<int> callback = null)
		{
		}

		private void ShowListAlertLeaderboards(Action<int> callback = null)
		{
		}

		private void ShowListAlertAccessories(Action<int> callback = null)
		{
		}

		private void ShowListAlertSavedLives()
		{
		}

		public void UpdateNavigationButton()
		{
		}

		public void UpdateNavigationButtonOrExculsion(bool ExcludeNavigationButton)
		{
		}

		private void SetupNavFor_Nothing()
		{
		}

		private void SetupNavFor_ByCount()
		{
		}

		public void DismissAllLists()
		{
		}

		public void RemoveList()
		{
		}

		private void DeleteSubList()
		{
		}

		private static void RecordBreadcrumbBeforeDismiss(UIList listBeingRemoved)
		{
		}

		private void ResetHorizGroupSize()
		{
		}

		public void ShuffleListBack()
		{
		}

		private void ShuffleListForwardImmediately()
		{
		}

		public void ShuffleListForward()
		{
		}

		private IEnumerator ShuffleTitleForwardAnimation()
		{
			return null;
		}

		private IEnumerator ShuffleTitleBackAnimation(Color oldTitleColor)
		{
			return null;
		}

		public void UpdateQuestionMark(int index)
		{
		}

		private IEnumerator ShuffleListBackAnimation()
		{
			return null;
		}

		private IEnumerator ShuffleListForwardAnimation()
		{
			return null;
		}

		private void UAP_SelectFirstListItem(int index = 0)
		{
		}

		private IEnumerator UAP_SelectFirstItemAfterUnityUpdate()
		{
			return null;
		}

		private void ShuffleListForwardNoAnimation()
		{
		}

		public IEnumerator ShowListPanel(float topSpace, bool isHidingNavBar)
		{
			return null;
		}

		public void ShowListPanelImmediately(float topSpace, bool isHidingNavBar)
		{
		}

		public IEnumerator HideListPanel(float topSpace, bool isHidingNavBar, float duration = 0.3f)
		{
			return null;
		}
	}
}
