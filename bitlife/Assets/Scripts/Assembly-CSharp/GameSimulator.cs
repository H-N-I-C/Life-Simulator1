using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class GameSimulator : MonoBehaviour
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<CandyButton, bool> _003C_003E9__15_0;

		internal bool _003CClickRandomTPanel_003Eb__15_0(CandyButton x)
		{
			return false;
		}
	}

	private sealed class _003CSearchInsideOfTPanel_003Ed__16 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public GameSimulator _003C_003E4__this;

		public CandyButton tPanel;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSearchInsideOfTPanel_003Ed__16(int _003C_003E1__state)
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

	private sealed class _003CSimluationDelay_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public int age;

		public GameSimulator _003C_003E4__this;

		private int _003Ci_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSimluationDelay_003Ed__13(int _003C_003E1__state)
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

	public static GameSimulator Inst;

	public GameObject ScenarioManager;

	public GameObject DarklyManager;

	public CandyButton AgeUpButton;

	public CandyButton NewLifeButton;

	public List<CandyButton> ListOfTPanels;

	public GameObject HorizontalLine;

	public GameObject AlertView;

	public Action<int> Simulate;

	private int TimesEnteredTPanel;

	private bool SearchingTPanel;

	public void Awake()
	{
	}

	private void Simulation(int age = 100)
	{
	}

	private IEnumerator SimluationDelay(int age)
	{
		return null;
	}

	private void DoSimulation()
	{
	}

	private void ClickRandomTPanel()
	{
	}

	private IEnumerator SearchInsideOfTPanel(CandyButton tPanel)
	{
		return null;
	}

	private static void SelectButtonOption(Transform content)
	{
	}
}
