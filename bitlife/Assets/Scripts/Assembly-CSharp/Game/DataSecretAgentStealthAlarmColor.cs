using System.Collections.Generic;

namespace Game
{
	public class DataSecretAgentStealthAlarmColor
	{
		private List<string> DoorColor__BackingField;

		private List<string> CorrectWireColor__BackingField;

		private List<string> IncorrectWireColors__BackingField;

		public List<string> DoorColor
		{
			get
			{
				return DoorColor__BackingField;
			}
			set
			{
				DoorColor__BackingField = value;
			}
		}

		public List<string> CorrectWireColor
		{
			get
			{
				return CorrectWireColor__BackingField;
			}
			set
			{
				CorrectWireColor__BackingField = value;
			}
		}

		public List<string> IncorrectWireColors
		{
			get
			{
				return IncorrectWireColors__BackingField;
			}
			set
			{
				IncorrectWireColors__BackingField = value;
			}
		}
	}
}
