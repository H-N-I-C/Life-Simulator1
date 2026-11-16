using System.Collections.Generic;

namespace Backend.Authentication
{
	public class SeasonsSettingsWelcome
	{
		private string Title__BackingField;

		private string Body__BackingField;

		private string ExternalTitle__BackingField;

		private List<string> TableStrings__BackingField;

		private string Emoji__BackingField;

		public string Title
		{
			get
			{
				return Title__BackingField;
			}
			set
			{
				Title__BackingField = value;
			}
		}

		public string Body
		{
			get
			{
				return Body__BackingField;
			}
			set
			{
				Body__BackingField = value;
			}
		}

		public string ExternalTitle
		{
			get
			{
				return ExternalTitle__BackingField;
			}
			set
			{
				ExternalTitle__BackingField = value;
			}
		}

		public List<string> TableStrings
		{
			get
			{
				return TableStrings__BackingField;
			}
			set
			{
				TableStrings__BackingField = value;
			}
		}

		public string Emoji
		{
			get
			{
				return Emoji__BackingField;
			}
			set
			{
				Emoji__BackingField = value;
			}
		}
	}
}
