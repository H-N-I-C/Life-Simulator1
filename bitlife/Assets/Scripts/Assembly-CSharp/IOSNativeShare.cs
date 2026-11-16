using System.Runtime.InteropServices;

public class IOSNativeShare
{
	[PreserveSig]
	private static extern void SendSMSWith(string bodyString, string urlString, string imageString, string shareIndex);

	[PreserveSig]
	private static extern void SendEmailWith(string bodyString, string urlString, string imageString, string shareIndex);

	[PreserveSig]
	private static extern bool CanSendSMS();

	[PreserveSig]
	private static extern bool CanSendEmail();

	public static bool AbleToSendSMS()
	{
		return false;
	}

	public static bool AbleToSendEmail()
	{
		return false;
	}

	public static void SendSMS(string bodyString, string urlString = "", string imageString = "", string shareIndex = "")
	{
	}

	public static void SendEmail(string bodyString, string urlString, string imageString = "", string shareIndex = "")
	{
	}
}
