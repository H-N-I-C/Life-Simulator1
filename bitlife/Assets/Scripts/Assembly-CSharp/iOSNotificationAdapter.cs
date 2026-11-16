using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.iOS;

public class iOSNotificationAdapter : ILocalNotificationAdapter
{
	private sealed class _003CRequestAuthorization_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public iOSNotificationAdapter _003C_003E4__this;

		private AuthorizationRequest _003Creq_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CRequestAuthorization_003Ed__7(int _003C_003E1__state)
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

	private readonly NotificationLogger _logger;

	public iOSNotificationAdapter(NotificationLogger logger)
	{
	}

	public void RegisterNotifications()
	{
	}

	public NOTIFICATION_PERMISSION GetNotificationPermission()
	{
		return NOTIFICATION_PERMISSION.NONE;
	}

	public void ScheduleNotification(LocalNotificationV1 notification)
	{
	}

	public void CancelAllNotifications()
	{
	}

	public string GetLastRespondedDataJson()
	{
		return null;
	}

	private IEnumerator RequestAuthorization()
	{
		return null;
	}
}
