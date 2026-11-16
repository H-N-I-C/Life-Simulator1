using System.Collections.Generic;
using Genesis.Controllers;

namespace Genesis.Lists.Models
{
	public class GenesisListItemDefinition
	{
		private string TileID__BackingField;

		private string Title__BackingField;

		private string SecondLine__BackingField;

		private string Subtitle__BackingField;

		private string Icon__BackingField;

		private string IsEnabled__BackingField;

		private string IsVisible__BackingField;

		private string FocusMemoryKey__BackingField;

		private string EntityObjectDataFocusTag__BackingField;

		private string FocusEntityReference__BackingField;

		private GenesisListBarGraphDefinition BarGraph__BackingField;

		private string ButtonStyle__BackingField;

		private string ButtonStyleEmoji__BackingField;

		private string IapGate__BackingField;

		private GenesisListIapNotMetBehavior IapNotMetBehavior__BackingField;

		private string PaywallIapOrigin__BackingField;

		private List<ScenarioAction> ActionList__BackingField;

		private bool IsSeparator__BackingField;

		private GenesisListSpecialItemType SpecialItem__BackingField;

		private string ConfirmationString__BackingField;

		public string TileID
		{
			get
			{
				return TileID__BackingField;
			}
			set
			{
				TileID__BackingField = value;
			}
		}

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

		public string EntityObjectDataFocusTag
		{
			get
			{
				return EntityObjectDataFocusTag__BackingField;
			}
			set
			{
				EntityObjectDataFocusTag__BackingField = value;
			}
		}

		public string FocusEntityReference
		{
			get
			{
				return FocusEntityReference__BackingField;
			}
			set
			{
				FocusEntityReference__BackingField = value;
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

		public string ButtonStyle
		{
			get
			{
				return ButtonStyle__BackingField;
			}
			set
			{
				ButtonStyle__BackingField = value;
			}
		}

		public string TileButtonImageOverride
		{
			set
			{
			}
		}

		public string OverrideTileButtonStyle
		{
			set
			{
			}
		}

		public string ButtonStyleEmoji
		{
			get
			{
				return ButtonStyleEmoji__BackingField;
			}
			set
			{
				ButtonStyleEmoji__BackingField = value;
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

		public string TileFilter
		{
			set
			{
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

		public GenesisListIapNotMetBehavior TileFilterExperienceIfNotMet
		{
			set
			{
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

		public bool IsSeparator
		{
			get
			{
				return IsSeparator__BackingField;
			}
			set
			{
				IsSeparator__BackingField = value;
			}
		}

		public GenesisListSpecialItemType SpecialItem
		{
			get
			{
				return SpecialItem__BackingField;
			}
			set
			{
				SpecialItem__BackingField = value;
			}
		}

		public string ConfirmationString
		{
			get
			{
				return ConfirmationString__BackingField;
			}
			set
			{
				ConfirmationString__BackingField = value;
			}
		}

		public string TileConfirmationString
		{
			set
			{
			}
		}
	}
}
