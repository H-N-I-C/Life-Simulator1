using System.Collections.Generic;

public abstract class DumbScenarioLazyActivity<TData> : Scenario where TData : IDumbScenarioData
{
	private TData _data;

	public bool CanShowScenario => false;

	public TData Data => default;

	private TData LoadData()
	{
		return default;
	}

	public void SetDataForced(TData data)
	{
	}

	protected abstract List<TData> GetDataSet();

	protected abstract bool IsDataValid(TData data);

	public abstract void ApplyImpacts(IDumbChoice choice);

	public abstract void HandleGenderBracket(string genderBracket);

	public abstract Scenario GetScenario();

	public virtual bool MeetCriteria()
	{
		return false;
	}
}
