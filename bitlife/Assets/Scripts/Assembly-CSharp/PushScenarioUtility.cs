using System;
using System.Collections.Generic;
using System.Reflection;

public static class PushScenarioUtility
{
	[Serializable]
	private sealed class _003C_003Ec__2<T> where T : Attribute
	{
		public static readonly _003C_003Ec__2<T> _003C_003E9;

		public static Func<MethodInfo, bool> _003C_003E9__2_0;

		internal bool _003CGetStaticAttributeMethods_003Eb__2_0(MethodInfo x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass9_0
	{
		public string id;

		internal bool _003CValidateScenarioHasUniqueID_003Eb__0(Type x)
		{
			return false;
		}
	}

	public static PushScenarioAttribute GetPushScenarioAttribute(Type scenarioType)
	{
		return null;
	}

	public static ConstructorInfo GetDefaultConstructor(Type scenarioType)
	{
		return null;
	}

	public static IEnumerable<MethodInfo> GetStaticAttributeMethods<T>(Type scenarioType) where T : Attribute
	{
		return null;
	}

	public static bool ValidatePushScenarioType(Type type)
	{
		return false;
	}

	public static Type FindScenarioByID(string id)
	{
		return null;
	}

	public static Type[] GetScenarioTypesMatching(string category, string subCategory = null)
	{
		return null;
	}

	public static Type[] GetPushScenarioTypes()
	{
		return null;
	}

	private static Assembly GetGameAssembly()
	{
		return null;
	}

	private static bool ValidateTypeIsPushScenario(Type scenarioType)
	{
		return false;
	}

	private static bool ValidateScenarioHasUniqueID(Type scenarioType)
	{
		return false;
	}

	private static bool ValidateScenarioHasValidation(Type scenarioType)
	{
		return false;
	}

	private static bool ValidateScenarioHasValidConstructor(Type scenarioType)
	{
		return false;
	}
}
