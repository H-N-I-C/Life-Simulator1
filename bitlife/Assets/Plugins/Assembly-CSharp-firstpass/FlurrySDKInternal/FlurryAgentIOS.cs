using System.Collections.Generic;
using System.Runtime.InteropServices;
using FlurrySDK;

namespace FlurrySDKInternal
{
	public class FlurryAgentIOS : FlurryAgent
	{
		public class AgentBuilderIOS : AgentBuilder
		{
			public override void Build(string apiKey)
			{
			}

			public override void WithCrashReporting(bool crashReporting)
			{
			}

			public override void WithLogEnabled(bool enableLog)
			{
			}

			public override void WithLogLevel(Flurry.LogLevel logLevel)
			{
			}

			public override void WithMessaging(bool enableMessaging)
			{
			}
		}

		[PreserveSig]
		private static extern void initializeFlurrySessionBuilder();

		[PreserveSig]
		private static extern void flurryWithCrashReporting(bool crashReporting);

		[PreserveSig]
		private static extern void flurryWithLogEnabled(bool logEnabled);

		[PreserveSig]
		private static extern void flurryWithLogLevel(int logLevel);

		[PreserveSig]
		private static extern void flurrySetupMessagingWithAutoIntegration();

		[PreserveSig]
		private static extern void flurryStartSessionWithSessionBuilder(string apiKey);

		[PreserveSig]
		private static extern int flurryLogEvent(string eventId);

		[PreserveSig]
		private static extern int flurryLogEventWithParameter(string eventName, string keys, string values);

		public override int LogEvent(string eventId)
		{
			return 0;
		}

		public override int LogEvent(string eventId, IDictionary<string, string> parameters)
		{
			return 0;
		}

		public override void Dispose()
		{
		}

		private static void ToKeyValue(IDictionary<string, string> dictionary, out string keys, out string values)
		{
			keys = null;
			values = null;
		}
	}
}
