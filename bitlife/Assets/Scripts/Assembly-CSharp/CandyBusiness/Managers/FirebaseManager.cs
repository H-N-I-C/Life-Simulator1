using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Firebase;
using Firebase.Messaging;

namespace CandyBusiness.Managers
{
	public static class FirebaseManager
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<FirebaseApp> _003C_003E9__9_1;

			public static Action<Task<DependencyStatus>> _003C_003E9__9_0;

			internal void _003CStartFirebase_003Eb__9_0(Task<DependencyStatus> task)
			{
			}

			internal FirebaseApp _003CStartFirebase_003Eb__9_1()
			{
				return null;
			}
		}

		private static FirebaseApp FirebaseApplication__BackingField;

		private static bool FirebaseIsReady__BackingField;

		private static bool _loading;

		public static FirebaseApp FirebaseApplication
		{
			get
			{
				return FirebaseApplication__BackingField;
			}
			private set
			{
				FirebaseApplication__BackingField = value;
			}
		}

		public static bool FirebaseIsReady
		{
			get
			{
				return FirebaseIsReady__BackingField;
			}
			private set
			{
				FirebaseIsReady__BackingField = value;
			}
		}

		public static void StartFirebase()
		{
		}

		private static void CompleteInitialization(Task<DependencyStatus> task, Func<FirebaseApp> createApp)
		{
		}

		private static void InitializationFailed(string reason)
		{
		}

		private static void OnRegistrationReceived(object sender, RegistrationReceivedEventArgs token)
		{
		}

		public static void LogEvent(string eventName, APP_PLATFORM _platform, bool _devMode, Dictionary<string, string> eventParameters = null)
		{
		}
	}
}
