using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class GameReference
{
	private sealed class _003C_003Ec__DisplayClass127_0
	{
		public int iapType;

		internal bool _003CStringForMarketplaceItemBanner_003Eb__0(KeyValuePair<string, object> x)
		{
			return false;
		}
	}

	public static bool LoggerActive;

	public static readonly List<string> ImpactFilterList;

	public static uint AdditionalStackFrames;

	private static readonly System.Random getrandom;

	public static readonly string LETTERS;

	private static (int, int) ValueMax_LowRed;

	private static (int, int) ValueMax_HighRed;

	private const int ValueMax = 100;

	public static readonly Color UIBarGraphGreen;

	public static readonly Color UIBarGraphYellow;

	public static readonly Color UIBarGraphRed;

	public static readonly Color UIBarGraphGold;

	public static readonly Color UIBarGraphMagenta;

	public static readonly Color UIBarGraphAuraRingBoost;

	public static string AppName()
	{
		return null;
	}

	public static string IosAppStoreID()
	{
		return null;
	}

	public static string AppHashtag()
	{
		return null;
	}

	public static string AppURL()
	{
		return null;
	}

	public static string VersionText()
	{
		return null;
	}

	public static string BundleIdentifier()
	{
		return null;
	}

	public static string SemaphoreURL()
	{
		return null;
	}

	public static string AppPrivacyPolicyURL()
	{
		return null;
	}

	public static string AppTermsOfServiceURL()
	{
		return null;
	}

	public static int GetRandomNumber(int min, int max)
	{
		return 0;
	}

	public static double GetRandomDouble()
	{
		return 0.0;
	}

	public static float GetRandomFloatNumber(float minValue, float maxValue)
	{
		return 0f;
	}

	public static double GetRandomDoubleNumber(double minValue, double maxValue)
	{
		return 0.0;
	}

	public static bool DiceRoll(float chanceOfSuccess)
	{
		return false;
	}

	public static string GetRandomStringFromList(List<string> listString)
	{
		return null;
	}

	public static string GetRandomIdentifier()
	{
		return null;
	}

	public static bool KeepItClean()
	{
		return false;
	}

	public static bool DevTestingFame()
	{
		return false;
	}

	public static bool UserIsOnMetricSystem()
	{
		return false;
	}

	public static ArrayList RandomizeOrderOfArray(ArrayList mArray)
	{
		return null;
	}

	public static Color GetColorWithRedGreenBlue(int r, int g, int b)
	{
		return default;
	}

	public static CandyBarGraphOptions GetUIBarGraphColorInfo(float value, int colorScheme)
	{
		return null;
	}

	public static Color GetUIBarGraphColorForValueAndColorScheme(float value, int colorScheme)
	{
		return default;
	}

	public static bool ExperiencedEnoughForReviewPrompt()
	{
		return false;
	}

	public static bool NewLifeRewardedIsEnabled()
	{
		return false;
	}

	public static bool ExperiencedEnoughForInterstitialAndVideoAds()
	{
		return false;
	}

	public static bool ExperiencedEnoughForBannerAds()
	{
		return false;
	}

	public static string CurrencyStringForAmount(double amount)
	{
		return null;
	}

	public static string CurrencyStringForAmountIncludingDecimal(double amount)
	{
		return null;
	}

	public static string CurrencyStringForAmountInCountryCode(double amount, string countryCode, bool includeDecimal = false)
	{
		return null;
	}

	public static long AmountFromCurrencyString(string s)
	{
		return 0L;
	}

	public static double AmountFromCurrencyStringDouble(string s)
	{
		return 0.0;
	}

	public static long AmountFromCurrencyStringInCountryCode(string s, string countryCode)
	{
		return 0L;
	}

	public static double AmountFromCurrencyStringInCountryCodeWithDouble(string s, string countryCode)
	{
		return 0.0;
	}

	public static string ApproximatedCurrencyStringForAmount(double amount)
	{
		return null;
	}

	private static string GetCurrencyString(double amount, string original)
	{
		return null;
	}

	public static string CurrencyStringForAmountWithBigNumberShorthand(double amount, bool useDecimal = false)
	{
		return null;
	}

	public static string CurrencyStringForAmountWithBigNumberShorthandTranslated(double amount, bool useDecimal = false)
	{
		return null;
	}

	private static string FormatLargeQuantityNumberString(double estimatedAmount, string currencyString, bool maxNumber = false)
	{
		return null;
	}

	public static string ApproximatedCurrencyStringForAmountInCountryCode(double amount, string countryCode)
	{
		return null;
	}

	public static long AmountFromApproximatedCurrencyString(string s)
	{
		return 0L;
	}

	public static long AmountFromApproximatedCurrencyStringInCountryCode(string s, string countryCode)
	{
		return 0L;
	}

	public static string NumberGroupingStringForAmount(int amount)
	{
		return null;
	}

	public static string NumberGroupingStringForDoubleAmount(double amount)
	{
		return null;
	}

	public static string PercentStringForValueWithFractionDigits(float f, int digits)
	{
		return null;
	}

	public static string PercentStringForValueWithFractionDigitsForDouble(double f, int digits)
	{
		return null;
	}

	public static string PercentStringForValueWithSingleFractionDigitForDouble(double f)
	{
		return null;
	}

	public static string NumberGroupingStringForDoubleAmountWithSingleDigit(double amount)
	{
		return null;
	}

	public static string LonghandStringForAmount(int amount)
	{
		return null;
	}

	public static string LonghandStringForDoubleAmount(double amount, bool includeSingleDigit = false)
	{
		return null;
	}

	public static string TildePrefixedCurrencyStringForAmount(double amount)
	{
		return null;
	}

	public static string TildePrefixedNumberGroupingStringForAmount(int amount)
	{
		return null;
	}

	public static string StringForAmountWithBigNumberShorthand(double amount, bool useDecimal = false)
	{
		return null;
	}

	public static string ConvertAstroNumber(double input)
	{
		return null;
	}

	public static int SecondsSinceDate(DateTime date)
	{
		return 0;
	}

	public static int MinutesSinceDate(DateTime date)
	{
		return 0;
	}

	public static int HoursSinceDate(DateTime date)
	{
		return 0;
	}

	public static string AddIndefiniteArticleToString(string s, string insert = "")
	{
		return null;
	}

	private static bool IsIndefiniteArticleException(string s)
	{
		return false;
	}

	public static string AddPossessionToNameString(string s)
	{
		return null;
	}

	public static string StringForPersonArray(ArrayList array)
	{
		return null;
	}

	public static string CapitalizeString(string s)
	{
		return null;
	}

	public static string CapitalizeAllMajorWordsInString(string s)
	{
		return null;
	}

	public static string CapitalizeAllSentencesInString(string s)
	{
		return null;
	}

	public static string CapitalizeFirstLetterInSentenceStringAfterCharacterSkipIfFollowedByCharacter(string sentence, char character, string skipCharacter)
	{
		return null;
	}

	public static string CapitalizeFirstFoundLetterOfSentenceSkipIfStartingWithCharacter(string sentence, string skipCharacter)
	{
		return null;
	}

	public static string CapitalizeFirstLetterOfSentenceString(string s)
	{
		return null;
	}

	public static string UncapitalizeFirstLetterOfSentenceString(string s)
	{
		return null;
	}

	public static string ConvertToLowercaseExceptAcronym(string s)
	{
		return null;
	}

	public static string GroupedStringForStringArray(ArrayList array)
	{
		return null;
	}

	public static string AncestorStringForGenerationsBackAndGender(int generationsBack, int gender)
	{
		return null;
	}

	public static string CleanUpDuplicatePunctuationInString(string s)
	{
		return null;
	}

	public static string PickRandomFromStringArray(ArrayList array)
	{
		return null;
	}

	public static bool FastCompareStringWithString(string s1, string s2)
	{
		return false;
	}

	public static string OrdinalStringForInt(int x)
	{
		return null;
	}

	public static string LonghandOrdinalStringForInt(int x)
	{
		return null;
	}

	public static double GetRoundedNumber(double number, double associatedNumber = -1.0)
	{
		return 0.0;
	}

	public static string RemoveEncasingSingleQuotesFromTitle(string s)
	{
		return null;
	}

	public static string GetCountdownDisplayStringForSecondsAndIncludeSeconds(int seconds, bool includeSeconds)
	{
		return null;
	}

	public static string GetStringForMonth(int monthNI)
	{
		return null;
	}

	public static bool ShouldAllowChange(string newString, int characterLimit, int numberOfAllowedSpaces, bool allowNumbers)
	{
		return false;
	}

	public static string ShouldAllowFinalEdit(string input, string original)
	{
		return null;
	}

	public static string StringForExpansionPack(int expansionPack)
	{
		return null;
	}

	public static string StringForJobPack(int jobPack)
	{
		return null;
	}

	public static int AttributeAdjustmentPointsForImpactScore(int impactScore)
	{
		return 0;
	}

	public static int AttributeImpactSkewPositive()
	{
		return 0;
	}

	public static int AttributeImpactSkewNegative()
	{
		return 0;
	}

	public static int AttributeImpactSkewNeutral()
	{
		return 0;
	}

	public static SimPlace GetRandomPlaceForNewLife()
	{
		return null;
	}

	public static DateTime GetRandomCalendarDate()
	{
		return default;
	}

	public static string GetBirthdayStringForDate(DateTime date)
	{
		return null;
	}

	public static string GetStarSignForDate(DateTime date)
	{
		return null;
	}

	public static string IapOriginString(int iapOrigin)
	{
		return null;
	}

	public static string RewardedVideoOriginString(int rewardedVideoOrigin)
	{
		return null;
	}

	public static string InterstitialOriginString(int interstitialOrigin)
	{
		return null;
	}

	public static string ShareOriginString(int shareOrigin)
	{
		return null;
	}

	public static string DogLifeCrossPromoOriginString(int origin)
	{
		return null;
	}

	public static string CatLifeCrossPromoOriginString(int origin)
	{
		return null;
	}

	public static string RankLifeCrossPromoOriginString(int origin)
	{
		return null;
	}

	public static string WhatTheFateCrossPromoOriginString(int origin)
	{
		return null;
	}

	public static string BitLifeGoCrossPromoOriginString(int origin)
	{
		return null;
	}

	public static ArrayList ProfanityArray()
	{
		return null;
	}

	public static bool IsStringFreeOfProfanity(string s)
	{
		return false;
	}

	public static string SanitizeStringWithCheck(string theString, bool allowSpace, int characterLimit, bool allowNumbers, int allowedSpaces = -1)
	{
		return null;
	}

	public static string SanitizeStringWithCheckAndClip(string theString, bool allowSpace, int characterLimit, bool allowNumbers, int allowedSpaces = -1)
	{
		return null;
	}

	private static string SanitizeStringWithoutCheck(string theString, bool allowSpace, int characterLimit, bool allowNumbers, int allowedSpaces = -1)
	{
		return null;
	}

	public static bool IsEntirelyNonLetterString(string pString)
	{
		return false;
	}

	public static string RemoveAllButFirstInstanceOfStringFromString(int pNumberOfInstances, string pRemoveString, string pString)
	{
		return null;
	}

	public static string ForceCityRulesInString(string pString)
	{
		return null;
	}

	public static string ForceNameRulesInStringOverrideCapitalizationAndPunctuationRulesOverrideSpaceRemoval(string pString, bool pOverrideCapitalizationAndPunctuationRules, bool pOverrideSpaceRemoval, bool pOverrideNumberAtBeginning = false)
	{
		return null;
	}

	public static string ForceCapitalizeFirstCharacterAfterMcInString(string pString)
	{
		return null;
	}

	public static string ForceCapitalizeFirstCharacterInString(string pString)
	{
		return null;
	}

	public static SimPlace GetDeserializedPlace(string countryCode)
	{
		return null;
	}

	public static string StringForMarketplaceItemBanner(int marketplaceItem)
	{
		return null;
	}

	public static int GetInstantFameTypeForCurrentCareer()
	{
		return 0;
	}

	public static T PickRandomEnumValue<T>()
	{
		return default;
	}

	public static void OpenStore()
	{
	}

	public static string RoundToNearest99(string price)
	{
		return null;
	}

	public static string ReplaceVersions(string bundleName)
	{
		return null;
	}
}
