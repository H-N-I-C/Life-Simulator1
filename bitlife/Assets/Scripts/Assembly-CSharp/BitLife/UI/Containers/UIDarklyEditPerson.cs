using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyEditPerson : UIDarkly
	{
		private CandyImage titleEmoji;

		private RectTransform titleContainer;

		private CandyText details;

		private CandyButton nameButton;

		private CandyButton appearanceButton;

		private CandyButton attributesButton;

		private CandyButton specialTalentButton;

		private GameObject specialTalentButtonSpacer;

		private CandyButton saveButton;

		private RectTransform saveButtonSpacer;

		private RectTransform stack;

		private GodModePersonConfig personConfig;

		private int gender;

		private string firstName;

		private string lastName;

		private int originalAccessoryHeadwear;

		private int originalAccessorySunglasses;

		private int originalAccessoryMasks;

		private int originalAccessoryForeheadTattoo;

		private int originalAccessoryLeftCheekTattoo;

		private int originalAccessoryRightCheekTattoo;

		private string originalVoltronData;

		public void Setup(Scenario scenario)
		{
		}

		public void Setup()
		{
		}

		public int UpdateSpecialTalentButton()
		{
			return 0;
		}

		private void RebuildNameButton()
		{
		}

		private void ChooseName()
		{
		}

		private void ChooseAppearance()
		{
		}

		private void ChooseAttributes()
		{
		}

		private void SaveButtonPress()
		{
		}

		public void BackButtonProcessing()
		{
		}

		public override string GetFirstName()
		{
			return null;
		}

		public override string GetLastName()
		{
			return null;
		}

		public override int GetGender()
		{
			return 0;
		}

		public override DataCountry GetCountry()
		{
			return null;
		}

		public override GodModePersonConfig GetPersonConfig()
		{
			return null;
		}

		public void UpdateWithPersonConfig(GodModePersonConfig personConfig)
		{
		}

		public void UpdateWithNames(string firstName, string lastName)
		{
		}

		public void UpdateWithSpecialTalent(int specialTalent)
		{
		}

		public void ChooseSpecialTalent()
		{
		}
	}
}
