using System;

[Serializable]
public class SimLoan
{
	private int YearsCompleted__BackingField;

	private int YearsLeft__BackingField;

	private int LoanType__BackingField;

	private LoanTerms Terms__BackingField;

	private bool Active__BackingField;

	private float DownPayment__BackingField;

	private float FinancedAmount__BackingField;

	private float Balance__BackingField;

	public int YearsCompleted
	{
		get
		{
			return YearsCompleted__BackingField;
		}
		set
		{
			YearsCompleted__BackingField = value;
		}
	}

	public int YearsLeft
	{
		get
		{
			return YearsLeft__BackingField;
		}
		set
		{
			YearsLeft__BackingField = value;
		}
	}

	public int LoanType
	{
		get
		{
			return LoanType__BackingField;
		}
		set
		{
			LoanType__BackingField = value;
		}
	}

	public LoanTerms Terms
	{
		get
		{
			return Terms__BackingField;
		}
		set
		{
			Terms__BackingField = value;
		}
	}

	public bool Active
	{
		get
		{
			return Active__BackingField;
		}
		set
		{
			Active__BackingField = value;
		}
	}

	public float DownPayment
	{
		get
		{
			return DownPayment__BackingField;
		}
		set
		{
			DownPayment__BackingField = value;
		}
	}

	public float FinancedAmount
	{
		get
		{
			return FinancedAmount__BackingField;
		}
		set
		{
			FinancedAmount__BackingField = value;
		}
	}

	public float Balance
	{
		get
		{
			return Balance__BackingField;
		}
		set
		{
			Balance__BackingField = value;
		}
	}

	private SimLoan()
	{
	}

	public SimLoan(int theType, float amount)
	{
	}

	public SimLoan(int years, float interest, float amount)
	{
	}

	public SimLoan(bool initHere)
	{
	}

	public float MakeAnnualPayment()
	{
		return 0f;
	}

	public float MakeMonthlyPayment()
	{
		return 0f;
	}

	public float StandardMonthlyPaymentAmount()
	{
		return 0f;
	}

	public static float GetEstimatedMonthlyPaymentForFullAmountWithLoanType(float amount, int loanType)
	{
		return 0f;
	}

	public static float GetMonthlyPaymentForFinancedAmountWithLoanTerms(float amount, LoanTerms terms)
	{
		return 0f;
	}

	public static float GetAnnualPaymentForFinancedAmountWithLoanTerms(float amount, LoanTerms terms)
	{
		return 0f;
	}

	public static LoanTerms TermsForLoanType(int loanType)
	{
		return default;
	}

	public static LoanTerms TermsForLoanForInterest(float interest, int years)
	{
		return default;
	}

	public float GetYearlyPayment()
	{
		return 0f;
	}

	public float GetTotalBalanceWithInterest()
	{
		return 0f;
	}

	public float GetTotalInterestAmount()
	{
		return 0f;
	}

	private float GetAnnualPaymentAmountForRemainingTerm()
	{
		return 0f;
	}

	private SimLoan CreateSimulationCopy()
	{
		return null;
	}
}
