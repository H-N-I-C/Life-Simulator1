public struct MartialArtContext : IContextConvert<MartialArtContext, FightClubMoveContext>
{
	public DataMartialArt martialArt;

	public MartialArtContext(DataMartialArt artStudied)
	{
		martialArt = null;
	}

	public FightClubMoveContext Convert(MartialArtContext from)
	{
		return default;
	}

	private static FightClubMoveContext ConvertToFightClubMoveContext(MartialArtContext context)
	{
		return default;
	}
}
