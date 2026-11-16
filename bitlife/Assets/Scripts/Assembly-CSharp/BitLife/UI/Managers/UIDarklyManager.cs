using System;
using System.Collections;
using System.Collections.Generic;
using Backend;
using BitLife.UI.CandyObjects;
using BitLife.UI.Containers;
using BitLife.UI.Utils;
using CandyBusiness.Confidence.Variants;
using Game.Seasons.Models;
using Tracking;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Managers
{
	public class UIDarklyManager : MonoBehaviour
	{
		private enum UIDARKLY_ANIMATION_DIRECTION
		{
			LEFT = 0,
			RIGHT = 1
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<UIDarkly, bool> _003C_003E9__94_0;

			public static Func<GraphicRaycaster, bool> _003C_003E9__120_0;

			public static Action<UIDarklyStats> _003C_003E9__137_1;

			public static Action<UIDarklyPopup> _003C_003E9__137_2;

			public static Action<UIDarklyChooseLife> _003C_003E9__137_3;

			public static Action<UIDarklyImportContacts> _003C_003E9__137_26;

			public static Action<UIDarklyEmigrate> _003C_003E9__137_27;

			public static Action<UIDarklyChooseSpecialLife> _003C_003E9__137_30;

			public static Action<UIDarklyChooseUseTokens> _003C_003E9__137_31;

			internal bool _003CIsShowingNonPromotionDarkly_003Eb__94_0(UIDarkly darkly)
			{
				return false;
			}

			internal bool _003CSuspendDailyRewardForMarketplace_003Eb__120_0(GraphicRaycaster raycaster)
			{
				return false;
			}

			internal void _003CCreateNewDarkly_003Eb__137_1(UIDarklyStats result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_2(UIDarklyPopup result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_3(UIDarklyChooseLife result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_26(UIDarklyImportContacts result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_27(UIDarklyEmigrate result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_30(UIDarklyChooseSpecialLife result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__137_31(UIDarklyChooseUseTokens result)
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec__70<T> where T : UIDarkly
		{
			public static readonly _003C_003Ec__70<T> _003C_003E9;

			public static Func<UIDarkly, bool> _003C_003E9__70_0;

			internal bool _003CGetDarkly_003Eb__70_0(UIDarkly i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass103_0
		{
			public UIDarklyManager _003C_003E4__this;

			public UIDarklyPromotion result;

			internal void _003CShowDarklyPromotion_003Eb__0(bool _)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass108_0
		{
			public UIDarklyManager _003C_003E4__this;

			public UIDarklyPromotion promotion;

			internal void _003CUpdatePromotionPresentation_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass119_0
		{
			public UIDarklyManager _003C_003E4__this;

			public UIDarklySeasonsDailyReward owner;

			public string account;

			public string life;

			internal void _003CShowDailyRewardMarketplace_003Eb__0(bool success)
			{
			}

			internal bool _003CShowDailyRewardMarketplace_003Eb__1()
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass134_0
		{
			public UIDarklyManager _003C_003E4__this;

			public Action onShown;

			public UIDarkly paywall;

			internal void _003CShowDarklyPaywall_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_0
		{
			public UIDarklyManager _003C_003E4__this;

			public int darklyType;

			public Scenario scenario;

			public StuffItem tokenLifeItem;

			public int challengeID;

			public int IAP_Origin;

			public Scenario followUpChainedScenario;

			public int jobType;

			public int exPackType;

			public int expansionPackIAPOrigin;

			public int itemType;

			public int bundleType;

			public IAP_TYPE iapType;

			public int infoType;

			internal void _003CCreateNewDarkly_003Eg__Create_007C0<T>(string fileTitle, Action<T> success, bool makeActive = true, bool moveTop = true) where T : UIDarkly
			{
			}

			internal void _003CCreateNewDarkly_003Eb__4(UIDarklyTimeMachine result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__5(UIDarklyChildren result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__6(UIDarklyCustomLife result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__7(UIDarklyCustomLifeSubGroup result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__8(UIDarklyName result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__9(UIDarklyAttributes result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__10(UIDarklyAppearance result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__11(UIDarklyEditPerson result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__12(UIDarklyChallenge result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__13(UIDarklyChallengeReward result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__14(UIDarklyPaywallCombo result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__15(UIDarklyPaywallCombo result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__16(UIDarklyPaywallComboCareer result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__17(UIDarklyPaywallSpecialCareerPromo result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__18(UIDarklyPaywallExpansionPackPromo result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__19(UIDarklyPaywallThemePromo result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__20(UIDarklyPaywallChallengeVault result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__21(UIDarklyPaywallExpansionPack result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__22(UIDarklyPaywallItem result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__23(UIDarklyPaywallBundle result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__24(UIDarklyPaywallConsumable result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__25(UIDarklyPaywallGenericIAP result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__28(UIDarklyInformation result)
			{
			}

			internal void _003CCreateNewDarkly_003Eb__29(UIDarklyGoldenDiploma result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_1
		{
			public UIDarklyChildren result;

			internal void _003CCreateNewDarkly_003Eb__32(UIDarklyChooseLife life)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_2
		{
			public UIDarklyCustomLifeSubGroup result;

			internal void _003CCreateNewDarkly_003Eb__33(UIDarkly life)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_3
		{
			public UIDarklyName result;

			internal void _003CCreateNewDarkly_003Eb__34(UIDarkly life)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_4
		{
			public UIDarklyAttributes result;

			internal void _003CCreateNewDarkly_003Eb__35(UIDarkly life)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_5
		{
			public UIDarklyAppearance result;

			internal void _003CCreateNewDarkly_003Eb__36(UIDarkly life)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass137_6
		{
			public UIDarklyChallengeReward result;

			internal void _003CCreateNewDarkly_003Eb__37(UIDarklyChallenge challenge)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass140_0
		{
			public UIDarklyManager _003C_003E4__this;

			public GameObject darklyGO;

			public RectTransform darklyRect;

			public Action onShown;

			public UIDarkly darkly;

			public int translationOwner;

			public Life translationLife;

			public Func<bool> translationCurrent;

			internal bool _003CRevealDarkly_003Eb__0()
			{
				return false;
			}

			internal void _003CRevealDarkly_003Eb__1(bool op)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass73_0
		{
			public int type;

			internal bool _003CCheckForDarkly_003Eb__0(UIDarkly x)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass80_0
		{
			public UIDarklyManager _003C_003E4__this;

			public Action onShown;

			public GameObject darklyGo;

			public int capturedA;

			public int capturedB;

			public Vector3 capturedPos;

			public int translationOwner;

			public Life translationLife;

			public Func<bool> translationCurrent;

			internal bool _003CTransitionDarklies_003Eb__0()
			{
				return false;
			}

			internal void _003CTransitionDarklies_003Eb__1(bool op)
			{
			}
		}

		private sealed class _003CFadeOutDarklyAnimation_003Ed__143 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyManager _003C_003E4__this;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeOutDarklyAnimation_003Ed__143(int _003C_003E1__state)
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

		private sealed class _003CHideDarklyAnimation_003Ed__145 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform darklyRect;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHideDarklyAnimation_003Ed__145(int _003C_003E1__state)
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

		private sealed class _003CInvokeAfterTransitionWhenRevealed_003Ed__128 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarkly darkly;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CInvokeAfterTransitionWhenRevealed_003Ed__128(int _003C_003E1__state)
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

		private sealed class _003CRefreshUIAfterTranslate_003Ed__82 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GameObject darklyGo;

			public UIDarklyManager _003C_003E4__this;

			private ScrollRect[] _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			private RectTransform _003Ccontent_003E5__4;

			private ContentSizeFitter _003CcontentSizeFitter_003E5__5;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRefreshUIAfterTranslate_003Ed__82(int _003C_003E1__state)
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

		private sealed class _003CRevealControlsAnimation_003Ed__141 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealControlsAnimation_003Ed__141(int _003C_003E1__state)
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

		private sealed class _003CRevealDarklyAnimation_003Ed__142 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform darklyRect;

			public UIDarklyManager _003C_003E4__this;

			public Action onShown;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealDarklyAnimation_003Ed__142(int _003C_003E1__state)
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

		private sealed class _003CRevealTitleAnimation_003Ed__147 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealTitleAnimation_003Ed__147(int _003C_003E1__state)
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

		private sealed class _003CShowSeasonsCommunityGoal_003Ed__125 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsCommunityGoal result;

			public SeasonModel data;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowSeasonsCommunityGoal_003Ed__125(int _003C_003E1__state)
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

		private sealed class _003CShowSeasonsTrack_003Ed__101 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsTrack result;

			public SeasonModel data;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CShowSeasonsTrack_003Ed__101(int _003C_003E1__state)
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

		private sealed class _003CTransitionDarkliesAnimation_003Ed__146 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyManager _003C_003E4__this;

			public Action onShown;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTransitionDarkliesAnimation_003Ed__146(int _003C_003E1__state)
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

		private sealed class _003CWaitForDailyRewardMarketplace_003Ed__121 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklySeasonsDailyReward owner;

			public string account;

			public string life;

			public UIDarklyManager _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitForDailyRewardMarketplace_003Ed__121(int _003C_003E1__state)
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

		private const string UAP_STRING_CLOSE = "Close window";

		private const string UAP_STRING_BACK = "Go to previous window";

		private const string UAP_STRING_RANDOMIZE = "Randomize all values";

		public static UIDarklyManager Inst;

		private const float REVEAL_DARKLY_DURATION_PART_A = 0.2f;

		private const float REVEAL_DARKLY_DURATION_PART_B = 0.1f;

		private const float HIDE_DURATION = 0f;

		private const float TRANSITION_DURATION = 0.35f;

		private const float TITLE_TRANSITION_DURATION = 0.35f;

		private const int MIN_DARKLY_COUNT_TO_CLOSE = 2;

		private AccessibleUIGroupRoot uapGroup;

		private RectTransform rect;

		private ConstraintScaleBgUI scale;

		private CanvasGroup canvasGroup;

		private FadeCandyImage shade;

		private RectTransform controls;

		private CandyButton topLeftButton;

		private AccessibleCandyButton uapTopLeftButton;

		private CandyButton topRightButton;

		private AccessibleCandyButton uapTopRightButton;

		private RectTransform sharedTitleContainer;

		private CandyImage titleEmoji;

		private CandyText titleText;

		private AccessibleLabel uapTitleText;

		private Color shadeColor;

		private Vector2 smallButtonSize;

		private Vector2 largeButtonSize;

		private int initialDarklyType;

		private int currentDarklyType;

		private int previousDarklyType;

		private int currentDarklyIndex;

		private int previousDarklyIndex;

		private int customLifeSubGroupType;

		private List<UIDarkly> darklies;

		private float topY;

		private Vector2 top;

		private Vector2 bottom;

		private Vector2 size_Default;

		private Vector2 size_Large;

		private UIDARKLY_ANIMATION_DIRECTION animDir;

		private Vector2 titleTop;

		private Vector2 titleBottom;

		private Scenario currentScenario;

		private bool _translationPending;

		private UIDarklyPromotion pendingPromotion;

		private bool promotionTranslated;

		private bool promotionVisible;

		private bool promotionClosing;

		private int promotionResumeFrame;

		private Action promotionFirstShown;

		private Coroutine promotionAnimation;

		private Coroutine promotionControlsAnimation;

		private bool retryPromotionsAfterOnboarding;

		private UIDarklySeasonsDailyReward dailyMarketplaceOwner;

		private Action dailyRewardClosed;

		private bool dismissalInProgress;

		private bool dailyMarketplacePresented;

		private float dailyMarketplacePreviousAlpha;

		private bool dailyMarketplacePreviousRaycasts;

		private bool dailyMarketplacePreviousInteractable;

		private GraphicRaycaster[] dailyMarketplaceSuspendedRaycasters;

		private int translationGeneration;

		public bool IsDailyRewardTombstoneReady => false;

		private bool IsDailyRewardNestedAtTombstone => false;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void LateUpdate()
		{
		}

		private void OnOnboardingActivityChanged()
		{
		}

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void Setup()
		{
		}

		public T GetDarkly<T>() where T : UIDarkly
		{
			return null;
		}

		public void UpdateDarklyLayout()
		{
		}

		public void UpdateDarklyButtons()
		{
		}

		private bool CheckForDarkly(int type, out int index)
		{
			index = default;
			return false;
		}

		private void SetTexture(CandyImage image, Texture2D texture)
		{
		}

		public CandyButton SetDarklyButtonSizeToLarge(CandyButton button)
		{
			return null;
		}

		public CandyButton SetDarklyButtonSizeToSmall(CandyButton button)
		{
			return null;
		}

		public CandyButton SetupDarklyButton(UIDarklyButton darklyButton, StuffItem item, string overrideName = "")
		{
			return null;
		}

		public CandyButton SetupDarklyButton(CandyButton button, Transform parent, int type, int colorType, string firstLinePrimaryText, string firstLineSecondaryText = null, string emojiString = null, string secondLineText = null, string iconString = null, bool isIAPStyle = false, bool isSaveStyle = false, bool iconIsOnTheRight = false)
		{
			return null;
		}

		public void RecalculateDarklyButtonHorizontalLayout(UIDarklyButton darklyButton)
		{
		}

		public void TransitionDarklies(int B, Action onShown = null)
		{
		}

		public void RefreshDarklyAfterTranslate(GameObject darklyGo)
		{
		}

		private IEnumerator RefreshUIAfterTranslate(GameObject darklyGo)
		{
			return null;
		}

		private void UpdateCommonUIElements(int A, int B, Vector3 pos)
		{
		}

		private void UpdateSharedTitleContainerText(string name)
		{
		}

		private void UpdateSharedTitleContainer(int A, int B, Vector2 pos)
		{
		}

		private void SetCurrentDarklyType(int type, int index)
		{
		}

		private void SetPreviousDarklyType(int type, int index)
		{
		}

		private void SetupTopLeftButton()
		{
		}

		private void SetupTopRightButton()
		{
		}

		private void TopLeftButtonPress()
		{
		}

		private void TopLeftButtonPressTelemetry()
		{
		}

		private void TopRightButtonPress()
		{
		}

		public bool IsShowingDarkly()
		{
			return false;
		}

		public bool IsShowingNonPromotionDarkly()
		{
			return false;
		}

		public void ReactivateCurrentContainerForAccessibility()
		{
		}

		public void Exit()
		{
		}

		public void ReleaseRetainedScenario()
		{
		}

		private void DismissAllDarklies()
		{
		}

		private void ResetTypes()
		{
		}

		public void ShowDarklySeasonsTrack(SeasonModel data)
		{
		}

		private IEnumerator ShowSeasonsTrack(UIDarklySeasonsTrack result, SeasonModel data)
		{
			return null;
		}

		public void ShowDarklySeasonsTrackInfo()
		{
		}

		public bool ShowDarklyPromotion(TriggerPromotionVariant data, ConfidenceVisualPromoTelemetryContext telemetryContext = null, Action onShown = null)
		{
			return false;
		}

		private void OnPromotionTranslated(UIDarklyPromotion promotion)
		{
		}

		private bool IsCurrentPromotion()
		{
			return false;
		}

		public bool CanInteractWithPromotion(UIDarklyPromotion promotion)
		{
			return false;
		}

		private static bool IsPromotionInputReleased()
		{
			return false;
		}

		private void UpdatePromotionPresentation()
		{
		}

		private void OnPromotionShown(UIDarklyPromotion promotion)
		{
		}

		private void SuspendPromotion()
		{
		}

		private void ClearPromotionState()
		{
		}

		private void CancelPendingPromotion()
		{
		}

		private void TrackCurrentPromotionDismissal()
		{
		}

		public bool ShowDarklySeasonsDailyReward(SeasonsDailyRewardModel data, double? apiCallStartedAt = null, Action onClosed = null)
		{
			return false;
		}

		public void ShowDailyRewardMarketplace()
		{
		}

		private void SuspendDailyRewardForMarketplace(UIDarklySeasonsDailyReward owner)
		{
		}

		private IEnumerator WaitForDailyRewardMarketplace(UIDarklySeasonsDailyReward owner, string account, string life)
		{
			return null;
		}

		private void CancelDailyRewardMarketplace(UIDarklySeasonsDailyReward owner)
		{
		}

		private void RestoreDailyRewardAfterMarketplace(UIDarklySeasonsDailyReward owner)
		{
		}

		public void ShowDarklySeasonsCommunityGoal(SeasonModel data)
		{
		}

		private IEnumerator ShowSeasonsCommunityGoal(UIDarklySeasonsCommunityGoal result, SeasonModel data)
		{
			return null;
		}

		public void ShowDarklyCustomLife(StuffItem tokenLifeItem = null)
		{
		}

		public void ShowDarklyIsolatedReward()
		{
		}

		private IEnumerator InvokeAfterTransitionWhenRevealed(UIDarkly darkly)
		{
			return null;
		}

		public void ShowDarklyIsolated(UIDARKLY_TYPE darklyType)
		{
		}

		public void ShowDarklyWithScenario(Scenario scenario, int darklyType)
		{
		}

		public void ShowDarklyWithChallenge(int challengeID)
		{
		}

		public void ShowDarklyWithReleaseNotes(ArrayList releaseNotes)
		{
		}

		public void ShowDarklyInformation(int infoType)
		{
		}

		public void ShowDarklyPaywall(int paywallType, int IAP_Origin, Scenario followUpScenario = null, Scenario followUpChainedScenario = null, int jobType = 0, int exPackType = 0, int nestedIAPOrigin = -1, int bundleType = 0, int itemType = 0, IAP_TYPE iapType = IAP_TYPE.NONE, Action onShown = null)
		{
		}

		private bool TryGetCurrent<T>(Action<T> success) where T : UIDarkly
		{
			return false;
		}

		private bool TryCreate<T>(UIDARKLY_TYPE darklyType, string fileTitle, out T result, bool makeActive = true, bool moveTop = true, Scenario scenario = null) where T : UIDarkly
		{
			result = null;
			return false;
		}

		private void CreateNewDarkly(int darklyType, Scenario scenario = null, int IAP_Origin = -1, int challengeID = -1, Scenario followUpChainedScenario = null, int jobType = 0, int exPackType = 0, int expansionPackIAPOrigin = -1, int bundleType = 0, int itemType = 0, int infoType = 0, IAP_TYPE iapType = IAP_TYPE.NONE, StuffItem tokenLifeItem = null)
		{
		}

		public int GetPreviousDarklyType()
		{
			return 0;
		}

		public int GetCurrentDarklyType()
		{
			return 0;
		}

		private void RevealDarkly(Action onShown = null)
		{
		}

		private IEnumerator RevealControlsAnimation()
		{
			return null;
		}

		private IEnumerator RevealDarklyAnimation(RectTransform darklyRect, Action onShown = null)
		{
			return null;
		}

		private IEnumerator FadeOutDarklyAnimation()
		{
			return null;
		}

		private void CompleteDarklyDismissal()
		{
		}

		private IEnumerator HideDarklyAnimation(RectTransform darklyRect)
		{
			return null;
		}

		private IEnumerator TransitionDarkliesAnimation(Action onShown = null)
		{
			return null;
		}

		private IEnumerator RevealTitleAnimation()
		{
			return null;
		}
	}
}
