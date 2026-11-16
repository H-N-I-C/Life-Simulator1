using System;
using System.Collections.Generic;

namespace OPS.Serialization.IO
{
	internal class UnitySerializer : ISerializerComponent
	{
		internal enum EUnityType : byte
		{
			BONEWEIGHT = 0,
			BOUNDS = 1,
			COLOR = 2,
			COLOR32 = 3
		}

		internal static Dictionary<Type, EUnityType> TypeDictionary;

		public bool Serialize(Type _Type, object _Object, out byte[] _Bytes)
		{
			_Bytes = null;
			return false;
		}

		public bool DeSerialize(Type _Type, byte[] _Bytes, out object _Object)
		{
			_Object = null;
			return false;
		}
	}
}
