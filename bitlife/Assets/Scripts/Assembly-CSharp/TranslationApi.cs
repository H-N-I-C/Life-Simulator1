using System;
using System.Collections.Generic;
using Backend.Authentication;
using Game.Translation;

public class TranslationApi
{
	public static class ERROR_CODE
	{
		public const int TRANSLATION_API_NOT_READY = 600;

		public const int PROHIBITED_CONTENT = 601;

		public const int PARSE_ERROR = 602;

		public const int LANGUAGE_NOT_READY = 603;
	}

	private sealed class Request
	{
		public TranslationRequestContext Context;

		public List<TranslationEntry> Entries;

		public Action<List<TranslationEntry>, bool, long> Complete;

		public SimPerson Npc;

		public double Started;

		public bool Completed;

		public bool Failed;

		public bool Background;
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<TranslationEntry, TranslationEntry> _003C_003E9__50_0;

		public static Func<TranslationEntry, bool> _003C_003E9__51_0;

		public static Func<TranslationEntry, TranslationEntry> _003C_003E9__51_1;

		public static Action<GetConfigResponse> _003C_003E9__83_0;

		public static Func<TranslationEntry, bool> _003C_003E9__90_0;

		public static Func<TranslationEntry, TranslationEntry> _003C_003E9__90_1;

		public static Func<TranslationEntry, bool> _003C_003E9__91_1;

		internal TranslationEntry _003CCompleteUI_003Eb__50_0(TranslationEntry e)
		{
			return null;
		}

		internal bool _003CFinish_003Eb__51_0(TranslationEntry e)
		{
			return false;
		}

		internal TranslationEntry _003CFinish_003Eb__51_1(TranslationEntry e)
		{
			return null;
		}

		internal void _003CUnityInit_003Eb__83_0(GetConfigResponse model)
		{
		}

		internal bool _003CSendTextAnalysisRequest_003Eb__90_0(TranslationEntry e)
		{
			return false;
		}

		internal TranslationEntry _003CSendTextAnalysisRequest_003Eb__90_1(TranslationEntry e)
		{
			return null;
		}

		internal bool _003CDispatch_003Eb__91_1(TranslationEntry e)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass86_0
	{
		public string language;

		internal bool _003CIsLanguageSupported_003Eb__0(string item)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass91_0
	{
		public Request request;

		public Func<Request, bool> _003C_003E9__2;

		internal void _003CDispatch_003Eb__0(List<TranslationEntry> entries, bool success, long code)
		{
		}

		internal bool _003CDispatch_003Eb__2(Request r)
		{
			return false;
		}
	}

	private static bool IsChangingConfiguration__BackingField;

	private static int Generation__BackingField;

	private static int ConsecutiveFailures__BackingField;

	private static bool HasTranslationFailure__BackingField;

	private static int FailureToastCount__BackingField;

	private static bool offlineNotified;

	private static bool serviceNotified;

	private static bool lastOffline;

	private static TranslationRuntime runtime;

	private static readonly List<Request> requests;

	private static Request recovery;

	private static bool recovering;

	private static double retryAt;

	private static int retryStep;

	private static Action<bool> m_RecoveryNotice;

	private static Action m_TranslationFailed;

	private static Action m_ConnectivityChanged;

	private static TranslationApiType SelectedSystem__BackingField;

	private static TranslationApiType resolvedSystem__BackingField;

	private static string _language;

	private static TranslationInitModel InitModel__BackingField;

	private static List<TranslationApiProfile> TranslationApiProfiles__BackingField;

	private static Action<string> OnLanguageChanged__BackingField;

	private static bool Initialized__BackingField;

	internal static double Now => 0.0;

	public static bool IsChangingConfiguration
	{
		get
		{
			return IsChangingConfiguration__BackingField;
		}
		private set
		{
			IsChangingConfiguration__BackingField = value;
		}
	}

	public static int Generation
	{
		get
		{
			return Generation__BackingField;
		}
		private set
		{
			Generation__BackingField = value;
		}
	}

	public static bool IsOffline => false;

	public static int ConsecutiveFailures
	{
		get
		{
			return ConsecutiveFailures__BackingField;
		}
		private set
		{
			ConsecutiveFailures__BackingField = value;
		}
	}

	public static bool HasTranslationFailure
	{
		get
		{
			return HasTranslationFailure__BackingField;
		}
		private set
		{
			HasTranslationFailure__BackingField = value;
		}
	}

	public static int FailureToastCount
	{
		get
		{
			return FailureToastCount__BackingField;
		}
		private set
		{
			FailureToastCount__BackingField = value;
		}
	}

	public static bool NeedsLanguageSelection => false;

	public static TranslationApiType SelectedSystem
	{
		get
		{
			return SelectedSystem__BackingField;
		}
		private set
		{
			SelectedSystem__BackingField = value;
		}
	}

	public static TranslationApiType resolvedSystem
	{
		get
		{
			return resolvedSystem__BackingField;
		}
		private set
		{
			resolvedSystem__BackingField = value;
		}
	}

	public static TranslationInitModel InitModel
	{
		get
		{
			return InitModel__BackingField;
		}
		private set
		{
			InitModel__BackingField = value;
		}
	}

	public static List<TranslationApiProfile> TranslationApiProfiles
	{
		get
		{
			return TranslationApiProfiles__BackingField;
		}
		private set
		{
			TranslationApiProfiles__BackingField = value;
		}
	}

	public static Action<string> OnLanguageChanged
	{
		get
		{
			return OnLanguageChanged__BackingField;
		}
		set
		{
			OnLanguageChanged__BackingField = value;
		}
	}

	public static bool Initialized
	{
		get
		{
			return Initialized__BackingField;
		}
		private set
		{
			Initialized__BackingField = value;
		}
	}

	public static bool ShouldSkipTranslation => false;

	public static bool IsEnglishSelected => false;

	public static event Action<bool> RecoveryNotice
	{
		add
		{
		}
		remove
		{
		}
	}

	public static event Action TranslationFailed
	{
		add
		{
		}
		remove
		{
		}
	}

	public static event Action ConnectivityChanged
	{
		add
		{
		}
		remove
		{
		}
	}

	private static void EnsureRuntime()
	{
	}

	public static void CancelRequests()
	{
	}

	public static void KeepTrying()
	{
	}

	private static void Failed(Request request)
	{
	}

	private static void CompleteUI(Request request, bool success, long code)
	{
	}

	private static void Finish(Request request, bool success, long code, bool countFailure = true)
	{
	}

	public static void Tick()
	{
	}

	public static bool LanguageInitializated()
	{
		return false;
	}

	public static void UnityInit()
	{
	}

	public static bool IsSystemLanguageSupported()
	{
		return false;
	}

	public static bool IsLanguageSupported(string language)
	{
		return false;
	}

	private static bool IsLanguageSupported(string language, bool allowCompatibleMatch)
	{
		return false;
	}

	public static void SetLanguage(string newLanguage)
	{
	}

	public static void SetSelectedSystem(TranslationApiType selectedSystem)
	{
	}

	public static void TurnOffTranslationSystem()
	{
	}

	public static void SendTextAnalysisRequest(List<TranslationEntry> text, Action<List<TranslationEntry>, bool, long> onLoad, SimPerson npc = null, Func<bool> isCurrent = null)
	{
	}

	private static void Dispatch(Request request)
	{
	}

	public static string GetLanguage()
	{
		return null;
	}

	public static string GetLanguageCode()
	{
		return null;
	}

	public static bool HasValidLanguageSelected()
	{
		return false;
	}
}
