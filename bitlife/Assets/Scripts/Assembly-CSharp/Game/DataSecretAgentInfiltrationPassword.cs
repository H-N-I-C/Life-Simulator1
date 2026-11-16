using System.Collections.Generic;

namespace Game
{
	public class DataSecretAgentInfiltrationPassword
	{
		private string Password__BackingField;

		private List<string> Hints__BackingField;

		private List<string> IncorrectChoices__BackingField;

		private int CommonnessScore__BackingField;

		public string Password
		{
			get
			{
				return Password__BackingField;
			}
			set
			{
				Password__BackingField = value;
			}
		}

		public List<string> Hints
		{
			get
			{
				return Hints__BackingField;
			}
			set
			{
				Hints__BackingField = value;
			}
		}

		public List<string> IncorrectChoices
		{
			get
			{
				return IncorrectChoices__BackingField;
			}
			set
			{
				IncorrectChoices__BackingField = value;
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
