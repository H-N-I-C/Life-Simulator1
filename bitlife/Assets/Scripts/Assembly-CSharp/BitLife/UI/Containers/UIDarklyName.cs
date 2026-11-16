using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyName : UIDarkly
	{
		public static int NAME_CHAR_LIMIT;

		private CandyImage roundedBackground;

		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyInputField firstName;

		private CandyInputField lastName;

		private CandyButton saveButton;

		private string firstNameText;

		private string lastNameText;

		private int gender;

		private new DataCountry country;

		private GodModePersonConfig personConfig;

		public void Setup(string firstNameText, string lastNameText, int gender, DataCountry country, GodModePersonConfig personConfig)
		{
		}

		private void SaveButtonPress()
		{
		}

		private string SetFirstName(string firstNameText)
		{
			return null;
		}

		private string SetLastName(string lastNameText)
		{
			return null;
		}

		public override string GetFirstName()
		{
			return null;
		}

		public override string GetLastName()
		{
			return null;
		}

		public void UpdateSetup(string firstNameText, string lastNameText, int gender, DataCountry country, GodModePersonConfig personConfig)
		{
		}

		public void Randomize()
		{
		}
	}
}
