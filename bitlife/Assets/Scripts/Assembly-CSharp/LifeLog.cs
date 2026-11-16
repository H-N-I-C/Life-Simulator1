using System;
using System.Collections;
using System.Collections.Generic;
using Game.Translation;

[Serializable]
public class LifeLog
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Converter<LifeLogTranslationEntry, TranslationEntry> _003C_003E9__40_1;

		internal TranslationEntry _003CTryStartTranslation_003Eb__40_1(LifeLogTranslationEntry e)
		{
			return null;
		}
	}

	private sealed class _003C_003Ec__DisplayClass40_0
	{
		public bool complete;

		public LifeLog _003C_003E4__this;

		public object batchIdentity;

		public int generation;

		public List<LifeLogTranslationEntry> batch;

		internal void _003CTryStartTranslation_003Eb__0(List<string> translated)
		{
		}

		internal bool _003CTryStartTranslation_003Eb__2()
		{
			return false;
		}
	}

	private ArrayList _LogArray__BackingField;

	private bool BlankLineQueued__BackingField;

	private int AgeAtLogStart__BackingField;

	private bool IntraYear__BackingField;

	private ArrayList _IntraYearArray__BackingField;

	[NonSerialized]
	private Queue<LifeLogTranslationEntry> pendingTranslations;

	[NonSerialized]
	private object activeBatch;

	private static int activeTranslations;

	private ArrayList _LogArray
	{
		get
		{
			return _LogArray__BackingField;
		}
		set
		{
			_LogArray__BackingField = value;
		}
	}

	public ArrayList LogArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public bool BlankLineQueued
	{
		get
		{
			return BlankLineQueued__BackingField;
		}
		set
		{
			BlankLineQueued__BackingField = value;
		}
	}

	public int AgeAtLogStart
	{
		get
		{
			return AgeAtLogStart__BackingField;
		}
		set
		{
			AgeAtLogStart__BackingField = value;
		}
	}

	public bool IntraYear
	{
		get
		{
			return IntraYear__BackingField;
		}
		set
		{
			IntraYear__BackingField = value;
		}
	}

	private ArrayList _IntraYearArray
	{
		get
		{
			return _IntraYearArray__BackingField;
		}
		set
		{
			_IntraYearArray__BackingField = value;
		}
	}

	public ArrayList IntraYearArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public static bool TranslationPending => false;

	private LifeLog()
	{
	}

	public LifeLog(bool initHere)
	{
	}

	public void IncrementAge(LifeLogIntraYear intraYear = null)
	{
	}

	public void IncrementForIntraYear()
	{
	}

	public void Log(string s)
	{
	}

	public void QueueBlankLine()
	{
	}

	public bool HasContentBeenAddedToCurrentYear()
	{
		return false;
	}

	public void RefreshUI()
	{
	}

	private void QueueLifeLogTranslation(string prefix, string englishText)
	{
	}

	private void TryStartTranslation()
	{
	}

	private void AppendLifeLogEntry(string addString, bool showEntry)
	{
	}
}
