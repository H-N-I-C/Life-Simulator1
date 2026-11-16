using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WobbleEffect : FullScreenEffect
{
	private sealed class _003CStartEffect_003Ed__0 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public RawImage image;

		public Texture2D texture;

		private Material _003CeffectInstance_003E5__2;

		private int _003CstrengthPropertyID_003E5__3;

		private float _003Ctime_003E5__4;

		private float _003CeffectDuration_003E5__5;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CStartEffect_003Ed__0(int _003C_003E1__state)
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

	public override IEnumerator StartEffect(RawImage image, Texture2D texture)
	{
		return null;
	}
}
