namespace Backend.Authentication
{
	public class AuthDataModel
	{
		public string UserId;

		public string JwtToken;

		public string RefreshToken;

		public string Method;

		public string Email;

		public string Guid;

		public static AuthDataModel Load()
		{
			return null;
		}

		public void Save()
		{
		}

		public static void Reset()
		{
		}
	}
}
