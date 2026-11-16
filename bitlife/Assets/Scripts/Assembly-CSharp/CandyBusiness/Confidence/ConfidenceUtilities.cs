using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UnityEngine;

namespace CandyBusiness.Confidence
{
	public static class ConfidenceUtilities
	{
		[Serializable]
		private sealed class _003C_003Ec__0<T>
		{
			public static readonly _003C_003Ec__0<T> _003C_003E9;

			public static Func<FieldInfo, bool> _003C_003E9__0_0;

			public static Func<FieldInfo, object> _003C_003E9__0_1;

			internal bool _003CGetNestedStaticReadonlyFieldsOfType_003Eb__0_0(FieldInfo x)
			{
				return false;
			}

			internal object _003CGetNestedStaticReadonlyFieldsOfType_003Eb__0_1(FieldInfo x)
			{
				return null;
			}
		}

		private sealed class _003CEnumerateOwnedMemberValues_003Ed__9 : IEnumerable<object>, IEnumerable, IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private object instance;

			public object _003C_003E3__instance;

			private BindingFlags flags;

			public BindingFlags _003C_003E3__flags;

			private Type root;

			public Type _003C_003E3__root;

			private Type _003Ctype_003E5__2;

			private FieldInfo[] _003C_003E7__wrap2;

			private int _003C_003E7__wrap3;

			private PropertyInfo[] _003C_003E7__wrap4;

			object IEnumerator<object>.Current => null;

			object IEnumerator.Current => null;

			public _003CEnumerateOwnedMemberValues_003Ed__9(int _003C_003E1__state)
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

			IEnumerator<object> IEnumerable<object>.GetEnumerator()
			{
				return null;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CToTask_003Ed__11 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder _003C_003Et__builder;

			public Awaitable awaitable;

			private Awaitable.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[StructLayout((LayoutKind)3)]
		private struct _003CToTask_003Ed__12<T> : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncTaskMethodBuilder<T> _003C_003Et__builder;

			public Awaitable<T> awaitable;

			private Awaitable<T>.Awaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		public static HashSet<T> GetNestedStaticReadonlyFieldsOfType<T>(Type root)
		{
			return null;
		}

		public static Dictionary<TValue, TAttr> GetNestedStaticReadonlyFieldsWithAttribute<TValue, TAttr>(Type root) where TAttr : Attribute
		{
			return null;
		}

		public static void SafeInvoke(Action action, string context = null)
		{
		}

		public static void SafeInvoke<T>(Action<T> action, T arg, string context = null)
		{
		}

		public static void SafeInvoke<T1, T2>(Action<T1, T2> action, T1 arg1, T2 arg2, string context = null)
		{
		}

		private static void ReportListenerException(Exception e, string context)
		{
		}

		private static void CollectStaticReadonlyFieldsWithAttribute<TValue, TAttr>(Type root, Dictionary<TValue, TAttr> found) where TAttr : Attribute
		{
		}

		public static void DisposeRecursive(Type root, object instance)
		{
		}

		private static void DisposeRecursive(Type root, object instance, HashSet<object> visited)
		{
		}

		private static IEnumerable<object> EnumerateOwnedMemberValues(Type root, object instance, BindingFlags flags)
		{
			return null;
		}

		private static bool IsOwnedMemberType(Type root, Type memberType)
		{
			return false;
		}

		public static Task ToTask(Awaitable awaitable)
		{
			return null;
		}

		public static Task<T> ToTask<T>(Awaitable<T> awaitable)
		{
			return null;
		}
	}
}
