using System;
using System.Collections;
using System.Collections.Generic;
using Backend;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyChooseSpecialLife : UIDarkly
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<StuffItem, bool> _003C_003E9__6_0;

			internal bool _003CSetup_003Eb__6_0(StuffItem i)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass7_0
		{
			public StuffItem data;

			internal void _003CSetData_003Eb__1()
			{
			}
		}

		private sealed class _003CSetData_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChooseSpecialLife _003C_003E4__this;

			public List<StuffItem> lives;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CSetData_003Ed__7(int _003C_003E1__state)
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

		private CandyImage titleEmoji;

		private CandyText details;

		private CandyButton buttonTemplate;

		private GameObject stack;

		private List<CandyButton> buttons;

		protected override void Awake()
		{
		}

		public void Setup()
		{
		}

		public IEnumerator SetData(List<StuffItem> lives)
		{
			return null;
		}

		private void _003CSetData_003Eb__7_0(int index, CandyButton item, StuffItem data)
		{
		}
	}
}
