using System;

[Serializable]
public class SimRacingSponsor
{
	private string SponsorName__BackingField;

	private bool UsesDefiniteArticle__BackingField;

	public string SponsorName
	{
		get
		{
			return SponsorName__BackingField;
		}
		private set
		{
			SponsorName__BackingField = value;
		}
	}

	public bool UsesDefiniteArticle
	{
		get
		{
			return UsesDefiniteArticle__BackingField;
		}
		private set
		{
			UsesDefiniteArticle__BackingField = value;
		}
	}

	public SimRacingSponsor(string sponsorName, bool usesDefiniteArticle)
	{
	}
}
