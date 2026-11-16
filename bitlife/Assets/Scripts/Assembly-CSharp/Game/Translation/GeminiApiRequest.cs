using System;

namespace Game.Translation
{
	[Serializable]
	public class GeminiApiRequest
	{
		public GeminiContent contents;

		public GeminiContent system_instruction;

		public GeminiGenerationConfig generationConfig;
	}
}
