using System.Collections.Generic;
using UnityEngine;

namespace Game.Seasons.Controllers
{
	public class SeasonsAssetsController
	{
		private readonly Dictionary<SeasonsAssetTypes, Sprite> _seasonAssets;

		private readonly Dictionary<string, Sprite> _seasonSearchAssets;

		private readonly Dictionary<SeasonsAudioAssetTypes, AudioClip> _seasonAudioAssets;

		public void SetData(List<Object> allAssets)
		{
		}

		private SeasonsAssetTypes TranslateType(string name)
		{
			return SeasonsAssetTypes.None;
		}

		private SeasonsAudioAssetTypes TranslateAudioType(string name)
		{
			return SeasonsAudioAssetTypes.None;
		}

		public Sprite GetSprite(SeasonsAssetTypes assetType, SeasonsAssetTypes fallbackType)
		{
			return null;
		}

		public Sprite GetSprite(SeasonsAssetTypes assetType)
		{
			return null;
		}

		public Sprite GetSprite(string assetName)
		{
			return null;
		}

		public Sprite FindSprite(string itemId)
		{
			return null;
		}

		public AudioClip GetAudio(SeasonsAudioAssetTypes assetType)
		{
			return null;
		}

		public void PlayAudio(SeasonsAudioAssetTypes audioName)
		{
		}

		public bool HasAsset(string itemId)
		{
			return false;
		}
	}
}
