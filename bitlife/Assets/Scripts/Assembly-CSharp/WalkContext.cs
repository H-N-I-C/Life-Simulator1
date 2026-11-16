using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct WalkContext : IContextConvert<WalkContext, FightClubMoveContext>
{
	public FightClubMoveContext Convert(WalkContext from)
	{
		return default;
	}
}
