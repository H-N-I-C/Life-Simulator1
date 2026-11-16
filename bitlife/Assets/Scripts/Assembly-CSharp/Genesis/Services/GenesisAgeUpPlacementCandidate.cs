using System.Collections.Generic;
using Game.Genesis.Components;

namespace Genesis.Services
{
	public sealed class GenesisAgeUpPlacementCandidate
	{
		private string ID__BackingField;

		private AgeUpPlacement Placement__BackingField;

		private List<GenesisFilterModel> Filters__BackingField;

		public string ID
		{
			get
			{
				return ID__BackingField;
			}
			set
			{
				ID__BackingField = value;
			}
		}

		public AgeUpPlacement Placement
		{
			get
			{
				return Placement__BackingField;
			}
			set
			{
				Placement__BackingField = value;
			}
		}

		public List<GenesisFilterModel> Filters
		{
			get
			{
				return Filters__BackingField;
			}
			set
			{
				Filters__BackingField = value;
			}
		}
	}
}
