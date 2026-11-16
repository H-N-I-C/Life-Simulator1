using System.Collections.Generic;

namespace Genesis.Controllers
{
	public class GenesisCustomScenarios
	{
		private const string MdScenarioResource = "genesis_movie_director";

		private const string MdActionScenarioId = "scenario.movieDirector.action";

		public static bool GetAgeUpScenarios(out List<Scenario> result)
		{
			result = null;
			return false;
		}

		private static void GetMovieDirectorScenario(List<Scenario> list)
		{
		}
	}
}
