using System;

namespace Backend
{
	public class QueueDataModel
	{
		public string Slot;

		public FileType Type;

		public Action<bool> Callback;

		public IAuthentication Auth;
	}
}
