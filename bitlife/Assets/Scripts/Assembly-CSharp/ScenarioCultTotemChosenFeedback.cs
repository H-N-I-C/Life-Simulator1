using Game;

public class ScenarioCultTotemChosenFeedback : Scenario
{
	private string _narrative;

	private readonly SimAuctionItem _auctionItem;

	private readonly SimHeirloom _heirloom;

	private readonly SimJewel _jewel;

	private readonly SimArtifact _artifact;

	private readonly SimModelGift _modelGift;

	private bool _useImpacts;

	private readonly DataCultDefaultTotem _defaultTotem;

	private SimCult _cult;

	public ScenarioCultTotemChosenFeedback(SimAuctionItem auctionItem, bool useImpacts)
	{
	}

	public ScenarioCultTotemChosenFeedback(SimHeirloom heirloom, bool useImpacts)
	{
	}

	public ScenarioCultTotemChosenFeedback(SimJewel jewel, bool useImpacts)
	{
	}

	public ScenarioCultTotemChosenFeedback(SimArtifact artifact, bool useImpacts)
	{
	}

	public ScenarioCultTotemChosenFeedback(DataCultDefaultTotem defaultTotem, bool useImpacts)
	{
	}

	public ScenarioCultTotemChosenFeedback(SimModelGift modelGift, bool useImpacts)
	{
	}

	public override void RefreshScenario()
	{
	}
}
