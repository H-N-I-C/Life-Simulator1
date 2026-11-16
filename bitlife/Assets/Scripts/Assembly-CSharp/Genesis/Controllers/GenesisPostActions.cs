using System;
using System.Collections.Generic;

namespace Genesis.Controllers
{
	public class GenesisPostActions
	{
		[Serializable]
		private sealed class _003C_003Ec
		{
			public static readonly _003C_003Ec _003C_003E9;

			public static Func<string, bool> _003C_003E9__1_0;

			internal bool _003CAddPostProcessAction_003Eb__1_0(string action)
			{
				return false;
			}
		}

		private static List<string> _postProcessActions;

		public static void AddPostProcessAction(List<string> actions)
		{
		}

		public static void PostProcess(ScenarioController owningController)
		{
		}
	}
}
