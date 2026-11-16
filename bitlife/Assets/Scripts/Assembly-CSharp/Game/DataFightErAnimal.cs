using System.Collections.Generic;

namespace Game
{
	public class DataFightErAnimal
	{
		private string Animal__BackingField;

		private int StreetCredsGained__BackingField;

		private List<string> AttackVerb__BackingField;

		private int DangerScore__BackingField;

		private int CommonnessScore__BackingField;

		public string Animal
		{
			get
			{
				return Animal__BackingField;
			}
			set
			{
				Animal__BackingField = value;
			}
		}

		public int StreetCredsGained
		{
			get
			{
				return StreetCredsGained__BackingField;
			}
			set
			{
				StreetCredsGained__BackingField = value;
			}
		}

		public List<string> AttackVerb
		{
			get
			{
				return AttackVerb__BackingField;
			}
			set
			{
				AttackVerb__BackingField = value;
			}
		}

		public int DangerScore
		{
			get
			{
				return DangerScore__BackingField;
			}
			set
			{
				DangerScore__BackingField = value;
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
	}
}
