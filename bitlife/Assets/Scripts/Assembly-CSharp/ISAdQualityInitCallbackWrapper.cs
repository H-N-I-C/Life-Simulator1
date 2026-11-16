using UnityEngine;

public class ISAdQualityInitCallbackWrapper : MonoBehaviour
{
	private ISAdQualityInitCallback mCallback;

	private ISAdQualityiOSInitHandler adQualityiOSInitHandler;

	public ISAdQualityInitCallback AdQualityInitCallback
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private void Awake()
	{
	}

	private void adQualitySdkInitSuccess()
	{
	}

	private void onAdQualitySdkInitFailed(ISAdQualityInitError sdkInitError, string errorMsg)
	{
	}

	private void registerAdQualityiOSInitEvents()
	{
	}

	private void _003CregisterAdQualityiOSInitEvents_003Eb__8_0()
	{
	}

	private void _003CregisterAdQualityiOSInitEvents_003Eb__8_1(ISAdQualityInitError sdkInitError, string errorMsg)
	{
	}
}
