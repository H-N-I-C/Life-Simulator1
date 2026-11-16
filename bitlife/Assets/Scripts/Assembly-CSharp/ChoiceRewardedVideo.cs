using System;
using System.Collections;

public class ChoiceRewardedVideo
{
	private readonly REWARDED_VIDEO_ORIGIN _origin;

	private readonly string _choiceLabel;

	private bool WasChoiceAdded__BackingField;

	public bool WasChoiceAdded
	{
		get
		{
			return WasChoiceAdded__BackingField;
		}
		private set
		{
			WasChoiceAdded__BackingField = value;
		}
	}

	public ChoiceRewardedVideo(REWARDED_VIDEO_ORIGIN origin, string choiceLabel)
	{
	}

	public void AddScenarioChoice(ArrayList scenarioChoices)
	{
	}

	public bool TryGetScenarioChoice(out ScenarioChoice choice)
	{
		choice = null;
		return false;
	}

	public void WatchVideo(Scenario currentScenario, Scenario scenarioAfterVideo, Action callback = null)
	{
	}

	private static void AwaitCallback(Scenario currentScenario, Scenario scenarioAfterCallback)
	{
	}

	public void NotifyNotWatched()
	{
	}

	private static bool OfferRewardedVideoToWatchMovie()
	{
		return false;
	}
}
