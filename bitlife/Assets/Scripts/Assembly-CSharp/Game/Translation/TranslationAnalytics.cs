using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Translation
{
	public class TranslationAnalytics
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action<string> _003C_003E9__21_0;

			internal void _003CInitialize_003Eb__21_0(string deviceId)
			{
			}
		}

		private static readonly Stopwatch translationStopwatch;

		private static long totalTranslationTimeMs;

		private static int translationCount;

		private static bool initialized;

		private static int _localCacheHits;

		private static int _localCacheMiss;

		private const int BEDROCK_ANALYTICS_TIMER = 3000;

		private static readonly Stopwatch bedRockTranslationStopwatch;

		private static readonly Stopwatch cacheTranslationCompletedStopwatch;

		private static int _bedRockCachedItems;

		private static int _bedRockCacheHit;

		private static int _bedRockDynamoDb;

		private static int _bedRockLlm;

		private static int _bedRockInputTokens;

		private static int _bedRockOutputTokens;

		private static int _bedRockTotalTokens;

		private static Dictionary<ulong, string> translationCompletedTracked;

		private static int _translation_completed_server_count;

		private static int _translation_completed_cache_count;

		private static int _translation_missed_server_count;

		private static int _translation_missed_cache_count;

		public static void Initialize()
		{
		}

		public static void SubscribeEvents()
		{
		}

		private static void OnCacheHit(string text, string translated)
		{
		}

		private static void OnCacheMiss(string text)
		{
		}

		private static void OnServerTranslationCompleted(List<TranslationEntry> entries)
		{
		}

		private static void TrySendServerTranslationCompleted()
		{
		}

		private static void TrySendLocalCacheTranslationCompleted()
		{
		}

		private static void OnLocalCacheHit(int count)
		{
		}

		private static void OnLocalCacheMiss(int count)
		{
		}

		private static void OnYearIncremented(int year)
		{
		}

		public static void UnsubscribeEvents()
		{
		}

		private static void OnBedRockTranslationResponse(TranslationServerResult result)
		{
		}

		private static void ResetBedRockTranslationData()
		{
		}

		private static void OnTranslationIndicatorChanged(bool show)
		{
		}
	}
}
