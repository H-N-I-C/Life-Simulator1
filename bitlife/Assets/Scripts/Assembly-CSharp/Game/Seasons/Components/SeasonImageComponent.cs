using Game.Seasons.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Seasons.Components
{
	public class SeasonImageComponent : MonoBehaviour
	{
		private SeasonsAssetTypes seasonsAssetType;

		private Image spriteToReplace;

		private SeasonsAssetTypes shopDisabledAssetType;

		private Sprite fallback;

		public bool isContentTile;

		private void Start()
		{
		}

		public void Refresh()
		{
		}
	}
}
