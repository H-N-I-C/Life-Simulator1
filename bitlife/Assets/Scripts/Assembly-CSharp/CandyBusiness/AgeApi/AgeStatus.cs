namespace CandyBusiness.AgeApi
{
	public enum AgeStatus
	{
		NullResponse = -2,
		Unknown = -1,
		Verified = 0,
		Supervised = 1,
		ApprovalPending = 2,
		ApprovalDenied = 3,
		Declared = 4
	}
}
