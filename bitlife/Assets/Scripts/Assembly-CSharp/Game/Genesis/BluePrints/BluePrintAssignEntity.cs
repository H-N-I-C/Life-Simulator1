using Genesis.Entities;

namespace Game.Genesis.BluePrints
{
	public class BluePrintAssignEntity : IBluePrint, ISuppressFollowUp
	{
		private string _containerId;

		private string _collectionName;

		private string _targetEntityId;

		private string _flagProperty;

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

		public string TargetEntityId => null;

		public string FlagProperty => null;

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

		private void WriteFlag(IGenesisEntity entity, string value)
		{
		}

		private string ResolveTargetId(string value)
		{
			return null;
		}
	}
}
