using System;
using System.Collections;
using System.Collections.Generic;
using Backend.Authentication;
using Unity.Notifications.iOS;

public class NotificationManager : ILocalNotificationManager
{
	private sealed class _003C_003Ec__DisplayClass12_0
	{
		public iOSNotification iOSNotificationIntentData;

		internal bool _003CCheckForIntent_003Eb__0(LocalNotification x)
		{
			return false;
		}
	}

	private sealed class _003CRequestAuthorization_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		private string _003CPromptShownKey_003E5__2;

		private AuthorizationRequest _003Creq_003E5__3;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CRequestAuthorization_003Ed__23(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}
	}

	private string channelID;

	private string channelName;

	private string channelDescription;

	public List<LocalNotification> NotificationList;

	private static Action _actionDelayed;

	private static bool _serverReady;

	private static NotificationManager _Inst;

	public static NotificationManager Inst => null;

	private static void Init()
	{
	}

	private static void ServerReady(GetConfigResponse _)
	{
	}

	public void CheckForIntent()
	{
	}

	public void RouteNotification(LocalNotification clickedNotification, DateTime fireDate = default(DateTime))
	{
	}

	public static int GetRandomEligibleNotificationType()
	{
		return 0;
	}

	public void SetDeepDailyNotificationArray()
	{
	}

	public void CancelLocalNotificationsOnNewSession()
	{
	}

	private int DaysUntilDailyNotificationInOrderNI(int orderNI)
	{
		return 0;
	}

	public void SetLocalNotificationsOnClose()
	{
	}

	public void UpdateNotificationsLanguage()
	{
	}

	private void CreateNotification(string title, string body, string smallImageID, string largeImageID, DateTime fireTimeOfDay, int ID)
	{
	}

	private bool NotificationsAvailable()
	{
		return false;
	}

	public void RegisterNotifications()
	{
	}

	private IEnumerator RequestAuthorization()
	{
		return null;
	}

	public void SaveNotifications()
	{
	}

	public void LoadNotifications()
	{
	}
}
