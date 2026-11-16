using System;

public class AppsFlyerGoogleReceipt
{
	[Serializable]
	private struct GooglePurchaseReceipt
	{
		public string Payload;
	}

	[Serializable]
	private struct GooglePurchasePayload
	{
		public string json;

		public string signature;
	}

	public string inAppPurchaseData;

	public string inAppDataSignature;

	public AppsFlyerGoogleReceipt(string receipt)
	{
	}
}
