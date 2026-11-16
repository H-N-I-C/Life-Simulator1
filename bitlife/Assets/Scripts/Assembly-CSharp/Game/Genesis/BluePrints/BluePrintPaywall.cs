using System;
using System.Collections.Generic;
using Genesis.FeatureContainers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintPaywall : IBluePrint
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Predicate<BluePrintDataKeyValue> _003C_003E9__6_0;

			internal bool _003CSetup_003Eb__6_0(BluePrintDataKeyValue item)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass13_0
		{
			public Life life;

			public string containerId;

			public GenesisFeatureContainer container;

			public string propertyName;

			internal void _003CTryPrepareExposureMarker_003Eb__0()
			{
			}
		}

		private int _intValue;

		private string _stringValue;

		private int _paywallCategory;

		private int _darklyType;

		private string _shownProperty;

		private List<BluePrintDataKeyValue> _objectData;

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

		private bool TryPrepareExposureMarker(out Action onShown)
		{
			onShown = null;
			return false;
		}
	}
}
