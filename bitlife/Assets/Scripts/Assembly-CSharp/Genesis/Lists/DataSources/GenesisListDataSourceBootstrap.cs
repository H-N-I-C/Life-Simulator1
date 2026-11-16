using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Genesis.Lists.DataSources
{
	internal static class GenesisListDataSourceBootstrap
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<Type, bool> _003C_003E9__4_0;

			internal bool _003CDiscoverAnnotatedProviders_003Eb__4_0(Type type)
			{
				return false;
			}
		}

		private sealed class _003CDiscoverAnnotatedProviders_003Ed__4 : IEnumerable<(string, IGenesisListDataSourceProvider)>, IEnumerable, IEnumerator<(string, IGenesisListDataSourceProvider)>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private (string, IGenesisListDataSourceProvider) _003C_003E2__current;

			private int _003C_003El__initialThreadId;

			private Assembly[] _003C_003E7__wrap1;

			private int _003C_003E7__wrap2;

			private Type[] _003C_003E7__wrap3;

			private int _003C_003E7__wrap4;

			private Type _003Ctype_003E5__6;

			private IGenesisListDataSourceProvider _003Cprovider_003E5__7;

			private GenesisListDataSourceAttribute[] _003C_003E7__wrap7;

			private int _003C_003E7__wrap8;

			(string, IGenesisListDataSourceProvider) IEnumerator<(string, IGenesisListDataSourceProvider)>.Current => System_002ECollections_002EGeneric_002EIEnumerator_003C_0028System_002EStringKey_002CGenesis_002ELists_002EDataSources_002EIGenesisListDataSourceProviderProvider_0029_003E_002Eget_Current();

			object IEnumerator.Current => null;

			public _003CDiscoverAnnotatedProviders_003Ed__4(int _003C_003E1__state)
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

			private (string, IGenesisListDataSourceProvider) System_002ECollections_002EGeneric_002EIEnumerator_003C_0028System_002EStringKey_002CGenesis_002ELists_002EDataSources_002EIGenesisListDataSourceProviderProvider_0029_003E_002Eget_Current()
			{
				return default;
			}

			void IEnumerator.Reset()
			{
			}

			IEnumerator<(string, IGenesisListDataSourceProvider)> IEnumerable<(string, IGenesisListDataSourceProvider)>.GetEnumerator()
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

		private static void Initialize()
		{
		}

		private static IEnumerable<(string, IGenesisListDataSourceProvider)> DiscoverAnnotatedProviders()
		{
			return null;
		}

		private static bool TryCreateProviderInstance(Type type, out IGenesisListDataSourceProvider provider)
		{
			provider = null;
			return false;
		}
	}
}
