using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Game.Genesis.Components;
using Genesis.Controllers;
using Genesis.Entities;

namespace Genesis.Services
{
	public class GenesisFilterProcessing
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<GenesisAgeUpPlacementCandidate, (string, AgeUpPlacement)> _003C_003E9__2_0;

			public static Func<string, bool> _003C_003E9__4_1;

			public static Func<string, int> _003C_003E9__4_0;

			internal (string, AgeUpPlacement) _003CCollectAgeUpPlacements_003Eb__2_0(GenesisAgeUpPlacementCandidate candidate)
			{
				return default;
			}

			internal int _003CTryParseAgeUpPlacementFilters_003Eb__4_0(string part)
			{
				return 0;
			}

			internal bool _003CTryParseAgeUpPlacementFilters_003Eb__4_1(string condition)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass1_0
		{
			public GENESIS_FILTER filter;

			public Func<GenesisFilterModel, bool> _003C_003E9__0;

			internal bool _003CExecuteFilter_003Eb__0(GenesisFilterModel t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public ScenarioController controller;

			public Action _003C_003E9__0;

			public Action _003C_003E9__1;

			public Action _003C_003E9__2;

			public Action _003C_003E9__3;

			internal void _003CFilterResult_003Eb__0()
			{
			}

			internal void _003CFilterResult_003Eb__1()
			{
			}

			internal void _003CFilterResult_003Eb__2()
			{
			}

			internal void _003CFilterResult_003Eb__3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_1
		{
			public SimParent parent;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals1;

			internal void _003CFilterResult_003Eb__4()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_10
		{
			public SimCoworker copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals10;

			internal void _003CFilterResult_003Eb__13()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_11
		{
			public SimCoworker supervisor;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals11;

			internal void _003CFilterResult_003Eb__14()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_12
		{
			public SimClassmate copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals12;

			internal void _003CFilterResult_003Eb__15()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_13
		{
			public SimTenant copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals13;

			internal void _003CFilterResult_003Eb__16()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_14
		{
			public SimSeniorPerson copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals14;

			internal void _003CFilterResult_003Eb__17()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_15
		{
			public SimZooAnimal copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals15;

			internal void _003CFilterResult_003Eb__18()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_16
		{
			public SimVampireHunter copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals16;

			internal void _003CFilterResult_003Eb__19()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_2
		{
			public SimPerson copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals2;

			internal void _003CFilterResult_003Eb__5()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_3
		{
			public SimPerson copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals3;

			internal void _003CFilterResult_003Eb__6()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_4
		{
			public SimChild copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals4;

			internal void _003CFilterResult_003Eb__7()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_5
		{
			public SimLover copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals5;

			internal void _003CFilterResult_003Eb__8()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_6
		{
			public SimPet copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals6;

			internal void _003CFilterResult_003Eb__9()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_7
		{
			public SimHouse copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals7;

			internal void _003CFilterResult_003Eb__10()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_8
		{
			public SimCar copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals8;

			internal void _003CFilterResult_003Eb__11()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_9
		{
			public SimGhost copy;

			public _003C_003Ec__DisplayClass7_0 CS_0024_003C_003E8__locals9;

			internal void _003CFilterResult_003Eb__12()
			{
			}
		}

		public const BindingFlags FlagsForGenesis = (BindingFlags)117;

		private const string FocusedEntityMemoryKey = "entity";

		public static string ExecuteFilter(GENESIS_FILTER filter, ScenarioController controller)
		{
			return null;
		}

		public static List<(string, AgeUpPlacement)> CollectAgeUpPlacements(ScenarioController controller)
		{
			return null;
		}

		public static List<GenesisAgeUpPlacementCandidate> CollectAgeUpPlacementCandidates(ScenarioController controller)
		{
			return null;
		}

		private static bool TryParseAgeUpPlacementFilters(string filterString, string scenarioId, out List<GenesisFilterModel> filters)
		{
			filters = null;
			return false;
		}

		private static void LogMalformedAgeUpPlacementFilter(string scenarioId, string filterString)
		{
		}

		public static string ExecuteFilter(string filterString, ScenarioController controller, string targetID)
		{
			return null;
		}

		public static bool FilterResult(List<GenesisFilterModel> filters, string[] itemValue, ScenarioController controller, ScenarioCSVData evaluationCsvData = null)
		{
			return false;
		}

		private static bool CheckIfExistsGenesis(GenesisFilterModel filter)
		{
			return false;
		}

		private static T2 CheckCollectionSize<T1, T2>(T1 collection, GenesisFilterModel filter, out bool wasNumberOrEmpty) where T1 : IList
		{
			wasNumberOrEmpty = default;
			return default;
		}

		private static T2 CheckIfExistsAndWithDataInArray<T1, T2>(T1 collection, GenesisFilterModel filter) where T1 : IList
		{
			return default;
		}

		private static bool CheckIfExistsAndWithData(SimPerson target, GenesisFilterModel filter)
		{
			return false;
		}

		private static bool CheckIfExistsAndWithData(object target, GenesisFilterModel filter)
		{
			return false;
		}

		private static bool EqualsAnyOredValue(string methodOutput, string data)
		{
			return false;
		}

		private static bool CheckFilterResults(string methodOutput, string data, MATH_OPERATOR mathOperator)
		{
			return false;
		}

		private static bool CheckGfcFilter(GenesisFilterModel filter)
		{
			return false;
		}

		private static bool CheckEntityFilter(GenesisFilterModel filter, ScenarioController controller)
		{
			return false;
		}

		private static IGenesisEntity ResolveFocusedEntity(ScenarioController controller)
		{
			return null;
		}

		private static string ReadFocusedEntityProperty(IGenesisEntity entity, string propertyKey)
		{
			return null;
		}

		private static bool CheckFeatureFilter(GenesisFilterModel filter, ScenarioController controller)
		{
			return false;
		}

		private static bool CheckAliasFilter(GenesisFilterModel filter, ScenarioController controller)
		{
			return false;
		}

		private static bool CheckCandidateFilter(GenesisFilterModel filter, ScenarioController controller)
		{
			return false;
		}

		private static bool CheckResolvedRootValue(GenesisFilterModel filter, string resolvedValue, string rootLabel)
		{
			return false;
		}
	}
}
