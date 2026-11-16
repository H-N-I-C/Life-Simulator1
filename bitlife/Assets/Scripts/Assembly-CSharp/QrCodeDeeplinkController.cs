using System;

public class QrCodeDeeplinkController
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action _003C_003E9__5_0;

		public static Action _003C_003E9__5_1;

		internal void _003CInit_003Eb__5_0()
		{
		}

		internal void _003CInit_003Eb__5_1()
		{
		}
	}

	private static JsonExportHeroActivity _activity;

	private static bool _isLogged;

	private static bool _isOnboardFinished;

	private static bool _isConfidenceFlagsResolved;

	private static string _cachedCode;

	private static void Init()
	{
	}

	private static void OnConfidenceFlagsUpdated()
	{
	}

	public static void ProcessQrCode(string code)
	{
	}
}
