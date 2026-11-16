public class DataBlackMarketStreetChemistCutIngredient
{
	private string Ingredient__BackingField;

	private int CommonnessScore__BackingField;

	private bool CanBeFatal__BackingField;

	public string Ingredient
	{
		get
		{
			return Ingredient__BackingField;
		}
		set
		{
			Ingredient__BackingField = value;
		}
	}

	public int CommonnessScore
	{
		get
		{
			return CommonnessScore__BackingField;
		}
		set
		{
			CommonnessScore__BackingField = value;
		}
	}

	public bool CanBeFatal
	{
		get
		{
			return CanBeFatal__BackingField;
		}
		set
		{
			CanBeFatal__BackingField = value;
		}
	}
}
