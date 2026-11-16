using System.Collections.Generic;
using Genesis.Controllers;

namespace Genesis.Lists.Models
{
	public class GenesisListDataSourceItem
	{
		private string Title__BackingField;

		private string SecondLine__BackingField;

		private string Subtitle__BackingField;

		private string Icon__BackingField;

		private string IsEnabled__BackingField;

		private string IsVisible__BackingField;

		private GenesisListBarGraphDefinition BarGraph__BackingField;

		private List<ScenarioAction> ActionList__BackingField;

		private object FocusObject__BackingField;

		private string FocusMemoryKey__BackingField;

		private string IapGate__BackingField;

		private GenesisListIapNotMetBehavior IapNotMetBehavior__BackingField;

		private string PaywallIapOrigin__BackingField;

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
			}
		}

		public string SecondLine
		{
			get
			{
				return SecondLine__BackingField;
			}
			set
			{
				SecondLine__BackingField = value;
			}
		}

		public string Subtitle
		{
			get
			{
				return Subtitle__BackingField;
			}
			set
			{
				Subtitle__BackingField = value;
			}
		}

		public string Icon
		{
			get
			{
				return Icon__BackingField;
			}
			set
			{
				Icon__BackingField = value;
			}
		}

		public string IsEnabled
		{
			get
			{
				return IsEnabled__BackingField;
			}
			set
			{
				IsEnabled__BackingField = value;
			}
		}

		public string IsVisible
		{
			get
			{
				return IsVisible__BackingField;
			}
			set
			{
				IsVisible__BackingField = value;
			}
		}

		public GenesisListBarGraphDefinition BarGraph
		{
			get
			{
				return BarGraph__BackingField;
			}
			set
			{
				BarGraph__BackingField = value;
			}
		}

		public List<ScenarioAction> ActionList
		{
			get
			{
				return ActionList__BackingField;
			}
			set
			{
				ActionList__BackingField = value;
			}
		}

		public object FocusObject
		{
			get
			{
				return FocusObject__BackingField;
			}
			set
			{
				FocusObject__BackingField = value;
			}
		}

		public string FocusMemoryKey
		{
			get
			{
				return FocusMemoryKey__BackingField;
			}
			set
			{
				FocusMemoryKey__BackingField = value;
			}
		}

		public string IapGate
		{
			get
			{
				return IapGate__BackingField;
			}
			set
			{
				IapGate__BackingField = value;
			}
		}

		public GenesisListIapNotMetBehavior IapNotMetBehavior
		{
			get
			{
				return IapNotMetBehavior__BackingField;
			}
			set
			{
				IapNotMetBehavior__BackingField = value;
			}
		}

		public string PaywallIapOrigin
		{
			get
			{
				return PaywallIapOrigin__BackingField;
			}
			set
			{
				PaywallIapOrigin__BackingField = value;
			}
		}
	}
}
