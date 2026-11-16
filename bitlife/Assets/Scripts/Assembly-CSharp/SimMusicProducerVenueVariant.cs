public class SimMusicProducerVenueVariant
{
	public struct Range
	{
		public readonly int Min;

		public readonly int Max;

		public Range(int min, int max)
		{
			Min = 0;
			Max = 0;
		}
	}

	public readonly string Title;

	public readonly string EventType;

	public readonly string Body;

	public readonly string Name;

	public readonly VENUE_QUALITY Quality;

	public TALENT_QUALITY TalentQuality;

	public Range Vocal;

	public Range GetVocalRangeByTalentQuality()
	{
		return default;
	}

	private TALENT_QUALITY GetTalentQualityByVenueQuality(VENUE_QUALITY venueQuality, int talentPercent)
	{
		return TALENT_QUALITY.AMATEUR;
	}

	private string GetTitleByQuality(VENUE_QUALITY venueQuality)
	{
		return null;
	}

	private string GetBodyByQuality(VENUE_QUALITY venueQuality)
	{
		return null;
	}
}
