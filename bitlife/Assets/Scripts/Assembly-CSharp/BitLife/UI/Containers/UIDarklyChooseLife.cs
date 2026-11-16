using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarklyChooseLife : UIDarkly
	{
		private sealed class _003CContinueAsDelay_003Ed__23 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChooseLife _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CContinueAsDelay_003Ed__23(int _003C_003E1__state)
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

		private sealed class _003CCreateNewRandomDelay_003Ed__22 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChooseLife _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CCreateNewRandomDelay_003Ed__22(int _003C_003E1__state)
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

		private sealed class _003CTryAgainDelay_003Ed__21 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public UIDarklyChooseLife _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CTryAgainDelay_003Ed__21(int _003C_003E1__state)
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

		private const float TITLE_TO_DETAILES_SPACE = 25f;

		private RectTransform stack;

		private CandyText continueDetails;

		private CandyButton moreInfoButton;

		private CandyButton continueAsChildButton;

		private GameObject separatorParent;

		private CandyImage separator;

		private CandyButton newLifeButton;

		private CandyButton customLifeButton;

		private CandyButton tryAgainButton;

		private ArrayList childArray;

		private bool bitizenOverride;

		public ArrayList GetChildArray()
		{
			return null;
		}

		public float GetTopOfRect()
		{
			return 0f;
		}

		public void Setup()
		{
		}

		private string GetTextForDetailsLabel()
		{
			return null;
		}

		private void TryAgainButtonPress()
		{
		}

		private void NewCustomLifeButtonPress()
		{
		}

		private void NewRandomLifeButtonPress()
		{
		}

		private void ContinueAsChildButtonPress()
		{
		}

		private void MoreInfoButtonPress()
		{
		}

		private IEnumerator TryAgainDelay()
		{
			return null;
		}

		private IEnumerator CreateNewRandomDelay()
		{
			return null;
		}

		private IEnumerator ContinueAsDelay()
		{
			return null;
		}
	}
}
