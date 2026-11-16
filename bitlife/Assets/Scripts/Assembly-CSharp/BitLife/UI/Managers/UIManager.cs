using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Containers;
using BitLife.UI.Data;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using Game.Seasons.Components;
using TMPro;
using UnityEngine;

namespace BitLife.UI.Managers
{
	public class UIManager : MonoBehaviour
	{
		private sealed class _003CActivateChallengeTickerAfterDelay_003Ed__76 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public UIManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CActivateChallengeTickerAfterDelay_003Ed__76(int _003C_003E1__state)
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

		private sealed class _003CResetScrollViewOnNextFrame_003Ed__134 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CResetScrollViewOnNextFrame_003Ed__134(int _003C_003E1__state)
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

		private sealed class _003CRevealGDPRPolicyNextFrame_003Ed__98 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public Action _startSDKAction;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealGDPRPolicyNextFrame_003Ed__98(int _003C_003E1__state)
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

		private sealed class _003CSelectToolBar_003Ed__66 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSelectToolBar_003Ed__66(int _003C_003E1__state)
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

		private sealed class _003CStartUpcomingChallengeTickerTimer_003Ed__81 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStartUpcomingChallengeTickerTimer_003Ed__81(int _003C_003E1__state)
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

		private sealed class _003CWaitForLifeLogToBeAvailable_003Ed__166 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIManager _003C_003E4__this;

			public ArrayList LogArray;

			public ArrayList IntraYears;

			public int AgeAtLogStart;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitForLifeLogToBeAvailable_003Ed__166(int _003C_003E1__state)
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

		private RectTransform mainCanvas;

		private ConstraintScaleBgUI bgUIScaleCon;

		private GameObject uapBlankStartOfChain;

		private UINavBar navBar;

		private UIHeroBar heroBar;

		private RectTransform lifeLogRect;

		private UILifeLog lifeLog;

		private RectTransform tslGroup;

		private UIListManager listManager;

		private UIToolbar toolbar;

		private UIStats stats;

		private UIAchievementBanner achievementBanner;

		private UIScenarioManager scenarioManager;

		private UITicker ticker;

		private UIAdBannerBacker adBannerBacker;

		private CandyDropDown themeSelection;

		private UIDarklyManager darklyManager;

		private GameObject fullScreenBlocker;

		private GameObject startSequence;

		private CandyImage canvasBackground;

		private RectTransform bgUIScale;

		private RectTransform fgUIScale;

		private UIBackButtonManager backButtonManager;

		private UIMultiPurposeButtonManager multiPurposeButtonManager;

		private UIRightButtonManager rightButtonManager;

		public GameObject loadingIndicator;

		public Hashtable supportObjectTable;

		public GameObject[] supportObjectPrefabs;

		private bool adBannerHasBeenStarted;

		private TextMeshPro textMeshProToTexture;

		private bool toTexture2DHasFailed;

		private bool toTexture2DPopHasFailed;

		public bool playedAccessiblityNewLife;

		public GameObject StartSequence => null;

		public UIScenarioManager ScenarioManager => null;

		public UIDarklyManager DarklyManager => null;

		public UIListContainer CurrentListContainer => null;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		private void OnDestroy()
		{
		}

		private void OnShowSeasonsBar(UISeasonsBar view, bool visible)
		{
		}

		private void OnShowCommunityBar(UISeasonsCommunityBar view, bool visible)
		{
		}

		public void InitWatermark()
		{
		}

		public void Init()
		{
		}

		public void ReInit()
		{
		}

		public Texture2D ToTexture2D(string pString)
		{
			return null;
		}

		public void Activate_NavBar(bool isNowActive)
		{
		}

		public void Activate_HeroBar(bool isNowActive)
		{
		}

		public void Activate_LifeLog(bool isNowActive)
		{
		}

		public void Activate_ToolBar(bool isNowActive)
		{
		}

		public void Activate_Stats(bool isNowActive)
		{
		}

		public void Activate_MultiPurpseButton(bool isNowActive)
		{
		}

		public void Activate_AdBannerBacker(bool isNowActive)
		{
		}

		public void Activate_All_BgUI_Groups(bool isNowActive)
		{
		}

		private void OnAccessibilityModeChanged(bool enabled)
		{
		}

		public void SelectLastLogEntry()
		{
		}

		public void SetUAPBlankAtStartOfChainActive(bool isActive)
		{
		}

		public string GetLastLogEntry()
		{
			return null;
		}

		public void StartLife()
		{
		}

		public void StartLife(SOUND sound)
		{
		}

		public void PlayAccessibilityNewLife()
		{
		}

		public void ContinuousReadingHasStoppedCallback()
		{
		}

		private IEnumerator SelectToolBar()
		{
			return null;
		}

		public void ContinueLife()
		{
		}

		public void UpdateMultiPurposeButtonVisibility()
		{
		}

		public void DisableMultiPurposeButtonVisibilityLogic()
		{
		}

		public void EnableMultiPurposeButtonVisibilityLogic()
		{
		}

		private void UpdateMultiPurposeButton()
		{
		}

		public void UpdateRightButtonVisibility(float delay = 0.35f, float animTime = 0.35f)
		{
		}

		public void UpdateRightButton()
		{
		}

		public void HideRightButton()
		{
		}

		public void StartActiveChallengeTicker(float delay = 0f)
		{
		}

		private IEnumerator ActivateChallengeTickerAfterDelay(float delay)
		{
			return null;
		}

		public void StartUpcomingChallengeTicker()
		{
		}

		private void StartChallengeTickerWithType(int type)
		{
		}

		public void RemoveChallengeTicker()
		{
		}

		public void ChallengeInterfaceGetNoActiveChallengeButNextChallengeStartsInSeconds(int seconds)
		{
		}

		private IEnumerator StartUpcomingChallengeTickerTimer()
		{
			return null;
		}

		public void ChallengeInterfaceGetNoActiveChallenge()
		{
		}

		public void ChallengeInterfacePostSuccessWithGlobalPositionForChallenge(int globalPosition, int challengeID)
		{
		}

		public void PauseChallengeTicker()
		{
		}

		public void ResumeChallengeTicker()
		{
		}

		public void ShowPaywallChallengeVault(int IAP_Origin)
		{
		}

		public void ShowPaywallThemePromo(int IAP_Origin)
		{
		}

		public void ShowPaywallSpecialCareerPromoWithIAPOrigin(int IAP_Origin, int jobType)
		{
		}

		public void ShowPaywallWithIAPOrigin(int IAP_Origin, int darklyType, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int jobType = 0, int exPackType = 0, int nestedIAPOrigin = -1, int bundleType = 0, int itemType = 0, bool isFeatured = false, IAP_TYPE iapType = IAP_TYPE.NONE, Action onShown = null)
		{
		}

		public void ShowPaywallConsumableIAP(int IAP_Origin, int darklyType, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int nestedIAPOrigin = -1, int itemType = 0, bool isFeatured = false, IAP_TYPE iapType = IAP_TYPE.NONE)
		{
		}

		public void ShowPaywallGenericIAP(int IAP_Origin, IAP_TYPE iapType, Scenario followUpScenario = null, Scenario followUpChainedScenario = null)
		{
		}

		public void ShowPaywallGenericIAP(int IAP_Origin, IAP_TYPE iapType, string paywallName, Scenario followUpScenario = null, Scenario followUpChainedScenario = null)
		{
		}

		public void ShowBitizenshipPaywallWithIAPOrigin(int IAP_Origin)
		{
		}

		public void ShowGodModePaywallWithIAPOrigin(int IAP_Origin)
		{
		}

		public void LaunchBitizenshipPaywall()
		{
		}

		public void LaunchBitizenshipPaywallFromAdDismiss()
		{
		}

		public void ShowGDPRPolicy(Action _startSDKAction)
		{
		}

		private IEnumerator RevealGDPRPolicyNextFrame(Action _startSDKAction)
		{
			return null;
		}

		public void CheckMissionVarsAndUpdateAdBannerExistance()
		{
		}

		public void TurnOffAdBannerBacker()
		{
		}

		public void StartAdBannerBacker()
		{
		}

		public void UpdateAdBannerBackerScale()
		{
		}

		public float GetAdBannerHeight()
		{
			return 0f;
		}

		public void ShowAdBannerBacker()
		{
		}

		public void HideAdBannerBacker()
		{
		}

		public string GetScenarioCustomSoundFileName()
		{
			return null;
		}

		public void DoDamage(int damage, bool includeSound = true)
		{
		}

		public void ShowScenario(Scenario scenario)
		{
		}

		public void UpdateDarklyButtons()
		{
		}

		public void InjectScenario(Scenario scenario)
		{
		}

		public void ShowBoost(int attribute, ScenarioBoost boostScenario)
		{
		}

		public void HideBoosts()
		{
		}

		public void RemoveBoosts()
		{
		}

		public void UpdateChallengeButton()
		{
		}

		public void UpdateBitizenBadges()
		{
		}

		public void ShowFullScreenBlocker()
		{
		}

		public void HideFullScreenBlocker()
		{
		}

		public void ShowBannerForAchievementType(string achievement, int type)
		{
		}

		public void ShowChallengeTakeoverForChallenge(int challengeID)
		{
		}

		public LIFE_EVENT_THEME_NAME GetLifeEventTheme()
		{
			return LIFE_EVENT_THEME_NAME.NONE;
		}

		public T CreateSupportObject<T>(IguiOptions boxedOptions, Transform parent = null) where T : IguiObject
		{
			return default;
		}

		public float GetTopSafeAreaHeight()
		{
			return 0f;
		}

		public float GetBottomSafeAreaHeight()
		{
			return 0f;
		}

		public float GetNavBarHeight()
		{
			return 0f;
		}

		public void MainMenuButton()
		{
		}

		public void HideChallengeNavBarButton()
		{
		}

		public void ShowChallengeNavBarButton(int challengID = -1)
		{
		}

		public void UpdateChallengeNavBarButton()
		{
		}

		public void HideSnowWhenMenuOpensInTheWinter()
		{
		}

		public void UnHideSnowWhenMenuClosesInWinter()
		{
		}

		public float GetHeroBarHeight()
		{
			return 0f;
		}

		public void HeroBarButton()
		{
		}

		public void HeroBarSwap()
		{
		}

		private IEnumerator ResetScrollViewOnNextFrame()
		{
			return null;
		}

		public float GetLowerPanelHeight()
		{
			return 0f;
		}

		public void UpdateAgeDownButton()
		{
		}

		public bool IsTButtonActiveAndAvailable(int TIndex)
		{
			return false;
		}

		public int GetT1ButtonState()
		{
			return 0;
		}

		private void DoBigButton()
		{
		}

		private void DoAgeDown()
		{
		}

		private void DoButton1()
		{
		}

		private void DoButton2()
		{
		}

		private void DoButton3()
		{
		}

		private void DoButton4()
		{
		}

		public void SetDoStockMarketUpdate(bool flag)
		{
		}

		public void SetDoPortfolioUpdate(bool flag)
		{
		}

		public void ShowList(List list)
		{
		}

		public void ShowList(List list, bool isAnimated = true)
		{
		}

		public void ShowList(List list, bool isAnimated, Action<bool> onPresentationCompleted, Func<bool> canPresent = null)
		{
		}

		public void DismissAllListsAndShowList(List list)
		{
		}

		public void DismissAllLists(bool isAnimated)
		{
		}

		public void UpdateList(List list, bool doParentListItemUpdate)
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

		public bool IsListOnScreen()
		{
			return false;
		}

		public bool IsListOnScreenAndIsThatListOfMenuType()
		{
			return false;
		}

		public int GetCurrentListSortState()
		{
			return 0;
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

		public void ShowListAlertCustomPeople()
		{
		}

		public void ScrollCheckForBlindUserControls(bool isScrollingUp)
		{
		}

		public void LifeLogScrollToBottom(bool isAnimated)
		{
		}

		public void ConstructLifeLogOnceAvailable(ArrayList LogArray, ArrayList IntraYears, int AgeAtLogStart = 0)
		{
		}

		private IEnumerator WaitForLifeLogToBeAvailable(ArrayList LogArray, ArrayList IntraYears, int AgeAtLogStart = 0)
		{
			return null;
		}

		public void UpdateLifeLogTextSize()
		{
		}

		public bool ShouldShowMultiPurposeButton()
		{
			return false;
		}

		private void SetupLifeLog(bool newLife = true, bool serialization = false)
		{
		}

		public void AgeUp(LifeLogIntraYear intraYear = null)
		{
		}

		public void AddEventToCurrentYear(string lifeEvent)
		{
		}

		public float GetLifeLogBirthEntriesHeight()
		{
			return 0f;
		}

		public void UpdateTheme()
		{
		}

		public void UpdateLanguage(string language)
		{
		}

		public void ShowTranslationFailureWarning()
		{
		}

		public void UpdateOccupationString(string text)
		{
		}

		public void RefreshUI(int T1ButtonState, string heroFullName, string occupationString, string bankBalanceString, Color bankBalanceColor, string heroCountryCode, Sprite occupationImage)
		{
		}

		public void ShowGravestone()
		{
		}

		public void ShowGravestone(Grave grave, bool includeJournal, bool includeBulldozer)
		{
		}

		public void GravestonePostRemovalProcess()
		{
		}

		public void Challenge2XRewardOpen()
		{
		}

		private void Challenge2XRewardOpenCo()
		{
		}

		private bool _003CWaitForLifeLogToBeAvailable_003Eb__166_0()
		{
			return false;
		}
	}
}
