using System;

public class StaticSROptionContainerAttribute : Attribute
{
	private readonly string Category__BackingField;

	public string Category => Category__BackingField;

	public StaticSROptionContainerAttribute(string category)
	{
	}
}
