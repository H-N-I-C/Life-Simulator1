using System;
using System.Collections.Generic;
using Backend;

namespace Game.DeepLinking
{
	public static class MigrationRewardController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<StuffItem, bool> _003C_003E9__38_1;

			public static Func<StuffItem, bool> _003C_003E9__46_1;

			internal bool _003CStartMigrationLife_003Eb__38_1(StuffItem owned)
			{
				return false;
			}

			internal bool _003CRefreshAfterQueuedRetry_003Eb__46_1(StuffItem item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass38_0
		{
			public int requestVersion;

			public string ownerId;

			public Action<bool, string> _003C_003E9__2;

			internal void _003CStartMigrationLife_003Eb__0(List<StuffItem> items)
			{
			}

			internal void _003CStartMigrationLife_003Eb__2(bool success, string errorMessage)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass39_0
		{
			public int requestVersion;

			internal void _003CTryRedeemWhenReady_003Eb__0(MYSTUFF_REDEEM result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass46_0
		{
			public int requestVersion;

			public string ownerId;

			internal void _003CRefreshAfterQueuedRetry_003Eb__0(List<StuffItem> items)
			{
			}
		}

		public const string ActivationCode = "CUSTOM_LIFE_MIGRATION";

		public const string ItemId = "SEASON_LIFE_036";

		public const string GiftButtonName = "migration_reward";

		private const string GiftButtonResourcePath = "deeplink_reward";

		public static readonly int[] RecoveryDelaysSeconds;

		private static bool _pending;

		private static bool _redeemInFlight;

		private static bool _awaitingBackendRetry;

		private static bool _inventoryRefreshInFlight;

		private static bool _alreadyClaimedPending;

		private static bool _welcomeOpen;

		private static bool _startLifeInFlight;

		private static bool _failureShown;

		private static bool _catalogWarningLogged;

		private static string _ownerId;

		private static int _requestVersion;

		private static int _recoveryAttempts;

		private static bool _giftButtonVisible;

		private static bool _giftButtonRendered;

		private static bool _infoButtonsReady;

		private static DateTime _nextRecoveryUtc;

		private static string PreferredTranslationLanguage__BackingField;

		public static string PreferredTranslationLanguage
		{
			get
			{
				return PreferredTranslationLanguage__BackingField;
			}
			private set
			{
				PreferredTranslationLanguage__BackingField = value;
			}
		}

		public static bool IsGiftButtonVisible => false;

		public static bool HasPendingReward => false;

		private static void Init()
		{
		}

		public static void QueueMigrationReward(string preferredTranslationLanguage)
		{
		}

		public static bool TryOpenWelcomeScenario()
		{
			return false;
		}

		public static void MarkWelcomeShown()
		{
		}

		public static void DismissGiftButton()
		{
		}

		public static bool IsMigrationGiftButton(string buttonName)
		{
			return false;
		}

		public static DataMyStuffCatalog GetMigrationCatalogItem()
		{
			return null;
		}

		public static void ResetForAccount()
		{
		}

		private static void CheckAccount()
		{
		}

		public static void StartMigrationLife(string ownerId)
		{
		}

		private static void TryRedeemWhenReady()
		{
		}

		private static bool IsReadyForMigrationReward()
		{
			return false;
		}

		private static void OnRedeemCompleted(MYSTUFF_REDEEM result)
		{
		}

		private static void CompleteDelivery()
		{
		}

		private static bool ScheduleRecoveryAttempt()
		{
			return false;
		}

		public static MigrationRewardDeliveryOutcome GetDeliveryOutcome(MYSTUFF_REDEEM result)
		{
			return MigrationRewardDeliveryOutcome.ShowGift;
		}

		public static bool IsMigrationCatalogItem(string itemId, string redeemId)
		{
			return false;
		}

		private static void RefreshAfterQueuedRetry()
		{
		}

		private static void PresentRewardIfReady()
		{
		}

		private static void ShowGiftButton()
		{
		}

		private static void OnInfoButtonsReady()
		{
		}

		private static void RenderGiftButtonIfReady()
		{
		}

		private static void ShowBackendError(string title, string body)
		{
		}
	}
}
