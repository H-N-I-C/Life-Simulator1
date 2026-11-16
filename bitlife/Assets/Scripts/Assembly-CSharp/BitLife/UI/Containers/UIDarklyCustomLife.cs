using System.Collections.Generic;
using Backend;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyCustomLife : UIDarkly
	{
		private sealed class _003C_003Ec__DisplayClass38_0
		{
			public CandyButton button;

			internal void _003CSetupSpecialButtons_003Eb__1(List<string> result)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass38_1
		{
			public List<StuffItem> specialLives;

			public List<StuffItem> useTokens;

			internal void _003CSetupSpecialButtons_003Eb__2(StuffItem i)
			{
			}
		}

		private CandyImage titleEmoji;

		private CandyText details;

		private CandyButton specialLivesButton;

		private CandyButton useTokensButton;

		private UIDarklyButton startTokenLifeButton;

		private GameObject specialLivesButtonSeparator;

		private GameObject useTokensButtonSeparator;

		private CandyButton startLifeButton;

		private CandyButton customLifeButton;

		private CandyImage separator;

		private CandyScroller scroller;

		private CandyButton nameButton;

		private CandyButton genderButton;

		private CandyButton countryButton;

		private CandyButton cityButton;

		private CandyButton royalStatusButton;

		private CandyButton specialTalentButton;

		private CandyButton appearanceButton;

		private CandyButton attributesButton;

		private CandyButton piggyBankButton;

		private Transform space;

		private bool lifeModified;

		private int chosenSubGroup;

		private bool nameEdited;

		private GodModePersonConfig personConfig;

		private string city;

		private int gender;

		private string firstName;

		private string lastName;

		private new DataCountry country;

		private bool isInitialDarkly;

		private StuffItem tokenLifeItem;

		private string nameButtonEmojiString;

		private bool hasBeenInitiatied;

		private bool goToPaywall;

		public override bool ShouldTranslateUI => false;

		public override void UpdateDarklyButtons()
		{
		}

		private void SetupSpecialButtons()
		{
		}

		private void RefreshSetup()
		{
		}

		public void Setup(bool isInitialDarkly, StuffItem tokenLifeItem)
		{
		}

		public int UpdateSpecialTalentButton()
		{
			return 0;
		}

		private string EmojiStringForRoyalStatus()
		{
			return null;
		}

		public int UpdateRoyalStatusButton()
		{
			return 0;
		}

		public string GetNameButtonEmojiString()
		{
			return null;
		}

		public void RandomizeCustomLifeWithSkew(bool skew)
		{
		}

		public void CreateRandomCity()
		{
		}

		public void CreateRandomName()
		{
		}

		public void StartCustomLife()
		{
		}

		public void StartCustomLifeWithPiggyBank()
		{
		}

		public void Randomize()
		{
		}

		public void ChooseName()
		{
		}

		public void ChooseGender()
		{
		}

		public void ChooseCountry()
		{
		}

		public void ChooseCity()
		{
		}

		public void ChooseSpecialTalent()
		{
		}

		public void ChooseRoyalStatus()
		{
		}

		public void ChooseAppearance()
		{
		}

		public void ChooseAttributes()
		{
		}

		private void ChooseSpecialLife()
		{
		}

		private void ChooseUseTokens()
		{
		}

		public override int GetSubGroupType()
		{
			return 0;
		}

		public override DataCountry GetCountry()
		{
			return null;
		}

		public override string GetCity()
		{
			return null;
		}

		public override int GetGender()
		{
			return 0;
		}

		public override string GetFirstName()
		{
			return null;
		}

		public override string GetLastName()
		{
			return null;
		}

		public override GodModePersonConfig GetPersonConfig()
		{
			return null;
		}

		public void UpdateWithRandomize()
		{
		}

		public void UpdateWithNames(string firstName, string lastName)
		{
		}

		public void UpdateWithGender(int gender)
		{
		}

		public void UpdateWithCountryName(string countryName)
		{
		}

		public void UpdateWithCity(string city)
		{
		}

		public void UpdateWithRoyalRank(int royalRank)
		{
		}

		public void UpdateWithSpecialTalent(int specialTalent)
		{
		}

		public void UpdateWithPersonalConfig(GodModePersonConfig personConfig)
		{
		}

		private void _003CSetupSpecialButtons_003Eb__38_0(List<StuffItem> items)
		{
		}

		private void _003CSetup_003Eb__40_0(List<string> result)
		{
		}
	}
}
