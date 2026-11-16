using System;
using System.Collections.Generic;
using System.IO;

namespace OPS.Serialization.IO
{
	public static class Serializer
	{
		private static List<ISerializerComponent> serializerComponentList;

		static Serializer()
		{
		}

		public static void RegisterSerializerComponent(ISerializerComponent _SerializerComponent)
		{
		}

		public static byte[] Serialize(object _Object, bool _Compress = false, bool _Encrypt = false, string _EncryptionKey = "")
		{
			return null;
		}

		public static void SerializeToStream(Stream _WriteToStream, object _Object, bool _Compress = false, bool _Encrypt = false, string _EncryptionKey = "")
		{
		}

		internal static byte[] Internal_Serialize(Type _Type, object _Object)
		{
			return null;
		}

		public static T DeSerialize<T>(byte[] _Bytes, bool _DeCompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
			return default;
		}

		public static T DeSerializeFromStream<T>(Stream _ReadFromStream, bool _DeCompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
			return default;
		}

		public static object DeSerialize(Type _Type, byte[] _Bytes, bool _DeCompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
			return null;
		}
	}
}
