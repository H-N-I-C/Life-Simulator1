using System;
using System.Collections;
using System.Collections.Generic;
using Gadsme;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CandyBusiness.NativeAds
{
	public sealed class GadsmeNativePlacementInteraction : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		private sealed class _003CResetInteractionAfterTimeout_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public GadsmeNativePlacementInteraction _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CResetInteractionAfterTimeout_003Ed__6(int _003C_003E1__state)
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

		private const float InteractionTimeoutSeconds = 5f;

		private GadsmePlacementBanner _placement;

		private bool _interactionInProgress;

		private Coroutine _interactionTimeoutCoroutine;

		public void Initialize(GadsmePlacementBanner placement)
		{
		}

		public void OnPointerClick(PointerEventData eventData)
		{
		}

		private IEnumerator ResetInteractionAfterTimeout()
		{
			return null;
		}

		private void ResetInteractionState()
		{
		}

		private void OnApplicationFocus(bool hasFocus)
		{
		}

		private void OnApplicationPause(bool pauseStatus)
		{
		}
	}
}
