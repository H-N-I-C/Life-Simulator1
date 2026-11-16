using System;
using System.Runtime.InteropServices;

public static class DeclaredAgeRangeIOS
{
	private class CallbackContext
	{
		public Action<AgeRangeResult> OnResult;

		public Action<string> OnError;
	}

	private delegate void DeclaredAgeRangeCallback(int responseType, int declaration, int lowerBound, int upperBound, IntPtr error, IntPtr context);

	[PreserveSig]
	private static extern void requestAgeRangeWithThreshold1(int threshold1, int threshold2, int threshold3, DeclaredAgeRangeCallback callback, IntPtr context);

	public static void RequestAgeRange(int threshold1, int threshold2, int threshold3, Action<AgeRangeResult> onResult, Action<string> onError)
	{
	}

	private static void OnNativeCallback(int responseType, int declaration, int lowerBound, int upperBound, IntPtr errorPtr, IntPtr contextPtr)
	{
	}
}
