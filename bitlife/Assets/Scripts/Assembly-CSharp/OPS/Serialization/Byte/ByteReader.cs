using System;
using System.Text;
using UnityEngine;

namespace OPS.Serialization.Byte
{
	internal class ByteReader
	{
		private Buffer m_buf;

		private const int maxStringLength = 65535;

		private const int maxByteLength = 2147483647;

		private const int k_InitialStringBufferSize = 1024;

		private static byte[] s_StringReaderBuffer;

		private static Encoding s_Encoding;

		private static UIntFloat s_FloatConverter;

		private static LongDouble s_DoubleConverter;

		public uint Position => 0u;

		public int Length => 0;

		public ByteReader()
		{
		}

		public ByteReader(byte[] buffer)
		{
		}

		private static void Initialize()
		{
		}

		public void SeekZero()
		{
		}

		internal void Replace(byte[] buffer)
		{
		}

		public uint ReadPackedUInt32()
		{
			return 0u;
		}

		public ulong ReadPackedUInt64()
		{
			return 0uL;
		}

		public object Read(Type _Type)
		{
			return null;
		}

		public byte ReadByte()
		{
			return 0;
		}

		public sbyte ReadSByte()
		{
			return 0;
		}

		public short ReadInt16()
		{
			return 0;
		}

		public ushort ReadUInt16()
		{
			return 0;
		}

		public int ReadInt32()
		{
			return 0;
		}

		public uint ReadUInt32()
		{
			return 0u;
		}

		public long ReadInt64()
		{
			return 0L;
		}

		public ulong ReadUInt64()
		{
			return 0uL;
		}

		public decimal ReadDecimal()
		{
			return 0m;
		}

		public float ReadSingle()
		{
			return 0f;
		}

		public double ReadDouble()
		{
			return 0.0;
		}

		public string ReadString()
		{
			return null;
		}

		public char ReadChar()
		{
			return '\0';
		}

		public bool ReadBoolean()
		{
			return false;
		}

		public byte[] ReadBytes(int count)
		{
			return null;
		}

		public byte[] ReadBytesAndSize()
		{
			return null;
		}

		public Vector2 ReadVector2()
		{
			return default;
		}

		public Vector3 ReadVector3()
		{
			return default;
		}

		public Vector4 ReadVector4()
		{
			return default;
		}

		public Color ReadColor()
		{
			return default;
		}

		public Color32 ReadColor32()
		{
			return default;
		}

		public Quaternion ReadQuaternion()
		{
			return default;
		}

		public Rect ReadRect()
		{
			return default;
		}

		public Plane ReadPlane()
		{
			return default;
		}

		public Ray ReadRay()
		{
			return default;
		}

		public Matrix4x4 ReadMatrix4x4()
		{
			return default;
		}

		public override string ToString()
		{
			return null;
		}

		private object GetDefaultValue(Type _Type)
		{
			return null;
		}
	}
}
