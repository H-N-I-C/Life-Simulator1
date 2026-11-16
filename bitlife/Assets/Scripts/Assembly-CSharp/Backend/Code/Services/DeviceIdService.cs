using System;
using System.Runtime.InteropServices;

namespace Backend.Code.Services
{
	public class DeviceIdService
	{
		private static string ForceDeviceId__BackingField;

		private static string _cachedDeviceId;

		public static string ForceDeviceId
		{
			get
			{
				return ForceDeviceId__BackingField;
			}
			set
			{
				ForceDeviceId__BackingField = value;
			}
		}

		public static string CachedId => null;

		[PreserveSig]
		private static extern string _Get_Device_id();

		[PreserveSig]
		private static extern string _getIDFV();

		public static void GetDeviceId(Action<string> result)
		{
		}

		public static bool IsDeviceIdValid()
		{
			return false;
		}

		public static bool IsDeviceIdValid(string deviceId)
		{
			return false;
		}

		public static string GetDeviceIdCached()
		{
			return null;
		}

		public static string GetIDFV()
		{
			return null;
		}
	}
}
