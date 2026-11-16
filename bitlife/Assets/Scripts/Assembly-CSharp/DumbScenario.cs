using System;

public class DumbScenario : Scenario
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<char, bool> _003C_003E9__54_0;

		internal bool _003CRefreshScenario_003Eb__54_0(char t)
		{
			return false;
		}
	}

	protected bool _diagnostics;

	public DataDumbScenario Data;

	public SimPerson Friend;

	public SimSpecialJobPolitician PoliticianSpecialJob;

	public SimPoliticalCampaign PoliticalCampaign;

	public SimSpecialJobAthlete AthleteSpecialJob;

	public SimSpecialJobMafia MafiaSpecialJob;

	public SimSpecialJobMusician MusicianSpecialJob;

	public SimMusicAlbum MusicAlbum;

	public SimSpecialJobActor ActorSpecialJob;

	public SimSpecialJobHustler HustlerSpecialJob;

	public SimSpecialJobBusiness BusinessSpecialJob;

	public SimPropertyLeaseContract LeaseContract;

	public SimSpecialJobAstronaut AstronautSpecialJob;

	public SimSpaceMission AstronautMission;

	public SimSpecialJobModel ModelSpecialJob;

	public SimBlackMarketVendor BlackMarketVendor;

	public SimMuseum BlackMarketMuseum;

	public SimDisplayItem MuseumExhibit;

	public SimCult TheCult;

	public SimCommunard ACommunard;

	public SimSpyAgency TheSpyAgency;

	public SimSecretAgent TheSecretAgent;

	public SimZoo TheZoo;

	public SimCasino TheCasino;

	public SimCasinoGambler TheCasinoGambler;

	public SimCasinoGameRoom TheCasinoGameRoom;

	public SimZooEmployee TheZooEmployee;

	public SimZooAnimal TheZooAnimal;

	public SimSpecialJobDealer TheDealer;

	public SimDealerCartelMember CartelMember;

	public DumbScenario(DataDumbScenario theData, SimPerson theFriend, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioPolitical theData, SimSpecialJobPolitician theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioPolitical theData, SimPoliticalCampaign theCampaign, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpecialJobAthlete theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpecialJobMusician theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimMusicAlbum theAlbum, SimSpecialJobMusician theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioMafia theData, SimSpecialJobMafia theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioActor theData, SimSpecialJobActor theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioHustler theData, SimSpecialJobHustler theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioBusiness theData, SimSpecialJobBusiness theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioLandlord theData, SimPropertyLeaseContract randomContract, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpecialJobAstronaut theSpecialJob, SimSpaceMission mission, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpecialJobAstronaut theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpecialJobModel theSpecialJob, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimSpyAgency theAgency, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimZoo theZoo, bool inDiagnostics, SimZooAnimal theZooAnimal = null, SimZooEmployee theZooEmployee = null)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimCasino theCasino, bool inDiagnostics, SimCasinoGambler theCasinoGambler = null, SimCasinoGameRoom theCasinoGameRoom = null)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimBlackMarketVendor theVendor, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimMuseum theMuseum, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimDisplayItem theExhibit, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenario theData, SimCult theCult, SimCommunard aCommunard, bool inDiagnostics)
	{
	}

	public DumbScenario(DataDumbScenarioDealer theData, SimSpecialJobDealer theSpecialJob, bool inDiagnostics)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
