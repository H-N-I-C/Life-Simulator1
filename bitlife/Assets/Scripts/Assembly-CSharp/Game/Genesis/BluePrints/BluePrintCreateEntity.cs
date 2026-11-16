using System.Collections.Generic;

namespace Game.Genesis.BluePrints
{
	public class BluePrintCreateEntity : IBluePrint
	{
		private string _kind;

		private string _entityType;

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

		private bool TryPrepareNpcCreation(out int? overrideAge, out int? overrideGender, out Dictionary<string, string> customProperties)
		{
			overrideAge = null;
			overrideGender = null;
			customProperties = null;
			return false;
		}

		private void CreateNpc(int? overrideAge, int? overrideGender, Dictionary<string, string> customProperties)
		{
		}

		private void CreateObjectEntity(Dictionary<string, string> resolvedProperties)
		{
		}

		private bool TryResolveProperties(out Dictionary<string, string> resolvedProperties)
		{
			resolvedProperties = null;
			return false;
		}

		private string ResolvePropertyValue(string value)
		{
			return null;
		}

		private static bool TryParseGenderOverride(string value, out int? overrideGender)
		{
			overrideGender = null;
			return false;
		}

		private static bool ContainsResolutionSyntax(string value)
		{
			return false;
		}

		private void ParseProperties(string propsString)
		{
		}
	}
}
