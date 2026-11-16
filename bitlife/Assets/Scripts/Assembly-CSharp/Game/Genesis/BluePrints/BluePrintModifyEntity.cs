using System.Collections.Generic;
using Genesis.Entities;

namespace Game.Genesis.BluePrints
{
	public class BluePrintModifyEntity : IBluePrint
	{
		private struct PropertyModification
		{
			public string Key;

			public string Value;

			public bool IsRelative;

			public int Delta;
		}

		private string _entityKey;

		private List<PropertyModification> _modifications;

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

		public bool ConditionSelected()
		{
			return false;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private IGenesisEntity TryResolveSlotPathTarget()
		{
			return null;
		}

		private void ApplyModificationsToNpc(SimEntity entity)
		{
		}

		private void ApplyModificationsToDataEntity(GenesisDataEntity dataEntity)
		{
		}

		private string ResolveModValue(string value)
		{
			return null;
		}

		private void ParseModifications(string propsString)
		{
		}
	}
}
