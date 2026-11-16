using System.Collections.Generic;

namespace Game.Genesis.Components
{
	public class GenesisFilterParser
	{
		public static List<GenesisFilterModel> ParseFilter(string[] filterParts)
		{
			return null;
		}

		private static GenesisFilterModel CreateFilterModel(string filter)
		{
			return null;
		}

		private static string RejoinPath(string[] command)
		{
			return null;
		}

		private static GenesisFilterModel CreateRootPathCondition(GENESIS_FILTER filterSet, bool not, string fullPath, string rootAlias, object[] parameters, bool logIfMalformed)
		{
			return null;
		}

		private static bool TrySplitOperator(string expr, out string left, out MATH_OPERATOR mathOperator, out string right)
		{
			left = null;
			mathOperator = default;
			right = null;
			return false;
		}

		private static bool TrySplitOn(string expr, string token, MATH_OPERATOR op, ref string left, ref MATH_OPERATOR mathOperator, ref string right)
		{
			return false;
		}

		private static GenesisFilterModel CreateHeroCondition(bool not, string target, string conditions, object[] parameters)
		{
			return null;
		}

		private static GenesisFilterModel CreateCondition(GENESIS_FILTER filterSet, bool not, string target, string conditions, object[] parameters)
		{
			return null;
		}
	}
}
