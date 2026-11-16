using System;
using System.Collections.Generic;
using Genesis.Entities;
using Genesis.FeatureContainers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintAddToCappedCollection : IBluePrint, ISuppressFollowUp
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<_003C_003Ef__AnonymousType4<string, int, double>, double> _003C_003E9__18_1;

			internal double _003CSelectEvictionTarget_003Eb__18_1(_003C_003Ef__AnonymousType4<string, int, double> x)
			{
				return 0.0;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public BluePrintAddToCappedCollection _003C_003E4__this;

			public GenesisEntityContainer entityContainer;

			internal _003C_003Ef__AnonymousType4<string, int, double> _003CSelectEvictionTarget_003Eb__0(string id, int index)
			{
				return null;
			}
		}

		private const string EvictLowest = "lowest";

		private const string EvictHighest = "highest";

		private string _containerId;

		private string _collectionName;

		private string _entityId;

		private int _cap;

		private string _metric;

		private string _evict;

		private BluePrintData _data;

		private bool _parsed;

		private BluePrintType BluePrintType__BackingField;

		public BluePrintType BluePrintType
		{
			get
			{
				return BluePrintType__BackingField;
			}
			set
			{
				BluePrintType__BackingField = value;
			}
		}

		public string ContainerId => null;

		public string CollectionName => null;

		public string EntityId => null;

		public int Cap => 0;

		public string Metric => null;

		public string Evict => null;

		public bool Parsed => false;

		public void Setup(BluePrintData data)
		{
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private void EnforceCap(GenesisFeatureContainer container, GenesisEntityContainer entityContainer)
		{
		}

		private string SelectEvictionTarget(List<string> ids, GenesisEntityContainer entityContainer)
		{
			return null;
		}

		private double ReadMetricOrderValue(GenesisEntityContainer entityContainer, string entityId)
		{
			return 0.0;
		}

		private static bool IsEvictKeyword(string token)
		{
			return false;
		}

		private static string ExtractMetricPropertyName(string raw)
		{
			return null;
		}

		private string ResolveEntityId(string value)
		{
			return null;
		}
	}
}
