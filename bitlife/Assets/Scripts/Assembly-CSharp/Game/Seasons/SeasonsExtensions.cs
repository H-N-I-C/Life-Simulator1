using System;
using System.Collections.Generic;
using Tracking;
using UnityEngine;

namespace Game.Seasons
{
	public static class SeasonsExtensions
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<Sprite, bool> _003C_003E9__16_0;

			public static Func<Sprite, float> _003C_003E9__16_1;

			internal bool _003CCombineSpritesLeftToRight_003Eb__16_0(Sprite s)
			{
				return false;
			}

			internal float _003CCombineSpritesLeftToRight_003Eb__16_1(Sprite s)
			{
				return 0f;
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public float targetHeight;

			internal int _003CCombineSpritesLeftToRight_003Eb__2(Sprite s)
			{
				return 0;
			}
		}

		private static Dictionary<ObjectiveType, string> ObjectiveTypeToId;

		private static Dictionary<string, ObjectiveType> ObjectiveIdToType;

		private static Dictionary<CommunityGoalType, string> CommunityGoalTypeToId;

		private static Dictionary<SeasonsItemType, BitLifeUIClicks> SeasonsItemTypeToBitLifeUIClicks;

		private static Dictionary<STREAK_STATUS, BitLifeUIClicks> StreakStatusToBitLifeUIClicks;

		public static string ToObjectiveId(ObjectiveType self)
		{
			return null;
		}

		public static bool TryGetObjectiveType(string self, out ObjectiveType objectiveType)
		{
			objectiveType = default;
			return false;
		}

		public static string ToCommunityGoalId(CommunityGoalType self)
		{
			return null;
		}

		public static BitLifeUIClicks ToBitLifeUIClicks(SeasonsItemType type)
		{
			return BitLifeUIClicks.WebShop_DirectToCheckout;
		}

		public static BitLifeUIClicks ToBitLifeUIClicks(STREAK_STATUS type)
		{
			return BitLifeUIClicks.WebShop_DirectToCheckout;
		}

		public static string DisplayTime(TimeSpan self)
		{
			return null;
		}

		public static string DebugDisplayTime(TimeSpan self)
		{
			return null;
		}

		public static TimeSpan Max(TimeSpan self, TimeSpan value)
		{
			return default;
		}

		public static TimeSpan Min(TimeSpan self, TimeSpan value)
		{
			return default;
		}

		public static string ToIso8601UtcString(DateTime dateTime)
		{
			return null;
		}

		public static DateTime FromIso8601UtcString(string isoString)
		{
			return default;
		}

		public static Texture2D CombineSpritesLeftToRight(IEnumerable<Sprite> self, int horizontalPadding = 0)
		{
			return null;
		}
	}
}
