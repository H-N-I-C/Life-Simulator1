namespace Game.Translation
{
	public class TranslationServerCacheInfo
	{
		private int cacheHit__BackingField;

		private int cachedItems__BackingField;

		public int cacheHit
		{
			get
			{
				return cacheHit__BackingField;
			}
			set
			{
				cacheHit__BackingField = value;
			}
		}

		public int cachedItems
		{
			get
			{
				return cachedItems__BackingField;
			}
			set
			{
				cachedItems__BackingField = value;
			}
		}
	}
}
