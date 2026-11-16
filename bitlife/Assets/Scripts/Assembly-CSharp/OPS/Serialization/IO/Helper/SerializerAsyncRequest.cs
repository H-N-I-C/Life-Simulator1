using System;
using UnityEngine;

namespace OPS.Serialization.IO.Helper
{
	public class SerializerAsyncRequest : CustomYieldInstruction
	{
		private bool serialize;

		private bool isFinished;

		public object Object;

		public byte[] Bytes;

		public bool IsSerialize => false;

		public override bool keepWaiting => false;

		public SerializerAsyncRequest(object _Object, bool _Compress = false, bool _Encrypt = false, string _EncryptionKey = "")
		{
		}

		public SerializerAsyncRequest(Type _Type, byte[] _Bytes, bool _Uncompress = false, bool _Decrypt = false, string _DecryptionKey = "")
		{
		}

		private void OnSerialize(object _Object, byte[] _Bytes)
		{
		}

		private void OnDeserialize(object _Object)
		{
		}
	}
}
