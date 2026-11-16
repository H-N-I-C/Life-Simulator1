using System.Collections;

public class CareerCollectibleManager
{
	protected ArrayList CompletedCareersArray;

	private static CareerCollectibleManager Instance;

	public static CareerCollectibleManager SharedCareerCollectibleManager => null;

	public ArrayList GetAllCareerStringsInOrder()
	{
		return null;
	}

	public CareerCollectibleData GetCareerCollectibleDataForCareer(string pCareer)
	{
		return null;
	}

	public void CompleteCareer(string careerString)
	{
	}

	public bool HasCompletedCareer(string careerString)
	{
		return false;
	}

	public void LoadCompletedCareers()
	{
	}

	public void SaveCareerCollectibles()
	{
	}

	public ArrayList GetCompletedCareerCollectibles()
	{
		return null;
	}

	public int NumberOfCompletedCareers()
	{
		return 0;
	}

	public int NumberOfPossibleCareers()
	{
		return 0;
	}

	public string GetEmojiStringForCareerString(string careerString)
	{
		return null;
	}

	public string GetCareerStringForMilitaryBranchAndStatus(int branch, int status)
	{
		return null;
	}

	public static int YearsRequiredForCareerString(string careerString)
	{
		return 0;
	}
}
