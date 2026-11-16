using System;
using System.Collections.Generic;
using FlurrySDK;

namespace FlurrySDKInternal
{
	public abstract class FlurryAgent : IDisposable
	{
		public abstract class AgentBuilder
		{
			public abstract void Build(string apiKey);

			public abstract void WithCrashReporting(bool crashReporting);

			public abstract void WithLogEnabled(bool enableLog);

			public abstract void WithLogLevel(Flurry.LogLevel logLevel);

			public abstract void WithMessaging(bool enableMessaging);
		}

		public abstract int LogEvent(string eventId);

		public abstract int LogEvent(string eventId, IDictionary<string, string> parameters);

		public abstract void Dispose();
	}
}
