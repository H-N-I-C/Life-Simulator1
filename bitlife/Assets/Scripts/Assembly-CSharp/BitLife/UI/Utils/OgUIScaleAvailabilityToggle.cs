using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BitLife.UI.Utils
{
	public class OgUIScaleAvailabilityToggle : MonoBehaviour
	{
		private sealed class _003CWaitForManagersThenRefresh_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public OgUIScaleAvailabilityToggle _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CWaitForManagersThenRefresh_003Ed__5(int _003C_003E1__state)
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

		private Canvas targetCanvas;

		private GraphicRaycaster targetRaycaster;

		private Coroutine waitForManagersRoutine;

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private IEnumerator WaitForManagersThenRefresh()
		{
			return null;
		}

		private bool HasScenarioManager()
		{
			return false;
		}

		private void Refresh()
		{
		}
	}
}
