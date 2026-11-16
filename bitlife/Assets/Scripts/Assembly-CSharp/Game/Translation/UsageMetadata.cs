using System;

namespace Game.Translation
{
	[Serializable]
	public class UsageMetadata
	{
		public int promptTokenCount;

		public int candidatesTokenCount;

		public int totalTokenCount;
	}
}
