using System;
using System.Collections;
using System.Collections.Generic;

namespace Tracking
{
	public abstract class GameTelemetry
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<string, string> _003C_003E9__30_0;

			internal string _003CInitOtherUIViewEventNames_003Eb__30_0(string i)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public Type eventType;

			internal bool _003CDispatchEvent_003Eb__0(KeyValuePair<BitLifeEvents, Type> t)
			{
				return false;
			}
		}

		private sealed class _003CHeartBeat_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CHeartBeat_003Ed__9(int _003C_003E1__state)
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

		private sealed class _003CInitializeSystem_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private bool _003CfiredAppStartEarly_003E5__2;

			private int _003CwaitUntilAppStart_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CInitializeSystem_003Ed__8(int _003C_003E1__state)
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

		private sealed class _003CWaitOneSecond_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitOneSecond_003Ed__15(int _003C_003E1__state)
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

		private static int _coolDown;

		private static Dictionary<BitLifeEvents, Type> _events;

		private static bool _initialized;

		private static readonly GameTelemetryPreReadyEventDrain _preReadyEventDrain;

		private static Dictionary<BitLifeUIClicks, DateTime> LastSentBitLifeUIClick;

		private static TimeSpan BitLifeUIClickCooldown;

		private static Dictionary<BitLifeEvents, GameTelemetryCoolDownTimer> ConditionalEvents;

		private static Dictionary<string, DateTime> LastSentBitLifeUIView;

		private static readonly TimeSpan BitLifeUIViewCooldown;

		private static List<string> OtherWhiteListedBitLifeUIView;

		public static bool IsReady => false;

		public static void Init()
		{
		}

		private static void InitializeSession()
		{
		}

		private static IEnumerator InitializeSystem()
		{
			return null;
		}

		private static IEnumerator HeartBeat()
		{
			return null;
		}

		public static bool SendEvent(BitLifeUIClicks clickType, string subCategory = "")
		{
			return false;
		}

		public static void SendEvent(IGameTelemetryEvent telemetryEvent)
		{
		}

		private static IEnumerator WaitOneSecond()
		{
			return null;
		}

		private static void DispatchEvent(IGameTelemetryEvent telemetryEvent)
		{
		}

		private static void SendBitLifeEvent(BitLifeEvents bitLifeEvent, bool logBusinessManagerEvent = false)
		{
		}

		private static void SendBitLifeEvent(BitLifeEvents bitLifeEvent, IGameTelemetryEvent telemetryEvent, bool logBusinessManagerEvent = false)
		{
		}

		public static void TriggerUniqueEvent(IGameTelemetryEvent bitLifeEvent)
		{
		}

		public static void TrackAgeEvents(int age)
		{
		}

		public static void SendEventAfterCoolDown(int minutes, IGameTelemetryEvent bitLifeEvents)
		{
		}

		public static void SendIAPPurchaseTelemetry(string productID)
		{
		}

		public static bool TrySendBitLifeUIView(BitLifeUIViews view)
		{
			return false;
		}

		public static bool TrySendBitLifeUIViewWebShop(bool direct)
		{
			return false;
		}

		public static bool TrySendBitLifeUIView(List list)
		{
			return false;
		}

		private static bool TrySendBitLifeUIView(string screenName)
		{
			return false;
		}

		private static void InitOtherUIViewEventNames()
		{
		}
	}
}
