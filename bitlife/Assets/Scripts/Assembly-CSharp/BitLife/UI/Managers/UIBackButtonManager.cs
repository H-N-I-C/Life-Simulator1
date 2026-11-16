using System;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIBackButtonManager : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass19_0
		{
			public UIBackButtonManager _003C_003E4__this;

			public long restoreAttempt;

			public List<string> savedBreadcrumbs;

			public List baseList;

			public List<string> breadcrumbsToRestore;

			internal void _003CTryRestoreList_003Eb__0(bool presentationSucceeded)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass22_0
		{
			public UIBackButtonManager _003C_003E4__this;

			public long restoreAttempt;

			public List<string> savedBreadcrumbs;

			internal void _003CContinueRestoreAfterListShown_003Eb__0(bool restoredComplete)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass35_0
		{
			public UIBackButtonManager _003C_003E4__this;

			public long restoreAttempt;

			public Action<bool> onComplete;

			public List<string> breadcrumbs;
		}

		private sealed class _003C_003Ec__DisplayClass35_1
		{
			public List list;

			public _003C_003Ec__DisplayClass35_0 CS_0024_003C_003E8__locals1;

			internal void _003CShowSubListForListWithBreadcrumbs_003Eb__0(bool presentationSucceeded)
			{
			}
		}

		public static UIBackButtonManager Inst;

		private const float TABLET_WIDTH = 433f;

		private const float BOTTOM = -96f;

		private const float ANIM_DUR = 0.35f;

		internal const string TakeoverBreadcrumbSeparator = "**";

		private List<string> masterBreadcrumbs;

		private bool isRestoringList;

		private long restoreAttemptGeneration;

		private bool isShowing;

		public bool recordedBreadcrumbs;

		private CandyButton backButton;

		public bool IsShowing => false;

		private void Awake()
		{
		}

		public void Init()
		{
		}

		public void UpdateTheme()
		{
		}

		private void BackButtonPress()
		{
		}

		public bool ShouldShowBackButton()
		{
			return false;
		}

		public void RestoreList()
		{
		}

		public bool TryRestoreList()
		{
			return false;
		}

		private bool IsCurrentRestoreAttempt(long restoreAttempt)
		{
			return false;
		}

		private static List ResolveRootList(LifeInterface lifeInterface, string breadcrumb)
		{
			return null;
		}

		private void ContinueRestoreAfterListShown(long restoreAttempt, List baseList, List<string> breadcrumbsToRestore, List<string> savedBreadcrumbs)
		{
		}

		private void CompleteRestore(long restoreAttempt, bool restoredComplete, List<string> savedBreadcrumbs)
		{
		}

		public string GetIndexAppendedBreadcrumb(string breadcrumb, int index)
		{
			return null;
		}

		private string GetBreadcrumbStrippedOfIndex(string breadcrumb)
		{
			return null;
		}

		private int GetIndexOfBreadcrumb(string breadcrumb)
		{
			return 0;
		}

		private int BreadcrumbSeparatorIndex(string breadcrumb)
		{
			return 0;
		}

		private string BreadcrumbIndexSeparator()
		{
			return null;
		}

		private bool AdvancedBackButtonModeActive()
		{
			return false;
		}

		private bool BreadcrumbMatched(string theBreadcrumb, ListItem theItem, List theParentList, int theIndex, List resolvedList)
		{
			return false;
		}

		private static bool HasCompetingBreadcrumbMatch(string strippedBreadcrumb, ListItem candidate, List parentList)
		{
			return false;
		}

		private bool ShouldSkipDeferredCandidateBeforeResolution(string breadcrumb, ListItem candidate, List parentList, int candidateIndex)
		{
			return false;
		}

		private static bool HasRestorableListTarget(ListItem item)
		{
			return false;
		}

		private static bool TryResolveRestorableList(ListItem item, out List list, out bool isTakeoverList)
		{
			list = null;
			isTakeoverList = default;
			return false;
		}

		private void ShowSubListForListWithBreadcrumbs(long restoreAttempt, List parentList, List<string> breadcrumbs, Action<bool> onComplete)
		{
		}

		private List<string> ExtractBreadcrumbsFromTakeoverListBreadcrumb(string breadcrumb)
		{
			return null;
		}

		public bool GetIsRestoringList()
		{
			return false;
		}

		public void UpdateBackButtonState(bool shouldAnimate)
		{
		}

		public void InsertBreadcrumb(int positionIndex, string breadcrumb, int indexInParentList = -1)
		{
		}

		public void AddBreadcrumb(List list, int indexInParentList = -1)
		{
		}

		public void AddCustomBreadcrumbString(string breadcrumbString)
		{
		}

		public void RemoveBreadcrumb(List list)
		{
		}

		public List<string> CurrentBreadcrumbListCopy()
		{
			return null;
		}

		public void OverrideBreadcrumbList(List<string> newBreadcrumbList)
		{
		}

		public void RemoveLastBreadcrumb()
		{
		}

		public bool TryRemoveLastBreadcrumb()
		{
			return false;
		}

		private void UpdateBackButtonStateIfReady(bool shouldAnimate)
		{
		}

		public void ResetBackButton(bool shouldAnimate)
		{
		}

		private void SetupText()
		{
		}

		private void Reveal(bool isAnimated = true)
		{
		}

		public void Hide(bool isAnimated = true)
		{
		}

		private void SetIsShowingFalse()
		{
		}

		private void SetIsShowingTrue()
		{
		}
	}
}
