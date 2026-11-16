namespace GGS.Util
{
	public sealed class Either<T1, T2>
	{
		private readonly bool _isFirstType;

		private readonly T1 _firstTypeValue;

		private readonly T2 _secondTypeValue;

		public Either(T1 value)
		{
		}

		public Either(T2 value)
		{
		}

		public object GetValue()
		{
			return null;
		}

		public bool Is<T>()
		{
			return false;
		}

		public bool TryGetValueAs(out T1 value)
		{
			value = default;
			return false;
		}

		public bool TryGetValueAs(out T2 value)
		{
			value = default;
			return false;
		}
	}
}
