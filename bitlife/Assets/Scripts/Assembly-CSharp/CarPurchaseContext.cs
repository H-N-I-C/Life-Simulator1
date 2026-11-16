using System.Runtime.InteropServices;

[StructLayout((LayoutKind)0, Size = 1)]
public struct CarPurchaseContext : IContextConvert<CarPurchaseContext, FightClubMoveContext>
{
	public FightClubMoveContext Convert(CarPurchaseContext from)
	{
		return default;
	}
}
