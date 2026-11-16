using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChallengeManager : MonoBehaviour
{
	public enum TestState
	{
		NotCompleted = 0,
		NotCompleted_Active = 1,
		Completed_NotRanked_NoRewards = 2,
		Completed_NotRanked_HasRewards = 3,
		Completed_Ranked_NoRewards = 4,
		Completed_Ranked_HasRewards = 5
	}

	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ChallengeMetadata, string> _003C_003E9__48_0;

		public static Func<bool, bool> _003C_003E9__49_5;

		public static Func<ChallengeMetadata, decimal> _003C_003E9__49_1;

		public static Func<ChallengeMetadata, int> _003C_003E9__49_2;

		public static Func<ChallengeMetadata, string> _003C_003E9__49_3;

		internal string _003CDevChallengeList_003Eb__48_0(ChallengeMetadata challenge)
		{
			return null;
		}

		internal decimal _003CUnfinishedLegacyChallengesList_003Eb__49_1(ChallengeMetadata x)
		{
			return 0m;
		}

		internal bool _003CUnfinishedLegacyChallengesList_003Eb__49_5(bool a)
		{
			return false;
		}

		internal int _003CUnfinishedLegacyChallengesList_003Eb__49_2(ChallengeMetadata x)
		{
			return 0;
		}

		internal string _003CUnfinishedLegacyChallengesList_003Eb__49_3(ChallengeMetadata x)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass49_0
	{
		public List<CompletedChallenge> completeChallengeList;

		internal bool _003CUnfinishedLegacyChallengesList_003Eb__0(ChallengeMetadata x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass49_1
	{
		public ChallengeMetadata x;

		internal bool _003CUnfinishedLegacyChallengesList_003Eb__4(CompletedChallenge a)
		{
			return false;
		}
	}

	private sealed class _003CExpandableFallbackList_003Ed__19 : IEnumerable<ChallengeMetadata>, IEnumerable, IEnumerator<ChallengeMetadata>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private ChallengeMetadata _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		ChallengeMetadata IEnumerator<ChallengeMetadata>.Current => null;

		object IEnumerator.Current => null;

		public _003CExpandableFallbackList_003Ed__19(int _003C_003E1__state)
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

		IEnumerator<ChallengeMetadata> IEnumerable<ChallengeMetadata>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	private sealed class _003CGetOnlineChallenge_003Ed__28 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ChallengeManager _003C_003E4__this;

		private UnityWebRequest _003CwebRequest_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetOnlineChallenge_003Ed__28(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}
	}

	private sealed class _003CUploadCompletedChallenge_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WWWForm postData;

		public ChallengeManager _003C_003E4__this;

		private UnityWebRequest _003CwebRequest_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CUploadCompletedChallenge_003Ed__25(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}
	}

	private sealed class _003CUploadQueuedOfflineChallenges_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public WWWForm postData;

		public ChallengeManager _003C_003E4__this;

		public int challengeID;

		private UnityWebRequest _003CwebRequest_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CUploadQueuedOfflineChallenges_003Ed__26(int _003C_003E1__state)
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

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}
	}

	private const string ChallengesURLString = "https://cywrbitlifechallenges.appspot.com/api/challenge";

	public static ChallengeManager SharedChallengeManager;

	public ArrayList CompletedChallengeArray;

	public ArrayList MetadataArray;

	public TestState DevOverrideForcedPathState;

	public int PostingChallengeID;

	public int DevOverrideActiveChallengeID;

	private int SecondsUntilNextChallengeFromLastChallengeRequest;

	private Challenge ActiveChallenge__BackingField;

	private DateTime DeviceDateOfLastChallengeRequest__BackingField;

	public Challenge ActiveChallenge
	{
		get
		{
			return ActiveChallenge__BackingField;
		}
		private set
		{
			ActiveChallenge__BackingField = value;
		}
	}

	public DateTime DeviceDateOfLastChallengeRequest
	{
		get
		{
			return DeviceDateOfLastChallengeRequest__BackingField;
		}
		private set
		{
			DeviceDateOfLastChallengeRequest__BackingField = value;
		}
	}

	private void Awake()
	{
	}

	private void LoadMetaData()
	{
	}

	private ChallengeMetadata GetFallback()
	{
		return null;
	}

	private IEnumerable<ChallengeMetadata> ExpandableFallbackList()
	{
		return null;
	}

	public void SaveCompletedChallenges()
	{
	}

	public void LoadCompletedChallenges()
	{
	}

	private void MigrateChallenges()
	{
	}

	private void MigrateChallengesIOS()
	{
	}

	public void MigrateChallengesAndroid()
	{
	}

	private IEnumerator UploadCompletedChallenge(WWWForm postData)
	{
		return null;
	}

	private IEnumerator UploadQueuedOfflineChallenges(WWWForm postData, int challengeID)
	{
		return null;
	}

	public void RequestChallenge()
	{
	}

	private IEnumerator GetOnlineChallenge()
	{
		return null;
	}

	private void TranslateWebResponse(string response)
	{
	}

	private bool CheckForOverride(int currentChallengeId, out int newChallengeId)
	{
		newChallengeId = default;
		return false;
	}

	public ChallengeMetadata GetMetadataForChallenge(int pChallengeID)
	{
		return null;
	}

	public bool CheckForChallengeCompletionIfApplicable()
	{
		return false;
	}

	public void CheckForUnsubmittedChallengeCompletions()
	{
	}

	public void CompleteChallenge(int challengeID = 0)
	{
	}

	private void PostCompletedChallengeID(int pTheChallengeID)
	{
	}

	private void GetActiveChallenge(Challenge theChallenge)
	{
	}

	public CompletedChallenge GetCompletionForChallenge(int pChallengeID)
	{
		return null;
	}

	public bool IsActiveChallengeComplete()
	{
		return false;
	}

	private bool IsChallengeComplete(int challengeID)
	{
		return false;
	}

	private bool IsChallengeComplete(Challenge challenge)
	{
		return false;
	}

	public int GetChallengeIDForChallengeName(string name)
	{
		return 0;
	}

	public string GetAppID()
	{
		return null;
	}

	public int MinutesBetweenActiveChallengeScrollers()
	{
		return 0;
	}

	public int MinutesBetweenUpcomingChallengeScrollers()
	{
		return 0;
	}

	public int GetSecondsUntilNextChallenge()
	{
		return 0;
	}

	public bool CheckOfflineCompletedChallenges()
	{
		return false;
	}

	public static List DevChallengeList()
	{
		return null;
	}

	public static List<ChallengeMetadata> UnfinishedLegacyChallengesList()
	{
		return null;
	}

	public static bool LegacyChallengeIsIneligibleForPurchase(ChallengeMetadata metadata)
	{
		return false;
	}

	public static int TotalAccessibleChallengeCount()
	{
		return 0;
	}

	public static List<Type> FutureChallengesList()
	{
		return null;
	}

	public bool TryAddChallengeItemToList(ChallengeMetadata metaData, bool checkVault, Action<ChallengeMetadata, int, int> addItem)
	{
		return false;
	}

	public bool TryAddUnCompletedChallenges(bool activeOnly, Action<ChallengeMetadata, int, int> addItem)
	{
		return false;
	}

	public bool TryAddActiveChallenge(Action<ChallengeMetadata, int, int> addItem)
	{
		return false;
	}

	public bool TryAddUnFinishedLegacyChallenges(Action<ChallengeMetadata, int, int> addItem)
	{
		return false;
	}
}
