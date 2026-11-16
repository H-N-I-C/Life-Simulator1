public struct HuntAppearanceByLikelihood
{
	public int Likelihood;

	public HuntAppearance HuntAppearance;

	public HuntAppearanceByLikelihood(int likelihood, HuntAppearance huntAppearance)
	{
		Likelihood = 0;
		HuntAppearance = HuntAppearance.None;
	}
}
