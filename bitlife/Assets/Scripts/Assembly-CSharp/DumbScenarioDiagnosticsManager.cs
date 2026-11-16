using System;
using System.Collections;

public class DumbScenarioDiagnosticsManager
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<ListItem, string> _003C_003E9__8_0;

		public static Func<DataDumbScenario, string> _003C_003E9__8_1;

		internal string _003CGetList_003Eb__8_0(ListItem x)
		{
			return null;
		}

		internal string _003CGetList_003Eb__8_1(DataDumbScenario x)
		{
			return null;
		}
	}

	private ArrayList titlesArray;

	private static DumbScenarioDiagnosticsManager sharedDumbScenarioDiagnotics;

	public static DumbScenarioDiagnosticsManager SharedDumbScenarioDiagnosticsManager => null;

	public bool GetTitleExists(string title)
	{
		return false;
	}

	public List GetDumbScenarioDiagnosticsList()
	{
		return null;
	}

	private ListItem GetListItem(string name, int type)
	{
		return null;
	}

	public List GetList(string name, int type, bool placeholderList = true)
	{
		return null;
	}
}
