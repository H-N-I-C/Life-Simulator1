using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Entities;

namespace Game.Genesis.BluePrints
{
	public class BluePrintFocusEntity : IBluePrint, IAdvancedLogicFocusSetup
	{
		private enum SourceMode
		{
			EntityType = 0,
			GfcCollection = 1,
			Current = 2
		}

		private enum Selector
		{
			First = 0,
			Last = 1,
			Random = 2,
			Index = 3,
			Id = 4
		}

		private SourceMode _sourceMode;

		private string _entityType;

		private string _gfcContainerId;

		private string _gfcCollectionKey;

		private Selector _selector;

		private int _selectorIndex;

		private string _selectorId;

		private string _memoryPrefix;

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

		public void Setup(BluePrintData data)
		{
		}

		private bool ParseGfcSource(string source)
		{
			return false;
		}

		public bool ConditionSelected()
		{
			return false;
		}

		public bool TryApplyAdvancedLogicFocus()
		{
			return false;
		}

		private bool ApplyFocus(bool performOrdinaryFocusWork, bool tolerateLookupFailure, string callerName)
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private static void BakeEntityDrawTags(GenesisDataEntity entity, ScenarioController controller)
		{
		}

		private List<IGenesisEntity> GetEntityTypeCandidates(GenesisEntityContainer container)
		{
			return null;
		}

		private static List<IGenesisEntity> GetCurrentCandidate(GenesisEntityContainer container)
		{
			return null;
		}

		private List<IGenesisEntity> GetGfcCollectionCandidates(GenesisEntityContainer entityContainer)
		{
			return null;
		}

		private IGenesisEntity SelectEntity(List<IGenesisEntity> candidates)
		{
			return null;
		}
	}
}
