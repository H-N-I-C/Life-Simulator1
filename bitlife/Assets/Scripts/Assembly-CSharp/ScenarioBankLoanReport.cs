public class ScenarioBankLoanReport : ScenarioLazyCsvBase
{
	private readonly BankLoanAgeUpActivity _activity;

	public ScenarioBankLoanReport(BankLoanAgeUpActivity activity, ChoiceCallback choiceCallback)
		: base(null)
	{
	}

	protected override void PrepareParser(BracketParser parser)
	{
	}
}
