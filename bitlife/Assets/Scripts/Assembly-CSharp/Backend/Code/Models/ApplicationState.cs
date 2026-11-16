namespace Backend.Code.Models
{
	public class ApplicationState
	{
		private static bool IsGameRunning__BackingField;

		public static bool IsGameRunning
		{
			get
			{
				return IsGameRunning__BackingField;
			}
			set
			{
				IsGameRunning__BackingField = value;
			}
		}
	}
}
