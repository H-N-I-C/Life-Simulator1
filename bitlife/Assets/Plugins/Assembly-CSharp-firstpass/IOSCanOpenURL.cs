using System.Runtime.InteropServices;

public static class IOSCanOpenURL
{
	[PreserveSig]
	private static extern bool _IOSCanOpenURL(string url);

	public static bool CheckUrl(string url)
	{
		return false;
	}
}
