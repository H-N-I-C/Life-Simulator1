using System;
using System.Collections;
using System.Collections.Generic;
using Backend;

namespace Game
{
	public class UIIAPManager
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Action _003C_003E9__277_3;

			internal void _003CRestorePurchases_003Eb__277_3()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass277_0
		{
			public bool nativeDone;

			public bool webshopDone;

			public int nativeProductCount;

			public int webshopProductCount;

			internal void _003CRestorePurchases_003Eg__CompleteIfBothDone_007C0()
			{
			}

			internal void _003CRestorePurchases_003Eb__1(int count)
			{
			}

			internal void _003CRestorePurchases_003Eb__2(int productCount)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass285_0
		{
			public Scenario scenario;

			internal void _003CProcessPurchaseOperation_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass287_0
		{
			public Scenario feedbackScenario;

			internal void _003CSetupNextScenarioWithFollowUp_003Eb__0()
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass64_0
		{
			public Action onFinishedRedeem;

			internal void _003CStreakSaverPurchased_003Eb__0(MYSTUFF_REDEEM status)
			{
			}
		}

		private sealed class _003CDelayedFame_003Ed__171 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIIAPManager _003C_003E4__this;

			public ScenarioFameStatusChange fameScenario;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CDelayedFame_003Ed__171(int _003C_003E1__state)
			{
			}

			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			void IEnumerator.Reset()
			{
			}
		}

		private int IapOrigin__BackingField;

		private bool IsFeatured__BackingField;

		private string SpecialCareerPushPaywallShown__BackingField;

		private string NestedOrigin__BackingField;

		private int TimeMachineYearsToGoBack__BackingField;

		private bool TimeMachineIsInPrison__BackingField;

		private bool TimeMachineIsDead__BackingField;

		private TIME_MACHINE_ORIGIN Origin__BackingField;

		private Action callbackForUI;

		private Scenario _followUpScenario;

		private static UIIAPManager inst;

		private Action m_OnPurchaseActivityDone;

		private static Action m_OnUniversalPostPurchaseActivities;

		public int IapOrigin
		{
			get
			{
				return IapOrigin__BackingField;
			}
			set
			{
				IapOrigin__BackingField = value;
			}
		}

		public bool IsFeatured
		{
			get
			{
				return IsFeatured__BackingField;
			}
			set
			{
				IsFeatured__BackingField = value;
			}
		}

		public string SpecialCareerPushPaywallShown
		{
			get
			{
				return SpecialCareerPushPaywallShown__BackingField;
			}
			set
			{
				SpecialCareerPushPaywallShown__BackingField = value;
			}
		}

		public string NestedOrigin
		{
			get
			{
				return NestedOrigin__BackingField;
			}
			set
			{
				NestedOrigin__BackingField = value;
			}
		}

		public int TimeMachineYearsToGoBack
		{
			get
			{
				return TimeMachineYearsToGoBack__BackingField;
			}
			set
			{
				TimeMachineYearsToGoBack__BackingField = value;
			}
		}

		public bool TimeMachineIsInPrison
		{
			get
			{
				return TimeMachineIsInPrison__BackingField;
			}
			set
			{
				TimeMachineIsInPrison__BackingField = value;
			}
		}

		public bool TimeMachineIsDead
		{
			get
			{
				return TimeMachineIsDead__BackingField;
			}
			set
			{
				TimeMachineIsDead__BackingField = value;
			}
		}

		public TIME_MACHINE_ORIGIN Origin
		{
			get
			{
				return Origin__BackingField;
			}
			set
			{
				Origin__BackingField = value;
			}
		}

		public static UIIAPManager Inst => null;

		public event Action OnPurchaseActivityDone
		{
			add
			{
			}
			remove
			{
			}
		}

		public static event Action OnUniversalPostPurchaseActivities
		{
			add
			{
			}
			remove
			{
			}
		}

		public void SetIAPOrigin(int theIapOrigin)
		{
		}

		public string GetPriceStringForBitizenship()
		{
			return null;
		}

		public void PurchaseBitizenship(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BitizenshipPurchased(bool isOriginalPurchase)
		{
		}

		public void BitizenshipRestored()
		{
		}

		public void LegacyBitizenshipPurchased(bool isOriginalPurchase)
		{
		}

		public void LegacyBitizenshipRestored()
		{
		}

		public string GetPriceStringForGodMode()
		{
			return null;
		}

		public void PurchaseGodMode(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GodModePurchased(bool isOriginalPurchase)
		{
		}

		public void GodModeRestored()
		{
		}

		public string GetPriceStringForBitizenshipAndGodMode()
		{
			return null;
		}

		public void PurchaseBitizenshipAndGodMode(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BitizenshipAndGodModePurchased(bool isOriginalPurchase)
		{
		}

		public void BitizenshipAndGodModeRestored()
		{
		}

		public string GetPriceStringForRemoveAds()
		{
			return null;
		}

		public void PurchaseRemoveAds(int theIapOrigin)
		{
		}

		public void RemoveAdsPurchased(bool isOriginalPurchase)
		{
		}

		public void RemoveAdsRestored()
		{
		}

		public string GetPriceStringForTimeMachine()
		{
			return null;
		}

		public void PurchaseTimeMachine(int yearsToGoBack, bool isInPrison, bool isDead, TIME_MACHINE_ORIGIN origin)
		{
		}

		public void TimeMachinePurchased(bool isOriginalPurchase)
		{
		}

		public string GetPriceStringForStreakSaver()
		{
			return null;
		}

		public void PurchaseStreakSaver(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void StreakSaverPurchased(bool isOriginalPurchase, string transactionId, Action onFinishedRedeem)
		{
		}

		public string GetPriceStringForBattlePass()
		{
			return null;
		}

		public void PurchaseBattlePass(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BattlePassPurchased(bool isOriginalPurchase)
		{
		}

		public string GetPriceStringForBossMode()
		{
			return null;
		}

		public void PurchaseBossMode(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BossModePurchased(bool isOriginalPurchase)
		{
		}

		public void BossModeRestored()
		{
		}

		public string GetPriceStringForFighterMode()
		{
			return null;
		}

		public void PurchaseFighterMode(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void FighterModePurchased(bool isOriginalPurchase)
		{
		}

		public void FighterModeRestored()
		{
		}

		public string GetPriceStringForMovieDirector()
		{
			return null;
		}

		private Dictionary<string, string> GetRevenueEventValues(string bundle)
		{
			return null;
		}

		public void MovieDirectorPurchased(bool isOriginalPurchase)
		{
		}

		public void MovieDirectorRestored()
		{
		}

		public string GetPriceStringForBundleMovieDirectorHollywoodStar()
		{
			return null;
		}

		public void PurchaseBundleMovieDirectorHollywoodStar(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleMovieDirectorHollywoodStarPurchased(bool isOriginalPurchase)
		{
		}

		public void BundleMovieDirectorHollywoodStarRestored()
		{
		}

		public string GetPriceStringForCareerPolitician()
		{
			return null;
		}

		public void PurchaseCareerPolitician(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerPoliticianPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerPoliticianRestored()
		{
		}

		public string GetPriceStringForCareerAthlete()
		{
			return null;
		}

		public void PurchaseCareerAthlete(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerAthletePurchased(bool isOriginalPurchase)
		{
		}

		public void CareerAthleteRestored()
		{
		}

		public string GetPriceStringForCareerMusician()
		{
			return null;
		}

		public void PurchaseCareerMusician(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerMusicianPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerMusicianRestored()
		{
		}

		public string GetPriceStringForCareerMusicProducer()
		{
			return null;
		}

		public void PurchaseCareerMusicProducer(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerMusicProducerPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerMusicProducerRestored()
		{
		}

		public string GetPriceStringForCareerMafia()
		{
			return null;
		}

		public void PurchaseCareerMafia(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerMafiaPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerMafiaRestored()
		{
		}

		public string GetPriceStringForCareerActor()
		{
			return null;
		}

		public void PurchaseCareerActor(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerActorPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerActorRestored()
		{
		}

		public string GetPriceStringForCareerHustler()
		{
			return null;
		}

		public void PurchaseCareerHustler(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerHustlerPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerHustlerRestored()
		{
		}

		public string GetPriceStringForCareerBusiness()
		{
			return null;
		}

		public void PurchaseCareerBusiness(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerBusinessPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerBusinessRestored()
		{
		}

		public string GetPriceStringForCareerAstronaut()
		{
			return null;
		}

		public void PurchaseCareerAstronaut(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerAstronautPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerAstronautRestored()
		{
		}

		public string GetPriceStringForCareerModel()
		{
			return null;
		}

		public void PurchaseCareerModel(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerModelPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerModelRestored()
		{
		}

		public string GetPriceStringForChallengeVault()
		{
			return null;
		}

		public void PurchaseChallengeVault(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ChallengeVaultPurchased(bool isOriginalPurchase)
		{
		}

		public void ChallengeVaultRestored()
		{
		}

		public string GetPriceStringForUnlimitedTimeMachine()
		{
			return null;
		}

		public void PurchaseUnlimitedTimeMachine(int theIapOrigin, TIME_MACHINE_ORIGIN origin)
		{
		}

		public void UnlimitedTimeMachinePurchased(bool isOriginalPurchase)
		{
		}

		public void UnlimitedTimeMachineRestored()
		{
		}

		public string GetPriceStringForGoldTheme()
		{
			return null;
		}

		public void PurchaseGoldTheme(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldThemePurchased(bool isOriginalPurchase)
		{
		}

		public void GoldThemeRestored()
		{
		}

		public string GetPriceStringForGoldenPacifier()
		{
			return null;
		}

		public void PurchaseGoldenPacifier(int theIapOrigin)
		{
		}

		public void GoldenPacifierPurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenPacifierRestored()
		{
		}

		public string GetPriceStringForGoldenDiploma()
		{
			return null;
		}

		public void PurchaseGoldenDiploma(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenDiplomaPurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenDiplomaRestored()
		{
		}

		public string GetPriceStringForGoldenNekoStatue()
		{
			return null;
		}

		public void PurchaseGoldenNekoStatue(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenNekoStatuePurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenNekoStatueRestored()
		{
		}

		public string GetPriceStringForFriendshipBracelet()
		{
			return null;
		}

		public void PurchaseFriendshipBracelet(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void FriendshipBraceletPurchased(bool isOriginalPurchase)
		{
		}

		public void FriendshipBraceletRestored()
		{
		}

		public string GetPriceStringForAuraRing()
		{
			return null;
		}

		public void PurchaseAuraRing(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void AuraRingPurchased(bool isOriginalPurchase)
		{
		}

		public void AuraRingRestored()
		{
		}

		public string GetPriceStringForGoldenPassport()
		{
			return null;
		}

		public void PurchaseGoldenPassport(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenPassportPurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenPassportRestored()
		{
		}

		public string GetPriceStringForPromiscuityPotion()
		{
			return null;
		}

		public void PurchasePromiscuityPotion(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void PromiscuityPotionPurchased(bool isOriginalPurchase)
		{
		}

		public void PromiscuityPotionRestored()
		{
		}

		public string GetPriceStringForAssassinsBlade()
		{
			return null;
		}

		public void PurchaseAssassinsBlade(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void AssassinsBladePurchased(bool isOriginalPurchase)
		{
		}

		public void AssassinsBladeRestored()
		{
		}

		public string GetPriceStringForHollywoodStar()
		{
			return null;
		}

		public void PurchaseHollywoodStar(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void HollywoodStarPurchased(bool isOriginalPurchase)
		{
		}

		public IEnumerator DelayedFame(ScenarioFameStatusChange fameScenario)
		{
			return null;
		}

		public void HollywoodStarRestored()
		{
		}

		public string GetPriceStringForSpikedKnuckles()
		{
			return null;
		}

		public void PurchaseSpikedKnuckles(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void SpikedKnucklesPurchased(bool isOriginalPurchase)
		{
		}

		public void SpikedKnucklesRestored()
		{
		}

		public string GetPriceStringForFameAndFortuneBundle(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleFameAndFortune(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleFameAndFortunePurchased(bool isOriginalPurchase)
		{
		}

		public void BundleFameAndFortuneRestored()
		{
		}

		public string GetPriceStringForGoldenPiggyBank()
		{
			return null;
		}

		public void PurchaseGoldenPiggyBank(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenPiggyBankPurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenPiggyBankRestored()
		{
		}

		public string GetPriceStringForLuxuryAccess()
		{
			return null;
		}

		public void PurchaseLuxuryAccess(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void LuxuryAccessPurchased(bool isOriginalPurchase)
		{
		}

		public void LuxuryAccessRestored()
		{
		}

		public string GetPriceStringForOutdoorLifestyle()
		{
			return null;
		}

		public void PurchaseOutdoorLifestyle(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void OutdoorLifestylePurchased(bool isOriginalPurchase)
		{
		}

		public void OutdoorLifestyleRestored()
		{
		}

		public string GetPriceStringForGoldenWrench()
		{
			return null;
		}

		public void PurchaseGoldenWrench(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenWrenchPurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenWrenchRestored()
		{
		}

		public string GetPriceStringForGoldenResume()
		{
			return null;
		}

		public void PurchaseGoldenResume(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GoldenResumePurchased(bool isOriginalPurchase)
		{
		}

		public void GoldenResumeRestored()
		{
		}

		public string GetPriceStringForGetOutJailCard()
		{
			return null;
		}

		public void PurchaseGetOutJailCard(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void GetOutJailCardPurchased(bool isOriginalPurchase)
		{
		}

		public void GetOutJailCardRestored()
		{
		}

		public string GetPriceStringForExpansionPackInvestor()
		{
			return null;
		}

		public void PurchaseExpansionPackInvestor(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackInvestorPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackInvestorRestored()
		{
		}

		public string GetPriceStringForExpansionPackLandlord()
		{
			return null;
		}

		public void PurchaseExpansionPackLandlord(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackLandlordPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackLandlordRestored()
		{
		}

		public string GetPriceStringForExpansionPackBlackMarket()
		{
			return null;
		}

		public void PurchaseExpansionPackBlackMarket(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackBlackMarketPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackBlackMarketRestored()
		{
		}

		public string GetPriceStringForExpansionPackCult()
		{
			return null;
		}

		public void PurchaseExpansionPackCult(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackCultPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackCultRestored()
		{
		}

		public string GetPriceStringForExpansionPackSecretAgent()
		{
			return null;
		}

		public void PurchaseExpansionPackSecretAgent(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackSecretAgentPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackSecretAgentRestored()
		{
		}

		public string GetPriceStringForExpansionPackZoo()
		{
			return null;
		}

		public void PurchaseExpansionPackZoo(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackZooPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackZooRestored()
		{
		}

		public string GetPriceStringForExpansionPackRacing()
		{
			return null;
		}

		public void PurchaseExpansionPackRacing(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackRacingPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackRacingRestored()
		{
		}

		public string GetPriceStringForExpansionPackCasino()
		{
			return null;
		}

		public void PurchaseExpansionPackCasino(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackCasinoPurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackCasinoRestored()
		{
		}

		public string GetPriceStringForExpansionPackVampire()
		{
			return null;
		}

		public void PurchaseExpansionPackVampire(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void ExpansionPackVampirePurchased(bool isOriginalPurchase)
		{
		}

		public void ExpansionPackVampireRestored()
		{
		}

		public string GetPriceStringForCareerDealer()
		{
			return null;
		}

		public void PurchaseCareerDealer(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void CareerDealerPurchased(bool isOriginalPurchase)
		{
		}

		public void CareerDealerRestored()
		{
		}

		public string GetPriceStringForBundleBillionaire(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleBillionaire(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleBillionairePurchased(bool isOriginalPurchase)
		{
		}

		public void BundleBillionaireRestored()
		{
		}

		public string GetPriceStringForBundleBirthdayStarter(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleBirthdayStarter(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleBirthdayStarterPurchased(bool isOriginalPurchase)
		{
		}

		public void BundleBirthdayStarterRestored()
		{
		}

		public string GetPriceStringForBundleBirthdayLegends(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleBirthdayLegends(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleBirthdayLegendsPurchased(bool isOriginalPurchase)
		{
		}

		public void BundleBirthdayLegendsRestored()
		{
		}

		public string GetPriceStringForBundleTimeIsMoney(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleTimeIsMoney(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleTimeIsMoneyPurchased(bool isOriginalPurchase)
		{
		}

		public void BundleTimeIsMoneyRestored()
		{
		}

		public string GetPriceStringForBundleStarter1(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleStarter1(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleStarter1Purchased(bool isOriginalPurchase)
		{
		}

		public void BundleStarter1Restored()
		{
		}

		public string GetPriceStringForBundleStarter2(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleStarter2(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleStarter2Purchased(bool isOriginalPurchase)
		{
		}

		public void BundleStarter2Restored()
		{
		}

		public string GetPriceStringForBundlePartyAnimal(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundlePartyAnimal(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundlePartyAnimalPurchased(bool isOriginalPurchase)
		{
		}

		public void BundlePartyAnimalRestored()
		{
		}

		public string GetPriceStringForBundleCrime(bool darkly = false)
		{
			return null;
		}

		public void PurchaseBundleCrime(int theIapOrigin, bool isFeatured = false)
		{
		}

		public void BundleCrimePurchased(bool isOriginalPurchase)
		{
		}

		public void BundleCrimeRestored()
		{
		}

		public void RestorePurchases()
		{
		}

		public void SetUIPurchaseProcessCallback(Action callbackForUI)
		{
		}

		public void UniversalPostPurchaseActivities()
		{
		}

		public static void AppStoreConnectionErrorAlert()
		{
		}

		public void SetupFollowBackScenario(Scenario followUpScenario)
		{
		}

		public void ProcessPurchaseOperation(Scenario extraScenario = null)
		{
		}

		public Scenario ConsumeNextScenario()
		{
			return null;
		}

		private void SetupNextScenarioWithFollowUp(Scenario feedbackScenario)
		{
		}
	}
}
