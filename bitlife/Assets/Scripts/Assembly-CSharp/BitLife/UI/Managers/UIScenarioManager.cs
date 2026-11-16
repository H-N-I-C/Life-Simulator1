using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Containers;
using BitLife.UI.Interfaces;
using BitLife.UI.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Managers
{
	public class UIScenarioManager : MonoBehaviour
	{
		private sealed class _003C_003Ec__DisplayClass105_0
		{
			public UIScenarioManager _003C_003E4__this;

			public int choiceIndex;

			public ScenarioChoice choice;

			internal void _003CXChoiceButtonPress_003Eb__0(int index)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass98_0
		{
			public UIScenarioManager _003C_003E4__this;

			public int owner;

			public Life life;

			public Func<bool> isCurrent;

			public Scenario dataObject;

			internal bool _003CLaunchScenarioOrDarkly_003Eb__0()
			{
				return false;
			}

			internal void _003CLaunchScenarioOrDarkly_003Eb__1(bool result)
			{
			}
		}

		private sealed class _003CDie_003Ed__104 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public ScenarioDeathHero deathHero;

			private CandyImage _003Cci_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDie_003Ed__104(int _003C_003E1__state)
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

		private sealed class _003CFadeAnimation_003Ed__84 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public int damage;

			private Color _003CstartColor_003E5__2;

			private Color _003CendColor_003E5__3;

			private int _003Ci_003E5__4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CFadeAnimation_003Ed__84(int _003C_003E1__state)
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

		private sealed class _003CLightningAnimationWithOptionalDelay_003Ed__128 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public Scenario cachedScenario;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CLightningAnimationWithOptionalDelay_003Ed__128(int _003C_003E1__state)
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

		private sealed class _003CMorph_003Ed__95 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public RectTransform rect;

			public float startSize;

			public float endSize;

			public float duration;

			private float _003CelapsedTime_003E5__2;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CMorph_003Ed__95(int _003C_003E1__state)
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

		private sealed class _003CPressButton_003Ed__93 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public Scenario originalScenario;

			public GameObject originalCard;

			public RectTransform targetRect;

			public int goalIndex;

			public RectTransform handRect;

			private GameObject _003CbuttonObject_003E5__2;

			private CandyButton _003CgoalCandyButton_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CPressButton_003Ed__93(int _003C_003E1__state)
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

		private sealed class _003CRedAlert_003Ed__83 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public bool includeSound;

			public int damage;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRedAlert_003Ed__83(int _003C_003E1__state)
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

		private sealed class _003CRevealCard_003Ed__120 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public UIScenarioCard card;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRevealCard_003Ed__120(int _003C_003E1__state)
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

		private sealed class _003CSetOverlayWithDelay_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public CandyButton button;

			public bool state;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetOverlayWithDelay_003Ed__94(int _003C_003E1__state)
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

		private sealed class _003CStartEmittingAfterFrame_003Ed__108 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			public SCENARIO_BACKGROUND_EFFECT bgEffect;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStartEmittingAfterFrame_003Ed__108(int _003C_003E1__state)
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

		private sealed class _003CSurpriseMeAnimation_003Ed__90 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIScenarioManager _003C_003E4__this;

			private Scenario _003CoriginalScenario_003E5__2;

			private GameObject _003CoriginalCard_003E5__3;

			private List<RectTransform> _003CoriginalButtonRects_003E5__4;

			private RectTransform _003ChandRect_003E5__5;

			private int _003CgoalIndex_003E5__6;

			private RectTransform _003CtargetButtonRect_003E5__7;

			private List<int> _003Ctargets_003E5__8;

			private List<int>.Enumerator _003C_003E7__wrap8;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSurpriseMeAnimation_003Ed__90(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			void IEnumerator.Reset()
			{
			}
		}

		private const float SPACER_TOP_HEIGHT = 124f;

		private const float SPACER_TOP_HEIGHT_FEEDBACK = 150f;

		private const float FOCUSBAR_HEIGHT = 122f;

		private const float SPACER_UNDER_FOCUSBAR_HEIGHT = 116f;

		private const float SPACER_30 = 30f;

		private const float SPACER_55 = 55f;

		private const float SPACER_60 = 60f;

		private const float SPACER_85 = 85f;

		private const float SPACER_BOTTOM_HEIGHT = 120f;

		private const float SPACER_SIDES = 34f;

		private const float SLIDER_EMOJI_SIZE = 50f;

		private const string SURPRISE_ME = "Surprise me!";

		private const float SURPRISE_ME_EMOJI_SPACE = 35f;

		private const float SCENARIO_BUTTON_HEIGHT = 161f;

		private const float SCENARIO_BUTTON_WIDTH = 942f;

		private const float DAMAGE_ALPHA_MIN = 0f;

		private const float DAMAGE_ALPHA_MAX = 0.65f;

		private const float DAMAGE_FADE_DURATION = 0.1f;

		private UIScenarioCardStyle style;

		private Dictionary<string, UIScenarioCardStyle> styles;

		private UIScenarioColorScheme colorScheme;

		private Color color_0_90_152_darkBlue;

		private Color color_0_104_185_thirdDarkBlue;

		private Color color_98_103_107_darkBlueGray;

		private Color color_255_241_0_yellow;

		private CandyImageOptions spacer55;

		private CandyImageOptions spacer85;

		private CandyImageOptions spacer30;

		private CandyImageOptions spacerOverTitle;

		private Color yellow;

		private Color orange;

		private Color red;

		private Color purple;

		private Color blue;

		private Color green;

		private Color lime;

		private Scenario scenario;

		public List<RectTransform> buttonRects;

		private Image canvasBG;

		private GameObject inputBlocker;

		private GameObject surpriseMeButtonHand;

		private CandyImage scenarioDamageLayer;

		private GameObject scenarioShadeLayer;

		private UIEmoticonEmitterManager emoticonManager;

		private GameObject logoBanner;

		private CandyText logoBannerVersion;

		private string alertChoiceVideoIconName;

		private string alertChoiceCoinIconName;

		private string sliderHandleName;

		private string diceGraphicName;

		private RectTransform bloodCurtain;

		private GameObject scenarioCardPrefab;

		private Transform scenarioCardParent;

		private RectTransform canvasRect;

		private RectTransform safeZoneRect;

		private GameObject[] candyObjectPrefabs;

		private Hashtable candyObjectTable;

		private List<Color> dropDownColors;

		private UIScenarioCardOptions[] cards;

		private int currentCardIndex;

		private GameObject currentCard;

		private UIScenarioCard scenarioCard;

		private readonly HashSet<Scenario> ageUpPresentedScenarios;

		private List<string> inputFieldStrings;

		public string customScenarioSoundFileName;

		public static Action OnScenarioUpdate;

		private Action<Scenario> m_OnAnyClosed;

		private bool _isSurpriseMeRunning;

		private int translationGeneration;

		public static Action<Scenario> OnRevealScenario;

		private readonly List<GenesisSliderPool> _genesisSliderPools;

		public Scenario CurrentScenario
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public ConstraintScaleScenario ScenarioCardScale => null;

		public bool IsCurrentCardAvailable => false;

		public event Action<Scenario> OnAnyClosed
		{
			add
			{
			}
			remove
			{
			}
		}

		private void OnDisable()
		{
		}

		private void Start()
		{
		}

		public UIScenarioCard GetScenarioCard()
		{
			return null;
		}

		public void UpdateScenarioTheme()
		{
		}

		public void DoDamage(int damage, bool includeSound = true)
		{
		}

		private IEnumerator RedAlert(int damage, bool includeSound = true)
		{
			return null;
		}

		private IEnumerator FadeAnimation(int damage)
		{
			return null;
		}

		private UIScenarioColorScheme GetColorScheme(int index)
		{
			return null;
		}

		public UIScenarioCardOptions SetupScenario(Scenario data)
		{
			return null;
		}

		private string CallbackForSetInputFieldString(string inputString, int listIndex, ScenarioInputField field)
		{
			return null;
		}

		private void SetupScenarioCardStyles()
		{
		}

		private void SurpriseMeOnClick()
		{
		}

		private IEnumerator SurpriseMeAnimation()
		{
			return null;
		}

		private Vector3 DeriveGoToPosition(int target)
		{
			return default;
		}

		private void CleanupSurpriseMe()
		{
		}

		private IEnumerator PressButton(int goalIndex, RectTransform handRect, Scenario originalScenario, GameObject originalCard, RectTransform targetRect)
		{
			return null;
		}

		private IEnumerator SetOverlayWithDelay(CandyButton button, bool state, float delay)
		{
			return null;
		}

		private IEnumerator Morph(RectTransform rect, float startSize, float endSize, float duration)
		{
			return null;
		}

		public void LaunchFakeScenarioCard()
		{
		}

		public void LaunchScenarioCard(UIScenarioCardOptions scenarioCardOptions)
		{
		}

		public void LaunchScenarioOrDarkly(Scenario dataObject)
		{
		}

		private void HandleInvalidScenarioLaunch(Scenario invalidScenario)
		{
		}

		private void CompleteScenarioChain(Scenario endScenario)
		{
		}

		private void ContinueScenario(Scenario dataObject)
		{
		}

		public void ShowVampireTransformation(Scenario nextScenario)
		{
		}

		public void OnVampireTransformationEnded()
		{
		}

		private IEnumerator Die(ScenarioDeathHero deathHero)
		{
			return null;
		}

		public void XChoiceButtonPress(int choiceIndex)
		{
		}

		private void AcceptChoice(ScenarioChoice choice, int choiceIndex)
		{
		}

		public void TurnOnShadeAndStartEmoticonEmittingManager(Scenario dataObject)
		{
		}

		private IEnumerator StartEmittingAfterFrame(SCENARIO_BACKGROUND_EFFECT bgEffect)
		{
			return null;
		}

		public void TurnOffShade()
		{
		}

		public void FadeOutShade(float delay = 0f)
		{
		}

		public void RemoveAlert()
		{
		}

		private void AdvanceScenarioChain(Scenario currentScenario)
		{
		}

		public void XDismissButtonPress()
		{
		}

		public GameObject CreateCandyObject(IguiOptions boxedOptions)
		{
			return null;
		}

		public GameObject CreateDisplayObject(CandyImageOptions imageOptions, CandyTextOptions textOptions)
		{
			return null;
		}

		public GameObject CreateScenarioTextLabel(float width, string title)
		{
			return null;
		}

		public GameObject CreateScenarioButtonDisplayObject(float width, string title, bool isDefaultStyle, int iconStyle, string IAP, bool forceGreen, int iconType = 0)
		{
			return null;
		}

		public GameObject CreateSurpriseMeDisplayObject(CandyImageOptions imageOptions, CandyTextOptions textOptions)
		{
			return null;
		}

		public UIScenarioCard CreateCard(UIScenarioCardOptions tempScenario)
		{
			return null;
		}

		private IEnumerator RevealCard(UIScenarioCard card)
		{
			return null;
		}

		private void SetSliderThumbHandleScale()
		{
		}

		private void WireGenesisSliderPools(UIScenarioCard card)
		{
		}

		private GameObject InstantiateMiniGame(int miniGame, GameObject layoutNode)
		{
			return null;
		}

		private float GetMiniGameWidth(int miniGame)
		{
			return 0f;
		}

		private bool IsMiniGameAvailableForType(int type)
		{
			return false;
		}

		private IEnumerator LightningAnimationWithOptionalDelay(Scenario cachedScenario)
		{
			return null;
		}

		public void InjectScenario(Scenario newScenario)
		{
		}
	}
}
