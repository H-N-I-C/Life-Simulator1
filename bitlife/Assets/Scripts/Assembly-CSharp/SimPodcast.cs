using System;
using Genesis.Simulation;

[Serializable]
public class SimPodcast : SimPodcastContent, ISimDataProvider
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimPodcastContent, int> _003C_003E9__59_0;

		public static Func<SimPodcastContent, int> _003C_003E9__62_0;

		public static Func<SimPodcastContent, int> _003C_003E9__63_0;

		public static Func<SimPodcastContent, bool> _003C_003E9__70_0;

		internal int _003CGetEpisodeCount_003Eb__59_0(SimPodcastContent content)
		{
			return 0;
		}

		internal int _003CGetContentPopularity_003Eb__62_0(SimPodcastContent content)
		{
			return 0;
		}

		internal int _003CGetNumberOfActiveUsers_003Eb__63_0(SimPodcastContent content)
		{
			return 0;
		}

		internal bool _003CGetSeasonCount_003Eb__70_0(SimPodcastContent content)
		{
			return false;
		}
	}

	private string Platform__BackingField;

	private int _subscriberCount__BackingField;

	private int AgeAtLastPodcastPromotion__BackingField;

	private int PodcastPromotionCount__BackingField;

	private int SubscriptionFee__BackingField;

	private int NumberOfEpisodes__BackingField;

	private int AgeAtLastContentReleased__BackingField;

	private int LiveBroadcastsRecordedThisYear__BackingField;

	private int Att_Popularity__BackingField;

	private bool HasRecordedSeasonThisYear__BackingField;

	private int PodcastEpisodesThisYear__BackingField;

	private int ViralContentCount__BackingField;

	private SimPodcastArchive Archive__BackingField;

	public const int MinSubscriptionFee = 4;

	public const int MaxSubscriptionFee = 15;

	public string Platform
	{
		get
		{
			return Platform__BackingField;
		}
		set
		{
			Platform__BackingField = value;
		}
	}

	private int _subscriberCount
	{
		get
		{
			return _subscriberCount__BackingField;
		}
		set
		{
			_subscriberCount__BackingField = value;
		}
	}

	public int SubscriberCount => 0;

	public int AgeAtLastPodcastPromotion
	{
		get
		{
			return AgeAtLastPodcastPromotion__BackingField;
		}
		set
		{
			AgeAtLastPodcastPromotion__BackingField = value;
		}
	}

	public int PodcastPromotionCount
	{
		get
		{
			return PodcastPromotionCount__BackingField;
		}
		set
		{
			PodcastPromotionCount__BackingField = value;
		}
	}

	public int SubscriptionFee
	{
		get
		{
			return SubscriptionFee__BackingField;
		}
		set
		{
			SubscriptionFee__BackingField = value;
		}
	}

	public int NumberOfEpisodes
	{
		get
		{
			return NumberOfEpisodes__BackingField;
		}
		set
		{
			NumberOfEpisodes__BackingField = value;
		}
	}

	public int AgeAtLastContentReleased
	{
		get
		{
			return AgeAtLastContentReleased__BackingField;
		}
		set
		{
			AgeAtLastContentReleased__BackingField = value;
		}
	}

	public int LiveBroadcastsRecordedThisYear
	{
		get
		{
			return LiveBroadcastsRecordedThisYear__BackingField;
		}
		set
		{
			LiveBroadcastsRecordedThisYear__BackingField = value;
		}
	}

	public int Att_Popularity
	{
		get
		{
			return Att_Popularity__BackingField;
		}
		set
		{
			Att_Popularity__BackingField = value;
		}
	}

	public bool HasRecordedSeasonThisYear
	{
		get
		{
			return HasRecordedSeasonThisYear__BackingField;
		}
		set
		{
			HasRecordedSeasonThisYear__BackingField = value;
		}
	}

	public int PodcastEpisodesThisYear
	{
		get
		{
			return PodcastEpisodesThisYear__BackingField;
		}
		set
		{
			PodcastEpisodesThisYear__BackingField = value;
		}
	}

	public int ViralContentCount
	{
		get
		{
			return ViralContentCount__BackingField;
		}
		set
		{
			ViralContentCount__BackingField = value;
		}
	}

	public SimPodcastArchive Archive
	{
		get
		{
			return Archive__BackingField;
		}
		private set
		{
			Archive__BackingField = value;
		}
	}

	public override void IncrementAge()
	{
	}

	private void FollowerIncrementAge()
	{
	}

	public override int GetEpisodeCount()
	{
		return 0;
	}

	public override double GetNetEarnings()
	{
		return 0.0;
	}

	public override int GetActiveYears()
	{
		return 0;
	}

	public override int GetContentPopularity()
	{
		return 0;
	}

	public override int GetNumberOfActiveUsers()
	{
		return 0;
	}

	public void CalculateInitialValues()
	{
	}

	private void CalculateInitialPopularity()
	{
	}

	private void CalculateInitialSubscriberCount()
	{
	}

	public int CalculateSubscriptionFeeImpact()
	{
		return 0;
	}

	public void AddContent(SimPodcastContent content)
	{
	}

	public void RemoveContent(SimPodcastContent content)
	{
	}

	public int GetSeasonCount()
	{
		return 0;
	}

	public bool CanCreatePodcast()
	{
		return false;
	}

	public bool CanRecordSeason()
	{
		return false;
	}

	public string GetDataByKey(string key)
	{
		return null;
	}

	public void SetSubscriberCount(int count)
	{
	}
}
