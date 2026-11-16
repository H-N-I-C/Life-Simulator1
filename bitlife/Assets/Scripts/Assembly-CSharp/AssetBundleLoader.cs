using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader
{
	private enum State
	{
		Idle = 0,
		LoadingManifest = 1,
		WaitingResponse = 2,
		LoadingBundle = 3,
		Done = 4,
		Error = 5
	}

	private sealed class _003C_003Ec__DisplayClass17_0
	{
		public AssetBundleLoader _003C_003E4__this;

		public bool unloadAfterUse;

		internal void _003CStartBundleRequest_003Eb__0(AsyncOperation _)
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public AssetBundleLoader _003C_003E4__this;

		public bool unloadAfterUse;

		public string reason;

		internal void _003CRetry_003Eb__0()
		{
		}
	}

	private readonly string _baseUrl;

	public static bool IsLogging;

	public static int MaxRetries;

	private const string _majorVersion = "v2";

	private readonly string _platform;

	private readonly string _bundleName;

	private readonly Action<List<UnityEngine.Object>> _onSuccess;

	private readonly Action _onError;

	private UnityWebRequest _req;

	private UnityWebRequest _reqDisposed;

	private State _state;

	private int _attempt;

	private uint _crc;

	private bool _hasCrc;

	private readonly int _version;

	public AssetBundleLoader(string baseUrl, string bundleName, int version, bool unloadAfterUse, Action<List<UnityEngine.Object>> onSuccess, Action onError)
	{
	}

	private void StartBundleRequest(bool unloadAfterUse, string reason = null)
	{
	}

	private void HandleBundleResponse(bool unloadAfterUse)
	{
	}

	private void Retry(bool unloadAfterUse, string reason)
	{
	}

	private void Fail(string message)
	{
	}
}
