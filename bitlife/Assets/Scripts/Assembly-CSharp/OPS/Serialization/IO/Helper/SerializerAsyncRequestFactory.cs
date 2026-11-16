using System;

namespace OPS.Serialization.IO.Helper
{
	public static class SerializerAsyncRequestFactory
	{
		public static SerializerAsyncRequest CreateSerializerAsyncRequest(object _Object, bool _Compress = false, bool _Encrypt = false, string _EncryptionKey = "")
		{
			return null;
		}

		public static SerializerAsyncRequest CreateDeserializerAsyncRequest(Type _Type, byte[] _Bytes, bool _Uncompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
			return null;
		}
	}
}
