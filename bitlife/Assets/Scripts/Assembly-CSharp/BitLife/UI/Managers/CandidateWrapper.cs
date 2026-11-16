using System;
using System.Collections;
using System.Collections.Generic;

namespace BitLife.UI.Managers
{
	public class CandidateWrapper
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<CandidateWrapper, string> _003C_003E9__18_1;

			internal string _003CGetEmojiForVariant_003Eb__18_1(CandidateWrapper t)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass16_0
		{
			public AGE_VARIANT ageVariant;

			public int gender;

			internal bool _003CGetCandidates_003Eb__0(CandidateWrapper t)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass18_0
		{
			public AGE_VARIANT ageVariant;

			public int gender;

			internal bool _003CGetEmojiForVariant_003Eb__0(CandidateWrapper t)
			{
				return false;
			}
		}

		private OnboardingCandidate Candidate__BackingField;

		private string CountryCode__BackingField;

		private string EmojiString__BackingField;

		private AGE_VARIANT AgeVariant__BackingField;

		public OnboardingCandidate Candidate
		{
			get
			{
				return Candidate__BackingField;
			}
			set
			{
				Candidate__BackingField = value;
			}
		}

		public string CountryCode
		{
			get
			{
				return CountryCode__BackingField;
			}
			set
			{
				CountryCode__BackingField = value;
			}
		}

		public string EmojiString
		{
			get
			{
				return EmojiString__BackingField;
			}
			set
			{
				EmojiString__BackingField = value;
			}
		}

		public AGE_VARIANT AgeVariant
		{
			get
			{
				return AgeVariant__BackingField;
			}
			set
			{
				AgeVariant__BackingField = value;
			}
		}

		public static ArrayList GetCandidates(List<CandidateWrapper> candidatesStatic, AGE_VARIANT ageVariant, int gender)
		{
			return null;
		}

		public static int GetAgeForVariant(AGE_VARIANT ageVariant)
		{
			return 0;
		}

		public static List<string> GetEmojiForVariant(List<CandidateWrapper> candidatesStatic, AGE_VARIANT ageVariant, int gender)
		{
			return null;
		}
	}
}
