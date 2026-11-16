using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Genesis.Services
{
	public static class CsvDownloader
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public UnityWebRequest request;

			public Action<string> onCompleted;

			public string csvId;

			public string url;

			internal void _003CGetCsvContent_003Eb__0(AsyncOperation _)
			{
			}
		}

		public static void GetCsvContent(string csvId, string url, Action<string> onCompleted)
		{
		}
	}
}
