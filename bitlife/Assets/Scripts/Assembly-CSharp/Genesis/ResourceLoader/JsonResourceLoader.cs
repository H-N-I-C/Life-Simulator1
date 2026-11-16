using System.Collections.Generic;

namespace Genesis.ResourceLoader
{
	public class JsonResourceLoader : IResourceLoader
	{
		private Dictionary<string, object> _deserializedResources;

		public T[] Serialize<T>(string address)
		{
			return null;
		}
	}
}
