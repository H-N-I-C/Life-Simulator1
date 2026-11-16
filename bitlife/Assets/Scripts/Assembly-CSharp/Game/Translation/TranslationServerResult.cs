using System.Collections.Generic;

namespace Game.Translation
{
	public class TranslationServerResult
	{
		private List<TranslationServerResultItem> Entries__BackingField;

		private TranslationServerCacheInfo CacheInfo__BackingField;

		private TranslationServerStats TranslationStats__BackingField;

		private TranslationTokenConsumption TokenConsumption__BackingField;

		public List<TranslationServerResultItem> Entries
		{
			get
			{
				return Entries__BackingField;
			}
			set
			{
				Entries__BackingField = value;
			}
		}

		public TranslationServerCacheInfo CacheInfo
		{
			get
			{
				return CacheInfo__BackingField;
			}
			set
			{
				CacheInfo__BackingField = value;
			}
		}

		public TranslationServerStats TranslationStats
		{
			get
			{
				return TranslationStats__BackingField;
			}
			set
			{
				TranslationStats__BackingField = value;
			}
		}

		public TranslationTokenConsumption TokenConsumption
		{
			get
			{
				return TokenConsumption__BackingField;
			}
			set
			{
				TokenConsumption__BackingField = value;
			}
		}
	}
}
