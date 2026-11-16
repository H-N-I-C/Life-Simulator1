using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using UnityEngine;

public class UILightning : MonoBehaviour
{
	private sealed class _003CAnimateLightning_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CandyImage image;

		public UILightning _003C_003E4__this;

		private float _003CsmallScale_003E5__2;

		private float _003ClargeScale_003E5__3;

		private RectTransform _003Crect_003E5__4;

		private Color _003Ccolor_003E5__5;

		private Color _003ChalfAlpha_003E5__6;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CAnimateLightning_003Ed__9(int _003C_003E1__state)
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

	private sealed class _003CLightningStrikeAnimation_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UILightning _003C_003E4__this;

		private Color _003CshadeColor_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CLightningStrikeAnimation_003Ed__8(int _003C_003E1__state)
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

	public static UILightning Inst;

	private const float DUR_A = 0.05f;

	private const float DUR_B = 0.35f;

	private CandyImage shade;

	private CandyImage lightning1;

	private CandyImage lightning2;

	public void Awake()
	{
	}

	public void LightningStrike()
	{
	}

	private IEnumerator LightningStrikeAnimation()
	{
		return null;
	}

	private IEnumerator AnimateLightning(CandyImage image)
	{
		return null;
	}
}
