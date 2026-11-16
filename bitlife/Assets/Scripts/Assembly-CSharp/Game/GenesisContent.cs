using System.Collections.Generic;
using Game.Genesis.Components;
using Genesis.Controllers;

namespace Game
{
	public class GenesisContent
	{
		private string SheetName__BackingField;

		private string ID__BackingField;

		private int HeroMinAge__BackingField;

		private int HeroMaxAge__BackingField;

		private int HeroGender__BackingField;

		private string Npc__BackingField;

		private SimPerson NpcPerson__BackingField;

		private int NpcGender__BackingField;

		private int CommonnessScore__BackingField;

		private string Title__BackingField;

		private string Scenario__BackingField;

		private string Narrative__BackingField;

		private List<GenesisContentMinMax> MinMaxValues__BackingField;

		private string Filters__BackingField;

		private List<GenesisFilterModel> FilterModels__BackingField;

		private int Score__BackingField;

		private string Impacts__BackingField;

		private string Match__BackingField;

		public string SheetName
		{
			get
			{
				return SheetName__BackingField;
			}
			set
			{
				SheetName__BackingField = value;
			}
		}

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

		public int HeroMinAge
		{
			get
			{
				return HeroMinAge__BackingField;
			}
			set
			{
				HeroMinAge__BackingField = value;
			}
		}

		public int HeroMaxAge
		{
			get
			{
				return HeroMaxAge__BackingField;
			}
			set
			{
				HeroMaxAge__BackingField = value;
			}
		}

		public int HeroGender
		{
			get
			{
				return HeroGender__BackingField;
			}
			set
			{
				HeroGender__BackingField = value;
			}
		}

		public string Npc
		{
			get
			{
				return Npc__BackingField;
			}
			set
			{
				Npc__BackingField = value;
			}
		}

		public SimPerson NpcPerson
		{
			get
			{
				return NpcPerson__BackingField;
			}
			set
			{
				NpcPerson__BackingField = value;
			}
		}

		public int NpcGender
		{
			get
			{
				return NpcGender__BackingField;
			}
			set
			{
				NpcGender__BackingField = value;
			}
		}

		public int CommonnessScore
		{
			get
			{
				return CommonnessScore__BackingField;
			}
			set
			{
				CommonnessScore__BackingField = value;
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

		public string Scenario
		{
			get
			{
				return Scenario__BackingField;
			}
			set
			{
				Scenario__BackingField = value;
			}
		}

		public string Narrative
		{
			get
			{
				return Narrative__BackingField;
			}
			set
			{
				Narrative__BackingField = value;
			}
		}

		public List<GenesisContentMinMax> MinMaxValues
		{
			get
			{
				return MinMaxValues__BackingField;
			}
			set
			{
				MinMaxValues__BackingField = value;
			}
		}

		public string Filters
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

		public List<GenesisFilterModel> FilterModels
		{
			get
			{
				return FilterModels__BackingField;
			}
			set
			{
				FilterModels__BackingField = value;
			}
		}

		public int Score
		{
			get
			{
				return Score__BackingField;
			}
			set
			{
				Score__BackingField = value;
			}
		}

		public string Impacts
		{
			get
			{
				return Impacts__BackingField;
			}
			set
			{
				Impacts__BackingField = value;
			}
		}

		public string Match
		{
			get
			{
				return Match__BackingField;
			}
			set
			{
				Match__BackingField = value;
			}
		}

		public bool IsValid(ScenarioController controller, string toMatch)
		{
			return false;
		}

		public override string ToString()
		{
			return null;
		}
	}
}
