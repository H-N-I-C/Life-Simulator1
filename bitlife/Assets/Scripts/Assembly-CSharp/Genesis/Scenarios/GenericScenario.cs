using System.Collections;
using Genesis.Interfaces;

namespace Genesis.Scenarios
{
	public class GenericScenario : Scenario
	{
		private readonly IScenarioDataSource m_DataSource;

		public GenericScenario(IScenarioDataSource dataSource)
		{
		}

		public override void RefreshScenario()
		{
		}

		public override void ChoiceSelectedAtIndex(int indexZI)
		{
		}

		public override void ChoiceSelectedAtIndex(int indexZI, ArrayList inputFieldStrings, ArrayList dropDownStrings, ArrayList sliderNumbers)
		{
		}
	}
}
