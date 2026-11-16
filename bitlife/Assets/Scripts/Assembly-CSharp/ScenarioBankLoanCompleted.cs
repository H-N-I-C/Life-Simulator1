public class ScenarioBankLoanCompleted : ScenarioLazyCsvBase
{
	private readonly BankLoanAgeUpActivity _activity;

	public ScenarioBankLoanCompleted(BankLoanAgeUpActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
