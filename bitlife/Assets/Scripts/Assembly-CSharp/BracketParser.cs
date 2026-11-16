using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

public class BracketParser
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static MatchEvaluator _003C_003E9__159_5;

		public static MatchEvaluator _003C_003E9__159_6;

		public static Func<float, string> _003C_003E9__159_7;

		public static Func<float, string> _003C_003E9__159_8;

		public static Func<float, string> _003C_003E9__159_9;

		internal string _003CGetParsedUniversalStringAndIsBody_003Eb__159_5(Match match)
		{
			return null;
		}

		internal string _003CGetParsedUniversalStringAndIsBody_003Eb__159_6(Match match)
		{
			return null;
		}

		internal string _003CGetParsedUniversalStringAndIsBody_003Eb__159_7(float rnd)
		{
			return null;
		}

		internal string _003CGetParsedUniversalStringAndIsBody_003Eb__159_8(float rnd)
		{
			return null;
		}

		internal string _003CGetParsedUniversalStringAndIsBody_003Eb__159_9(float rnd)
		{
			return null;
		}
	}

	[StructLayout((LayoutKind)3)]
	private struct _003C_003Ec__DisplayClass159_0
	{
		public BracketParser _003C_003E4__this;

		public string text;
	}

	private sealed class _003C_003Ec__DisplayClass160_0
	{
		public List<string> texts;

		internal bool _003CIsFormattingTag_003Eb__0(string t)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass160_1
	{
		public string t;

		internal bool _003CIsFormattingTag_003Eb__1(string x)
		{
			return false;
		}
	}

	private Dictionary<string, string> BodyBracketDictionary__BackingField;

	private Dictionary<string, string> NarrativeBracketDictionary__BackingField;

	private int Gender__BackingField;

	private SimPerson Person__BackingField;

	private bool DoNotCapitalize__BackingField;

	private SimPerson SelectedFriend__BackingField;

	private SimParent SelectedParent__BackingField;

	private SimChild SelectedChild__BackingField;

	private SimPerson SelectedSibling__BackingField;

	private SimLover SelectedEx__BackingField;

	private SimCoworker SelectedCoworker__BackingField;

	private SimClassmate SelectedClassmate__BackingField;

	private SimTenant SelectedTenant__BackingField;

	private SimSeniorPerson SelectedSenior__BackingField;

	private SimVampireHunter SelectedVampireHunter__BackingField;

	private SimPerson SelectedSwinger__BackingField;

	private SimPerson SelectedSwingerLover__BackingField;

	private string SelectedRandomFullName__BackingField;

	private string SelectedRandomFirstName__BackingField;

	private string SelectedRandomNameMale__BackingField;

	private string SelectedRandomNameFemale__BackingField;

	private string SelectedSurname__BackingField;

	private string SelectedInsult__BackingField;

	private string SelectedAlcohol__BackingField;

	private string SelectedDrug__BackingField;

	private string SelectedDrugIllegal__BackingField;

	private string SelectedCarType__BackingField;

	private string SelectedStreetPerson__BackingField;

	private string SelectedBodyPart__BackingField;

	private string SelectedCountryOther__BackingField;

	private string SelectedMusician__BackingField;

	private string SelectedCelebrity__BackingField;

	private string SelectedCelebrityMale__BackingField;

	private string SelectedCelebrityFemale__BackingField;

	private string SelectedCircumstancePublic__BackingField;

	private string SelectedCircumstanceHome__BackingField;

	private string SelectedCircumstanceReturn__BackingField;

	private List<Dictionary<string, string>> SelectedNumberBracketList;

	private List<Dictionary<string, string>> SelectedRandomBracketList;

	public bool CacheRandomBrackets;

	private static readonly Dictionary<string, int> RB_Brackets;

	private static readonly List<string> FormattingTags;

	private static readonly Dictionary<string, Dictionary<GENDER, string>> MaleFemalePronounBracketsForGenderStrings;

	private static readonly Dictionary<string, Dictionary<bool, string>> HeroPronounBracketsForPointOfViewStrings;

	public Dictionary<string, string> BodyBracketDictionary
	{
		get
		{
			return BodyBracketDictionary__BackingField;
		}
		set
		{
			BodyBracketDictionary__BackingField = value;
		}
	}

	public Dictionary<string, string> NarrativeBracketDictionary
	{
		get
		{
			return NarrativeBracketDictionary__BackingField;
		}
		set
		{
			NarrativeBracketDictionary__BackingField = value;
		}
	}

	public int Gender
	{
		get
		{
			return Gender__BackingField;
		}
		set
		{
			Gender__BackingField = value;
		}
	}

	public SimPerson Person
	{
		get
		{
			return Person__BackingField;
		}
		set
		{
			Person__BackingField = value;
		}
	}

	public bool DoNotCapitalize
	{
		get
		{
			return DoNotCapitalize__BackingField;
		}
		set
		{
			DoNotCapitalize__BackingField = value;
		}
	}

	public SimPerson SelectedFriend
	{
		get
		{
			return SelectedFriend__BackingField;
		}
		set
		{
			SelectedFriend__BackingField = value;
		}
	}

	public SimParent SelectedParent
	{
		get
		{
			return SelectedParent__BackingField;
		}
		set
		{
			SelectedParent__BackingField = value;
		}
	}

	public SimChild SelectedChild
	{
		get
		{
			return SelectedChild__BackingField;
		}
		set
		{
			SelectedChild__BackingField = value;
		}
	}

	public SimPerson SelectedSibling
	{
		get
		{
			return SelectedSibling__BackingField;
		}
		set
		{
			SelectedSibling__BackingField = value;
		}
	}

	public SimLover SelectedEx
	{
		get
		{
			return SelectedEx__BackingField;
		}
		set
		{
			SelectedEx__BackingField = value;
		}
	}

	public SimCoworker SelectedCoworker
	{
		get
		{
			return SelectedCoworker__BackingField;
		}
		set
		{
			SelectedCoworker__BackingField = value;
		}
	}

	public SimClassmate SelectedClassmate
	{
		get
		{
			return SelectedClassmate__BackingField;
		}
		set
		{
			SelectedClassmate__BackingField = value;
		}
	}

	public SimTenant SelectedTenant
	{
		get
		{
			return SelectedTenant__BackingField;
		}
		set
		{
			SelectedTenant__BackingField = value;
		}
	}

	public SimSeniorPerson SelectedSenior
	{
		get
		{
			return SelectedSenior__BackingField;
		}
		set
		{
			SelectedSenior__BackingField = value;
		}
	}

	public SimVampireHunter SelectedVampireHunter
	{
		get
		{
			return SelectedVampireHunter__BackingField;
		}
		set
		{
			SelectedVampireHunter__BackingField = value;
		}
	}

	public SimPerson SelectedSwinger
	{
		get
		{
			return SelectedSwinger__BackingField;
		}
		set
		{
			SelectedSwinger__BackingField = value;
		}
	}

	public SimPerson SelectedSwingerLover
	{
		get
		{
			return SelectedSwingerLover__BackingField;
		}
		set
		{
			SelectedSwingerLover__BackingField = value;
		}
	}

	public string SelectedRandomFullName
	{
		get
		{
			return SelectedRandomFullName__BackingField;
		}
		set
		{
			SelectedRandomFullName__BackingField = value;
		}
	}

	public string SelectedRandomFirstName
	{
		get
		{
			return SelectedRandomFirstName__BackingField;
		}
		set
		{
			SelectedRandomFirstName__BackingField = value;
		}
	}

	public string SelectedRandomNameMale
	{
		get
		{
			return SelectedRandomNameMale__BackingField;
		}
		set
		{
			SelectedRandomNameMale__BackingField = value;
		}
	}

	public string SelectedRandomNameFemale
	{
		get
		{
			return SelectedRandomNameFemale__BackingField;
		}
		set
		{
			SelectedRandomNameFemale__BackingField = value;
		}
	}

	public string SelectedSurname
	{
		get
		{
			return SelectedSurname__BackingField;
		}
		set
		{
			SelectedSurname__BackingField = value;
		}
	}

	public string SelectedInsult
	{
		get
		{
			return SelectedInsult__BackingField;
		}
		set
		{
			SelectedInsult__BackingField = value;
		}
	}

	public string SelectedAlcohol
	{
		get
		{
			return SelectedAlcohol__BackingField;
		}
		set
		{
			SelectedAlcohol__BackingField = value;
		}
	}

	public string SelectedDrug
	{
		get
		{
			return SelectedDrug__BackingField;
		}
		set
		{
			SelectedDrug__BackingField = value;
		}
	}

	public string SelectedDrugIllegal
	{
		get
		{
			return SelectedDrugIllegal__BackingField;
		}
		set
		{
			SelectedDrugIllegal__BackingField = value;
		}
	}

	public string SelectedCarType
	{
		get
		{
			return SelectedCarType__BackingField;
		}
		set
		{
			SelectedCarType__BackingField = value;
		}
	}

	public string SelectedStreetPerson
	{
		get
		{
			return SelectedStreetPerson__BackingField;
		}
		set
		{
			SelectedStreetPerson__BackingField = value;
		}
	}

	public string SelectedBodyPart
	{
		get
		{
			return SelectedBodyPart__BackingField;
		}
		set
		{
			SelectedBodyPart__BackingField = value;
		}
	}

	public string SelectedCountryOther
	{
		get
		{
			return SelectedCountryOther__BackingField;
		}
		set
		{
			SelectedCountryOther__BackingField = value;
		}
	}

	public string SelectedMusician
	{
		get
		{
			return SelectedMusician__BackingField;
		}
		set
		{
			SelectedMusician__BackingField = value;
		}
	}

	public string SelectedCelebrity
	{
		get
		{
			return SelectedCelebrity__BackingField;
		}
		set
		{
			SelectedCelebrity__BackingField = value;
		}
	}

	public string SelectedCelebrityMale
	{
		get
		{
			return SelectedCelebrityMale__BackingField;
		}
		set
		{
			SelectedCelebrityMale__BackingField = value;
		}
	}

	public string SelectedCelebrityFemale
	{
		get
		{
			return SelectedCelebrityFemale__BackingField;
		}
		set
		{
			SelectedCelebrityFemale__BackingField = value;
		}
	}

	public string SelectedCircumstancePublic
	{
		get
		{
			return SelectedCircumstancePublic__BackingField;
		}
		set
		{
			SelectedCircumstancePublic__BackingField = value;
		}
	}

	public string SelectedCircumstanceHome
	{
		get
		{
			return SelectedCircumstanceHome__BackingField;
		}
		set
		{
			SelectedCircumstanceHome__BackingField = value;
		}
	}

	public string SelectedCircumstanceReturn
	{
		get
		{
			return SelectedCircumstanceReturn__BackingField;
		}
		set
		{
			SelectedCircumstanceReturn__BackingField = value;
		}
	}

	public BracketParser()
	{
	}

	public BracketParser(Dictionary<string, string> theDictionary)
	{
	}

	public BracketParser(Dictionary<string, string> theBodyBracketDictionary, Dictionary<string, string> theNarrativeBracketDictionary, int theGender, SimPerson thePerson)
	{
	}

	public void ClearCachedRandomBrackets()
	{
	}

	public void PrepareParser()
	{
	}

	public string GetParsedString(string theString)
	{
		return null;
	}

	public string GetParsedBodyString(string bodyString)
	{
		return null;
	}

	public string GetParsedNarrativeString(string narrativeString)
	{
		return null;
	}

	public string GetParsedUniversalStringAndIsBody(string text, bool isBody)
	{
		return null;
	}

	private bool IsFormattingTag(List<string> texts)
	{
		return false;
	}

	public static string ReplaceMaleFemalePronounBracketsInStringForGender(string text, int gender)
	{
		return null;
	}

	private static void ReplaceMaleFemalePronounBracketsInStringForGender(StringBuilder sb, int gender)
	{
	}

	private static void ReplaceHeroPronounBracketsInStringForPointOfView(StringBuilder sb, bool isBody)
	{
	}

	private static string ReplaceIndefiniteArticle(string text, bool capital)
	{
		return null;
	}

	private string GetParentString()
	{
		return null;
	}

	public static string[] GetBracketTextPermutations(string stringWithPossibleBracket, bool keepBracketBraces)
	{
		return null;
	}

	public static string CleanBrackets(string stringWithPossibleBrackets)
	{
		return null;
	}

	private bool _003CGetParsedUniversalStringAndIsBody_003Eg__ReplaceBrackets_007C159_0(string bracket, ref _003C_003Ec__DisplayClass159_0 P_1)
	{
		return false;
	}

	private bool _003CGetParsedUniversalStringAndIsBody_003Eg__ApplyBracketReplacements_007C159_1(string bracket, ref _003C_003Ec__DisplayClass159_0 P_1)
	{
		return false;
	}

	private bool _003CGetParsedUniversalStringAndIsBody_003Eg__ReplaceAndCacheRandomNumberBracket_007C159_2(string bracket, string name, Func<float, string> replaceWith, [Optional][DefaultParameterValue(1f)] float scale, ref _003C_003Ec__DisplayClass159_0 P_4)
	{
		return false;
	}

	internal static bool _003CGetParsedUniversalStringAndIsBody_003Eg__IsArticleBracket_007C159_3(string bracket)
	{
		return false;
	}

	private bool _003CGetParsedUniversalStringAndIsBody_003Eg__ReplaceAndCacheRandomBracket_007C159_4(string bracket, int index, ref _003C_003Ec__DisplayClass159_0 P_2)
	{
		return false;
	}
}
