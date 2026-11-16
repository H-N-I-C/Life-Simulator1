using System;
using System.Collections.Generic;
using Game.Dynamic.CustomLives;
using UnityEngine;
using UnityEngine.Networking;

namespace Genesis.Services
{
	public class LifeDownloader
	{
		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public string id;

			public UnityWebRequest request;

			internal void _003CDownloadLife_003Eb__0(AsyncOperation op)
			{
			}
		}

		private static readonly Dictionary<string, LifeDataHolderModel> Cache;

		private static readonly Dictionary<string, Action<bool, string>> Pending;

		public static void DownloadLife(CustomLivesModel config, string id, Action<bool, string> onComplete)
		{
		}

		private static void CompleteDownload(string id, bool success, string error)
		{
		}

		public static void CreateNewLifeInFolder(string newLifePath, string id)
		{
		}

		public static void PreLoadLife(string id, byte[] data, byte[] genesis)
		{
		}

		public static CustomLifeState IsCustomLifeAvailable(string id)
		{
			return CustomLifeState.NoLifeAvailable;
		}

		private static Dictionary<string, byte[]> UnzipData(byte[] data)
		{
			return null;
		}

		public static string GetCsvContent()
		{
			return null;
		}
	}
}
