public class AppsFlyerMilestoneLogger
{
	public static readonly AppsFlyerMilestoneLogger Achievements;

	public static readonly AppsFlyerMilestoneLogger Careers;

	public static readonly AppsFlyerMilestoneLogger Heirlooms;

	public static readonly AppsFlyerMilestoneLogger Ribbons;

	private const string UserAcquisition = "ua_";

	private const string CompletedSeparator = "_completed_";

	private readonly string _eventCollectible;

	private readonly int[] _milestones;

	private readonly bool _logOnlyOnce;

	private AppsFlyerMilestoneLogger(string collectible, int[] milestones, bool logOnlyOnce = false)
	{
	}

	public void OnCompleted(int completed, string identifier)
	{
	}
}
