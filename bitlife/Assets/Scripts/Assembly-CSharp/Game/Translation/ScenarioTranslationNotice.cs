using System;
using System.Collections;

namespace Game.Translation
{
	public sealed class ScenarioTranslationNotice : Scenario
	{
		private readonly bool offline;

		private readonly Action finished;

		private bool completed;

		public ScenarioTranslationNotice(bool offline, Action finished)
		{
		}

		public override string EmojiString()
		{
			return null;
		}

		public override void RefreshScenario()
		{
		}

		public override void DismissSelected()
		{
		}

		private void Complete()
		{
		}

		public override void ChoiceSelectedAtIndex(int indexZI)
		{
		}

		public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputs, ArrayList dropdowns, ArrayList sliders)
		{
		}
	}
}
