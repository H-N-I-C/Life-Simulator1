using System;
using System.Collections.Generic;

namespace Genesis.Lists.Models
{
	public sealed class GenesisListFallbackRoute : IEquatable<GenesisListFallbackRoute>
	{
		private sealed class _003C_003Ec__DisplayClass20_0
		{
			public string entityType;

			internal bool _003CMatchesEntityType_003Eb__0(string t)
			{
				return false;
			}
		}

		private GenesisListFallbackRouteType RouteType__BackingField;

		private string ListID__BackingField;

		private string ScenarioID__BackingField;

		private string ScenarioFile__BackingField;

		private List<string> EntityTypes__BackingField;

		public GenesisListFallbackRouteType RouteType
		{
			get
			{
				return RouteType__BackingField;
			}
			set
			{
				RouteType__BackingField = value;
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

		public List<string> EntityTypes
		{
			get
			{
				return EntityTypes__BackingField;
			}
			set
			{
				EntityTypes__BackingField = value;
			}
		}

		public bool MatchesEntityType(string entityType)
		{
			return false;
		}

		public static bool TryCreateFromLegacy(string legacyValue, out GenesisListFallbackRoute route, out string error)
		{
			route = null;
			error = null;
			return false;
		}

		public bool Equals(GenesisListFallbackRoute other)
		{
			return false;
		}

		public override bool Equals(object obj)
		{
			return false;
		}

		public override int GetHashCode()
		{
			return 0;
		}
	}
}
