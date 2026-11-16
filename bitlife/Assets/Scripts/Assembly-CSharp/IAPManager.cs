using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BitLife.UI.Managers;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;

public class IAPManager : MonoBehaviour
{
	private sealed class UnconfirmedOrder
	{
		public PendingOrder Order;

		public int Attempts;

		public Coroutine Retry;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ProductDefinition, string> _003C_003E9__248_0;

		public static Func<PendingOrder, string> _003C_003E9__262_0;

		public static Func<string, bool> _003C_003E9__262_1;

		public static Func<KeyValuePair<string, UnconfirmedOrder>, bool> _003C_003E9__285_0;

		public static Func<KeyValuePair<string, UnconfirmedOrder>, UnconfirmedOrder> _003C_003E9__285_2;

		public static Func<CultureInfo, RegionInfo> _003C_003E9__297_0;

		public static Func<Product, string> _003C_003E9__307_0;

		internal string _003COnProductsFetchFailed_003Eb__248_0(ProductDefinition aDefinition)
		{
			return null;
		}

		internal string _003COnPurchasesFetched_003Eb__262_0(PendingOrder anOrder)
		{
			return null;
		}

		internal bool _003COnPurchasesFetched_003Eb__262_1(string aTransactionId)
		{
			return false;
		}

		internal bool _003CReissueUnconfirmedOrders_003Eb__285_0(KeyValuePair<string, UnconfirmedOrder> aPair)
		{
			return false;
		}

		internal UnconfirmedOrder _003CReissueUnconfirmedOrders_003Eb__285_2(KeyValuePair<string, UnconfirmedOrder> aPair)
		{
			return null;
		}

		internal RegionInfo _003CGetIsoCurrencyCode_003Eb__297_0(CultureInfo culture)
		{
			return null;
		}

		internal string _003CCompleteRestore_003Eb__307_0(Product aProduct)
		{
			return null;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass136_0
	{
		public Dictionary<string, string> dict;
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass143_0
	{
		public List<ProductDefinition> products;

		public IAPManager _003C_003E4__this;
	}

	private sealed class _003C_003Ec__DisplayClass143_1
	{
		public string id;

		internal bool _003CBuildProductDefinitions_003Eb__3(ProductDefinition aDefinition)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass267_0
	{
		public Product product;

		internal bool _003CIsConsumable_003Eb__0(string consumableId)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass270_0
	{
		public string purchaseId;

		public IAPManager _003C_003E4__this;

		internal void _003CCallRestoreIfDuplicatedPurchase_003Eg__GrantTappedProduct_007C0(string origin)
		{
		}

		internal void _003CCallRestoreIfDuplicatedPurchase_003Eb__1(int count)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass282_0
	{
		public UnconfirmedOrder entry;

		public IAPManager _003C_003E4__this;

		public string transactionId;

		internal void _003CScheduleConfirmRetry_003Eb__0()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass285_0
	{
		public HashSet<string> onlyTransactions;

		internal bool _003CReissueUnconfirmedOrders_003Eb__1(KeyValuePair<string, UnconfirmedOrder> aPair)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass297_0
	{
		public string isoCode;

		internal bool _003CGetIsoCurrencyCode_003Eb__1(RegionInfo region)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass301_0
	{
		public int IAPType;

		internal bool _003CGetOriginalPriceString_003Eb__0(KeyValuePair<string, object> x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass306_0
	{
		public Coroutine timeout;

		public IAPManager _003C_003E4__this;

		public bool done;

		public Action<Orders> onFetched;

		public Action<PurchasesFetchFailureDescription> onFetchFailed;

		public Action<int> callback;

		public bool isRetry;

		internal void _003CFetchPurchasesForRestore_003Eg__StopTimeout_007C0()
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eg__Unhook_007C1()
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eg__Fail_007C2(string reason)
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eg__ArmTimeout_007C3(float seconds, string phase)
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eb__4(Orders orders)
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eb__5(PurchasesFetchFailureDescription failureDescription)
		{
		}

		internal void _003CFetchPurchasesForRestore_003Eb__6(bool success, string error)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass306_1
	{
		public float seconds;

		public string phase;

		public _003C_003Ec__DisplayClass306_0 CS_0024_003C_003E8__locals1;

		internal void _003CFetchPurchasesForRestore_003Eb__7()
		{
		}
	}

	private sealed class _003CAfterRealtimeDelay_003Ed__257 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float seconds;

		public Action action;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CAfterRealtimeDelay_003Ed__257(int _003C_003E1__state)
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

	private sealed class _003CCatalogRecoveryAfterDelay_003Ed__251 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public IAPManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CCatalogRecoveryAfterDelay_003Ed__251(int _003C_003E1__state)
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

	[StructLayout((LayoutKind)3)]
	private struct _003CConnectStore_003Ed__144 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public IAPManager _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CIssueReconnect_003Ed__246 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public IAPManager _003C_003E4__this;

		public string reason;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	private sealed class _003CReconnectAfterDelay_003Ed__244 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public IAPManager _003C_003E4__this;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CReconnectAfterDelay_003Ed__244(int _003C_003E1__state)
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

	private sealed class _003CSayPurchaseResultDelayed_003Ed__289 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public IAPManager _003C_003E4__this;

		public string message;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSayPurchaseResultDelayed_003Ed__289(int _003C_003E1__state)
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

	[StructLayout((LayoutKind)3)]
	private struct _003CStart_003Ed__141 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncVoidMethodBuilder _003C_003Et__builder;

		public IAPManager _003C_003E4__this;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003CStartPurchase_003Ed__142 : IAsyncStateMachine
	{
		public int _003C_003E1__state;

		public AsyncTaskMethodBuilder _003C_003Et__builder;

		public IAPManager _003C_003E4__this;

		public int platform;

		private TaskAwaiter _003C_003Eu__1;

		private void MoveNext()
		{
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	public static IAPManager Inst;

	private StoreController _store;

	private bool _storeConnected;

	private bool _productsFetched;

	private bool _purchasesFetched;

	private bool _startupPurchasesFetchRetried;

	private Coroutine _catalogRefetch;

	private int _catalogRefetchAttempt;

	private bool _catalogFetchInFlight;

	private bool _catalogFailureReported;

	private const float RestoreFetchTimeoutSeconds = 20f;

	private const float CatalogFetchWatchdogSeconds = 60f;

	private Coroutine _catalogWatchdog;

	private List<ProductDefinition> _productDefinitions;

	private readonly Dictionary<string, Order> _ordersByProductId;

	private readonly Dictionary<string, UnconfirmedOrder> _unconfirmedOrders;

	private const int ConfirmMaxAttempts = 4;

	private const float RestoreSyncGuardSeconds = 120f;

	private Coroutine _reconnect;

	private int _reconnectAttempt;

	private static Action<Product, DateTime> m_OnProductPurchased;

	public const string Consume_StreakSaver = "com.candywriter.bitlife.streaksaverv2";

	public const string Consume_StreakSaverIOSVariant = "com.candywriter.bitlife.streaksaverv2";

	public const string Consume_BattlePass = "com.candywriter.bitlife.premiumpass";

	public static string Consume_TimeMachine;

	public static string NonConsume_Bitizenship;

	public static string NonConsume_GodMode;

	public static string NonConsume_RemoveAds;

	public static string NonConsume_BitizenshipAndGodMode;

	public static string NonConsume_LegacyBitizenship;

	public static string NonConsume_BossMode;

	public static string NonConsume_CareerPolitican;

	public static string NonConsume_CareerAthlete;

	public static string NonConsume_CareerMusician;

	public static string NonConsume_CareerMusicProducer;

	public static string NonConsume_CareerMafia;

	public static string NonConsume_CareerActor;

	public static string NonConsume_CareerHustler;

	public static string NonConsume_CareerBusiness;

	public static string NonConsume_CareerAstronaut;

	public static string NonConsume_CareerModel;

	public static string NonConsume_ChallengeVault;

	public static string NonConsume_UnlimitedTimeMachine;

	public static string NonConsume_GoldTheme;

	public static string NonConsume_ExpansionPackInvestor;

	public static string NonConsume_ExpansionPackLandlord;

	public static string NonConsume_BundleBillionaire0;

	public static string NonConsume_BundleBillionaire1;

	public static string NonConsume_BundleTimeIsMoney;

	public static string NonConsume_ExpansionPackBlackMarket;

	public static string NonConsume_ExpansionPackCult;

	public static string NonConsume_ExpansionPackSecretAgent;

	public static string NonConsume_ExpansionPackZoo;

	public static string NonConsume_ExpansionPackRacing;

	public static string NonConsume_ExpansionPackCasino;

	public static string NonConsume_CareerDealer;

	public static string NonConsume_BundleBirthdayStarter;

	public static string NonConsume_BundleBirthdayLengends;

	public static string NonConsume_ExpansionPackVampire;

	public static string NonConsume_BundleFameAndFortune0;

	public static string NonConsume_BundleFameAndFortune1;

	public static string NonConsume_BundleCrime0;

	public static string NonConsume_BundleCrime1;

	public static string NonConsume_BundlePartyAnimal0;

	public static string NonConsume_BundlePartyAnimal1;

	public static string NonConsume_PromiscuityPotion;

	public static string NonConsume_GoldenPassport;

	public static string NonConsume_AssassinsBlade;

	public static string NonConsume_HollywoodStar;

	public static string NonConsume_SpikedKnuckles;

	public static string NonConsume_GoldenPiggyBank;

	public static string NonConsume_GetOutJailCard;

	public static string NonConsume_GoldenDiploma;

	public static string NonConsume_GoldenPacifier;

	public static string NonConsume_GoldenNekoStatue;

	public static string NonConsume_FriendshipBracelet;

	public static string NonConsume_LuxuryAccess;

	public static string NonConsume_OutdoorLifestyle;

	public static string NonConsume_GoldenWrench;

	public static string NonConsume_GoldenResume;

	public static string NonConsume_AuraRing;

	public static string NonConsume_FighterMode;

	public static string NonConsume_MovieDirector;

	public static string NonConsume_BundleMovieDirectorHollywoodStar;

	public static string NonConsume_BitizenshipAlt;

	public static string NonConsume_RemoveAdsAlt1;

	public static string NonConsume_RemoveAdsAlt2;

	public static string NonConsume_BossModeAlt1;

	public static string NonConsume_BossModeAlt2;

	public static string NonConsume_BossModeAlt3;

	public static string NonConsume_BossModeAlt4;

	public static string NonConsume_BossModeAlt5;

	public static string NonConsume_BossModeAlt6;

	public static string NonConsume_godmodesale1off;

	public static string NonConsume_godmodesale2off;

	public static string NonConsume_bitizenshipv2sale1off;

	public static string NonConsume_bitizenshipv2sale2off;

	public static string NonConsume_bitizenshipv2sale3off;

	public static string NonConsume_careerathletesale1off;

	public static string NonConsume_careerathletesale2off;

	public static string NonConsume_landlordsale1off;

	public static string NonConsume_landlordsale2off;

	public static string NonConsume_bossmodesale1off;

	public static string NonConsume_bossmodesale2off;

	public static string NonConsume_bitizenshipgodmodesale1off;

	public static string NonConsume_bitizenshipgodmodesale2off;

	public static string NonConsume_goldenpacifiersale3off;

	public static string NonConsume_goldenpacifiersale4off;

	public static string NonConsume_cultsale1off;

	public static string NonConsume_cultsale2off;

	public static string NonConsume_goldenpiggybanksale3off;

	public static string NonConsume_goldenpiggybanksale4off;

	public static string NonConsume_casinosale1off;

	public static string NonConsume_casinosale2off;

	public static string NonConsume_goldenpassportsale3off;

	public static string NonConsume_goldenpassportsale4off;

	public static string NonConsume_unlimitedtimemachinesale1off;

	public static string NonConsume_unlimitedtimemachinesale2off;

	public static string NonConsume_secretagentsale1off;

	public static string NonConsume_secretagentsale2off;

	public static string NonConsume_hollywoodstar3off;

	public static string NonConsume_hollywoodstar4off;

	public static string NonConsume_careermodelsale1off;

	public static string NonConsume_careermodelsale2off;

	public static string NonConsume_bitizenshipgodmodesale3off;

	public static string NonConsume_bitizenshipgodmodesale4off;

	public static string NonConsume_goldendiplomasale3off;

	public static string NonConsume_goldendiplomasale4of;

	public static string NonConsume_racingexpansionsale1off;

	public static string NonConsume_racingexpansionsale2off;

	public static string NonConsume_BundleStarter1;

	public static string NonConsume_BundleStarter2;

	private static Dictionary<string, string> BundleToEmojiString;

	private bool ReturnComplete;

	private string PurchaseConfirmString;

	private bool _userInitiatedPurchase;

	private readonly HashSet<string> _confirmedTransactions;

	private const float PurchaseAnnounceDelay = 1.5f;

	private Coroutine _purchaseAnnounce;

	private bool _restoreInProgress;

	private List<string> _consumableIdList;

	public const int RestoreFailed = -1;

	public static event Action<Product, DateTime> OnProductPurchased
	{
		add
		{
		}
		remove
		{
		}
	}

	private static Dictionary<string, string> BuildBundleToEmojiString()
	{
		return null;
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private Task StartPurchase(int platform)
	{
		return null;
	}

	private List<ProductDefinition> BuildProductDefinitions()
	{
		return null;
	}

	private Task ConnectStore()
	{
		return null;
	}

	public void BuyRemoveAds()
	{
	}

	public void BuyTimeMachine()
	{
	}

	public void BuyStreakSaver()
	{
	}

	public void BuySeasonBattlePass()
	{
	}

	public void BuyBitizenship()
	{
	}

	public void BuyGodMode()
	{
	}

	public void BuyBitizenshipAndGodMode()
	{
	}

	public void BuyBossMode()
	{
	}

	public void BuyCareerPolitician()
	{
	}

	public void BuyCareerAthlete()
	{
	}

	public void BuyCareerMusician()
	{
	}

	public void BuyCareerMusicProducer()
	{
	}

	public void BuyCareerMafia()
	{
	}

	public void BuyCareerActor()
	{
	}

	public void BuyCareerHustler()
	{
	}

	public void BuyCareerBusiness()
	{
	}

	public void BuyCareerAstronaut()
	{
	}

	public void BuyCareerModel()
	{
	}

	public void BuyChallengeVault()
	{
	}

	public void BuyUnlimitedTimeMachine()
	{
	}

	public void BuyGoldTheme()
	{
	}

	public void BuyExpansionPackInvestor()
	{
	}

	public void BuyExpansionPackLandlord()
	{
	}

	public void BuyExpansionPackBlackMarket()
	{
	}

	public void BuyExpansionPackCult()
	{
	}

	public void BuyExpansionPackSecretAgent()
	{
	}

	public void BuyExpansionPackZoo()
	{
	}

	public void BuyExpansionPackRacing()
	{
	}

	public void BuyExpansionPackCasino()
	{
	}

	public void BuyExpansionPackVampire()
	{
	}

	public void BuyCareerDealer()
	{
	}

	public void BuyPromiscuityPotion()
	{
	}

	public void BuyGoldenPacifier()
	{
	}

	public void BuyGoldenDiploma()
	{
	}

	public void BuyGoldenPassport()
	{
	}

	public void BuyGoldenNekoStatue()
	{
	}

	public void BuyFriendshipBracelet()
	{
	}

	public void BuyAuraRing()
	{
	}

	public void BuyFighterMode()
	{
	}

	public void BuyAssassinsBlade()
	{
	}

	public void BuyHollywoodStar()
	{
	}

	public void BuySpikedKnuckles()
	{
	}

	public void BuyGoldenPiggyBank()
	{
	}

	public void BuyLuxuryAccess()
	{
	}

	public void BuyOutdoorLifestyle()
	{
	}

	public void BuyGoldenWrench()
	{
	}

	public void BuyGoldenResume()
	{
	}

	public void BuyGetOutJailCard()
	{
	}

	public void BuyBundleFameAndFortune()
	{
	}

	public void BuyBundleBillionaire()
	{
	}

	public void BuyBundleTimeIsMoney()
	{
	}

	public void BuyBundlePartyAnimal()
	{
	}

	public void BuyBundleCrime()
	{
	}

	public void BuyBundleStarter1()
	{
	}

	public void BuyBundleStarter2()
	{
	}

	public void BuyBundleBirthdayStarter()
	{
	}

	public void BuyBundleBirthdayLegends()
	{
	}

	public void BuyBundleMovieDirectorHollywoodStar()
	{
	}

	public void BuyGodmodesale1off()
	{
	}

	public void BuyGodmodesale2off()
	{
	}

	public void BuyBitizenshipv2sale1off()
	{
	}

	public void BuyBitizenshipv2sale2off()
	{
	}

	public void BuyCareerathletesale1off()
	{
	}

	public void BuyCareerathletesale2off()
	{
	}

	public void BuyLandlordsale1off()
	{
	}

	public void BuyLandlordsale2off()
	{
	}

	public void BuyBitizenshipv2sale3off()
	{
	}

	public void BuyBossmodesale1off()
	{
	}

	public void BuyBossmodesale2off()
	{
	}

	public void BuyBitizenshipgodmodesale1off()
	{
	}

	public void BuyBitizenshipgodmodesale2off()
	{
	}

	public void BuyGoldenpacifiersale3off()
	{
	}

	public void BuyGoldenpacifiersale4off()
	{
	}

	public void BuyCultsale1off()
	{
	}

	public void BuyCultsale2off()
	{
	}

	public void BuyGoldenpiggybanksale3off()
	{
	}

	public void BuyGoldenpiggybanksale4off()
	{
	}

	public void BuyCasinosale1off()
	{
	}

	public void BuyCasinosale2off()
	{
	}

	public void BuyGoldenpassportsale3off()
	{
	}

	public void BuyGoldenpassportsale4off()
	{
	}

	public void BuyUnlimitedtimemachinesale1off()
	{
	}

	public void BuyUnlimitedtimemachinesale2off()
	{
	}

	public void Buysecretagentsale1off()
	{
	}

	public void Buysecretagentsale2off()
	{
	}

	public void BuyHollywoodstar3off()
	{
	}

	public void BuyHollywoodstar4off()
	{
	}

	public void BuyCareermodelsale1off()
	{
	}

	public void BuyCareermodelsale2off()
	{
	}

	public void BuyBitizenshipgodmodesale3off()
	{
	}

	public void BuyBitizenshipgodmodesale4off()
	{
	}

	public void BuyGoldendiplomasale3off()
	{
	}

	public void BuyGoldendiplomasale4off()
	{
	}

	public void BuyRacingExpansionSale1off()
	{
	}

	public void BuyRacingExpansionSale2off()
	{
	}

	private void OnStoreConnected()
	{
	}

	private void OnStoreDisconnected(StoreConnectionFailureDescription failureDescription)
	{
	}

	private void ScheduleReconnect(string reason)
	{
	}

	private void CancelScheduledReconnect()
	{
	}

	private IEnumerator ReconnectAfterDelay()
	{
		return null;
	}

	private void KickReconnect()
	{
	}

	private void IssueReconnect(string reason)
	{
	}

	private void OnProductsFetched(List<Product> products)
	{
	}

	private void OnProductsFetchFailed(ProductFetchFailed failure)
	{
	}

	private void ScheduleCatalogRecovery()
	{
	}

	private void CancelScheduledCatalogRecovery()
	{
	}

	private IEnumerator CatalogRecoveryAfterDelay()
	{
		return null;
	}

	private void KickCatalogRecovery()
	{
	}

	private void IssueCatalogRefetch(string reason)
	{
	}

	private void StartCatalogFetch()
	{
	}

	private void StartCatalogWatchdog()
	{
	}

	private void StopCatalogWatchdog()
	{
	}

	private IEnumerator AfterRealtimeDelay(float seconds, Action action)
	{
		return null;
	}

	public static void TracePurchase(string step)
	{
	}

	private void BeginPurchaseTrace(string productId)
	{
	}

	private void AppendPurchaseTrace(string step)
	{
	}

	private void FlushPurchaseTrace(string reason)
	{
	}

	private void OnPurchasesFetched(Orders orders)
	{
	}

	private void OnPurchasesFetchFailed(PurchasesFetchFailureDescription failureDescription)
	{
	}

	private void OnPurchaseDeferred(DeferredOrder deferredOrder)
	{
	}

	private void OnPurchasePending(PendingOrder pendingOrder)
	{
	}

	private bool IsConsumable(Product product)
	{
		return false;
	}

	private void OnPurchaseConfirmed(Order order)
	{
	}

	private void OnPurchaseFailed(FailedOrder failedOrder)
	{
	}

	private void CallRestoreIfDuplicatedPurchase(string purchaseId = null)
	{
	}

	private void CacheOrder(Order order, string origin)
	{
	}

	private static Product GetOrderedProduct(Order order)
	{
		return null;
	}

	public Order GetOrderForProduct(string productId)
	{
		return null;
	}

	public bool HasReceipt(Product product)
	{
		return false;
	}

	public string GetReceipt(Product product)
	{
		return null;
	}

	public string GetTransactionId(Product product)
	{
		return null;
	}

	public string GetAppleOriginalTransactionId(Product product)
	{
		return null;
	}

	public string GetGoogleOrderId(Product product)
	{
		return null;
	}

	public List<Product> GetOwnedProducts()
	{
		return null;
	}

	private void OnAppleEntitlementRevoked(string productId)
	{
	}

	private void ConfirmOrder(PendingOrder order, string reason)
	{
	}

	private void ScheduleConfirmRetry(UnconfirmedOrder entry, FailedOrder failure)
	{
	}

	private void StopConfirmRetry(UnconfirmedOrder entry)
	{
	}

	private void ForgetUnconfirmedOrder(string transactionId)
	{
	}

	private void ReissueUnconfirmedOrders(string reason, HashSet<string> onlyTransactions = null)
	{
	}

	private void AnnouncePurchaseResult(string message)
	{
	}

	private IEnumerator SayPurchaseResultDelayed(string message)
	{
		return null;
	}

	public bool IsInitialized()
	{
		return false;
	}

	public bool ArePurchasesReady()
	{
		return false;
	}

	private void SetConsumableProductIDs(int _platform)
	{
	}

	private void SetNonConsumableProductIDs(int _platform)
	{
	}

	public void BuyProductID(string productId)
	{
	}

	private ProductMetadata GetProductMetadata(string productId)
	{
		return null;
	}

	public decimal LocalizedPrice(int IAPType)
	{
		return 0m;
	}

	public RegionInfo GetIsoCurrencyCode(int IAPType)
	{
		return null;
	}

	public string PriceString(int IAPType)
	{
		return null;
	}

	public bool TryGetLocalizedPrice(string productId, out decimal price, out string currency)
	{
		price = 0m;
		currency = null;
		return false;
	}

	public decimal GetCurrentPrice(int IAPType)
	{
		return 0m;
	}

	public string GetOriginalPriceString(int IAPType)
	{
		return null;
	}

	public void RestorePurchases(int _platform, bool duplicatePurchase = false, Action<int> callback = null)
	{
	}

	private void FetchPurchasesForRestore(Action<int> callback, bool isRetry = false)
	{
	}

	private void CompleteRestore(Action<int> callback)
	{
	}

	public IPurchaseReceipt[] GetAllReceipts()
	{
		return null;
	}

	public bool PurchasedFullContentBitizenship()
	{
		return false;
	}

	public bool IsEligibleLegacyAdsPurchase()
	{
		return false;
	}

	public bool IsProductReceiptValid(Product product)
	{
		return false;
	}

	private IPurchaseReceipt[] GetAllAndroidReceipts()
	{
		return null;
	}

	private IPurchaseReceipt[] GetAllAppleReceipts()
	{
		return null;
	}

	public StoreController GetStoreController()
	{
		return null;
	}

	public string GetBundleForGameType(EXPANSION_PACK_TYPE exPackType)
	{
		return null;
	}

	public EXPANSION_PACK_TYPE GetExpansionPackTypeForBundle(string bundleId)
	{
		return EXPANSION_PACK_TYPE.NONE;
	}

	public string GetBundleForGameType(MINI_PAYWALL_TYPE paywallType)
	{
		return null;
	}

	public MINI_PAYWALL_TYPE GetMiniPaywallTypeForBundle(string bundleId)
	{
		return MINI_PAYWALL_TYPE.NONE;
	}

	public string GetPriceForGameType(MINI_PAYWALL_TYPE paywallType)
	{
		return null;
	}

	public string GetBundleForGameType(DARKLY_ITEM_TYPE itemType)
	{
		return null;
	}

	public DARKLY_ITEM_TYPE GetDarklyItemTypeForBundle(string bundle)
	{
		return DARKLY_ITEM_TYPE.NONE;
	}

	public string GetBundleForGameType(IAP_TYPE typeItem)
	{
		return null;
	}

	public IAP_TYPE GetIAPTypeForBundle(string bundle)
	{
		return IAP_TYPE.NONE;
	}

	public UIDARKLY_TYPE GetUidarklyTypeForBundle(string bundleId)
	{
		return UIDARKLY_TYPE.NONE;
	}

	public string GetBundleForGameType(BUNDLE_TYPE bundleType, bool isSecondTier)
	{
		return null;
	}

	public BUNDLE_TYPE GetBundleTypeForBundle(string bundle)
	{
		return BUNDLE_TYPE.NONE;
	}

	public string GetPriceForGameType(BUNDLE_TYPE bundleType, bool secondTier)
	{
		return null;
	}

	public string GetEmojiStringForBundle(string bundle)
	{
		return null;
	}

	public string GetBundleIDAlias(string originalBundleID)
	{
		return null;
	}

	public bool IsProductAvailableToRestore(string originalProductId)
	{
		return false;
	}

	public string GetSystemIapVersion(string bundle)
	{
		return null;
	}

	internal static void _003CBuildBundleToEmojiString_003Eg__Add_007C136_0(string key, string emoji, ref _003C_003Ec__DisplayClass136_0 P_2)
	{
	}

	private void _003CBuildProductDefinitions_003Eg__Add_007C143_0(string id, ProductType type, ref _003C_003Ec__DisplayClass143_0 P_2)
	{
	}

	private void _003CBuildProductDefinitions_003Eg__AddConsumable_007C143_1(string id, ref _003C_003Ec__DisplayClass143_0 P_1)
	{
	}

	private void _003CBuildProductDefinitions_003Eg__AddNonConsumable_007C143_2(string id, ref _003C_003Ec__DisplayClass143_0 P_1)
	{
	}

	private void _003CStartCatalogWatchdog_003Eb__255_0()
	{
	}

	private bool _003CGetOwnedProducts_003Eb__279_0(Product aProduct)
	{
		return false;
	}
}
