using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Game.Translation
{
	public class GeminiApi
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<TranslationEntry, int> _003C_003E9__6_0;

			public static Func<TranslationEntry, string> _003C_003E9__6_1;

			public static Func<TranslationEntry, bool> _003C_003E9__7_0;

			internal int _003CCallGeminiAPI_003Eb__6_0(TranslationEntry x)
			{
				return 0;
			}

			internal string _003CCallGeminiAPI_003Eb__6_1(TranslationEntry x)
			{
				return null;
			}

			internal bool _003CApplyPositionalTranslations_003Eb__7_0(TranslationEntry entry)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass6_0
		{
			public string prompt;

			public bool completed;

			public TranslationRequestContext context;

			public Action<List<TranslationEntry>, bool, long> onLoad;

			public List<TranslationEntry> text;

			public string url;

			public string jsonRequestBody;

			public int retryLimit;

			internal void _003CCallGeminiAPI_003Eb__2(string x)
			{
			}

			internal void _003CCallGeminiAPI_003Eg__Complete_007C3(bool success, long errorCode)
			{
			}

			internal void _003CCallGeminiAPI_003Eg__SendAttempt_007C4(int retryCount)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass6_1
		{
			public bool disposed;

			public CancellationTokenRegistration cancellationRegistration;

			public UnityWebRequest webRequest;

			public int retryCount;

			public _003C_003Ec__DisplayClass6_0 CS_0024_003C_003E8__locals1;

			public Action _003C_003E9__8;

			internal void _003CCallGeminiAPI_003Eg__DisposeRequest_007C5()
			{
			}

			internal void _003CCallGeminiAPI_003Eb__6()
			{
			}

			internal void _003CCallGeminiAPI_003Eb__8()
			{
			}

			internal void _003CCallGeminiAPI_003Eb__7(AsyncOperation _)
			{
			}
		}

		private static string apiKey;

		private static string modelName;

		private const int ERROR_503_RETRY_COUNT = 3;

		private const string PROHIBITED_CONTENT = "PROHIBITED_CONTENT";

		public const string UNAUTHORIZE_TRANSLATION = "(Unauthorized translation)";

		public static GeminiTokenDetail totaltokenDetails;

		public static void CallGeminiAPI(List<TranslationEntry> text, string language, Action<List<TranslationEntry>, bool, long> onLoad, TranslationRequestContext context = null)
		{
		}

		private static bool ApplyPositionalTranslations(List<TranslationEntry> text, string generatedText)
		{
			return false;
		}

		public static void Init(string modelGeminiApiKey, string modelGeminiUrl, string modelGeminiModel)
		{
		}
	}
}
