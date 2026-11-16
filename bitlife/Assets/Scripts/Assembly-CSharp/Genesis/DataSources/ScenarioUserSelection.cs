using Genesis.Interfaces;

namespace Genesis.DataSources
{
	public class ScenarioUserSelection : IScenarioUserSelection
	{
		private int originalIndex__BackingField;

		private int choiceIndex__BackingField;

		private ScenarioChoice SelectedChoice__BackingField;

		private string[] InputValues__BackingField;

		private string[] DropdownValues__BackingField;

		private float[] SliderValues__BackingField;

		public int originalIndex
		{
			get
			{
				return originalIndex__BackingField;
			}
			set
			{
				originalIndex__BackingField = value;
			}
		}

		public int choiceIndex
		{
			get
			{
				return choiceIndex__BackingField;
			}
			set
			{
				choiceIndex__BackingField = value;
			}
		}

		public ScenarioChoice SelectedChoice
		{
			get
			{
				return SelectedChoice__BackingField;
			}
			set
			{
				SelectedChoice__BackingField = value;
			}
		}

		public string[] InputValues
		{
			get
			{
				return InputValues__BackingField;
			}
			set
			{
				InputValues__BackingField = value;
			}
		}

		public string[] DropdownValues
		{
			get
			{
				return DropdownValues__BackingField;
			}
			set
			{
				DropdownValues__BackingField = value;
			}
		}

		public float[] SliderValues
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
