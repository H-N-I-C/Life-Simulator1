using System.Collections.Generic;

namespace Game
{
	public class DataClubUpdateUFLDumbScenario : IDumbScenarioData
	{
		private string Index__BackingField;

		private bool RandomizeChoices__BackingField;

		private string GenderBracket__BackingField;

		private string Title__BackingField;

		private string Body__BackingField;

		private string CTA__BackingField;

		private DataClubUpdateUFLDumbFilter Filter__BackingField;

		private List<DataClubUpdateUFLDumbChoice> _choices;

		public string Index
		{
			get
			{
				return Index__BackingField;
			}
			set
			{
				Index__BackingField = value;
			}
		}

		public bool RandomizeChoices
		{
			get
			{
				return RandomizeChoices__BackingField;
			}
			set
			{
				RandomizeChoices__BackingField = value;
			}
		}

		public string GenderBracket
		{
			get
			{
				return GenderBracket__BackingField;
			}
			set
			{
				GenderBracket__BackingField = value;
			}
		}

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

		public string CTA
		{
			get
			{
				return CTA__BackingField;
			}
			set
			{
				CTA__BackingField = value;
			}
		}

		public DataClubUpdateUFLDumbFilter Filter
		{
			get
			{
				return Filter__BackingField;
			}
			set
			{
				Filter__BackingField = value;
			}
		}

		public IReadOnlyList<IDumbChoice> Choices => null;

		public void SetChoices(List<DataClubUpdateUFLDumbChoice> choices)
		{
		}
	}
}
