public class WeightedEntry<T>
{
	private readonly float Weight__BackingField;

	private readonly T Value__BackingField;

	public float Weight => Weight__BackingField;

	public T Value => Value__BackingField;

	public WeightedEntry(T value, float weight)
	{
	}
}
