using System;

public class UIAdManager
{
	public class AdContext
	{
		private string Origin__BackingField;

		private string PostPlayLog__BackingField;

		private Scenario PostPlayScenario__BackingField;

		private Action OnSuccess__BackingField;

		public string Origin
		{
			get
			{
				return Origin__BackingField;
			}
			set
			{
				Origin__BackingField = value;
			}
		}

		public string PostPlayLog
		{
			get
			{
				return PostPlayLog__BackingField;
			}
			set
			{
				PostPlayLog__BackingField = value;
			}
		}

		public Scenario PostPlayScenario
		{
			get
			{
				return PostPlayScenario__BackingField;
			}
			set
			{
				PostPlayScenario__BackingField = value;
			}
		}

		public Action OnSuccess
		{
			get
			{
				return OnSuccess__BackingField;
			}
			set
			{
				OnSuccess__BackingField = value;
			}
		}
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Action<int> _003C_003E9__54_0;

		public static Action<int> _003C_003E9__54_1;

		public static Action<int> _003C_003E9__55_0;

		public static Action<int> _003C_003E9__55_1;

		internal void _003CRewardedVideoClosedPrematurely_003Eb__54_0(int index)
		{
		}

		internal void _003CRewardedVideoClosedPrematurely_003Eb__54_1(int index)
		{
		}

		internal void _003CRewardedVideoWatched_003Eb__55_0(int index)
		{
		}

		internal void _003CRewardedVideoWatched_003Eb__55_1(int index)
		{
		}
	}

	private Scenario RewardedVideoPostPlayScenario__BackingField;

	private string RewardedVideoPostPlayLog__BackingField;

	private int RewardedVideoOrigin__BackingField;

	private AdContext RewardedCurrentContext__BackingField;

	private bool _startedRewardedVideo;

	private bool RewardedVideoWasOffline__BackingField;

	private int RewardedVideoJobPack__BackingField;

	private int RewardedVideoExpansionPack__BackingField;

	private SimHeirloom RewardedVideoExtraHeirloom__BackingField;

	private Action RewardedVideoCallback__BackingField;

	private int InterstitialOrigin__BackingField;

	private bool InterstitialWasOffline__BackingField;

	private static UIAdManager inst;

	private static bool bannerTemporarilyHidden;

	private Scenario RewardedVideoPostPlayScenario
	{
		get
		{
			return RewardedVideoPostPlayScenario__BackingField;
		}
		set
		{
			RewardedVideoPostPlayScenario__BackingField = value;
		}
	}

	private string RewardedVideoPostPlayLog
	{
		get
		{
			return RewardedVideoPostPlayLog__BackingField;
		}
		set
		{
			RewardedVideoPostPlayLog__BackingField = value;
		}
	}

	private int RewardedVideoOrigin
	{
		get
		{
			return RewardedVideoOrigin__BackingField;
		}
		set
		{
			RewardedVideoOrigin__BackingField = value;
		}
	}

	public AdContext RewardedCurrentContext
	{
		get
		{
			return RewardedCurrentContext__BackingField;
		}
		set
		{
			RewardedCurrentContext__BackingField = value;
		}
	}

	public bool IsShowingRewardedAd => false;

	private bool RewardedVideoWasOffline
	{
		get
		{
			return RewardedVideoWasOffline__BackingField;
		}
		set
		{
			RewardedVideoWasOffline__BackingField = value;
		}
	}

	public int RewardedVideoJobPack
	{
		get
		{
			return RewardedVideoJobPack__BackingField;
		}
		set
		{
			RewardedVideoJobPack__BackingField = value;
		}
	}

	public int RewardedVideoExpansionPack
	{
		get
		{
			return RewardedVideoExpansionPack__BackingField;
		}
		set
		{
			RewardedVideoExpansionPack__BackingField = value;
		}
	}

	public SimHeirloom RewardedVideoExtraHeirloom
	{
		get
		{
			return RewardedVideoExtraHeirloom__BackingField;
		}
		set
		{
			RewardedVideoExtraHeirloom__BackingField = value;
		}
	}

	public Action RewardedVideoCallback
	{
		get
		{
			return RewardedVideoCallback__BackingField;
		}
		set
		{
			RewardedVideoCallback__BackingField = value;
		}
	}

	private int InterstitialOrigin
	{
		get
		{
			return InterstitialOrigin__BackingField;
		}
		set
		{
			InterstitialOrigin__BackingField = value;
		}
	}

	private bool InterstitialWasOffline
	{
		get
		{
			return InterstitialWasOffline__BackingField;
		}
		set
		{
			InterstitialWasOffline__BackingField = value;
		}
	}

	public static UIAdManager Inst => null;

	public static bool IsRewardedVideoAvailable()
	{
		return false;
	}

	public void StartRewardedVideo(AdContext context)
	{
	}

	public void StartRewardedVideo(Scenario postPlayScenario, string postPlayLog, int origin, Action callback = null)
	{
	}

	public void RewardedVideoClosedPrematurely()
	{
	}

	public void RewardedVideoWatched()
	{
	}

	public static bool IsInterstitialAvailable()
	{
		return false;
	}

	public void StartNewLifeAdIfApplicable()
	{
	}

	public void CheckForPeriodicPostScenarioAd()
	{
	}

	public void StartInterstitial(int origin)
	{
	}

	public void InterstitialWatched()
	{
	}

	public void RefreshBannerEligibility()
	{
	}

	public void StartBanner(bool adBannerBacker = true)
	{
	}

	public void StopBanner(bool adBannerBacker = true)
	{
	}

	public static void TempHideBanner()
	{
	}

	public static void TempShowBanner()
	{
	}
}
