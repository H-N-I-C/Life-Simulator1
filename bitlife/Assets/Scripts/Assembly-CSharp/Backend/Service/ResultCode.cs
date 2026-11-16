namespace Backend.Service
{
	public enum ResultCode
	{
		None = 0,
		ErrorData = 1,
		ErrorLife = 2,
		ErrorUserId = 3,
		Update = 4,
		FinishDownload = 5,
		ZipSuccess = 6,
		ZipFailed = 7,
		NoDataForFirstTimeDownload = 8
	}
}
