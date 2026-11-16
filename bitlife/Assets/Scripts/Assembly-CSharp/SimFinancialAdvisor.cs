using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimFinancialAdvisor : SimLover
{
	private int YearsOfExperience__BackingField;

	private float CommissionPercent__BackingField;

	private double AmountInvested__BackingField;

	private double CurrentPortfolioBalance__BackingField;

	private Dictionary<SimStock, double> StockInvestmentDict__BackingField;

	private Dictionary<SimBond, double> BondInvestmentDict__BackingField;

	private Dictionary<SimFund, double> FundInvestmentDict__BackingField;

	private ArrayList _FamousClientArray__BackingField;

	private float EarningsFromHero__BackingField;

	private int HeroAgeAtStartRepresenting__BackingField;

	private int HeroAgeAtStopRepresenting__BackingField;

	private int HeroAgeAtLastGaveAdvisorMoneyToInvest__BackingField;

	private int HeroAgeAtLastAdviceRefresh__BackingField;

	private float Att_reputation__BackingField;

	private float Att_competence__BackingField;

	public bool WillDropHeroThisYear;

	public int YearsOfExperience
	{
		get
		{
			return YearsOfExperience__BackingField;
		}
		set
		{
			YearsOfExperience__BackingField = value;
		}
	}

	public float CommissionPercent
	{
		get
		{
			return CommissionPercent__BackingField;
		}
		set
		{
			CommissionPercent__BackingField = value;
		}
	}

	public double AmountInvested
	{
		get
		{
			return AmountInvested__BackingField;
		}
		set
		{
			AmountInvested__BackingField = value;
		}
	}

	public double CurrentPortfolioBalance
	{
		get
		{
			return CurrentPortfolioBalance__BackingField;
		}
		set
		{
			CurrentPortfolioBalance__BackingField = value;
		}
	}

	public Dictionary<SimStock, double> StockInvestmentDict
	{
		get
		{
			return StockInvestmentDict__BackingField;
		}
		set
		{
			StockInvestmentDict__BackingField = value;
		}
	}

	public Dictionary<SimBond, double> BondInvestmentDict
	{
		get
		{
			return BondInvestmentDict__BackingField;
		}
		set
		{
			BondInvestmentDict__BackingField = value;
		}
	}

	public Dictionary<SimFund, double> FundInvestmentDict
	{
		get
		{
			return FundInvestmentDict__BackingField;
		}
		set
		{
			FundInvestmentDict__BackingField = value;
		}
	}

	private ArrayList _FamousClientArray
	{
		get
		{
			return _FamousClientArray__BackingField;
		}
		set
		{
			_FamousClientArray__BackingField = value;
		}
	}

	public ArrayList FamousClientArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float EarningsFromHero
	{
		get
		{
			return EarningsFromHero__BackingField;
		}
		set
		{
			EarningsFromHero__BackingField = value;
		}
	}

	public int HeroAgeAtStartRepresenting
	{
		get
		{
			return HeroAgeAtStartRepresenting__BackingField;
		}
		set
		{
			HeroAgeAtStartRepresenting__BackingField = value;
		}
	}

	public int HeroAgeAtStopRepresenting
	{
		get
		{
			return HeroAgeAtStopRepresenting__BackingField;
		}
		set
		{
			HeroAgeAtStopRepresenting__BackingField = value;
		}
	}

	public int HeroAgeAtLastGaveAdvisorMoneyToInvest
	{
		get
		{
			return HeroAgeAtLastGaveAdvisorMoneyToInvest__BackingField;
		}
		set
		{
			HeroAgeAtLastGaveAdvisorMoneyToInvest__BackingField = value;
		}
	}

	public int HeroAgeAtLastAdviceRefresh
	{
		get
		{
			return HeroAgeAtLastAdviceRefresh__BackingField;
		}
		set
		{
			HeroAgeAtLastAdviceRefresh__BackingField = value;
		}
	}

	public float Att_reputation
	{
		get
		{
			return Att_reputation__BackingField;
		}
		set
		{
			Att_reputation__BackingField = value;
		}
	}

	public float Att_competence
	{
		get
		{
			return Att_competence__BackingField;
		}
		set
		{
			Att_competence__BackingField = value;
		}
	}

	public SimFinancialAdvisor(bool initHere)
		: base(initHere: false)
	{
	}

	public override void IncrementAge()
	{
	}

	public override void Die()
	{
	}

	public void TerminateAdvisor()
	{
	}

	public void SetupForTier(int tier)
	{
	}

	public bool WillGiveHeroTimeOfDay()
	{
		return false;
	}

	public void ImpactReputation(int impactScore)
	{
	}
}
