using System.Collections;
using UnityEngine;

public class ChallengeMetadata
{
	public int ChallengeID;

	public string ResourceID;

	public int ChallengeDifficulty;

	public string Name;

	public string ColorfulDescription;

	public ArrayList RequirementsStringArray;

	public string EmojiString;

	public Color ScrollerBgColor;

	public Color ScrollerFgColor;

	public Color NavBarButtonStripeColor;

	public Color NavBarButtonTextColor;

	public bool DidJustComplete()
	{
		return false;
	}

	public virtual ArrayList GetRequirementsInProgressArray()
	{
		return null;
	}
}
