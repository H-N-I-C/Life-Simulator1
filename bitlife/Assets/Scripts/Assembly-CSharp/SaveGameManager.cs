using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveGameManager
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, int> _003C_003E9__11_0;

		public static Func<string, string> _003C_003E9__11_1;

		public static Func<string, int> _003C_003E9__14_1;

		public static Func<string, string> _003C_003E9__14_2;

		internal int _003CGetSavedLife_003Eb__11_0(string str)
		{
			return 0;
		}

		internal string _003CGetSavedLife_003Eb__11_1(string str)
		{
			return null;
		}

		internal int _003CCheckForOldLives_003Eb__14_1(string str)
		{
			return 0;
		}

		internal string _003CCheckForOldLives_003Eb__14_2(string str)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass14_0
	{
		public int index;

		internal bool _003CCheckForOldLives_003Eb__0(SaveSlot x)
		{
			return false;
		}
	}

	private sealed class _003CSaveLife_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public bool startingLifeState;

		public int cachedHeroAge;

		public Action yieldCallback;

		private Life _003CLife_003E5__2;

		private bool _003CsuccessfulSave_003E5__3;

		private string _003CtheFolderPath_003E5__4;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSaveLife_003Ed__8(int _003C_003E1__state)
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

	private static readonly BinaryFormatter lBinaryFormatter;

	private static Action<Life, int> m_OnSaveStarted;

	private static Action<Life, bool> m_OnSaveFinished;

	public static event Action<Life, int> OnSaveStarted
	{
		add
		{
		}
		remove
		{
		}
	}

	public static event Action<Life, bool> OnSaveFinished
	{
		add
		{
		}
		remove
		{
		}
	}

	public static void SaveCurrentLifeAndIsStartingLifeState(bool startingLifeState, Action yieldCallback = null)
	{
	}

	public static IEnumerator SaveLife(bool startingLifeState, Action yieldCallback, int cachedHeroAge)
	{
		return null;
	}

	public static void SaveCurrentLifeBinary()
	{
	}

	public static void ClearSavedLife()
	{
	}

	public static Life GetSavedLife()
	{
		return null;
	}

	public static Life GetTimeMachineSavedLifeForHeroAge(int heroAge)
	{
		return null;
	}

	public static Life GetTimeMachineSavedLifeForHeroAgeWithPath(int heroAge, string dataPath, string folder)
	{
		return null;
	}

	public static void CheckForOldLives()
	{
	}

	public static bool DoesUserHaveStrayLives()
	{
		return false;
	}

	public static bool SavedFileAvailable(int heroAge)
	{
		return false;
	}

	public static Life GetRepeatLife()
	{
		return null;
	}

	public static void CleanUpOprhanedTimeMachineLifeFiles()
	{
	}

	public static string FileName()
	{
		return null;
	}
}
