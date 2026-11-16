public class ScenarioBackendErrorCheckout : Scenario
{
	private readonly string _productId;

	private bool _canRestore;

	private string _emojiString;

	public ScenarioBackendErrorCheckout(string productId)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
