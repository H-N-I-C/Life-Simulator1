public interface IPromotableSocialMedia
{
	int AgeAtLastOfferedProductPromotion { get; set; }

	int NumProductsOfferedToPromoteAtCurrentAge { get; set; }

	int Channel { get; set; }

	DataSocialMediaChannel Data { get; set; }

	int Followers { get; set; }

	bool IsPostEligibleForProductPromotion();

	string GetChannelName();

	float GetUnadjustedCompensationForPromotion();

	int GetFollowers();

	bool IsFollowerEligibleForProductPromotion();

	void RecordEarnings(double earnings);

	SimSocialMediaTroll GetPostTroll();
}
