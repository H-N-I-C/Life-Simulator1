using System.Collections.Generic;

namespace Genesis.Controllers
{
	public struct TitleAndContent<T>
	{
		public string Id;

		public string Title;

		public List<T> Content;

		public bool Shuffle;

		public string ReasonString;
	}
}
