using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Lists.Runtime
{
	internal static class GenesisListPreBuildHookBootstrap
	{
		private struct GenesisListPreBuildHookRegistration
		{
			private readonly IReadOnlyList<string> ListIds__BackingField;

			private readonly string DataSourceKey__BackingField;

			private readonly Action<GenesisListDefinition, ScenarioController> Hook__BackingField;

			public IReadOnlyList<string> ListIds => ListIds__BackingField;

			public string DataSourceKey => DataSourceKey__BackingField;

			public Action<GenesisListDefinition, ScenarioController> Hook => Hook__BackingField;

			public GenesisListPreBuildHookRegistration(IReadOnlyList<string> listIds, string dataSourceKey, Action<GenesisListDefinition, ScenarioController> hook)
			{
				ListIds__BackingField = null;
				DataSourceKey__BackingField = null;
				Hook__BackingField = null;
			}
		}

		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<Type, bool> _003C_003E9__3_0;

			public static Func<string, bool> _003C_003E9__3_1;

			internal bool _003CDiscoverHooks_003Eb__3_0(Type type)
			{
				return false;
			}

			internal bool _003CDiscoverHooks_003Eb__3_1(string id)
			{
				return false;
			}
		}

		private sealed class _003CDiscoverHooks_003Ed__3 : IEnumerable<GenesisListPreBuildHookRegistration>, IEnumerable, IEnumerator<GenesisListPreBuildHookRegistration>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private GenesisListPreBuildHookRegistration _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private Assembly[] _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			private Type[] _003C_003E7__wrap3;

			private int _003C_003E7__wrap4;

			private MethodInfo[] _003C_003E7__wrap5;

			private int _003C_003E7__wrap6;

			private MethodInfo _003Cmethod_003E5__8;

			private IEnumerator<GenesisListPreBuildHookAttribute> _003C_003E7__wrap8;

			GenesisListPreBuildHookRegistration IEnumerator<GenesisListPreBuildHookRegistration>.Current => default;

			object IEnumerator.Current => null;

			public _003CDiscoverHooks_003Ed__3(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			void IEnumerator.Reset()
			{
			}

			IEnumerator<GenesisListPreBuildHookRegistration> IEnumerable<GenesisListPreBuildHookRegistration>.GetEnumerator()
			{
				return null;
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return null;
			}
		}

		private static bool _initialized;

		private static void RuntimeInitialize()
		{
		}

		internal static void EnsureInitialized()
		{
		}

		private static IEnumerable<GenesisListPreBuildHookRegistration> DiscoverHooks()
		{
			return null;
		}

		private static bool TryCreateDelegate(MethodInfo method, out Action<GenesisListDefinition, ScenarioController> hook)
		{
			hook = null;
			return false;
		}
	}
}
