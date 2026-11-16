public static class MusicProfessionExtensions
{
	private const string NONE = "None";

	private const string POP = "Pop";

	private const string RHYTHM_AND_BLUES = "Rhythm and Blues";

	private const string RAP = "Rap";

	private const string HEAVY_METAL = "Heavy Metal";

	private const string K_POP = "K-Pop";

	private const string ROCK = "Rock";

	private const string COUNTRY = "Country";

	private const string ALTERNATIVE = "Alternative";

	private const string FOLK = "Folk";

	private const string REGGAE = "Reggae";

	private const string WORSHIP = "Worship";

	private const string EDM = "EDM";

	private const string HIP_HOP = "Hip-Hop";

	public static MUSIC_PRODUCER_GENRE ToMusicProducerGenre(string musicGenre)
	{
		return MUSIC_PRODUCER_GENRE.NONE;
	}

	public static string AsString(MUSIC_PRODUCER_GENRE genre)
	{
		return null;
	}

	public static string GetEmojiString(MUSIC_PRODUCER_GENRE genre)
	{
		return null;
	}

	public static string AsString(MUSIC_PRODUCER_JOB_TITLE jobTitle)
	{
		return null;
	}
}
