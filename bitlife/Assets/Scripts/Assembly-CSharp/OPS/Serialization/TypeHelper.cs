using System;

namespace OPS.Serialization
{
	internal static class TypeHelper
	{
		public static bool IsNullable(Type type)
		{
			return false;
		}

		public static Type GetNullableUnderlyingType(Type type)
		{
			return null;
		}
	}
}
