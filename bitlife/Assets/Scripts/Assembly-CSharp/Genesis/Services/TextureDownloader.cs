using System;
using UnityEngine;
using UnityEngine.Networking;

namespace Genesis.Services
{
	public static class TextureDownloader
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public UnityWebRequest request;

			public string filePath;

			public string imageId;

			public Action<Texture2D> onCompleted;

			public string url;

			internal void _003CGetTexture_003Eb__0(AsyncOperation _)
			{
			}
		}

		public static void GetTexture(string imageId, string url, Action<Texture2D> onCompleted)
		{
		}
	}
}
