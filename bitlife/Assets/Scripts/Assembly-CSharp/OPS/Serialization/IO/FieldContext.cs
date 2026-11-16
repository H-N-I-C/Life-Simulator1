using System;
using System.Collections.Generic;
using System.Reflection;

namespace OPS.Serialization.IO
{
	internal static class FieldContext
	{
		private static object threadHandle;

		private static Dictionary<Type, FieldInfo[]> typeFieldDictionary;

		private static Dictionary<Type, int[]> typeFieldIdDictionary;

		private static Dictionary<Type, bool[]> typeFieldOptionalDictionary;

		public static void LoadFields(Type _Type, out FieldInfo[] _Fields, out int[] _Ids, out bool[] _Optionals)
		{
			_Fields = null;
			_Ids = null;
			_Optionals = null;
		}
	}
}
