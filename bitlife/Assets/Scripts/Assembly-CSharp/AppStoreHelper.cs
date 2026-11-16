using System.Runtime.InteropServices;

public static class AppStoreHelper
{
	[PreserveSig]
	private static extern string getStorefrontCountryCode();

	public static string GetStorefrontCountryCode()
	{
		return null;
	}
}
