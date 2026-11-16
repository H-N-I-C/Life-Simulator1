using System;
using System.Collections.Generic;

[Serializable]
public class RandomDeckCollection
{
	private Dictionary<int, RandomDeck> _decks;

	public bool Draw(DECK_ID deckId, float probabilityPercent)
	{
		return false;
	}
}
