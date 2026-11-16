public class ScenarioBankLoanLawSuit : ScenarioLazyCsvBase
{
	private readonly BankLoanAgeUpActivity _activity;

	public ScenarioBankLoanLawSuit(BankLoanAgeUpActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
