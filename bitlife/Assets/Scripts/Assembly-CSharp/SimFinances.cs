using System;
using System.Collections.Generic;

[Serializable]
public class SimFinances
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<SimInvestment, double> _003C_003E9__26_0;

		internal double _003CAssets_003Eb__26_0(SimInvestment x)
		{
			return 0.0;
		}
	}

	private double BankBalance__BackingField;

	private float Pension__BackingField;

	private float OutstandingFundsEmbezzled__BackingField;

	private Dictionary<INHERITANCE_ASSET_TYPE, double> InheritanceDictionary__BackingField;

	private bool HasTrackedMillionaire__BackingField;

	private SimBankLoan BankLoanContract__BackingField;

	public double BankBalance
	{
		get
		{
			return BankBalance__BackingField;
		}
		set
		{
			BankBalance__BackingField = value;
		}
	}

	public float Pension
	{
		get
		{
			return Pension__BackingField;
		}
		set
		{
			Pension__BackingField = value;
		}
	}

	public float OutstandingFundsEmbezzled
	{
		get
		{
			return OutstandingFundsEmbezzled__BackingField;
		}
		set
		{
			OutstandingFundsEmbezzled__BackingField = value;
		}
	}

	public Dictionary<INHERITANCE_ASSET_TYPE, double> InheritanceDictionary
	{
		get
		{
			return InheritanceDictionary__BackingField;
		}
		set
		{
			InheritanceDictionary__BackingField = value;
		}
	}

	public bool HasTrackedMillionaire
	{
		get
		{
			return HasTrackedMillionaire__BackingField;
		}
		set
		{
			HasTrackedMillionaire__BackingField = value;
		}
	}

	public SimBankLoan BankLoanContract
	{
		get
		{
			return BankLoanContract__BackingField;
		}
		set
		{
			BankLoanContract__BackingField = value;
		}
	}

	public string GetBankBalanceString()
	{
		return null;
	}

	public double NetWorth()
	{
		return 0.0;
	}

	public double Assets()
	{
		return 0.0;
	}

	public double HardAssetsTotalValue()
	{
		return 0.0;
	}

	public double Liabilities()
	{
		return 0.0;
	}

	public void AdjustBankBalanceByAmount(double amount)
	{
	}

	public void ClearOutBankBalance()
	{
	}

	public double EffectiveBankBalance()
	{
		return 0.0;
	}

	public void EarnIncome(double income)
	{
	}

	public void EarnTaxFreeIncome(double income)
	{
	}

	public void InheritAmount(double inheritance)
	{
	}

	public double CurrentYearIncomeAndIncludeSpouse(bool includeSpouse)
	{
		return 0.0;
	}

	public float AnnualLivingExpenses()
	{
		return 0f;
	}

	public void IncurAnnualLivingExpenses()
	{
	}

	public float MonthlyOutflow()
	{
		return 0f;
	}

	public float MonthlyLoanPayments()
	{
		return 0f;
	}

	public float EffectiveTaxRate()
	{
		return 0f;
	}

	public void MergeFinancesAtMarriageWithNoPrenup()
	{
	}

	public void MergeFinancesAtMarriageWithPrenupRequestedByHero(bool requestedByHero)
	{
	}

	public bool HasAPension()
	{
		return false;
	}

	public bool HasLoanDebt()
	{
		return false;
	}

	public void CreateNewLoanDebt()
	{
	}

	public float GetLoanYearlyPayment()
	{
		return 0f;
	}

	public float GetLoanTotalBalance()
	{
		return 0f;
	}

	public string GetFormattedBalance()
	{
		return null;
	}

	public string GetCurrencySymbol()
	{
		return null;
	}

	public float GetMaxBalance(float capValue)
	{
		return 0f;
	}
}
