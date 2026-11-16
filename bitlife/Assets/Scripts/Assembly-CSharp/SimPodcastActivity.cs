using System;

public class SimPodcastActivity
{
	private class OnlyFansPromotionProxy : IPromotableSocialMedia
	{
		private readonly SimSocialMediaChannel _onlyFansChannelProxy;

		private readonly SimPodcastActivity _activity;

		private readonly SimPodcast _podcast;

		public int AgeAtLastOfferedProductPromotion
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int NumProductsOfferedToPromoteAtCurrentAge
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public int Channel
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public DataSocialMediaChannel Data
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public int Followers
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		public OnlyFansPromotionProxy(SimPodcastActivity activity)
		{
		}

		public bool IsPostEligibleForProductPromotion()
		{
			return false;
		}

		public string GetChannelName()
		{
			return null;
		}

		public float GetUnadjustedCompensationForPromotion()
		{
			return 0f;
		}

		public int GetFollowers()
		{
			return 0;
		}

		public bool IsFollowerEligibleForProductPromotion()
		{
			return false;
		}

		public void RecordEarnings(double earnings)
		{
		}

		public SimSocialMediaTroll GetPostTroll()
		{
			return null;
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPodcastContent, bool> _003C_003E9__26_0;

		public static Func<SimPodcastContent, bool> _003C_003E9__43_0;

		public static Func<Scenario, bool> _003C_003E9__47_0;

		internal bool _003CAddSeasonBroadcast_003Eb__26_0(SimPodcastContent q)
		{
			return false;
		}

		internal bool _003CGetSeasonCount_003Eb__43_0(SimPodcastContent q)
		{
			return false;
		}

		internal bool _003CFameCheck_003Eb__47_0(Scenario x)
		{
			return false;
		}
	}

	private const int MAX_SUBSCRIBER_CEILING = 20000000;

	private const int MAX_SUBSCRIBER_SOFT_TARGET = 5000000;

	private const string SEASON_CONTENT = "season";

	private const string LIVE_BROADCAST_CONTENT = "live episode";

	private readonly SimPodcast _podcast;

	private readonly SimHero _hero;

	private readonly Life _Life;

	private readonly SimLover _CurrentLover;

	private SimPodcastContent _activePodcastContent;

	private int _successfulPodcastScore;

	private bool _setContentResults;

	public SimPodcast PodCast => null;

	public SimPodcastActivity(SimPodcast podcast)
	{
	}

	public string GetPodcastName()
	{
		return null;
	}

	public int GetPodcastSubscriptionFee()
	{
		return 0;
	}

	public int GetSubscriberCount()
	{
		return 0;
	}

	public void SetSubscriberCount(int subscriberCount)
	{
	}

	public void ImpactHappiness(int score)
	{
	}

	public int GetCurrentSubscriptionFee()
	{
		return 0;
	}

	public void SetSubscriptionFee(int subscriptionFee)
	{
	}

	public SimPodcastEpisode CreateLivePodcast(string episodeName, int initialCostToListen = 0)
	{
		return null;
	}

	public string GetGenre()
	{
		return null;
	}

	public bool CanHostLiveBroadcasts()
	{
		return false;
	}

	public void AddLiveBroadcast(SimPodcastEpisode liveEpisode)
	{
	}

	public void AddSeasonBroadcast(SimPodcastSeason season)
	{
	}

	public bool CanRecordSeason()
	{
		return false;
	}

	public void SetActiveContent(SimPodcastContent content)
	{
	}

	public SimPodcastContent GetActiveContent()
	{
		return null;
	}

	public string GetActiveContentType()
	{
		return null;
	}

	public int GetContentPopularity()
	{
		return 0;
	}

	public int GetPopularityScore()
	{
		return 0;
	}

	public void SetSuccessRating(int score)
	{
	}

	public int GetSuccessRating()
	{
		return 0;
	}

	public void ImpactLoverRelationship(int impact)
	{
	}

	public void ImpactPodcastPopularity(int score)
	{
	}

	public IPromotableSocialMedia GetOnlyFansPromotionalFeed()
	{
		return null;
	}

	public void SetActiveContentResults()
	{
	}

	public void SetActiveContentResults(int listeners, int addedSubscribers)
	{
	}

	public (int, int) CalculateContentListenerCount()
	{
		return default;
	}

	private (int, int) CalculateSeasonListenerCount()
	{
		return default;
	}

	private (int, int) CalculateEpisodeListenerCount()
	{
		return default;
	}

	public int GetSeasonCount()
	{
		return 0;
	}

	public string GetPodcastIconString()
	{
		return null;
	}

	public static void LogToConsole(string message)
	{
	}

	public void CalculateContentViralStatus()
	{
	}

	public static void FameCheck(Scenario optional = null)
	{
	}
}
