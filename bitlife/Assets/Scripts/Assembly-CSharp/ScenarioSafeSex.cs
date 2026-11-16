using Genesis.Controllers;

public class ScenarioSafeSex : Scenario
{
	private const string SeniorCenterSwingLoverLeftScenarioID = "SENIOR_CENTER_SWING_LOVER_LEFT";

	protected SimLover _lover;

	private SimPerson _friend;

	protected SimBandmate _bandmate;

	protected SimMobster _mobster;

	protected SimAgent _agent;

	protected SimCostar _costar;

	protected SimTenant _tenant;

	protected SimAstronaut _astronaut;

	protected SimCommunard _communard;

	protected SimDealerCartelMember _cartelMember;

	protected SimCasinoCelebrity _celebrity;

	protected SimPerson _cheatedOnFriend;

	protected string _friendLoverRelationship;

	protected bool _condomAvailable;

	protected bool _stdLikely;

	protected int _buttonSafeSexZI;

	protected int _buttonUnsafeSexZI;

	protected int _buttonBackOutZI;

	protected int _buttonPullOutZI;

	protected bool _isWithEx;

	protected string _origin;

	protected SimPerson _swinger;

	protected SimPerson _swingerLover;

	private readonly bool _isSwingingContext;

	private readonly string _swingingFollowupScenarioID;

	private readonly ScenarioController _swingingScenarioController;

	private readonly bool _isHouseParty;

	private readonly string _housePartyRowdinessLevel;

	public ScenarioSafeSex(SimLover theLover, string origin = "")
	{
	}

	public ScenarioSafeSex(SimLover theExLover, bool isEx, string origin = "")
	{
	}

	public ScenarioSafeSex(SimPerson theFriend)
	{
	}

	public ScenarioSafeSex(SimBandmate theBandmate)
	{
	}

	public ScenarioSafeSex(SimMobster theMobster)
	{
	}

	public ScenarioSafeSex(SimAgent theAgent)
	{
	}

	public ScenarioSafeSex(SimCostar theCostar)
	{
	}

	public ScenarioSafeSex(SimTenant theTenant)
	{
	}

	public ScenarioSafeSex(SimAstronaut astronaut)
	{
	}

	public ScenarioSafeSex(SimCommunard communard)
	{
	}

	public ScenarioSafeSex(SimDealerCartelMember member)
	{
	}

	public ScenarioSafeSex(SimCasinoCelebrity celebrity)
	{
	}

	public ScenarioSafeSex(SimLover theLover, SimPerson theCheatedOnFriend, string theFriendLoverRelationship)
	{
	}

	public ScenarioSafeSex(SimLover theLover, HousePartyActivity housePartyActivity)
	{
	}

	public ScenarioSafeSex(SimLover theLover, SimPerson swinger, SimPerson swingerLover, string swingingFollowupScenarioID = "", ScenarioController swingingScenarioController = null)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private static string GetSwingingCancellationNarrative(string loverFirstName, string swingerLoverFirstName, bool swingerLoverNotHook)
	{
		return null;
	}

	private static string GetRandomSwingingCancellationTitle(string[] titles)
	{
		return null;
	}

	private Scenario LoadSwingingFollowupScenario()
	{
		return null;
	}

	private SimLover GetHookupPerson()
	{
		return null;
	}
}
