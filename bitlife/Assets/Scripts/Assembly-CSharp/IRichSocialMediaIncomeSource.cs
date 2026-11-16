public interface IRichSocialMediaIncomeSource : IRichSocialMedia
{
	double EarningsThisYear { get; }

	double EarningsLastYear { get; }

	double TotalEarnings { get; }

	int AgeAtLastEarnings { get; }

	double GetTotalEarnings();
}
