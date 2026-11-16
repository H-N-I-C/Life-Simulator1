public class UnityKeyValuePair<K, V>
{
	private K Key__BackingField;

	private V Value__BackingField;

	public virtual K Key
	{
		get
		{
			return Key__BackingField;
		}
		set
		{
			Key__BackingField = value;
		}
	}

	public virtual V Value
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

	public UnityKeyValuePair()
	{
	}

	public UnityKeyValuePair(K key, V value)
	{
	}
}
