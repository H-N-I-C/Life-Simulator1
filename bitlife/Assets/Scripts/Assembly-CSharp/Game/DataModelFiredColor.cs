using System.Collections.Generic;

namespace Game
{
	public class DataModelFiredColor
	{
		private string String__BackingField;

		private List<MODEL_TERMINATION_TYPE> TerminationType__BackingField;

		public string String
		{
			get
			{
				return String__BackingField;
			}
			set
			{
				String__BackingField = value;
			}
		}

		public List<MODEL_TERMINATION_TYPE> TerminationType
		{
			get
			{
				return TerminationType__BackingField;
			}
			set
			{
				TerminationType__BackingField = value;
			}
		}
	}
}
