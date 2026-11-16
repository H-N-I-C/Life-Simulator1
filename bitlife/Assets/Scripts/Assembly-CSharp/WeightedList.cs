using System.Collections;
using System.Collections.Generic;

public class WeightedList<T> : IList<WeightedEntry<T>>, ICollection<WeightedEntry<T>>, IEnumerable<WeightedEntry<T>>, IEnumerable
{
	private sealed class _003C_003Ec__DisplayClass16_0
	{
		public T item;

		internal bool _003CContains_003Eb__0(WeightedEntry<T> x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass19_0
	{
		public T item;

		internal bool _003CRemove_003Eb__0(WeightedEntry<T> x)
		{
			return false;
		}
	}

	private sealed class _003C_003Ec__DisplayClass25_0
	{
		public T item;

		internal bool _003CIndexOf_003Eb__0(WeightedEntry<T> x)
		{
			return false;
		}
	}

	private readonly List<WeightedEntry<T>> _internalList;

	private float _weightedSum;

	private bool _dirty;

	// C# has no syntax for parameterized property 'Item'.
	public WeightedEntry<T> get_Item(int index)
	{
		return null;
	}

	public void set_Item(int index, WeightedEntry<T> value)
	{
	}

	public int Count => 0;

	public bool IsReadOnly => false;

	public WeightedList()
	{
	}

	public WeightedList(IEnumerable<WeightedEntry<T>> initialSet)
	{
	}

	public WeightedList(IEnumerable<T> initialSet, float weight)
	{
	}

	public IEnumerator<WeightedEntry<T>> GetEnumerator()
	{
		return null;
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return null;
	}

	public void AddEntry(T value, float weight)
	{
	}

	public void Add(WeightedEntry<T> item)
	{
	}

	public void AddRange(IEnumerable<WeightedEntry<T>> items)
	{
	}

	public void Clear()
	{
	}

	public bool Contains(WeightedEntry<T> item)
	{
		return false;
	}

	public bool Contains(T item)
	{
		return false;
	}

	public void CopyTo(WeightedEntry<T>[] array, int arrayIndex)
	{
	}

	public bool Remove(WeightedEntry<T> item)
	{
		return false;
	}

	public bool Remove(T item)
	{
		return false;
	}

	public int IndexOf(WeightedEntry<T> item)
	{
		return 0;
	}

	public int IndexOf(T item)
	{
		return 0;
	}

	public void Insert(int index, WeightedEntry<T> item)
	{
	}

	public void RemoveAt(int index)
	{
	}

	public WeightedEntry<T> UpdateItemWeight(T itemToUpdate, float weight)
	{
		return null;
	}

	public WeightedEntry<T> UpdateItemWeight(WeightedEntry<T> itemToUpdate, float weight)
	{
		return null;
	}

	public WeightedEntry<T> GetRandomWeightedEntry()
	{
		return null;
	}

	public bool TryGetRandomWeightedItemAndRemove(out T item)
	{
		item = default;
		return false;
	}

	private void MarkCollectionDirty()
	{
	}

	private void SumWeightedEntries()
	{
	}
}
