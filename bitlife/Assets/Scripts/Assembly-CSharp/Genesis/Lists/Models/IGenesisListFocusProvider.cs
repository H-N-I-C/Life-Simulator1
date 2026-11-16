using Genesis.Controllers;

namespace Genesis.Lists.Models
{
	public interface IGenesisListFocusProvider
	{
		void ApplyFocus(ScenarioController controller, string focusMemoryKey);
	}
}
