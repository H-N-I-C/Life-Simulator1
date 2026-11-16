using System;

namespace OPS.Serialization.IO
{
	public static class SerializerAsync
	{
		private sealed class _003C_003Ec__DisplayClass0_0
		{
			public Action<object, byte[]> _CalledWhenFinished;

			public object _Object;

			public bool _Compress;

			public bool _Encrypt;

			public string _EncryptionKey;

			internal void _003CSerialize_003Eb__0(object _003Cp0_003E)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass2_0
		{
			public Action<object> _CalledWhenFinished;

			public Type _Type;

			public byte[] _Bytes;

			public bool _Uncompress;

			public bool _Decrypt;

			public string _DecryptionKey;

			internal void _003CDeserialize_003Eb__0(object _003Cp0_003E)
			{
			}
		}

		private sealed class _003C_003Ec__DisplayClass4_0<T>
		{
			public Action<T> _CalledWhenFinished;

			public byte[] _Bytes;

			public bool _Uncompress;

			public bool _Decrypt;

			public string _DecryptionKey;

			internal void _003CDeserialize_003Eb__0(object _003Cp0_003E)
			{
			}
		}

		public static void Serialize(Action<object, byte[]> _CalledWhenFinished, object _Object, bool _Compress = false, bool _Encrypt = false, string _EncryptionKey = "")
		{
		}

		private static void SerializeAndCall(Action<object, byte[]> _CalledWhenFinished, object _Object, bool _Compress, bool _Encrypt, string _EncryptionKey)
		{
		}

		public static void Deserialize(Action<object> _CalledWhenFinished, Type _Type, byte[] _Bytes, bool _Uncompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
		}

		private static void DeserializeAndCall(Action<object> _CalledWhenFinished, Type _Type, byte[] _Bytes, bool _Uncompress, bool _Decrypt, string _DecryptionKey)
		{
		}

		public static void Deserialize<T>(Action<T> _CalledWhenFinished, byte[] _Bytes, bool _Uncompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
		}

		private static void DeserializeAndCall<T>(Action<T> _CalledWhenFinished, byte[] _Bytes, bool _Uncompress, bool _Decrypt, string _DecryptionKey)
		{
		}
	}
}
