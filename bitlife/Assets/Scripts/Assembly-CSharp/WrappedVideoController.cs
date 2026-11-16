using System;
using System.Collections;
using Newtonsoft.Json.Linq;

public class WrappedVideoController
{
	private sealed class _003C_003Ec__DisplayClass28_0
	{
		public WrappedVideoController _003C_003E4__this;

		public bool canShowDarkly;

		internal void _003CRefreshStatus_003Eb__0(JObject jObject, bool success)
		{
		}
	}

	private const string FrontendUrl = "https://wrapped.bitlifeapp.com";

	private readonly WrappedConfig _config;

	private readonly WrappedUserStats _userStats;

	private readonly GameManager _gameManager;

	private int _countTimer;

	public WrappedConfig Config => null;

	public WrappedVideoController(GameManager gameManager)
	{
	}

	private void OnUnityUpdateTimer()
	{
	}

	private void OnFinishStartSequence()
	{
	}

	private bool CanShowDarkly()
	{
		return false;
	}

	private bool IsEligibleToRequestVideo()
	{
		return false;
	}

	private bool IsEligibleToShowUI()
	{
		return false;
	}

	public void SendUserStatsToBackend()
	{
	}

	public void AddListItems(ArrayList listItems)
	{
	}

	public bool ShouldShowButton()
	{
		return false;
	}

	public void Save()
	{
	}

	public void ShowInfo(string originId)
	{
	}

	public void ShowDarkly(string originId)
	{
	}

	private void ShowDarklyPanel(string originId)
	{
	}

	public bool HasOpened()
	{
		return false;
	}

	public void OpenWrappedFrontend()
	{
	}

	public WrappedYearVars LazyYearVars()
	{
		return null;
	}

	private void SendUserStatsToBackend(string jsonData)
	{
	}

	private void InitializeConfig()
	{
	}

	private string GetJobId()
	{
		return null;
	}

	private bool ShouldSendUserStatsToBackend()
	{
		return false;
	}

	public string FormatUserStatsJson()
	{
		return null;
	}

	public void RefreshStatus(bool canShowDarkly)
	{
	}

	private void CheckWrappedVideoStatus(Action<JObject, bool> callback)
	{
	}

	private void OnVideoResponse(JObject jObject, bool success, bool canShowDarkly)
	{
	}

	public void ShowButton()
	{
	}

	public void RegisterSeenVideo()
	{
	}

	private void OnGraveResponse(JObject jObject, bool success)
	{
	}

	private string SaveJobId(JObject jObject)
	{
		return null;
	}
}
