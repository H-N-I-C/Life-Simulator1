using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct HypnotizeContext : IContextConvert<HypnotizeContext, FightClubMoveContext>
{
	public FightClubMoveContext Convert(HypnotizeContext from)
	{
		return default;
	}
}
