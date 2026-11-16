namespace Game.Seasons.Models
{
	public class SeasonNarrativeCompletionModel
	{
		private string NarrativeId__BackingField;

		private bool Completed__BackingField;

		private bool IsSuccess__BackingField;

		public string NarrativeId
		{
			get
			{
				return NarrativeId__BackingField;
			}
			set
			{
				NarrativeId__BackingField = value;
			}
		}

		public bool Completed
		{
			get
			{
				return Completed__BackingField;
			}
			set
			{
				Completed__BackingField = value;
			}
		}

		public bool IsSuccess
		{
			get
			{
				return IsSuccess__BackingField;
			}
			set
			{
				IsSuccess__BackingField = value;
			}
		}
	}
}
