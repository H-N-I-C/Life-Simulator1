using System;

namespace OPS.Serialization.IO
{
	internal class ArraySerializer : ISerializerComponent
	{
		private class ArrayIndexer_D_1
		{
			private readonly int totalLength;

			private readonly int lowerBound_0;

			private int current;

			public int Current_0 => 0;

			public ArrayIndexer_D_1(int _Length_0, int _LowerBound_0)
			{
			}

			public bool MoveNext()
			{
				return false;
			}
		}

		private class ArrayIndexer_D_2
		{
			private readonly int totalLength;

			private readonly int lastIndexLength;

			private readonly int length_0;

			private readonly int lowerBound_0;

			private readonly int lowerBound_1;

			private int current;

			private int currentZeroBased_0;

			private int currentZeroBased_1;

			public int Current_0 => 0;

			public int Current_1 => 0;

			public ArrayIndexer_D_2(int _Length_0, int _Length_1, int _LowerBound_0, int _LowerBound_1)
			{
			}

			public bool MoveNext()
			{
				return false;
			}
		}

		private class ArrayIndexer_D_N
		{
			private readonly int rank;

			private readonly int totalLength;

			private readonly int lastIndexLength;

			private readonly int[] lengths;

			private readonly int[] lowerBounds;

			private int current;

			private readonly int[] currentZeroBased;

			private int[] index;

			public int[] Current => null;

			public ArrayIndexer_D_N(int[] lengths, int[] lowerBounds)
			{
			}

			public bool MoveNext()
			{
				return false;
			}
		}

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
