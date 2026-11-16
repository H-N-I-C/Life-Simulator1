using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIVoteButton : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler
{
	public enum VoteResult
	{
		No = 0,
		Yes = 1
	}

	private enum AnimationState
	{
		Idle = 0,
		Pressed = 1,
		Selected = 2,
		NotSelected = 3
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public VoteResult targetResult;

		internal bool _003CSimulateSimultaneousClicks_003Eb__0(UIVoteButton i)
		{
			return false;
		}
	}

	private sealed class _003CSimulateSimultaneousClicks_003Ed__19 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public VoteResult targetResult;

		public List<UIVoteButton> buttons;

		private _003C_003Ec__DisplayClass19_0 _003C_003E8__1;

		private StringBuilder _003Csb_003E5__2;

		object IEnumerator<object>.Current => null;

		object IEnumerator.Current => null;

		public _003CSimulateSimultaneousClicks_003Ed__19(int _003C_003E1__state)
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

	private VoteResult result;

	private Image image;

	private IUIVoteButtonGroup voteButtonGroup;

	private AnimationState state;

	private Animator animator => null;

	public VoteResult Result => VoteResult.No;

	private void OnEnable()
	{
	}

	private void Update()
	{
	}

	public void Initialize(IUIVoteButtonGroup uiVoteButtonGroup)
	{
	}

	public void OnPointerDown(PointerEventData eventData)
	{
	}

	public void OnPointerUp(PointerEventData eventData)
	{
	}

	public void OnPointerExit(PointerEventData eventData)
	{
	}

	public void NotifySelected(UIVoteButton selected)
	{
	}

	private void SetState(AnimationState state)
	{
	}

	public void AnimationFinished()
	{
	}

	public static IEnumerator SimulateSimultaneousClicks(List<UIVoteButton> buttons, VoteResult targetResult)
	{
		return null;
	}
}
