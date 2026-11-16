using System;
using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Lists.DataSources
{
	public sealed class SeniorCenterListDataSourceProvider : IGenesisListDataSourceProvider
	{
		private sealed class LiveSeniorFocus : IGenesisListFocusProvider
		{
			private readonly SimSeniorPerson _senior;

			public LiveSeniorFocus(SimSeniorPerson senior)
			{
			}

			public void ApplyFocus(ScenarioController controller, string focusMemoryKey)
			{
			}
		}

		private struct StubSeniorData
		{
			private readonly string DisplayName__BackingField;

			private readonly int Age__BackingField;

			private readonly float RelationshipStrength__BackingField;

			private readonly string Description__BackingField;

			private readonly string Icon__BackingField;

			private readonly string PronounSubject__BackingField;

			private readonly string PronounObject__BackingField;

			private readonly string PronounPossessive__BackingField;

			private readonly string RelationshipStatus__BackingField;

			private readonly string MaritalStatus__BackingField;

			private readonly string Children__BackingField;

			private readonly string Education__BackingField;

			private readonly string Occupation__BackingField;

			private readonly int Smarts__BackingField;

			private readonly int Looks__BackingField;

			private readonly int Petulance__BackingField;

			private readonly string ScenarioId__BackingField;

			private readonly string ScenarioFile__BackingField;

			public string DisplayName => DisplayName__BackingField;

			public int Age => Age__BackingField;

			public float RelationshipStrength => RelationshipStrength__BackingField;

			public string Description => Description__BackingField;

			public string Icon => Icon__BackingField;

			public string PronounSubject => PronounSubject__BackingField;

			public string PronounObject => PronounObject__BackingField;

			public string PronounPossessive => PronounPossessive__BackingField;

			public string RelationshipStatus => RelationshipStatus__BackingField;

			public string MaritalStatus => MaritalStatus__BackingField;

			public string Children => Children__BackingField;

			public string Education => Education__BackingField;

			public string Occupation => Occupation__BackingField;

			public int Smarts => Smarts__BackingField;

			public int Looks => Looks__BackingField;

			public int Petulance => Petulance__BackingField;

			public string ScenarioId => ScenarioId__BackingField;

			public string ScenarioFile => ScenarioFile__BackingField;

			public StubSeniorData(string displayName, int age, float relationshipStrength, string description, string icon, string pronounSubject, string pronounObject, string pronounPossessive, string relationshipStatus, string maritalStatus, string children, string education, string occupation, int smarts, int looks, int petulance, string scenarioId, string scenarioFile)
			{
				DisplayName__BackingField = null;
				Age__BackingField = 0;
				RelationshipStrength__BackingField = 0f;
				Description__BackingField = null;
				Icon__BackingField = null;
				PronounSubject__BackingField = null;
				PronounObject__BackingField = null;
				PronounPossessive__BackingField = null;
				RelationshipStatus__BackingField = null;
				MaritalStatus__BackingField = null;
				Children__BackingField = null;
				Education__BackingField = null;
				Occupation__BackingField = null;
				Smarts__BackingField = 0;
				Looks__BackingField = 0;
				Petulance__BackingField = 0;
				ScenarioId__BackingField = null;
				ScenarioFile__BackingField = null;
			}
		}

		private sealed class StubSeniorFocus : IGenesisListFocusProvider
		{
			private readonly StubSeniorData _data;

			public StubSeniorFocus(StubSeniorData data)
			{
			}

			public void ApplyFocus(ScenarioController controller, string focusMemoryKey)
			{
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<SimSeniorPerson, string> _003C_003E9__6_0;

			public static Func<GenesisListFallbackRoute, bool> _003C_003E9__9_0;

			internal string _003CGetItems_003Eb__6_0(SimSeniorPerson s)
			{
				return null;
			}

			internal bool _003CGetFallbackRoutes_003Eb__9_0(GenesisListFallbackRoute route)
			{
				return false;
			}
		}

		public const string ProviderKey = "senior_center:seniors";

		private static readonly SeniorCenterListDataSourceProvider Instance__BackingField;

		public static SeniorCenterListDataSourceProvider Instance => Instance__BackingField;

		public static void SeedOverviewMetrics(GenesisListDefinition definition, ScenarioController controller)
		{
		}

		private SeniorCenterListDataSourceProvider()
		{
		}

		public IEnumerable<GenesisListDataSourceItem> GetItems(ScenarioController controller, GenesisListDefinition definition)
		{
			return null;
		}

		private static GenesisListDataSourceItem CreateMember(SimSeniorPerson senior, GenesisListFallbackRoute fallbackRoute)
		{
			return null;
		}

		private static float? TryCalculateLiveAverageRelationshipStrength()
		{
			return null;
		}

		private static List<GenesisListFallbackRoute> GetFallbackRoutes(GenesisListDefinition definition)
		{
			return null;
		}

		private static GenesisListFallbackRoute SelectFallbackRoute(List<GenesisListFallbackRoute> routes, int index)
		{
			return null;
		}

		private static List<ScenarioAction> BuildActionList(GenesisListFallbackRoute fallbackRoute, StubSeniorData? fallbackScenario = null)
		{
			return null;
		}

		private static ScenarioCondition ConvertFallbackRouteToCondition(GenesisListFallbackRoute fallbackRoute)
		{
			return null;
		}

		private static string GetIconForSenior(SimSeniorPerson senior)
		{
			return null;
		}

		private static string BuildRelationshipStatus(SimPerson senior)
		{
			return null;
		}

		private static string BuildMaritalStatus(SimLover senior)
		{
			return null;
		}

		private static string BuildChildrenSummary(SimLover senior)
		{
			return null;
		}

		private static string BuildEducationSummary(SimPerson senior)
		{
			return null;
		}

		private static string BuildOccupationSummary(SimPerson senior)
		{
			return null;
		}

		private static int ClampProfileStat(float value)
		{
			return 0;
		}

		private static int GetDisplayedRelationshipStrength(SimSeniorPerson senior)
		{
			return 0;
		}
	}
}
