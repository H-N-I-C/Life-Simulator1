using System;
using System.Collections.Generic;
using System.Reflection;

namespace Game.Util
{
	public static class ReflectionPathUtil
	{
		private const BindingFlags MemberFlags = (BindingFlags)125;

		private static readonly Dictionary<string, Type> TypeCache;

		private static readonly object TypeCacheLock;

		public static bool TryGetValue(string path, out object value)
		{
			value = null;
			return false;
		}

		public static bool TryGetValue(object root, string memberPath, out object value)
		{
			value = null;
			return false;
		}

		public static bool TryInvoke(string methodPath, out object result, object[] arguments)
		{
			result = null;
			return false;
		}

		public static bool TryInvoke(object root, string methodPath, out object result, object[] arguments)
		{
			result = null;
			return false;
		}

		public static bool TrySetPropertyValue(string propertyPath, object propertyValue)
		{
			return false;
		}

		public static bool TrySetPropertyValue(string assignmentExpression)
		{
			return false;
		}

		public static bool TrySetPropertyValue(object root, string propertyPath, object propertyValue)
		{
			return false;
		}

		private static bool TryInitializeFromGlobalPath(string path, out List<string> tokens, out object currentTarget, out Type currentType, out int startIndex)
		{
			tokens = null;
			currentTarget = null;
			currentType = null;
			startIndex = default;
			return false;
		}

		private static int GetRootStartIndex(IReadOnlyList<string> tokens, Type rootType)
		{
			return 0;
		}

		private static bool TryTraversePath(IReadOnlyList<string> tokens, int startIndex, int endExclusive, object startTarget, Type startType, out object endTarget, out Type endType)
		{
			endTarget = null;
			endType = null;
			return false;
		}

		private static bool TryReadMember(Type type, object target, string token, out object value, out Type valueType)
		{
			value = null;
			valueType = null;
			return false;
		}

		private static bool TryParseMethodToken(string token, out string methodName)
		{
			methodName = null;
			return false;
		}

		private static bool TryInvokeMethodInternal(Type targetType, object target, string methodName, object[] arguments, out object result, out Type resultType)
		{
			result = null;
			resultType = null;
			return false;
		}

		private static bool TryBuildInvocationArguments(MethodInfo method, object[] providedArguments, out object[] preparedArguments)
		{
			preparedArguments = null;
			return false;
		}

		private static bool TrySetPropertyInternal(Type type, object target, string propertyToken, object propertyValue)
		{
			return false;
		}

		private static bool TryParseAssignmentExpression(string assignmentExpression, out string propertyPath, out string valueExpression)
		{
			propertyPath = null;
			valueExpression = null;
			return false;
		}

		private static bool TryResolveAssignmentValue(string valueExpression, out object value)
		{
			value = null;
			return false;
		}

		private static bool TryCreateInstanceFromExpression(string valueExpression, out object instance)
		{
			instance = null;
			return false;
		}

		private static string UnwrapQuotedString(string value)
		{
			return null;
		}

		private static bool IsWrappedInQuotes(string value)
		{
			return false;
		}

		private static bool TryConvertValue(object rawValue, Type destinationType, out object converted)
		{
			converted = null;
			return false;
		}

		private static bool IsNumericType(Type type)
		{
			return false;
		}

		private static List<string> TokenizePath(string path)
		{
			return null;
		}

		private static Type ResolveType(string typeName)
		{
			return null;
		}

		public static object ConvertType(string value)
		{
			return null;
		}
	}
}
