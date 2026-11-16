namespace Backend
{
	public interface IStuffItem
	{
		int Amount { get; set; }

		bool CanUseItem();
	}
}
