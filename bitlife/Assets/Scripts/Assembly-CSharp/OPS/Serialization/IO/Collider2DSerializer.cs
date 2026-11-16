using System;

namespace OPS.Serialization.IO
{
	internal class Collider2DSerializer : ISerializerComponent
	{
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
