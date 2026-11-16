using System.Collections.Generic;

namespace Genesis.Lists.Models
{
	public class GenesisListSection
	{
		private string Header__BackingField;

		private string IsVisible__BackingField;

		private List<GenesisListItemDefinition> Items__BackingField;

		private string DataSource__BackingField;

		private string EntityTypeFilter__BackingField;

		private List<GenesisListFallbackRoute> FallbackRoutes__BackingField;

		private string TitleProperty__BackingField;

		private string SubtitleProperty__BackingField;

		private string SecondLineProperty__BackingField;

		private string SecondLine__BackingField;

		private string IconProperty__BackingField;

		private GenesisListBarGraphDefinition BarGraph__BackingField;

		private string ButtonStyle__BackingField;

		private string ButtonStyleEmoji__BackingField;

		private string IapGate__BackingField;

		private GenesisListIapNotMetBehavior IapNotMetBehavior__BackingField;

		private string PaywallIapOrigin__BackingField;

		private int DrawCount__BackingField;

		private string RerollPolicy__BackingField;

		private string SortTilesByMetric__BackingField;

		private bool SortTilesIsAscending__BackingField;

		private string DrawFilter__BackingField;

		private bool AutoPopulate__BackingField;

		private string AutoPopulateEntityType__BackingField;

		private int PoolSize__BackingField;

		private string AssignedFlagProperty__BackingField;

		public string Header
		{
			get
			{
				return Header__BackingField;
			}
			set
			{
				Header__BackingField = value;
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

		public List<GenesisListItemDefinition> Items
		{
			get
			{
				return Items__BackingField;
			}
			set
			{
				Items__BackingField = value;
			}
		}

		public string DataSource
		{
			get
			{
				return DataSource__BackingField;
			}
			set
			{
				DataSource__BackingField = value;
			}
		}

		public string EntityTypeFilter
		{
			get
			{
				return EntityTypeFilter__BackingField;
			}
			set
			{
				EntityTypeFilter__BackingField = value;
			}
		}

		public List<GenesisListFallbackRoute> FallbackRoutes
		{
			get
			{
				return FallbackRoutes__BackingField;
			}
			set
			{
				FallbackRoutes__BackingField = value;
			}
		}

		public string TitleProperty
		{
			get
			{
				return TitleProperty__BackingField;
			}
			set
			{
				TitleProperty__BackingField = value;
			}
		}

		public string SubtitleProperty
		{
			get
			{
				return SubtitleProperty__BackingField;
			}
			set
			{
				SubtitleProperty__BackingField = value;
			}
		}

		public string SecondLineProperty
		{
			get
			{
				return SecondLineProperty__BackingField;
			}
			set
			{
				SecondLineProperty__BackingField = value;
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

		public string IconProperty
		{
			get
			{
				return IconProperty__BackingField;
			}
			set
			{
				IconProperty__BackingField = value;
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

		public int DrawCount
		{
			get
			{
				return DrawCount__BackingField;
			}
			set
			{
				DrawCount__BackingField = value;
			}
		}

		public int DrawCountAlias
		{
			set
			{
			}
		}

		public string RerollPolicy
		{
			get
			{
				return RerollPolicy__BackingField;
			}
			set
			{
				RerollPolicy__BackingField = value;
			}
		}

		public string RerollPolicyAlias
		{
			set
			{
			}
		}

		public string SortTilesByMetric
		{
			get
			{
				return SortTilesByMetric__BackingField;
			}
			set
			{
				SortTilesByMetric__BackingField = value;
			}
		}

		public bool SortTilesIsAscending
		{
			get
			{
				return SortTilesIsAscending__BackingField;
			}
			set
			{
				SortTilesIsAscending__BackingField = value;
			}
		}

		public string DrawFilter
		{
			get
			{
				return DrawFilter__BackingField;
			}
			set
			{
				DrawFilter__BackingField = value;
			}
		}

		public bool AutoPopulate
		{
			get
			{
				return AutoPopulate__BackingField;
			}
			set
			{
				AutoPopulate__BackingField = value;
			}
		}

		public string AutoPopulateEntityType
		{
			get
			{
				return AutoPopulateEntityType__BackingField;
			}
			set
			{
				AutoPopulateEntityType__BackingField = value;
			}
		}

		public int PoolSize
		{
			get
			{
				return PoolSize__BackingField;
			}
			set
			{
				PoolSize__BackingField = value;
			}
		}

		public string AssignedFlagProperty
		{
			get
			{
				return AssignedFlagProperty__BackingField;
			}
			set
			{
				AssignedFlagProperty__BackingField = value;
			}
		}
	}
}
