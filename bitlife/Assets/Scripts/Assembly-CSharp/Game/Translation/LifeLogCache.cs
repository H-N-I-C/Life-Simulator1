using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Translation
{
	public static class LifeLogCache
	{
		private class LogEntry
		{
			public string original;

			public string translated;

			public int logIndex;

			public LogEntry(string o, string t, int i)
			{
			}
		}

		private class InternalCache
		{
			public Dictionary<ulong, LogEntry> cache;

			public List<LogEntry> cache_temp;

			public Dictionary<ulong, EntryState> states;

			public int saveSlotIndex;
		}

		private enum EntryState
		{
			None = 0,
			OnLog = 1,
			Saved = 2
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<bool> _003C_003E9__31_0;

			public static Func<bool> _003C_003E9__32_0;

			internal bool _003CWaitAndSaveAfterGameSave_003Eb__31_0()
			{
				return false;
			}

			internal bool _003CSaveAndReplaceLifeLogEntries_003Eb__32_0()
			{
				return false;
			}
		}

		private sealed class _003CSaveAndReplaceLifeLogEntries_003Ed__32 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSaveAndReplaceLifeLogEntries_003Ed__32(int _003C_003E1__state)
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

		private sealed class _003CWaitAndSaveAfterGameSave_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitAndSaveAfterGameSave_003Ed__31(int _003C_003E1__state)
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

		private static bool initiaized;

		private static bool enabled;

		private const string FILE_NAME = "lifelogcache";

		private static Dictionary<string, InternalCache> cacheContainer;

		private static InternalCache currentCache;

		private static bool GameSaving__BackingField;

		private static bool cacheSaving;

		private static bool cacheSavingAfterGameSave;

		private static Life currentLife;

		private static int currentSaveSlot;

		private static Dictionary<int, string> slotToLifeId;

		private static bool Ready => false;

		public static bool GameSaving
		{
			get
			{
				return GameSaving__BackingField;
			}
			private set
			{
				GameSaving__BackingField = value;
			}
		}

		public static void Initialize()
		{
		}

		public static void OffInitialize()
		{
		}

		private static void Enable()
		{
		}

		private static void Disable()
		{
		}

		public static void ClearCache()
		{
		}

		public static void AddNewEntry(string original, string translated, int? year = null)
		{
		}

		private static void AddNewEntry_Internal(string original, string translated, int logIndex)
		{
		}

		private static void OnLanguageChanged(string language)
		{
		}

		private static void OnLifeLoaded(Life life)
		{
		}

		private static void OnLifeRemoved(int saveSlotIndex)
		{
		}

		private static void OnGameSaveStarted(Life life, int slotIndex)
		{
		}

		private static void OnGameSaveFinished(Life life, bool successfulSave)
		{
		}

		private static IEnumerator WaitAndSaveAfterGameSave()
		{
			return null;
		}

		private static IEnumerator SaveAndReplaceLifeLogEntries()
		{
			return null;
		}

		private static ulong GetId(string original, int logIndex)
		{
			return 0uL;
		}

		private static void CheckCurrentLife(Life life)
		{
		}

		private static void CheckAndCleanCache()
		{
		}

		private static void ReplaceLogEntries()
		{
		}

		private static void Load()
		{
		}

		private static Dictionary<string, InternalCache> LoadAndDecrypt(string filePath)
		{
			return null;
		}

		private static void Save()
		{
		}
	}
}
