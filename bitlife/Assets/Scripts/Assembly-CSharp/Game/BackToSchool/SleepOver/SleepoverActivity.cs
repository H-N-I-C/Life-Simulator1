using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.BackToSchool.SleepOver
{
	public class SleepoverActivity
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<DataBacktoSchoolSleepoverActivitie, string> _003C_003E9__44_0;

			public static Func<SimPerson, bool> _003C_003E9__62_0;

			public static Func<object, SimPerson> _003C_003E9__62_1;

			public static Func<SimPerson, bool> _003C_003E9__62_2;

			internal string _003C_002Ector_003Eb__44_0(DataBacktoSchoolSleepoverActivitie activity)
			{
				return null;
			}

			internal bool _003CGetEligibleFriendList_003Eb__62_0(SimPerson person)
			{
				return false;
			}

			internal SimPerson _003CGetEligibleFriendList_003Eb__62_1(object friend)
			{
				return null;
			}

			internal bool _003CGetEligibleFriendList_003Eb__62_2(SimPerson person)
			{
				return false;
			}
		}

		private const int ParentsRelationshipImpactOnLoverRefusal = -1;

		private const int FriendRejectionImpact = -2;

		private const int FriendRejectionImpactBelievabilityThreshold = 3;

		private const int DumbScenarioChance = 40;

		private const int MinAgeForRandomPush = 7;

		private const int MaxAgeForRandomPush = 17;

		private const int MinRelationshipScoreForRandomPush = 50;

		private const int YearsBetweenRandomPush = 3;

		private const int HeroRefusalInviteImpact = -3;

		private static readonly int[] EnjoymentScoreImpacts;

		private readonly SimHero _hero;

		private SimPerson Friend__BackingField;

		private string ChosenActivity__BackingField;

		private string[] ActivityList__BackingField;

		private readonly List<string> _shuffledActivitySentences;

		private readonly List<DataBacktoSchoolSleepoverActivitie> _activitySet;

		private string RejectionColorText__BackingField;

		private bool InvitedByHero__BackingField;

		private float HeroEnjoyment__BackingField;

		private float FriendEnjoyment__BackingField;

		private string _sleepoverAcceptanceFeedbackStringText;

		public readonly BracketParser BracketParser;

		private readonly Dictionary<string, string> _pastTensesByActivity;

		public SimPerson Friend
		{
			get
			{
				return Friend__BackingField;
			}
			private set
			{
				Friend__BackingField = value;
			}
		}

		public string ChosenActivity
		{
			get
			{
				return ChosenActivity__BackingField;
			}
			set
			{
				ChosenActivity__BackingField = value;
			}
		}

		public string[] ActivityList
		{
			get
			{
				return ActivityList__BackingField;
			}
			private set
			{
				ActivityList__BackingField = value;
			}
		}

		public string RejectionColorText
		{
			get
			{
				return RejectionColorText__BackingField;
			}
			private set
			{
				RejectionColorText__BackingField = value;
			}
		}

		public bool InvitedByHero
		{
			get
			{
				return InvitedByHero__BackingField;
			}
			private set
			{
				InvitedByHero__BackingField = value;
			}
		}

		public float HeroEnjoyment
		{
			get
			{
				return HeroEnjoyment__BackingField;
			}
			private set
			{
				HeroEnjoyment__BackingField = value;
			}
		}

		public float FriendEnjoyment
		{
			get
			{
				return FriendEnjoyment__BackingField;
			}
			private set
			{
				FriendEnjoyment__BackingField = value;
			}
		}

		public SleepoverActivity(SimPerson friend, bool invitedByHero)
		{
		}

		public ArrayList ShuffleCapitalizedActivityList()
		{
			return null;
		}

		public bool IsOrphan()
		{
			return false;
		}

		public void SleepoverWithLoverRefusalImpactParentsRelationship()
		{
		}

		public bool ShowDumbScenario()
		{
			return false;
		}

		public static void LogToConsole(string message)
		{
		}

		public bool InviteFriendToSleepover()
		{
			return false;
		}

		private static List<string> CapitalizeFirstLetterOfEach(string[] activitySentenceArray)
		{
			return null;
		}

		private void FetchFriendRejection(bool impactRelationship)
		{
		}

		private void OnFriendAcceptedSleepover()
		{
		}

		public void OnHeroAcceptedSleepover()
		{
		}

		private void CalculateSleepoverResult()
		{
		}

		private void CalculateFriendRejectionImpact(int rejectionBelievabilityScore)
		{
		}

		public string GetSleepOverActivityFeedbackText()
		{
			return null;
		}

		public void ImpactHeroSleepover(int relationship, int looks, int smarts, int happiness, int karma)
		{
		}

		public string GetSleepoverAcceptanceFeedbackString()
		{
			return null;
		}

		public bool FriendAlreadyInvitedRejection()
		{
			return false;
		}

		public static bool ScenarioIsEligibleForRandomPush()
		{
			return false;
		}

		public static List<SimPerson> GetEligibleFriendList()
		{
			return null;
		}

		public void HeroRejectInvitationMakeExcuse()
		{
		}

		public void HeroRejectInvitation()
		{
		}

		public Dictionary<string, string> GetBracketParserDictionary(bool isBody)
		{
			return null;
		}

		public string GetPastTenseByActivity()
		{
			return null;
		}

		private Dictionary<string, string> MapToPastTense(List<string> capitalizedActivities)
		{
			return null;
		}

		private bool _003CCalculateSleepoverResult_003Eb__55_0(DataBacktoSchoolSleepoverAcceptanceFeedbackString activity)
		{
			return false;
		}

		private bool _003CGetSleepOverActivityFeedbackText_003Eb__57_0(DataBacktoSchoolSleepoverActivitie activity)
		{
			return false;
		}
	}
}
