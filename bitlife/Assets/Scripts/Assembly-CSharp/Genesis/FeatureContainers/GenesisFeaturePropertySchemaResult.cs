using System.Collections.Generic;

namespace Genesis.FeatureContainers
{
	public sealed class GenesisFeaturePropertySchemaResult
	{
		private static readonly IReadOnlyList<GenesisFeaturePropertyDefinition> EmptyDefinitions;

		private readonly GenesisFeaturePropertySchemaStatus Status__BackingField;

		private readonly IReadOnlyList<GenesisFeaturePropertyDefinition> Definitions__BackingField;

		private readonly string Diagnostic__BackingField;

		public GenesisFeaturePropertySchemaStatus Status => Status__BackingField;

		public IReadOnlyList<GenesisFeaturePropertyDefinition> Definitions => Definitions__BackingField;

		public string Diagnostic => Diagnostic__BackingField;

		public bool IsReady => false;

		internal GenesisFeaturePropertySchemaResult(GenesisFeaturePropertySchemaStatus status, IReadOnlyList<GenesisFeaturePropertyDefinition> definitions, string diagnostic)
		{
		}
	}
}
