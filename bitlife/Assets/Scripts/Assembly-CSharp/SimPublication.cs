using System;

[Serializable]
public class SimPublication
{
	private string PublicationTitle__BackingField;

	private int PeerReviewValue__BackingField;

	private int HeroAgeAtPublication__BackingField;

	private string PeerReviewColorString__BackingField;

	private bool ReceivedNobelPrize__BackingField;

	private SimDiscovery Discovery__BackingField;

	public string PublicationTitle
	{
		get
		{
			return PublicationTitle__BackingField;
		}
		set
		{
			PublicationTitle__BackingField = value;
		}
	}

	public int PeerReviewValue
	{
		get
		{
			return PeerReviewValue__BackingField;
		}
		set
		{
			PeerReviewValue__BackingField = value;
		}
	}

	public int HeroAgeAtPublication
	{
		get
		{
			return HeroAgeAtPublication__BackingField;
		}
		set
		{
			HeroAgeAtPublication__BackingField = value;
		}
	}

	public string PeerReviewColorString
	{
		get
		{
			return PeerReviewColorString__BackingField;
		}
		set
		{
			PeerReviewColorString__BackingField = value;
		}
	}

	public bool ReceivedNobelPrize
	{
		get
		{
			return ReceivedNobelPrize__BackingField;
		}
		set
		{
			ReceivedNobelPrize__BackingField = value;
		}
	}

	public SimDiscovery Discovery
	{
		get
		{
			return Discovery__BackingField;
		}
		set
		{
			Discovery__BackingField = value;
		}
	}
}
