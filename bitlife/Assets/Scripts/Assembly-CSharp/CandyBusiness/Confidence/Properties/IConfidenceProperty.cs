namespace CandyBusiness.Confidence.Properties
{
	public interface IConfidenceProperty
	{
		string CacheKey { get; }

		object CacheValue { get; }

		void Resolve(ConfidenceRuntime runtime);

		void Load(ConfidenceSnapshot snapshot);
	}
}
