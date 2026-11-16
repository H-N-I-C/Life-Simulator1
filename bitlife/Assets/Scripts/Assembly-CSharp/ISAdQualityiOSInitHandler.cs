using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ISAdQualityiOSInitHandler : MonoBehaviour
{
	private delegate void ISAdQualityUnityInitSuccessCallback(string args);

	private delegate void ISAdQualityUnityInitFailedCallback(string args);

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		internal void _003C_002Ecctor_003Eb__13_0()
		{
		}

		internal void _003C_002Ecctor_003Eb__13_1(ISAdQualityInitError _003Cp0_003E, string _003Cp1_003E)
		{
		}
	}

	private static Action m_OnAdQualitySdkInitSuccess;

	private static Action<ISAdQualityInitError, string> m_OnAdQualitySdkInitFailed;

	public static event Action OnAdQualitySdkInitSuccess
	{
		add
		{
		}
		remove
		{
		}
	}

	public static event Action<ISAdQualityInitError, string> OnAdQualitySdkInitFailed
	{
		add
		{
		}
		remove
		{
		}
	}

	[PreserveSig]
	private static extern int ironSourceAdQuality_registerInitSuccessCallback(ISAdQualityUnityInitSuccessCallback func);

	[PreserveSig]
	private static extern void ironSourceAdQuality_registerInitFailedCallback(ISAdQualityUnityInitFailedCallback func);

	public static void fireInitSuccessCallback(string message)
	{
	}

	public static void fireInitFailedCallback(string message)
	{
	}
}
