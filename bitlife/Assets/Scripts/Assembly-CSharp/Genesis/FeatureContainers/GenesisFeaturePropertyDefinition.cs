namespace Genesis.FeatureContainers
{
	public sealed class GenesisFeaturePropertyDefinition
	{
		private readonly string Key__BackingField;

		private readonly string Type__BackingField;

		private readonly string RawDefaultValue__BackingField;

		private readonly bool ResetOnAgeUp__BackingField;

		private readonly int? IntMinimum__BackingField;

		private readonly int? IntMaximum__BackingField;

		private readonly float? FloatMinimum__BackingField;

		private readonly float? FloatMaximum__BackingField;

		public string Key => Key__BackingField;

		public string Type => Type__BackingField;

		public string RawDefaultValue => RawDefaultValue__BackingField;

		public bool ResetOnAgeUp => ResetOnAgeUp__BackingField;

		public int? IntMinimum => IntMinimum__BackingField;

		public int? IntMaximum => IntMaximum__BackingField;

		public float? FloatMinimum => FloatMinimum__BackingField;

		public float? FloatMaximum => FloatMaximum__BackingField;

		public bool IsInt => false;

		public bool IsFloat => false;

		public bool HasNumericBounds => false;

		internal GenesisFeaturePropertyDefinition(string key, string type, string rawDefaultValue, bool resetOnAgeUp, int? intMinimum, int? intMaximum, float? floatMinimum, float? floatMaximum)
		{
		}
	}
}
