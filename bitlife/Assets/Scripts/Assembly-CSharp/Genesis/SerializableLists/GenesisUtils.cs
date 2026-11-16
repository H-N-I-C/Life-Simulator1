using System;

namespace Genesis.SerializableLists
{
	public static class GenesisUtils
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<Param, Type> _003C_003E9__2_0;

			public static Func<Param, object> _003C_003E9__2_1;

			internal Type _003CGetScenarioByName_003Eb__2_0(Param param)
			{
				return null;
			}

			internal object _003CGetScenarioByName_003Eb__2_1(Param param)
			{
				return null;
			}
		}

		[Serializable]
		private sealed class _003C_003Ec__3<TResult>
		{
			public static readonly _003C_003Ec__3<TResult> _003C_003E9;

			public static Func<Param, object> _003C_003E9__3_0;

			internal object _003CCallStaticMethod_003Eb__3_0(Param param)
			{
				return null;
			}
		}

		public static bool RunCondition(string rawCondition)
		{
			return false;
		}

		private static bool CheckCondition(string condition)
		{
			return false;
		}

		public static Scenario GetScenarioByName(ScenarioCall scenario)
		{
			return null;
		}

		public static TResult CallStaticMethod<TResult>(StaticCall call)
		{
			return default;
		}
	}
}
