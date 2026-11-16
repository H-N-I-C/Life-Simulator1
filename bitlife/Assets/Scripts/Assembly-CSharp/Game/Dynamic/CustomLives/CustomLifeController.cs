using System;
using System.Collections.Generic;
using Backend;
using Game.DeepLinking;

namespace Game.Dynamic.CustomLives
{
	public class CustomLifeController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__2_0;

			internal void _003CInit_003Eb__2_0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass10_0
		{
			public Action<bool> action;

			internal void _003CDownloadLife_003Eb__0(bool success, string text)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_0
		{
			public Action<bool> callback;

			public string lifeId;

			internal void _003CStartCustomLifeFromOnboarding_003Eb__0(List<StuffItem> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass12_1
		{
			public StuffItem stuffItem;

			public _003C_003Ec__DisplayClass12_0 CS_0024_003C_003E8__locals1;

			internal void _003CStartCustomLifeFromOnboarding_003Eb__1(MYSTUFF_USE_STATE success)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass9_0
		{
			public Action<bool> action;

			internal void _003CDownloadLife_003Eb__0(bool success, string text)
			{
			}
		}

		public static Action<Life> OnUpdateCustomLifeStatus;

		private CustomLivesModel _config;

		private static void Init()
		{
		}

		~CustomLifeController()
		{
		}

		private void TranslateCustomLife(Life life)
		{
		}

		public bool StartLife(string id)
		{
			return false;
		}

		public void LoadContent(CustomLivesModel data)
		{
		}

		public bool IsCustomLife(TagItem tagItem)
		{
			return false;
		}

		public CustomLifeState IsCustomLifeAvailable(TagItem tagItem)
		{
			return CustomLifeState.NoLifeAvailable;
		}

		public void DownloadLife(TagItem tagItem, Action<bool> action)
		{
		}

		public void DownloadLife(string itemId, Action<bool> action)
		{
		}

		public void DownloadLife(string itemId, Action<bool, string> result)
		{
		}

		public void StartCustomLifeFromOnboarding(string lifeId, Action<bool> callback)
		{
		}

		public bool LoadAndStartLife(string startLiveId)
		{
			return false;
		}
	}
}
