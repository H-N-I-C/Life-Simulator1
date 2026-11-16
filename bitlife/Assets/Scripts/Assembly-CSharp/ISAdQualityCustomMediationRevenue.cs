public class ISAdQualityCustomMediationRevenue
{
	private ISAdQualityMediationNetwork mediationNetwork;

	private ISAdQualityAdType adType;

	private double revenue;

	private string placement;

	public ISAdQualityMediationNetwork MediationNetwork
	{
		get
		{
			return ISAdQualityMediationNetwork.ADMOB;
		}
		set
		{
		}
	}

	public ISAdQualityAdType AdType
	{
		get
		{
			return ISAdQualityAdType.RICH_MEDIA;
		}
		set
		{
		}
	}

	public double Revenue
	{
		get
		{
			return 0.0;
		}
		set
		{
		}
	}

	public string Placement
	{
		get
		{
			return null;
		}
		set
		{
		}
	}
}
