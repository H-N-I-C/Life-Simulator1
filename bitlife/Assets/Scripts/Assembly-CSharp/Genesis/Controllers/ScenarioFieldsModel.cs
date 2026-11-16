using System.Collections.Generic;

namespace Genesis.Controllers
{
	public class ScenarioFieldsModel
	{
		private List<string> InputLists__BackingField;

		private List<string> DropDownLists__BackingField;

		private List<float> SliderValues__BackingField;

		public List<string> InputLists
		{
			get
			{
				return InputLists__BackingField;
			}
			set
			{
				InputLists__BackingField = value;
			}
		}

		public List<string> DropDownLists
		{
			get
			{
				return DropDownLists__BackingField;
			}
			set
			{
				DropDownLists__BackingField = value;
			}
		}

		public List<float> SliderValues
		{
			get
			{
				return SliderValues__BackingField;
			}
			set
			{
				SliderValues__BackingField = value;
			}
		}
	}
}
