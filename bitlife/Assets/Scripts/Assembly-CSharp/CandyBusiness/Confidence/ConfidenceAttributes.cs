using System;
using System.Collections.Generic;
using CandyBusiness.Confidence.Attributes;

namespace CandyBusiness.Confidence
{
	public static class ConfidenceAttributes
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			internal bool _003C_002Ecctor_003Eb__39_0(string _)
			{
				return false;
			}

			internal bool _003C_002Ecctor_003Eb__39_1(string _)
			{
				return false;
			}
		}

		public static readonly ConfidenceAttribute<string> BitLifeUserID;

		public static readonly ConfidenceAttribute<string> DeviceID;

		public static readonly ConfidenceAttribute<string> AppsFlyerID;

		public static readonly ConfidenceAttribute<string> UserExternalAccountConnection;

		public static readonly ConfidenceAttribute<string> Build;

		public static readonly ConfidenceAttribute<string> Platform;

		public static readonly ConfidenceAttribute<string> Country;

		public static readonly ConfidenceAttribute<int> Streak;

		public static readonly ConfidenceAttribute<string> SeasonName;

		public static readonly ConfidenceAttribute<int> LivesCompleted;

		public static readonly ConfidenceAttribute<string> TranslationLanguage;

		public static readonly ConfidenceAttribute<string> WebShopVariant;

		public static readonly ConfidenceAttribute<string> InstallTimeUTC;

		public static readonly ConfidenceAttribute<int> UserAge;

		public static readonly ConfidenceAttribute<bool> IsFirstSession;

		public static readonly ConfidenceAttribute<int> NumSessionsLifetime;

		public static readonly ConfidenceAttribute<bool> IsPayer;

		public static readonly ConfidenceAttribute<bool> IsPayerLast30;

		public static readonly ConfidenceAttribute<bool> IsPayerLast90;

		public static readonly ConfidenceAttribute<List<string>> PremiumPurchaseReport;

		public static readonly ConfidenceAttribute<int> EstimatedDepositLTVLifetime;

		public static readonly ConfidenceAttribute<int> EstimatedDepositLTVLast30;

		public static readonly ConfidenceAttribute<int> EstimatedDepositLTVLast90;

		public static readonly ConfidenceAttribute<bool> IsBitizenshipGodModeBuyer;

		public static readonly ConfidenceAttribute<bool> IsBossModeBuyer;

		public static readonly ConfidenceAttribute<bool> IsExpansionStarterSetBuyer;

		public static readonly ConfidenceAttribute<bool> IsExpansionMasterSetBuyer;

		public static readonly ConfidenceAttribute<bool> IsBitpassBuyer;

		public static readonly ConfidenceAttribute<bool> IsAdPassActive;

		public static readonly ConfidenceAttribute<string> AdPassLastRenewalTimestamp;

		public static readonly ConfidenceAttribute<int> PremiumPurchaseReportCount;

		public static readonly ConfidenceAttribute<bool> IsTutorialComplete;

		private static readonly IReadOnlyList<ConfidenceAttribute> All__BackingField;

		private static readonly IReadOnlyDictionary<ConfidenceAttribute, UserAttributeDependencyAttribute> Dependencies__BackingField;

		public static IReadOnlyList<ConfidenceAttribute> All => All__BackingField;

		public static IReadOnlyDictionary<ConfidenceAttribute, UserAttributeDependencyAttribute> Dependencies => Dependencies__BackingField;

		public static void ResetAll()
		{
		}
	}
}
