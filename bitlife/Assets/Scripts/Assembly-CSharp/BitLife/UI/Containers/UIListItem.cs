using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using BitLife.UI.Utils;
using Game.Dynamic.Marketplace;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Containers
{
	public class UIListItem : MonoBehaviour
	{
		private enum GradientDirection
		{
			TopToBottom = 0,
			BottomToTop = 1,
			LeftToRight = 2,
			RightToLeft = 3
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<int> _003C_003E9__153_0;

			public static Action<int> _003C_003E9__189_0;

			public static Action<bool> _003C_003E9__189_2;

			public static Action<int> _003C_003E9__189_3;

			public static Action<int> _003C_003E9__189_4;

			public static Action<int> _003C_003E9__189_5;

			public static Action<int> _003C_003E9__189_7;

			public static Action<int> _003C_003E9__189_8;

			internal void _003CPrivacyPolicyLinkClick_003Eb__153_0(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_0(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_2(bool success)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_3(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_4(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_5(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_7(int index)
			{
			}

			internal void _003CPerformListItemAction_003Eb__189_8(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass121_0
		{
			public UIListItem _003C_003E4__this;

			public string bannerString;

			internal void _003CSetupRightHandPriceBanner_003Eb__0(List<string> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass189_0
		{
			public int action;

			internal void _003CPerformListItemAction_003Eb__1(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass189_1
		{
			public Action executeAction;

			internal void _003CPerformListItemAction_003Eb__6(int index)
			{
			}
		}

		private sealed class _003CAnimateSaleTimer_003Ed__115 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public string timeString;

			public UIListItem _003C_003E4__this;

			private int _003Chours_003E5__2;

			private int _003Cminutes_003E5__3;

			private int _003Cseconds_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CAnimateSaleTimer_003Ed__115(int _003C_003E1__state)
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

		private sealed class _003CLaunchRiotScenario_003Ed__202 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListItem _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLaunchRiotScenario_003Ed__202(int _003C_003E1__state)
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

		private sealed class _003CLaunchRiotScenarioPart2_003Ed__203 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListItem _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLaunchRiotScenarioPart2_003Ed__203(int _003C_003E1__state)
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

		private sealed class _003CLoadTakeoverList_003Ed__187 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListItem _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLoadTakeoverList_003Ed__187(int _003C_003E1__state)
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

		private sealed class _003CLoadingSaveLife_003Ed__201 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLoadingSaveLife_003Ed__201(int _003C_003E1__state)
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

		private sealed class _003CPrimaryTextObjectPoolLogic_003Ed__143 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIListItem _003C_003E4__this;

			private SimStockEvent _003CpreviousSimStockEvent_003E5__2;

			private float _003ClongestWidth_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPrimaryTextObjectPoolLogic_003Ed__143(int _003C_003E1__state)
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

		private const float ALPHA_MIN_THRESHOLD = 0.01f;

		private const float HEIGHT_NORMAL = 224f;

		private const float HEIGHT_NORMAL_TABLET = 168f;

		private const float HEIGHT_SEPARATOR = 69f;

		private const float HEIGHT_SEPARATOR_TABLET = 52f;

		private const float HEIGHT_ADDITION_FOR_COPYRIGHT = 30f;

		private const float HEIGHT_ADDITION_FOR_COPYRIGHT_TABLET = 22.5f;

		private const float RIGHT_SIDE_GAP = 60f;

		private const float SECONDARY_IMAGE_SPACE = 150f;

		private const float SECONDARY_IMAGE_SPACE_WIDE = 260f;

		private const float RIGHT_SIDE_SPACE = 104f;

		private const float LIVE_IMAGE_SPACE = 120f;

		private const float PRIMARY_IMAGE_SPACE = 80f;

		private const float BUBBLE_TYPE_BANNER_POS_X = 27f;

		private const float BUBBLE_TYPE_BANNER_POS_Y = -18f;

		private const float ANGLED_TYPE_BANNER_POS_X = 0f;

		private const float ANGLED_TYPE_BANNER_POS_Y = 0f;

		private const float STOCK_TICKER_ANIM_DURATION_PER_100_PIXELS = 1f;

		private const string STOCK_COLOR_POSITIVE = "4c9845";

		private const string STOCK_COLOR_NEGATIVE = "e53000";

		private const char STOCK_UP = '▲';

		private const char STOCK_DOWN = '▼';

		private const string BLACKMARKET_BG = "FDE6FF";

		private const string BLACKMARKET_PRIMARY_TEXT = "525C7B";

		private const string BLACKMARKET_SECONDARY_TEXT = "49526D";

		private const string BLACKMARKET_EMOJI_FADE_COLOR = "2B233E";

		private const float BLACKMARKET_EMOJI_FADE_STRENGTH = 0.7f;

		private const float SALE_MODULE_WIDTH = 416f;

		public Transform CustomHolder;

		public UIList parentList;

		private LayoutElement layoutElement;

		private RectTransform rect;

		private UIFontManager fontManager;

		private ConstraintScaleBgUI scale;

		private CandyImage background;

		private CandyImage bgUnderlayGradient;

		private CandyImage bgOverlayPattern;

		private CandyButton itemButton;

		private AudioClip buttonSound;

		private CandyImage pressedStateDisplayObject;

		public RectTransform primaryTextContainer;

		private CandyText primaryText;

		public List<CandyText> primaryTextPoolObjects;

		public List<int> availablePoolObjectIndices;

		public List<int> inUsePoolObjectIndices;

		private List<SimStock> stocks;

		private int stockListLeaderIndex;

		private int stockListTailIndex;

		private CandyImage primaryEmoji;

		private CandyImage statusEmoji;

		private Texture2D tableLive;

		private Texture2D tableEasy;

		private Texture2D tableMedium;

		private Texture2D tableHard;

		private CandyImage secondaryImage;

		private CandyText rightHandText;

		private CandyImage rightHandBanner;

		private CandyText rightHandBannerText;

		private CandyImage rightHandImage;

		private CandyImage prisonBarsImage;

		private Texture2D prisonBarsTexture;

		private CandyText secondLine;

		private CandyImage barGraphShadowImage;

		private CandyBarGraph barGraph;

		private CandyImage bottomBorder;

		private CandyButton privacyPolicyButton;

		private CandyText[] privacyPolicyTexts;

		private RectTransform clockAnimator;

		private Transform salesModuleParent;

		private UIListItemSalesModule salesModuleTemplate;

		private UIListItemSalesModule salesModuleTabletTemplate;

		private UIListItemSalesModule salesModule;

		private CandyImage leftSperm;

		private CandyImage rightSperm;

		public GameObject sortGroup;

		private CandyText sort;

		private CandyButton datePlayed;

		private CandyButton legacy;

		private CandyButton netWorth;

		private CandyButton ribbon;

		private CandyButton age;

		private CandyButton children;

		private CandyButton lovers;

		private bool disabled;

		private float runningWidth;

		private float rightHandBannerWidth;

		private float rightHandTextWidth;

		private string marketPlacePriceForVoice;

		private Color standardBgColor;

		private float emojiWidth;

		private float parentWidth;

		public ListItem data;

		private bool Checked__BackingField;

		public string title;

		public string emojiString;

		public int indexInList;

		public int childListSortCategory;

		private bool isInEarnableList;

		private int themeOverride;

		private bool gameVarsThemeIsDark;

		private bool isAnOwnedItem;

		private bool showsTimedMarketplaceAccess;

		private int customType;

		private Coroutine animateSaleTimerCoroutine;

		public CandyText PrimaryText => null;

		public bool IsDisabled => false;

		private bool Checked
		{
			get
			{
				return Checked__BackingField;
			}
			set
			{
				Checked__BackingField = value;
			}
		}

		private void OnDisable()
		{
		}

		private void Awake()
		{
		}

		private void OnTimeUpdate()
		{
		}

		public void SetupListItem(ListItem data, bool isInEarnableList, AccessibleUIGroupRoot uapGroup, Color overrideColor, int indexInList, int childListSortCategory = 0, int customType = 0)
		{
		}

		private void SetupStandardItem()
		{
		}

		private IEnumerator AnimateSaleTimer(string timeString)
		{
			return null;
		}

		private void SaleModuleLogic()
		{
		}

		private void ThemeBackgroundLogic()
		{
		}

		private void OnShopTypeChanged(UIShopType shopType)
		{
		}

		private bool SetupRightHandPrice()
		{
			return false;
		}

		private void SetupRightHandPriceBanner()
		{
		}

		private void UpdateClock()
		{
		}

		private bool SeasonsRightHandTextLogic()
		{
			return false;
		}

		private void SetRightHandImage(Texture2D texture, bool force = false)
		{
		}

		private void SeasonsStatusLogic()
		{
		}

		private void SetSize(CandyImage image, Vector2 size)
		{
		}

		private void SetAnchors(RectTransform rect, Vector2 both)
		{
		}

		private void SetPivot(RectTransform rect, Vector2 pivot, bool matchAnchors = true)
		{
		}

		private bool IsOwned(int action)
		{
			return false;
		}

		private string SetIsOwnedAndGetPrice(int action)
		{
			return null;
		}

		private string AddCurrency(string p1, string p2)
		{
			return null;
		}

		private void UAPLogic()
		{
		}

		private void SetUAPText()
		{
		}

		private void StatusEmojiLogic(bool setSize = true)
		{
		}

		private void BarGraphLogic()
		{
		}

		public int GetBubbleCount()
		{
			return 0;
		}

		private void SecondLineTextLogic()
		{
		}

		public void StockMarketColoring()
		{
		}

		public void PortfolioColoring(SimInvestment theInvestment)
		{
		}

		private void BondColoringAtStart()
		{
		}

		private void PrimaryEmojiLogic()
		{
		}

		private void PrimaryTextLogic()
		{
		}

		public IEnumerator PrimaryTextObjectPoolLogic()
		{
			return null;
		}

		private float GetPercent(SimStock stock, ref SimStockEvent previousSimStockEvent)
		{
			return 0f;
		}

		private void RightHandImageLogic()
		{
		}

		public void BecomeBitizen()
		{
		}

		private bool IsDarkMode(string title)
		{
			return false;
		}

		private void SecondaryImageLogic()
		{
		}

		private void ShowEllipsis()
		{
		}

		private void SetupButtonStyleItem()
		{
		}

		public void SetupHeight(float height)
		{
		}

		private void SetupCopyrightItem()
		{
		}

		private void PrivacyPolicyLinkClick()
		{
		}

		public void SetBackGroundColor(Color color)
		{
		}

		public void SetBottomBorderColor(Color color)
		{
		}

		public void SetEmojiWithSprite(Sprite emojiSprite)
		{
		}

		private Texture2D BlendTextureWithGradient(Texture2D t1, GradientDirection dir, float strength)
		{
			return null;
		}

		private void SetupSeparatorItem(AccessibleUIGroupRoot uapGroup)
		{
		}

		private void SetupSeparatorBackground()
		{
		}

		private void SetupItemGlobalSpecs()
		{
		}

		private void StatusLogic()
		{
		}

		private bool SetLiveForChallenge(int challengeId)
		{
			return false;
		}

		private bool SetDifficultyForChallenge(ChallengeMetadata metaData)
		{
			return false;
		}

		private bool SetStatusEmoji(Texture2D texture, float width = 120f)
		{
			return false;
		}

		private bool SetStatusEmoji(Sprite sprite, float width = 120f)
		{
			return false;
		}

		private Texture2D TextureForChallengeDifficulty(int difficulty)
		{
			return null;
		}

		private void EmojiLogic()
		{
		}

		private void DisableLogic()
		{
		}

		private void SetupSpecialBackgroud()
		{
		}

		private void SetupSpecialSound()
		{
		}

		public void SetupSortTitles()
		{
		}

		public void TurnOffTitle()
		{
		}

		public void TurnOnSortGroup()
		{
		}

		public void SetSecondLineText(string text)
		{
		}

		public void SetSecondLineFontColor(Color color)
		{
		}

		public void SetPrimaryText(string text)
		{
		}

		public void SetPrimaryTextFont(FontsEnum font)
		{
		}

		public void SetPrimaryTextColor(Color color)
		{
		}

		public void SetPrimaryTextSize(int fontSize)
		{
		}

		public void TurnOnBottomBorder()
		{
		}

		public void SetupSperm()
		{
		}

		private void SetupGeneralAttributes(Color overrideColor)
		{
		}

		private void SetupBlankItem()
		{
		}

		private void SetupBottomBorder()
		{
		}

		private void OnClick()
		{
		}

		private IEnumerator LoadTakeoverList()
		{
			return null;
		}

		private void AccessibilityCallBackToQuit(int index)
		{
		}

		private void PerformListItemAction(int action, string title, bool isFeatured, string secondaryIDText, Scenario scenario = null)
		{
		}

		public List<int> MarketplaceValues()
		{
			return null;
		}

		private void SortDatePlayedButton()
		{
		}

		private void SortLegacyButton()
		{
		}

		private void SortNetWorthButton()
		{
		}

		private void SortRibbonButton()
		{
		}

		private void SortAgeButton()
		{
		}

		private void SortChildrenButton()
		{
		}

		private void SortLoversButton()
		{
		}

		private void OnDestroy()
		{
		}

		internal static bool ValidItemExistsInList(List list)
		{
			return false;
		}

		private void LoadSaveLife()
		{
		}

		private IEnumerator LoadingSaveLife()
		{
			return null;
		}

		private IEnumerator LaunchRiotScenario()
		{
			return null;
		}

		private IEnumerator LaunchRiotScenarioPart2()
		{
			return null;
		}

		public void SetupCustomItem(ListItem dataCustom)
		{
		}

		private bool _003CBondColoringAtStart_003Eb__140_0(SimInvestment x)
		{
			return false;
		}

		private void _003COnClick_003Eb__186_0(int index)
		{
		}
	}
}
