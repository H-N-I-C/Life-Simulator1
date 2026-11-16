using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Lists.Runtime
{
	public sealed class GenesisListItemRuntimeContext
	{
		private readonly ScenarioController Controller__BackingField;

		private readonly GenesisListDefinition ListDefinition__BackingField;

		private readonly GenesisListItemDefinition ItemDefinition__BackingField;

		private readonly object FocusObject__BackingField;

		private readonly string FocusMemoryKey__BackingField;

		public ScenarioController Controller => Controller__BackingField;

		public GenesisListDefinition ListDefinition => ListDefinition__BackingField;

		public GenesisListItemDefinition ItemDefinition => ItemDefinition__BackingField;

		public object FocusObject => FocusObject__BackingField;

		public string FocusMemoryKey => FocusMemoryKey__BackingField;

		public GenesisListItemRuntimeContext(ScenarioController controller, GenesisListDefinition listDefinition, GenesisListItemDefinition itemDefinition, object focusObject = null, string focusMemoryKey = null)
		{
		}
	}
}
