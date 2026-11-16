namespace CandyBusiness.NativeAds
{
	// Shim for the removed native-ads SDKs (Gadsme / Google Immersive).
	// Type identity only — no ad functionality.
	public enum NativeAdsProvider
	{
		None,
		Gadsme,
		Google,
		GoogleImmersive,
	}
}
