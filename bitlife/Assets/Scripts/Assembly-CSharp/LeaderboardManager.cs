using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class LeaderboardManager
{
	private sealed class _003CGetScoresWithDelegate_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public LeaderboardManager _003C_003E4__this;

		private UnityWebRequest _003Cwww_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetScoresWithDelegate_003Ed__23(int _003C_003E1__state)
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

	private sealed class _003CPostToServer_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Dictionary<string, object> allScoresDictionary;

		private UnityWebRequest _003Cwww_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CPostToServer_003Ed__21(int _003C_003E1__state)
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

	public static string LeaderboardURLString;

	public static string DevModeGetScoresVersion;

	public static int LeaderboardTimeoutSeconds;

	public static int NumScoresToPull;

	public DateTime LastPullDate;

	public ArrayList ScoresByAgeArray;

	public ArrayList ScoresByNetWorthArray;

	public ArrayList ScoresByChildrenArray;

	public ArrayList ScoresByLoversArray;

	public List<Grave> SubmittingGravesArray;

	private static LeaderboardManager inst;

	public static LeaderboardManager Inst
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public void Initialize()
	{
	}

	public bool IsSubmittingToLeaderboardAvailable()
	{
		return false;
	}

	public bool IsViewingLeaderboardAvailable()
	{
		return false;
	}

	public int CountOfLists()
	{
		return 0;
	}

	public string GetUserID()
	{
		return null;
	}

	public void SetUserID(string userID)
	{
	}

	public void SubmitOutstandingScores()
	{
	}

	private IEnumerator PostToServer(Dictionary<string, object> allScoresDictionary)
	{
		return null;
	}

	public string FilterNameForProfanity(string theName)
	{
		return null;
	}

	public IEnumerator GetScoresWithDelegate()
	{
		return null;
	}

	public LeaderboardScore GetScoreFromScoreDictionary(Dictionary<string, object> dictionary)
	{
		return null;
	}
}
