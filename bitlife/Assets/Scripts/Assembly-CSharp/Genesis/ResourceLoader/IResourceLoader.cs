namespace Genesis.ResourceLoader
{
	public interface IResourceLoader
	{
		T[] Serialize<T>(string address);
	}
}
