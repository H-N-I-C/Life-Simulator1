using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SeacrestManager : MonoBehaviour
{
	private sealed class _003CGetSeacrestSet_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string urlString;

		public SeacrestManager _003C_003E4__this;

		private UnityWebRequest _003Crequest_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetSeacrestSet_003Ed__9(int _003C_003E1__state)
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

	public ObjectList SeacrestActiveSet;

	public ObjectList SeacrestInactiveSet;

	public int DevBackdoorViewIndexZI;

	private static string SeacrestDevURLString;

	private static string SeacrestProdURLString;

	public static SeacrestManager SharedInstance;

	public static string DateOfLastSeacrestPull;

	private void Awake()
	{
	}

	public void UpdateSet()
	{
	}

	private IEnumerator GetSeacrestSet(string urlString)
	{
		return null;
	}

	public int GetViewCountForScenarioName(string scenarioName)
	{
		return 0;
	}

	public void IncrementViewCountForScenarioName(string scenarioName)
	{
	}
}
