using System;
using System.Collections.Generic;
using OPS.Serialization.Byte;

namespace OPS.Serialization.IO
{
	internal class ClassSerializer : ISerializerComponent
	{
		private static bool HasSerializeClass(Type _Type)
		{
			return false;
		}

		private static int GetInheritanceIndex(Type _Type, Type _InheritanceType)
		{
			return 0;
		}

		private static Type GetInheritanceType(Type _Type, int _InheritanceIndex)
		{
			return null;
		}

		public bool Serialize(Type _Type, object _Object, out byte[] _Bytes)
		{
			_Bytes = null;
			return false;
		}

		private void InternSerialize(object _Object, ByteWriter _Writer, List<Type> _SerializeAbleBaseList, int _CurrentIndex)
		{
		}

		public bool DeSerialize(Type _Type, byte[] _Bytes, out object _Object)
		{
			_Object = null;
			return false;
		}

		private void InternDeSerialize(object _Object, ByteReader _Reader, List<Type> _SerializeAbleBaseList, int _CurrentIndex)
		{
		}
	}
}
