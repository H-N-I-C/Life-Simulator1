using System.Collections.Generic;

namespace Game
{
	public class DataOutdoorCaveDiscoverie
	{
		private int HeroMinAge__BackingField;

		private int HeroMaxAge__BackingField;

		private string Title__BackingField;

		private string Scenario__BackingField;

		private string CTA__BackingField;

		private List<DataOutdoorCaveChoice> Choices__BackingField;

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

		public string CTA
		{
			get
			{
				return CTA__BackingField;
			}
			set
			{
				CTA__BackingField = value;
			}
		}

		public List<DataOutdoorCaveChoice> Choices
		{
			get
			{
				return Choices__BackingField;
			}
			set
			{
				Choices__BackingField = value;
			}
		}
	}
}
