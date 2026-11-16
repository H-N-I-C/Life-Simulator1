using System;
using System.Collections.Generic;

namespace Game.DeepLinking
{
	public class AttributionController
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__9_0;

			internal void _003CInit_003Eb__9_0()
			{
			}
		}

		private const string RewardIdKey = "rewardid";

		private const string TriggerIdKey = "triggerid";

		private static bool _deferredEvent;

		private static readonly List<TagItem> Cache;

		public static Action OnTagsProcessed;

		private static string LastRewardReceived__BackingField;

		public static string LastRewardReceived
		{
			get
			{
				return LastRewardReceived__BackingField;
			}
			set
			{
				LastRewardReceived__BackingField = value;
			}
		}

		private static void Init()
		{
		}

		public static void ProcessTags(Dictionary<string, object> attributionDataDictionary, bool isConversionData = false)
		{
		}

		public static TagItem GetAvailableTags()
		{
			return null;
		}

		private static void RoutePendingMigrationRewards()
		{
		}

		public static bool IsFirstLaunch(Dictionary<string, object> attributionData)
		{
			return false;
		}

		public static bool RemoveLastTag()
		{
			return false;
		}

		public static void AddRewardTag(string code)
		{
		}

		public static void AddTriggerTag(string code)
		{
		}
	}
}
