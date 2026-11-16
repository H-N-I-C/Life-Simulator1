using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dynamic
{
	public class AssetLoaderController
	{
		public static void LoadAssetBundle(string baseUrl, string name, int version, bool unloadAfterUse, Action<List<UnityEngine.Object>> onSuccess, Action onError)
		{
		}

		public static AssetBundle GetLoadedAssetBundle(string bundleName)
		{
			return null;
		}

		public static void UnloadAssetBundle(string bundleName)
		{
		}
	}
}
