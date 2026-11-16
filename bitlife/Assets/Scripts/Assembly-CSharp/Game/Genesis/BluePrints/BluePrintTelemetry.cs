using System.Collections.Generic;

namespace Game.Genesis.BluePrints
{
	public sealed class BluePrintTelemetry : IBluePrint, ISuppressFollowUp
	{
		private const int MaxKeyLength = 40;

		private const int MaxValueLength = 100;

		private BluePrintData _data;

		private string _eventName;

		private readonly List<KeyValuePair<string, string>> _rawParams;

		private bool _valid;

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

		public Dictionary<string, string> BuildResolvedPayload()
		{
			return null;
		}

		public Scenario GetNextScenario(string followupScenario)
		{
			return null;
		}

		private void Warn(string reason)
		{
		}

		private static bool IsTagged(string value)
		{
			return false;
		}

		private static bool MatchesDeclaredType(string value, string type)
		{
			return false;
		}
	}
}
