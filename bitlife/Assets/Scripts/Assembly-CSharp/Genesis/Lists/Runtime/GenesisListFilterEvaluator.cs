using System.Text.RegularExpressions;
using Genesis.Controllers;

namespace Genesis.Lists.Runtime
{
	public static class GenesisListFilterEvaluator
	{
		private sealed class _003C_003Ec__DisplayClass6_0
		{
			public ScenarioController controller;

			public bool bracketEvaluationFailed;

			internal string _003CEvaluateMixedFilter_003Eb__0(Match match)
			{
				return null;
			}
		}

		private static readonly Regex BracketGroupRegex;

		public static bool EvaluateIsVisible(ScenarioController controller, string expression)
		{
			return false;
		}

		public static bool EvaluateIsEnabled(ScenarioController controller, string expression)
		{
			return false;
		}

		private static bool Evaluate(ScenarioController controller, string expression, bool defaultValue)
		{
			return false;
		}

		private static bool EvaluateBooleanExpression(ScenarioController controller, string expression, bool defaultValue)
		{
			return false;
		}

		private static bool IsBracketOnlyExpression(string expression)
		{
			return false;
		}

		private static bool EvaluateMixedFilter(ScenarioController controller, string expression)
		{
			return false;
		}

		private static bool TryEvaluateBracketGroup(ScenarioController controller, Match group, out bool result)
		{
			result = default;
			return false;
		}

		private static bool EvaluateBracketFilter(ScenarioController controller, string expression, bool defaultValue)
		{
			return false;
		}
	}
}
