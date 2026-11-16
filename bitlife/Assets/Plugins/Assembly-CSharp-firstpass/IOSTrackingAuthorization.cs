using System.Runtime.InteropServices;

public class IOSTrackingAuthorization
{
	[PreserveSig]
	private static extern string IOSGetTrackingAuthorizationStatus();

	[PreserveSig]
	private static extern void IOSRequestTrackingAuthorization();

	[PreserveSig]
	private static extern bool IOSObserveATT();

	public static string GetTrackingAuthorizationStatus()
	{
		return null;
	}

	public static bool ObserveATT()
	{
		return false;
	}

	public static void RequestTrackingAuthorization()
	{
	}
}
