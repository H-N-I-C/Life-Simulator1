using System;
using System.Collections.Generic;

[Serializable]
public class WrappedVars
{
	public Dictionary<int, WrappedYearVars> VarsByYear;

	public Dictionary<int, bool> VarsByYearSeen;
}
