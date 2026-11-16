using System;

[Serializable]
public class CustomTeam
{
	public int TeamIndex;

	public int OverrideColorPrimary;

	public int OverrideColorSecondary;

	public string OverrideName;

	public string OverrideNearestCity;

	private CustomTeam()
	{
	}

	public CustomTeam(int theTeamIndex)
	{
	}
}
