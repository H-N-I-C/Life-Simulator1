using Game.Seasons.Models;

namespace Game.Seasons.Controllers
{
	public class QueueSaveData
	{
		public string JsonString;

		public QUEUE_TYPE QueueType;

		public int CoolDown;

		public QueueSaveData()
		{
		}

		public QueueSaveData(SeasonsPlayerDataModel playerData, int coolDown = 10)
		{
		}

		public QueueSaveData(QUEUE_TYPE actionType, int coolDown = 10)
		{
		}
	}
}
