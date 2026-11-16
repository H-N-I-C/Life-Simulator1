using Genesis.Controllers;

namespace Game.Genesis.BluePrints
{
	public class BluePrintFriendRecruit : IBluePrint
	{
		private sealed class ScenarioFriendRecruitContinuation : ScenarioBase, IPurchaseFollowUpScenario
		{
			private readonly ScenarioController _originatingController;

			private readonly SimPerson _selectedPerson;

			private readonly bool _requireFriendshipBraceletAccess;

			private bool _attemptedRecruitment;

			private bool _recruitmentSucceeded;

			public bool CanPresentAfterPurchase => false;

			internal ScenarioFriendRecruitContinuation(ScenarioController controller, string scenarioId, SimPerson selectedPerson, bool requireFriendshipBraceletAccess)
				: base(null)
			{
			}

			public override void RefreshScenario()
			{
			}

			private bool TryRecruitSelectedPerson()
			{
				return false;
			}

			private bool HasRequiredState()
			{
				return false;
			}
		}

		private const string SeniorDramaMarker = "started_drama";

		private const string FriendshipBraceletMode = "friendship_bracelet";

		private int _intValue;

		private bool _fromSeniorDrama;

		private bool _configuredModeRequested;

		private bool _configuredPayloadIsValid;

		private bool _configuredRouteWasResolved;

		private bool _usesFriendshipBracelet;

		private float _relationshipThreshold;

		private string _successScenarioId;

		private string _failureScenarioId;

		private Scenario _configuredScenario;

		private readonly ScenarioController _controller;

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

		public BluePrintFriendRecruit(ScenarioController controller)
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

		private Scenario EnsureConfiguredRoute()
		{
			return null;
		}

		private static string NormalizeScenarioId(string value)
		{
			return null;
		}

		private static bool IsFullScenarioId(string value)
		{
			return false;
		}
	}
}
