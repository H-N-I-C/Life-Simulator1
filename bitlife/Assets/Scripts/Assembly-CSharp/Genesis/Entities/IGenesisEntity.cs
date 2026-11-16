namespace Genesis.Entities
{
	public interface IGenesisEntity
	{
		string EntityType { get; }

		bool IsAlive { get; }

		string EntityId { get; }

		string GetName();

		string GetPronoun(int pronounType);

		float GetCondition();

		string GetEmoji();

		void IncrementAge();

		void Die();

		int GetAge();

		int GetYearsSinceDeath();
	}
}
