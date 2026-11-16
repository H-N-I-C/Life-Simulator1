using System;
using System.Collections.Generic;

namespace Genesis.Telemetry
{
	public static class GenesisTelemetryRegistry
	{
		private const string CsvResourceName = "genesis_telemetry_registry";

		private const string ResourcePath = "genesis/genesis_telemetry_registry";

		private const int ColEventName = 0;

		private const int ColProperties = 1;

		private const int ColCooldown = 2;

		private const int ColActiveFrom = 3;

		private const int ColActiveUntil = 4;

		private const int ColOwner = 5;

		private const int ColStatus = 6;

		private const int MinColumnCount = 7;

		private static readonly Dictionary<string, GenesisTelemetryRegistryEntry> _entries;

		private static bool _loaded;

		private static bool _warnedLoadFailure;

		private static int MalformedRowWarningCount__BackingField;

		private static int DroppedExpiredCount__BackingField;

		public static int MalformedRowWarningCount
		{
			get
			{
				return MalformedRowWarningCount__BackingField;
			}
			private set
			{
				MalformedRowWarningCount__BackingField = value;
			}
		}

		public static int DroppedExpiredCount
		{
			get
			{
				return DroppedExpiredCount__BackingField;
			}
			private set
			{
				DroppedExpiredCount__BackingField = value;
			}
		}

		private static void BootLoad()
		{
		}

		public static void EnsureLoaded()
		{
		}

		public static void LoadFromResources()
		{
		}

		private static void LoadFromResourcePath(string resourcePath)
		{
		}

		public static void LoadFromText(string csvText)
		{
		}

		public static void Clear()
		{
		}

		public static bool TryGetEntry(string eventName, out GenesisTelemetryRegistryEntry entry)
		{
			entry = null;
			return false;
		}

		public static bool IsAuthorableEvent(string eventName)
		{
			return false;
		}

		public static GenesisTelemetrySendDecision EvaluateSend(string eventName)
		{
			return GenesisTelemetrySendDecision.Send;
		}

		private static void TryParseRow(string line, int lineNumber)
		{
		}

		private static void WarnMalformed(int lineNumber, string reason)
		{
		}

		private static bool TryParseStatus(string raw, out GenesisTelemetryStatus status)
		{
			status = default;
			return false;
		}

		private static bool TryParseUtc(string raw, out DateTime? value)
		{
			value = null;
			return false;
		}

		private static HashSet<string> ParsePropertyNames(string rawProperties, out Dictionary<string, string> propertyTypes)
		{
			propertyTypes = null;
			return null;
		}

		internal static string[] SplitCsvLine(string line)
		{
			return null;
		}
	}
}
