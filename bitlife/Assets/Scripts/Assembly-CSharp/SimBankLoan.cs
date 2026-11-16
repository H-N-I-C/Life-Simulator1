using System;

[Serializable]
public class SimBankLoan
{
	private SimLoan BankLoan__BackingField;

	private string BankName__BackingField;

	private string BankMatch__BackingField;

	private int LoanYearsLeft__BackingField;

	[NonSerialized]
	private BankLoanAgeUpActivity _bankLoanActivity;

	public SimLoan BankLoan
	{
		get
		{
			return BankLoan__BackingField;
		}
		set
		{
			BankLoan__BackingField = value;
		}
	}

	public string BankName
	{
		get
		{
			return BankName__BackingField;
		}
		set
		{
			BankName__BackingField = value;
		}
	}

	public string BankMatch
	{
		get
		{
			return BankMatch__BackingField;
		}
		set
		{
			BankMatch__BackingField = value;
		}
	}

	public int LoanYearsLeft
	{
		get
		{
			return LoanYearsLeft__BackingField;
		}
		set
		{
			LoanYearsLeft__BackingField = value;
		}
	}

	public void Repay()
	{
	}

	public void SettleDebit()
	{
	}

	public void CloseLoan()
	{
	}

	public float GetTotalPendingBalance()
	{
		return 0f;
	}

	public float GetInterestSaved()
	{
		return 0f;
	}

	public void IncrementAge()
	{
	}

	public static float SimulateYearlyPayment()
	{
		return 0f;
	}

	public static float SimulateBalancePayment()
	{
		return 0f;
	}

	public static float GetSelectedLoanAmountFromMemory()
	{
		return 0f;
	}

	private static SimLoan CreatePreviewLoan()
	{
		return null;
	}

	public float GetDebtorSettlement()
	{
		return 0f;
	}

	public void ExtendLoan()
	{
	}
}
