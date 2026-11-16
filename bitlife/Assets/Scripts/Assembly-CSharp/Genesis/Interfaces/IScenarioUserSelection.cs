namespace Genesis.Interfaces
{
	public interface IScenarioUserSelection
	{
		int originalIndex { get; }

		int choiceIndex { get; }

		ScenarioChoice SelectedChoice { get; }

		string[] InputValues { get; }

		string[] DropdownValues { get; }

		float[] SliderValues { get; }
	}
}
