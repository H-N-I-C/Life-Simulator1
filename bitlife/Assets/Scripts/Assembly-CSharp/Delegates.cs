public static class Delegates
{
	public delegate string InputFieldEndEditDelegate(string input);

	public delegate string InputFieldEndEditDelegateIndexed(string input, int listIndex, ScenarioInputField scenarioInputField);

	public delegate string InputFieldSumbitEditDelegate(string input, string original);
}
