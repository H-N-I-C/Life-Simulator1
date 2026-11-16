public class BankLoanAgeUpActivity : ActivityLazyCsvBase
{
	private readonly SimBankLoan _simBankLoan;

	public SimBankLoan SimBankLoan => null;

	public BankLoanAgeUpActivity(SimBankLoan simBankLoan, Life life)
		: base(null)
	{
	}

	private Scenario LoanRepaymentCompleted()
	{
		return null;
	}

	private void OnLoanCompletedScenario(ChoiceSelection selection)
	{
	}

	private Scenario StartLoanReport()
	{
		return null;
	}

	private Scenario StartLoanLawSuit()
	{
		return null;
	}

	public Scenario GetScenario(SimLoan bankLoan, int loanYearsLeft)
	{
		return null;
	}

	public double GetDebtorSettlement()
	{
		return 0.0;
	}
}
