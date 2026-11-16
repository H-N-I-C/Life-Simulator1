using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

namespace Game.Util.Debugging
{
	public static class LogFileSink
	{
		private const string FileName = "game_log.txt";

		private const string PrevFileName = "game_log.prev.txt";

		private const int FlushIntervalMs = 1000;

		private static readonly object Gate;

		private static readonly Regex RichTextTag;

		private static readonly Dictionary<string, LogBufferWriter> Writers;

		private static readonly HashSet<string> FailedFiles;

		private static Thread _flushThread;

		private static bool _pumpRunning;

		private static bool _lifecycleHooked;

		private static string _persistentDataPath;

		private static string _sessionHeaderSuffix;

		public static bool SuppressCapture;

		public static string FilePath => null;

		private static string PersistentDir => null;

		private static void Initialize()
		{
		}

		public static string FilePathFor(string fileName)
		{
			return null;
		}

		public static void WriteLine(string line)
		{
		}

		public static void WriteLine(string fileName, string line)
		{
		}

		public static void FlushAll()
		{
		}

		public static void Shutdown()
		{
		}

		private static LogBufferWriter EnsureWriter(string fileName)
		{
			return null;
		}

		private static void StartPump()
		{
		}

		private static void PumpLoop()
		{
		}

		private static string PrevNameFor(string fileName)
		{
			return null;
		}

		private static string StripRichText(string line)
		{
			return null;
		}
	}
}
