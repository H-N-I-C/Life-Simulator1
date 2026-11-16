using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct WitchDoctorContext : IContextConvert<WitchDoctorContext, FightClubMoveContext>
{
	public FightClubMoveContext Convert(WitchDoctorContext from)
	{
		return default;
	}
}
