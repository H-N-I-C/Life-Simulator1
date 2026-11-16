using System;
using System.Collections.Generic;

namespace Genesis.Telemetry
{
	public sealed class GenesisTelemetryRegistryEntry
	{
		private readonly string EventName__BackingField;

		private readonly HashSet<string> PropertyNames__BackingField;

		private readonly Dictionary<string, string> PropertyTypes__BackingField;

		private readonly string Cooldown__BackingField;

		private readonly DateTime? ActiveFrom__BackingField;

		private readonly DateTime? ActiveUntil__BackingField;

		private readonly string Owner__BackingField;

		private readonly GenesisTelemetryStatus Status__BackingField;

		public string EventName => EventName__BackingField;

		public HashSet<string> PropertyNames => PropertyNames__BackingField;

		public Dictionary<string, string> PropertyTypes => PropertyTypes__BackingField;

		public string Cooldown => Cooldown__BackingField;

		public DateTime? ActiveFrom => ActiveFrom__BackingField;

		public DateTime? ActiveUntil => ActiveUntil__BackingField;

		public string Owner => Owner__BackingField;

		public GenesisTelemetryStatus Status => Status__BackingField;

		public GenesisTelemetryRegistryEntry(string eventName, HashSet<string> propertyNames, string cooldown, DateTime? activeFrom, DateTime? activeUntil, string owner, GenesisTelemetryStatus status, Dictionary<string, string> propertyTypes = null)
		{
		}

		public string GetPropertyType(string propertyName)
		{
			return null;
		}
	}
}
