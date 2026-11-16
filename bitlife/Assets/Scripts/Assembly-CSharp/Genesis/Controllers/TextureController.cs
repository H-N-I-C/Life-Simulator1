using System;
using System.Collections.Generic;
using Game.Genesis.Components;
using UnityEngine;

namespace Genesis.Controllers
{
	public class TextureController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<TextureLoaderItem, bool> _003C_003E9__5_0;

			internal bool _003CCheckQueue_003Eb__5_0(TextureLoaderItem t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public TextureLoaderItem newItem;

			internal void _003CStartDownload_003Eb__0(Texture2D tex)
			{
			}
		}

		public static Action<string> OnTextureDownloaded;

		private readonly Dictionary<string, TextureLoaderItem> _loadedTextures;

		private readonly TimerComponent _timer;

		public TextureController(TimerComponent timer)
		{
		}

		private void OnDestroy()
		{
		}

		private void CheckQueue()
		{
		}

		public void LoadTexture(string baseUrl, string id)
		{
		}

		private void StartDownload(string baseUrl, TextureLoaderItem newItem)
		{
		}

		public bool IsTextureReady(string id)
		{
			return false;
		}

		public Texture2D GetTexture2D(string id)
		{
			return null;
		}
	}
}
