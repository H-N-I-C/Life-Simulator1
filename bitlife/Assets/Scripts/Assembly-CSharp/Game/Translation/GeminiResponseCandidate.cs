using System;

namespace Game.Translation
{
	[Serializable]
	public class GeminiResponseCandidate
	{
		public string finishReason;

		public GeminiResponseContent content;
	}
}
