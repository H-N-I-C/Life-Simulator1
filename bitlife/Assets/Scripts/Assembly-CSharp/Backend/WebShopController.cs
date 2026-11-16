using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Backend.Authentication;
using Backend.Code.Models.Catalog;
using Backend.Code.Services;
using Game.DeepLinking;
using Game.Util.Debugging;

namespace Backend
{
	public class WebShopController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<ProductCatalogWebShop> _003C_003E9__22_0;

			public static Action<bool> _003C_003E9__24_0;

			public static Action<bool> _003C_003E9__30_1;

			public static Action _003C_003E9__30_0;

			public static Action<bool> _003C_003E9__31_1;

			public static Action _003C_003E9__31_0;

			public static Func<string, string, string> _003C_003E9__39_0;

			public static Action<bool> _003C_003E9__42_4;

			public static Action _003C_003E9__42_3;

			public static Action<ProductCatalogWebShop> _003C_003E9__57_0;

			internal void _003CRetryCatalog_003Eb__22_0(ProductCatalogWebShop result)
			{
			}

			internal void _003CCheckPurchasePendingAtRestart_003Eb__24_0(bool hasPurchases)
			{
			}

			internal void _003CGoToWebShopById_003Eb__30_0()
			{
			}

			internal void _003CGoToWebShopById_003Eb__30_1(bool hasPurchases)
			{
			}

			internal void _003CCheckPurchasesTryAgain_003Eb__31_0()
			{
			}

			internal void _003CCheckPurchasesTryAgain_003Eb__31_1(bool hasPurchases)
			{
			}

			internal string _003CGetClenBundleVersion_003Eb__39_0(string i, string j)
			{
				return null;
			}

			internal void _003CStartPurchase_003Eb__42_3()
			{
			}

			internal void _003CStartPurchase_003Eb__42_4(bool hasPurchases)
			{
			}

			internal void _003CStartCatalogRefresh_003Eb__57_0(ProductCatalogWebShop result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CGetCatalog_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass23_0 _003C_003E4__this;

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

			public string catalogVersion;

			public Action<ProductCatalogWebShop> callback;

			public Action<(string, bool)> _003C_003E9__1;

			public Action _003C_003E9__2;

			internal Task _003CGetCatalog_003Eb__0()
			{
				return null;
			}

			internal void _003CGetCatalog_003Eb__1((string, bool) result)
			{
			}

			internal void _003CGetCatalog_003Eb__2()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass23_1
		{
			public Exception e;

			internal void _003CGetCatalog_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass25_0
		{
			public bool requestCompleted;

			public Action<bool> callback;

			internal void _003CRecoverWebShopPurchases_003Eg__Complete_007C0(bool hasPurchases)
			{
			}

			internal void _003CRecoverWebShopPurchases_003Eg__OnRestored_007C1(List<TransactionItemModel> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass28_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CSetRedeemed_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass28_0 _003C_003E4__this;

				private TaskAwaiter<bool> _003C_003Eu__1;

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

			public TransactionItemModel product;

			internal Task _003CSetRedeemed_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass40_0
		{
			public string gameBundle;

			public string searchBundle;

			internal bool _003CGetWebShopPriceForBundle_003Eb__0(ProductGroup t)
			{
				return false;
			}

			internal bool _003CGetWebShopPriceForBundle_003Eb__1(ProductItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_0
		{
			[StructLayout((LayoutKind)3)]
			private struct _003C_003CStartPurchase_003Eb__0_003Ed : IAsyncStateMachine
			{
				public int _003C_003E1__state;

				public AsyncTaskMethodBuilder _003C_003Et__builder;

				public _003C_003Ec__DisplayClass42_0 _003C_003E4__this;

				private _003C_003Ec__DisplayClass42_1 _003C_003E8__1;

				private TaskAwaiter<(string, bool)> _003C_003Eu__1;

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

			public string productId;

			public string alias;

			internal Task _003CStartPurchase_003Eb__0()
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_1
		{
			public (string, bool) result;

			public _003C_003Ec__DisplayClass42_0 CS_0024_003C_003E8__locals1;

			internal void _003CStartPurchase_003Eb__1()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass42_2
		{
			public Exception e;

			public _003C_003Ec__DisplayClass42_1 CS_0024_003C_003E8__locals2;

			internal void _003CStartPurchase_003Eb__2()
			{
			}
		}

		private sealed class _003CRetryCatalog_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CRetryCatalog_003Ed__22(int _003C_003E1__state)
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

		private static Action<TransactionItemModel> m_OnSetRedeemed;

		private static Action m_OnCatalogData;

		private static Action<TransactionItemModel> m_OnRestoredWebShopPurchase;

		private static readonly Logger Log;

		private static string _setBundleToStartPurchase;

		private static string _currentUserGuid;

		private static int _countCheckPurchases;

		private static bool _isBanned;

		public static WebShopStatus OverrideWebShopType;

		private static int _currentCatalogVersion;

		private static List<string> _restoredIapType;

		private static bool _isLoading;

		private static ProductCatalogWebShop _catalog;

		private static GetConfigResponse _serverConfigData;

		private static WebShopCheckoutCacheService _checkoutCacheService;

		public static event Action<TransactionItemModel> OnSetRedeemed
		{
			add
			{
			}
			remove
			{
			}
		}

		public static event Action OnCatalogData
		{
			add
			{
			}
			remove
			{
			}
		}

		public static event Action<TransactionItemModel> OnRestoredWebShopPurchase
		{
			add
			{
			}
			remove
			{
			}
		}

		public static void Init(GetConfigResponse serverConfigData)
		{
		}

		private static IEnumerator RetryCatalog()
		{
			return null;
		}

		private static void GetCatalog(Action<ProductCatalogWebShop> callback)
		{
		}

		private static void CheckPurchasePendingAtRestart()
		{
		}

		private static void RecoverWebShopPurchases(Action<bool> callback)
		{
		}

		private static void SetPurchasePendingAtRestart()
		{
		}

		public static void SetRedeemedProduct(string productId, TransactionItemModel product)
		{
		}

		private static void SetRedeemed(TransactionItemModel product)
		{
		}

		public static List<string> GetText()
		{
			return null;
		}

		public static void GoToWebShopById(string productId = null)
		{
		}

		private static void CheckPurchasesTryAgain()
		{
		}

		private static void ResetCheckPurchases()
		{
		}

		public static WebShopStatus GetWebShopType()
		{
			return WebShopStatus.None;
		}

		private static bool IsUSPlayer()
		{
			return false;
		}

		private static bool ShouldWebShopGetCatalog()
		{
			return false;
		}

		private static bool RolloutCheck(int rolloutValue)
		{
			return false;
		}

		public static bool IsWebShopTablet()
		{
			return false;
		}

		public static bool IsCatalogAvailable()
		{
			return false;
		}

		private static string GetClenBundleVersion(string bundle)
		{
			return null;
		}

		private static ProductItem GetWebShopPriceForBundle(string bundle, bool isWebBundle = false)
		{
			return null;
		}

		private static string GetAliasBundleVersion(string bundle)
		{
			return null;
		}

		public static void StartPurchase(string productId)
		{
		}

		public static string GetWebShopPrice(MINI_PAYWALL_TYPE paywallType)
		{
			return null;
		}

		public static string GetWebShopPrice(DARKLY_ITEM_TYPE itemType)
		{
			return null;
		}

		public static string GetWebShopPrice(EXPANSION_PACK_TYPE exPackType)
		{
			return null;
		}

		public static string GetWebShopPrice(ProductItem item)
		{
			return null;
		}

		private static string WebShopCurrencyTranslation(string isoCurrency)
		{
			return null;
		}

		private static string GetReturnPrice(string currency, int amount, bool withCurrency = true)
		{
			return null;
		}

		public static string GetWebShopPrice(IAP_TYPE itemType, string fallback = "")
		{
			return null;
		}

		public static Price GetWebShopProductPrice(IAP_TYPE itemType)
		{
			return null;
		}

		public static string GetWebShopPrice(Price price, string fallback = "")
		{
			return null;
		}

		public static string GetWebShopPrice(BUNDLE_TYPE bundleType, bool isSecondTier)
		{
			return null;
		}

		public static void SetBannedState(bool isBanned)
		{
		}

		public static bool BannerEnabled()
		{
			return false;
		}

		public static string GetRolloutPercentage()
		{
			return null;
		}

		public static void ResetWebshopListPurchasedItems()
		{
		}

		public static void StartCatalogRefresh(string getUserGuid)
		{
		}

		private static bool IsGameReadyForWebShop()
		{
			return false;
		}

		public static bool BannerEnabledForUS()
		{
			return false;
		}

		public static bool IsIAPEnabled(IAP_TYPE iapType)
		{
			return false;
		}

		public static bool IsIAPEnabled(EXPANSION_PACK_TYPE exPackType)
		{
			return false;
		}

		public static bool IsWebShopTag(TagItem tag)
		{
			return false;
		}

		public static void OpenShopById(string tagId)
		{
		}

		private static void ShowPayWall(string bundleName)
		{
		}

		public static void SetOpenShopById(string tagId)
		{
		}
	}
}
