public class DataBankRobberyWeapon
{
	private string Name__BackingField;

	private bool IsPlural__BackingField;

	private int EffectivenessScore__BackingField;

	public string Name
	{
		get
		{
			return Name__BackingField;
		}
		set
		{
			Name__BackingField = value;
		}
	}

	public bool IsPlural
	{
		get
		{
			return IsPlural__BackingField;
		}
		set
		{
			IsPlural__BackingField = value;
		}
	}

	public int EffectivenessScore
	{
		get
		{
			return EffectivenessScore__BackingField;
		}
		set
		{
			EffectivenessScore__BackingField = value;
		}
	}

	public string GetNameWithArticle()
	{
		return null;
	}
}
