using System;
using System.Collections;
using System.Collections.Generic;

public abstract class UnityDictionary<K, V> : IDictionary<K, V>, ICollection<KeyValuePair<K, V>>, IEnumerable<KeyValuePair<K, V>>, IEnumerable
{
	internal sealed class UnityDictionaryEnumerator : IEnumerator<KeyValuePair<K, V>>, IEnumerator, IDisposable
	{
		private KeyValuePair<K, V>[] items;

		private int index;

		object IEnumerator.Current => null;

		public KeyValuePair<K, V> Current => default;

		public KeyValuePair<K, V> Entry => default;

		public K Key => default;

		public V Value => default;

		internal UnityDictionaryEnumerator()
		{
		}

		internal UnityDictionaryEnumerator(UnityDictionary<K, V> ud)
		{
		}

		public void Dispose()
		{
		}

		public bool MoveNext()
		{
			return false;
		}

		private void ValidateIndex()
		{
		}

		public void Reset()
		{
		}
	}

	private sealed class _003C_003Ec__DisplayClass11_0
	{
		public K key;

		internal bool _003CRemove_003Eb__0(UnityKeyValuePair<K, V> x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass13_0
	{
		public K key;

		internal bool _003CContainsKey_003Eb__0(UnityKeyValuePair<K, V> x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass5_0
	{
		public K key;

		internal bool _003Cget_Item_003Eb__0(UnityKeyValuePair<K, V> x)
		{
			return false;
		}
	}

	protected abstract List<UnityKeyValuePair<K, V>> KeyValuePairs { get; set; }

	// C# has no syntax for parameterized property 'Item'.
	public virtual V get_Item(K key)
	{
		return default;
	}

	public virtual void set_Item(K key, V value)
	{
	}

	public int Count => 0;

	public ICollection<K> Keys => null;

	public ICollection<V> Values => null;

	public ICollection<KeyValuePair<K, V>> Items => null;

	public V SyncRoot => default;

	public bool IsFixedSize => false;

	public bool IsReadOnly => false;

	public bool IsSynchronized => false;

	protected abstract void SetKeyValuePair(K k, V v);

	public void Add(K key, V value)
	{
	}

	public void Add(KeyValuePair<K, V> kvp)
	{
	}

	public bool TryGetValue(K key, out V value)
	{
		value = default;
		return false;
	}

	public bool Remove(KeyValuePair<K, V> item)
	{
		return false;
	}

	public bool Remove(K key)
	{
		return false;
	}

	public void Clear()
	{
	}

	public bool ContainsKey(K key)
	{
		return false;
	}

	public bool Contains(KeyValuePair<K, V> kvp)
	{
		return false;
	}

	public void CopyTo(KeyValuePair<K, V>[] array, int index)
	{
	}

	public KeyValuePair<K, V> ConvertUkvp(UnityKeyValuePair<K, V> ukvp)
	{
		return default;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
	{
		return null;
	}
}
public abstract class UnityDictionary<V> : UnityDictionary<string, V>
{
}
