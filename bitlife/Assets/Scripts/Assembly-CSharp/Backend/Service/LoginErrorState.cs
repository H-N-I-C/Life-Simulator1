namespace Backend.Service
{
	public enum LoginErrorState
	{
		RemoteData = 0,
		NoLocalData = 1,
		ErrorOnAutoReconnect = 2,
		DataInconsistency = 3,
		NewDataAvailable = 4
	}
}
