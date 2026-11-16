using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Genesis.Code
{
	public class CustomScenarioCode
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<Assembly, IEnumerable<Type>> _003C_003E9__0_0;

			internal IEnumerable<Type> _003CRunScenarioCode_003Eb__0_0(Assembly s)
			{
				return null;
			}
		}

		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public Type iScenarioCodeType;

			public string id;

			internal bool _003CRunScenarioCode_003Eb__1(Type p)
			{
				return false;
			}
		}

		public static IScenarioCode RunScenarioCode(string id)
		{
			return null;
		}
	}
}
