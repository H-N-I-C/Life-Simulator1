using System.Collections.Generic;
using FlurrySDKInternal;

namespace FlurrySDK
{
	public class Flurry
	{
		public class Builder
		{
			private FlurryAgent.AgentBuilder builder;

			public void Build(string apiKey)
			{
			}

			public Builder WithCrashReporting(bool crashReporting = true)
			{
				return null;
			}

			public Builder WithLogEnabled(bool enableLog = true)
			{
				return null;
			}

			public Builder WithLogLevel(LogLevel logLevel = LogLevel.WARN)
			{
				return null;
			}

			public Builder WithMessaging(bool enableMessaging = true)
			{
				return null;
			}
		}

		public enum LogLevel
		{
			VERBOSE = 2,
			DEBUG = 3,
			INFO = 4,
			WARN = 5,
			ERROR = 6,
			ASSERT = 7,
			LogVERBOSE = VERBOSE,
			LogDEBUG = DEBUG,
			LogINFO = INFO,
			LogWARN = WARN,
			LogERROR = ERROR,
			LogASSERT = ASSERT
		}

		public enum EventRecordStatus
		{
			FlurryEventFailed = 0,
			FlurryEventRecorded = 1,
			FlurryEventUniqueCountExceeded = 2,
			FlurryEventParamsCountExceeded = 3,
			FlurryEventLogCountExceeded = 4,
			FlurryEventLoggingDelayed = 5,
			FlurryEventAnalyticsDisabled = 6
		}

		private static FlurryAgent flurryAgent;

		static Flurry()
		{
		}

		public static EventRecordStatus LogEvent(string eventId)
		{
			return EventRecordStatus.FlurryEventFailed;
		}

		public static EventRecordStatus LogEvent(string eventId, IDictionary<string, string> parameters)
		{
			return EventRecordStatus.FlurryEventFailed;
		}
	}
}
