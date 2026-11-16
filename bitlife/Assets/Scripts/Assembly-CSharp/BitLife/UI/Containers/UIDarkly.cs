using System;
using System.Collections;
using System.Collections.Generic;
using BitLife.UI.CandyObjects;
using BitLife.UI.Managers;
using UnityEngine;

namespace BitLife.UI.Containers
{
	public class UIDarkly : MonoBehaviour
	{
		[Serializable]
		private sealed class _003C_003Ec__31<T, D> where T : Component
		{
			public static readonly _003C_003Ec__31<T, D> _003C_003E9;

			public static Action<T> _003C_003E9__31_0;

			public static Func<T, bool> _003C_003E9__31_1;

			internal void _003CQuickPool_003Eb__31_0(T item)
			{
			}

			internal bool _003CQuickPool_003Eb__31_1(T dataItem)
			{
				return false;
			}
		}

		private sealed class _003CQuickPool_003Ed__31<T, D> : IEnumerator<object>, IEnumerator, IDisposable where T : Component
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public List<T> items;

			public T template;

			public List<D> data;

			public Action<int, T, D> setData;

			private int _003Ci_003E5__2;

			private AsyncInstantiateOperation<T> _003CasyncGo_003E5__3;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CQuickPool_003Ed__31(int _003C_003E1__state)
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

		public UIDarklyManager darklyManager;

		public RectTransform rect;

		public CandyImage background;

		public CandyText title;

		protected UIDARKLY_TYPE type;

		protected Scenario scenario;

		protected int challengeID;

		protected int subGroupType;

		protected DataCountry country;

		public virtual bool ShouldTranslateUI => false;

		protected virtual void Awake()
		{
		}

		public virtual void DoVirtualOverrideMethodForAfterTransitionOntoScreen()
		{
		}

		public virtual void UpdateDarklyButtons()
		{
		}

		public virtual void UpdateDarklyLayout()
		{
		}

		public virtual void ChildPress(int index)
		{
		}

		public virtual int GetChildIndex(int index)
		{
			return 0;
		}

		public virtual void ChildPressMenu2(int index)
		{
		}

		public virtual int GetChildIndexMenu2(int index)
		{
			return 0;
		}

		public virtual void ChildSlide(float value, int index)
		{
		}

		public virtual string GetFirstName()
		{
			return null;
		}

		public virtual string GetLastName()
		{
			return null;
		}

		public virtual int GetGender()
		{
			return 0;
		}

		public virtual DataCountry GetCountry()
		{
			return null;
		}

		public virtual GodModePersonConfig GetPersonConfig()
		{
			return null;
		}

		public virtual int GetSubGroupType()
		{
			return 0;
		}

		public virtual string GetCity()
		{
			return null;
		}

		public virtual void RebuildUI()
		{
		}

		public void SetPosition(Vector2 pos)
		{
		}

		public void SetDarklyType(UIDARKLY_TYPE type)
		{
		}

		public int GetDarklyType()
		{
			return 0;
		}

		protected IEnumerator QuickPool<T, D>(T template, List<T> items, List<D> data, Action<int, T, D> setData) where T : Component
		{
			return null;
		}
	}
}
