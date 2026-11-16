using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Backend
{
	public class StoreCertificateTrust : MonoBehaviour
	{
		private class InspectCertificateHandler : CertificateHandler
		{
			protected override bool ValidateCertificate(byte[] certificateData)
			{
				return false;
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public UnityWebRequest www;

			internal void _003CCheckStoreCertificate_003Eb__0(AsyncOperation _)
			{
			}
		}

		private sealed class _003CStart_003Ed__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public StoreCertificateTrust _003C_003E4__this;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CStart_003Ed__1(int _003C_003E1__state)
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

		private static void Init()
		{
		}

		private IEnumerator Start()
		{
			return null;
		}

		private void CheckStoreCertificate()
		{
		}
	}
}
