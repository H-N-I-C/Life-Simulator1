using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct MeditationContext : IContextConvert<MeditationContext, FightClubMoveContext>
{
	public FightClubMoveContext Convert(MeditationContext from)
	{
		return default;
	}
}
