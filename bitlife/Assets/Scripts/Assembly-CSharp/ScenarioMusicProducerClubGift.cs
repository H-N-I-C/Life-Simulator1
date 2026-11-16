public class ScenarioMusicProducerClubGift : Scenario
{
	private enum GiftCategory
	{
		NONE = 0,
		Car = 1,
		Jewelry = 2,
		Boat = 3,
		Instrument = 4,
		End = 5
	}

	private readonly ClubTakeoverActivity _clubTakeoverActivity;

	private SimCar _car;

	private SimWatercraft _boat;

	private SimJewel _jewel;

	private SimMusicInstrumentPhysical _instrument;

	private GiftCategory _giftCategory;

	private BracketParser _bracketParser;

	public ScenarioMusicProducerClubGift(ClubTakeoverActivity activity)
	{
	}

	public override void RefreshScenario()
	{
	}

	public override void ChoiceSelectedAtIndex(int indexZI)
	{
	}

	private void PopulateRandomGiftAndSetImage(GiftCategory category)
	{
	}

	private string GetGiftDescription(GiftCategory category)
	{
		return null;
	}

	private void TakeIt()
	{
	}

	private void AcceptGift()
	{
	}

	private void LeaveIt()
	{
	}

	public override string EmojiString()
	{
		return null;
	}
}
