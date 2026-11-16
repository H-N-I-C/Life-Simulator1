using System;

namespace OPS.Serialization.IO
{
	public interface ISerializerComponent
	{
		bool Serialize(Type _Type, object _Object, out byte[] _Bytes);

		bool DeSerialize(Type _Type, byte[] _Bytes, out object _Object);
	}
}
