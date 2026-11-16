public sealed class SimpleValueSource<T> : ValueSource<T>
{
	private T Value__BackingField;

	public override T Value
	{
		get
		{
			return Value__BackingField;
		}
		set
		{
			Value__BackingField = value;
		}
	}

	public SimpleValueSource()
	{
	}

	public SimpleValueSource(T defaultValue)
	{
	}
}
