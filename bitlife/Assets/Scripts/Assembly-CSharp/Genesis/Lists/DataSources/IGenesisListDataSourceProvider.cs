using System.Collections.Generic;
using Genesis.Controllers;
using Genesis.Lists.Models;

namespace Genesis.Lists.DataSources
{
	public interface IGenesisListDataSourceProvider
	{
		IEnumerable<GenesisListDataSourceItem> GetItems(ScenarioController controller, GenesisListDefinition definition);
	}
}
