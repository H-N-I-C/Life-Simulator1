using System;
using System.Collections.Generic;

namespace Genesis.Controllers
{
	public class ScenarioCSVData
	{
		private string[] Lines__BackingField;

		private Dictionary<string, string[]> IDToData__BackingField;

		private string EntryID__BackingField;

		private Action<string, Scenario> GiveItemAction__BackingField;

		private bool IsDesignerFormat__BackingField;

		public string[] Lines
		{
			get
			{
				return Lines__BackingField;
			}
			set
			{
				Lines__BackingField = value;
			}
		}

		public Dictionary<string, string[]> IDToData
		{
			get
			{
				return IDToData__BackingField;
			}
			set
			{
				IDToData__BackingField = value;
			}
		}

		public string EntryID
		{
			get
			{
				return EntryID__BackingField;
			}
			set
			{
				EntryID__BackingField = value;
			}
		}

		public Action<string, Scenario> GiveItemAction
		{
			get
			{
				return GiveItemAction__BackingField;
			}
			set
			{
				GiveItemAction__BackingField = value;
			}
		}

		public bool IsDesignerFormat
		{
			get
			{
				return IsDesignerFormat__BackingField;
			}
			set
			{
				IsDesignerFormat__BackingField = value;
			}
		}

		public ScenarioCSVData(string text, Action<string, Scenario> giveItemAction, ScenarioController controller)
		{
		}
	}
}
