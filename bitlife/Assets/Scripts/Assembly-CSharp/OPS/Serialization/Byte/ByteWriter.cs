using System;
using System.Text;
using UnityEngine;

namespace OPS.Serialization.Byte
{
	internal class ByteWriter
	{
		private const int maxStringLength = 65535;

		private const int maxByteLength = 2147483647;

		private Buffer m_Buffer;

		private static Encoding s_Encoding;

		private static byte[] s_StringWriteBuffer;

		private static UIntFloat s_FloatConverter;

		private static LongDouble s_DoubleConverter;

		public uint Position => 0u;

		public ByteWriter()
		{
		}

		public ByteWriter(byte[] buffer)
		{
		}

		public byte[] ToArray()
		{
			return null;
		}

		public byte[] AsArray()
		{
			return null;
		}

		internal ArraySegment<byte> AsArraySegment()
		{
			return default;
		}

		public void WritePackedUInt32(uint value)
		{
		}

		public void WritePackedUInt64(ulong value)
		{
		}

		public void Write(Type _Type, object _Value)
		{
		}

		public void Write(char value)
		{
		}

		public void Write(byte value)
		{
		}

		public void Write(sbyte value)
		{
		}

		public void Write(short value)
		{
		}

		public void Write(ushort value)
		{
		}

		public void Write(int value)
		{
		}

		public void Write(uint value)
		{
		}

		public void Write(long value)
		{
		}

		public void Write(ulong value)
		{
		}

		public void Write(float value)
		{
		}

		public void Write(double value)
		{
		}

		public void Write(decimal value)
		{
		}

		public void Write(string value)
		{
		}

		public void Write(bool value)
		{
		}

		public void Write(byte[] buffer, int count)
		{
		}

		public void Write(byte[] buffer, int offset, int count)
		{
		}

		public void WriteBytesAndSize(byte[] buffer, int count)
		{
		}

		public void WriteBytesFull(byte[] buffer)
		{
		}

		public void Write(Vector2 value)
		{
		}

		public void Write(Vector3 value)
		{
		}

		public void Write(Vector4 value)
		{
		}

		public void Write(Color value)
		{
		}

		public void Write(Color32 value)
		{
		}

		public void Write(Quaternion value)
		{
		}

		public void Write(Rect value)
		{
		}

		public void Write(Plane value)
		{
		}

		public void Write(Ray value)
		{
		}

		public void Write(Matrix4x4 value)
		{
		}
	}
}
