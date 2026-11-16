using System.Collections.Generic;
using Game.Genesis.BluePrints;

namespace Genesis.Controllers
{
	public class ScenarioCondition
	{
		private BluePrintType BluePrintType__BackingField;

		private BluePrintData BluePrintData__BackingField;

		private List<ScenarioDefineModel> Define__BackingField;

		private List<ScenarioDefineModel> Variables__BackingField;

		private List<string> Methods__BackingField;

		private List<string> PostMethods__BackingField;

		private List<ScenarioConditionModel> Condition__BackingField;

		private string ScenarioID__BackingField;

		private string ScenarioFile__BackingField;

		private string ListID__BackingField;

		private ScenarioController Controller__BackingField;

		private string FollowUpScenario__BackingField;

		private Dictionary<string, BluePrintData> BluePrintPayload__BackingField;

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

		public BluePrintData BluePrintData
		{
			get
			{
				return BluePrintData__BackingField;
			}
			set
			{
				BluePrintData__BackingField = value;
			}
		}

		public List<ScenarioDefineModel> Define
		{
			get
			{
				return Define__BackingField;
			}
			set
			{
				Define__BackingField = value;
			}
		}

		public List<ScenarioDefineModel> Variables
		{
			get
			{
				return Variables__BackingField;
			}
			set
			{
				Variables__BackingField = value;
			}
		}

		public List<string> Methods
		{
			get
			{
				return Methods__BackingField;
			}
			set
			{
				Methods__BackingField = value;
			}
		}

		public List<string> PostMethods
		{
			get
			{
				return PostMethods__BackingField;
			}
			set
			{
				PostMethods__BackingField = value;
			}
		}

		public List<ScenarioConditionModel> Condition
		{
			get
			{
				return Condition__BackingField;
			}
			set
			{
				Condition__BackingField = value;
			}
		}

		public string ScenarioID
		{
			get
			{
				return ScenarioID__BackingField;
			}
			set
			{
				ScenarioID__BackingField = value;
			}
		}

		public string ScenarioFile
		{
			get
			{
				return ScenarioFile__BackingField;
			}
			set
			{
				ScenarioFile__BackingField = value;
			}
		}

		public string ListID
		{
			get
			{
				return ListID__BackingField;
			}
			set
			{
				ListID__BackingField = value;
			}
		}

		public ScenarioController Controller
		{
			get
			{
				return Controller__BackingField;
			}
			set
			{
				Controller__BackingField = value;
			}
		}

		public string FollowUpScenario
		{
			get
			{
				return FollowUpScenario__BackingField;
			}
			set
			{
				FollowUpScenario__BackingField = value;
			}
		}

		public Dictionary<string, BluePrintData> BluePrintPayload
		{
			get
			{
				return BluePrintPayload__BackingField;
			}
			set
			{
				BluePrintPayload__BackingField = value;
			}
		}

		public bool IsSelected(ScenarioFieldsModel data, out Scenario nextScenario, out IBluePrint executedBluePrint)
		{
			nextScenario = null;
			executedBluePrint = null;
			return false;
		}

		private void CreateRuntimeBluePrintData()
		{
		}

		private bool ProcessCondition()
		{
			return false;
		}

		public void SetDefines()
		{
		}

		public void SetVariables()
		{
		}

		public void SetMethods()
		{
		}
	}
}
