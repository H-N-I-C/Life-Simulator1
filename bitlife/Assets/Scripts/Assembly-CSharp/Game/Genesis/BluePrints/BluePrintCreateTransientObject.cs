using System.Collections.Generic;

namespace Game.Genesis.BluePrints
{
	public class BluePrintCreateTransientObject : IBluePrint
	{
		private string _entityType;

		private string _filterKey;

		private string _filterValue;

		private Dictionary<string, string> _properties;

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

		private string Resolve(string value)
		{
			return null;
		}

		private static bool ContainsUnresolved(string value)
		{
			return false;
		}
	}
}
