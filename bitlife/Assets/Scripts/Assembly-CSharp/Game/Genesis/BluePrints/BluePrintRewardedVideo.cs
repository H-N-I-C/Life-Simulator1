using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintRewardedVideo : IBluePrint, IDeferredRouting
	{
		private string _origin;

		private readonly ScenarioController _controller;

		private string _targetScenarioID;

		private bool _isConfigured;

		private BluePrintType BluePrintType__BackingField;

		private bool DidDeferRouting__BackingField;

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

		public bool DidDeferRouting
		{
			get
			{
				return DidDeferRouting__BackingField;
			}
			private set
			{
				DidDeferRouting__BackingField = value;
			}
		}

		public BluePrintRewardedVideo(ScenarioController controller, string targetScenarioID)
		{
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

		private static string Normalize(string value)
		{
			return null;
		}

		private static bool IsExecutableBlueprintId(string value)
		{
			return false;
		}

		private static bool IsFullScenarioId(string value)
		{
			return false;
		}
	}
}
