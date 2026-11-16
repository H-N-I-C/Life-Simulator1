using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using BitLife.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIList : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public UIListItem listItem;

			internal bool _003CLivePortfolioUpdate_003Eb__0(SimInvestment x)
			{
				return false;
			}
		}

		private sealed class _003CCreateListItems_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public List data;

			public UIList _003C_003E4__this;

			private ArrayList _003Clist_003E5__2;

			private bool _003CisEarnable_003E5__3;

			private int _003Cx_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCreateListItems_003Ed__45(int _003C_003E1__state)
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

		private sealed class _003CLivePortfolioUpdate_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIList _003C_003E4__this;

			private SimPortfolio _003Cportfolio_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLivePortfolioUpdate_003Ed__39(int _003C_003E1__state)
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

		private sealed class _003CLiveStockUpdate_003Ed__41 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIList _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLiveStockUpdate_003Ed__41(int _003C_003E1__state)
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

		private const int NUMBER_OF_BLANKS = 3;

		private const int BANNER_HEADER_HEIGHT = 58;

		private const int STOCK_HEADER_HEIGHT = 69;

		private const int LEADERBOARD_HEADER_HEIGHT = 150;

		private const int MULTISORT_HEADER_HEIGHT = 200;

		private const char STOCK_UP = '▲';

		private const char STOCK_DOWN = '▼';

		private const float STOCK_UPDATE_PERIOD_DURATION = 5f;

		private const float PORTFOLIO_UPDATE_PERIOD_DURATION = 5f;

		private Transform listItemParent;

		private CandyText parentListTitleCandyText;

		private string parentListTitle;

		public UIListManager listManager;

		public UIScenarioManager scenarioManager;

		public UIListItem listItem;

		public UIListItem listItemWebshop;

		private UIListItem subHeader7Cat;

		private UIListItem subHeader4Cat;

		private UIListItem subHeaderBanner;

		private UIListItem subHeaderStock;

		private UIListItem secondarySubHeader;

		private ScrollRect scrollRect;

		private ConstraintScaleBgUI scale;

		public List data;

		private List<GameObject> listItemObjects;

		public List<UIListItem> listItems;

		public int selectedSortState;

		public int parentListItemIndex;

		public bool beingDeleted;

		public AccessibleUIGroupRoot uapRoot;

		private Coroutine liveStockUpdate;

		public bool doStockUpdate;

		private Coroutine livePortfolioUpdate;

		public bool doPortfolioUpdate;

		private Coroutine createListItemsCoroutine;

		public string Title => null;

		private void OnDisable()
		{
		}

		public void BeginLivePortfolioUpdate()
		{
		}

		private IEnumerator LivePortfolioUpdate()
		{
			return null;
		}

		public void BeginLiveStockUpdate()
		{
		}

		private IEnumerator LiveStockUpdate()
		{
			return null;
		}

		public void UpdateListItemsForBitizenship()
		{
		}

		public string GetBreadcrumb()
		{
			return null;
		}

		public void CreateList(List data, int parentListItemIndex = -1, int sortCategory = 0)
		{
		}

		private IEnumerator CreateListItems(List data)
		{
			return null;
		}

		private void DestroyAllListItems()
		{
		}
	}
}
