using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class SimSocialMediaChannel : IPromotableSocialMedia
{
	private int Channel__BackingField;

	private int _Data;

	private int YearsOnChannel__BackingField;

	private int Posts__BackingField;

	private int Followers__BackingField;

	private int FakeFollowers__BackingField;

	private double TotalViews__BackingField;

	private int AgeAtLastPost__BackingField;

	private int NumPostsAtCurrentAge__BackingField;

	private ArrayList _PostTypeArray__BackingField;

	private int NumViralPosts__BackingField;

	private bool PostMonetization__BackingField;

	private double EarningsThisYear__BackingField;

	private double EarningsLastYear__BackingField;

	private double TotalEarnings__BackingField;

	private int AgeAtLastEarnings__BackingField;

	private bool Verified__BackingField;

	private int AgeAtLastVerificationRequest__BackingField;

	private int AgeAtLastBoughtFollowers__BackingField;

	private int NumFollowersBoughtAtCurrentAge__BackingField;

	private ArrayList CelebrityFollowerArray__BackingField;

	private int NumTimesActedCrazy__BackingField;

	private bool Suspended__BackingField;

	private int SuspensionReason__BackingField;

	private int AgeAtSuspension__BackingField;

	private int AgeAtLastSuspensionAppeal__BackingField;

	private int NumSuccessfulAppeals__BackingField;

	private int AgeAtLastOfferedProductPromotion__BackingField;

	private int NumProductsOfferedToPromoteAtCurrentAge__BackingField;

	private int NumTipsAvailableThisAgeUp__BackingField;

	private string LastCollabPersonOnlyFans__BackingField;

	private string LastCollabPostOnlyFans__BackingField;

	private string LastCollabPostTypeOnlyFans__BackingField;

	private int LastCollabPostOnlyFansControversy__BackingField;

	private int AgeAtOnlyFanArgue__BackingField;

	private int AgeAtLastCollabOnlyFans__BackingField;

	private double EarningFeesLastYear__BackingField;

	private List<string> _OnlyFansWishList__BackingField;

	private ArrayList _BlockListArray__BackingField;

	private ArrayList _TrollArray__BackingField;

	private float OnlyFansFee__BackingField;

	private int AgeAtLastChangeOnlyFansFee__BackingField;

	private int AgeAtLastCrossPromotion__BackingField;

	public int Channel
	{
		get
		{
			return Channel__BackingField;
		}
		set
		{
			Channel__BackingField = value;
		}
	}

	public DataSocialMediaChannel Data
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int YearsOnChannel
	{
		get
		{
			return YearsOnChannel__BackingField;
		}
		set
		{
			YearsOnChannel__BackingField = value;
		}
	}

	public int Posts
	{
		get
		{
			return Posts__BackingField;
		}
		set
		{
			Posts__BackingField = value;
		}
	}

	public int Followers
	{
		get
		{
			return Followers__BackingField;
		}
		set
		{
			Followers__BackingField = value;
		}
	}

	public int FakeFollowers
	{
		get
		{
			return FakeFollowers__BackingField;
		}
		set
		{
			FakeFollowers__BackingField = value;
		}
	}

	public double TotalViews
	{
		get
		{
			return TotalViews__BackingField;
		}
		set
		{
			TotalViews__BackingField = value;
		}
	}

	public int AgeAtLastPost
	{
		get
		{
			return AgeAtLastPost__BackingField;
		}
		set
		{
			AgeAtLastPost__BackingField = value;
		}
	}

	public int NumPostsAtCurrentAge
	{
		get
		{
			return NumPostsAtCurrentAge__BackingField;
		}
		set
		{
			NumPostsAtCurrentAge__BackingField = value;
		}
	}

	private ArrayList _PostTypeArray
	{
		get
		{
			return _PostTypeArray__BackingField;
		}
		set
		{
			_PostTypeArray__BackingField = value;
		}
	}

	public ArrayList PostTypeArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int NumViralPosts
	{
		get
		{
			return NumViralPosts__BackingField;
		}
		set
		{
			NumViralPosts__BackingField = value;
		}
	}

	public bool PostMonetization
	{
		get
		{
			return PostMonetization__BackingField;
		}
		set
		{
			PostMonetization__BackingField = value;
		}
	}

	public double EarningsThisYear
	{
		get
		{
			return EarningsThisYear__BackingField;
		}
		set
		{
			EarningsThisYear__BackingField = value;
		}
	}

	public double EarningsLastYear
	{
		get
		{
			return EarningsLastYear__BackingField;
		}
		set
		{
			EarningsLastYear__BackingField = value;
		}
	}

	public double TotalEarnings
	{
		get
		{
			return TotalEarnings__BackingField;
		}
		set
		{
			TotalEarnings__BackingField = value;
		}
	}

	public int AgeAtLastEarnings
	{
		get
		{
			return AgeAtLastEarnings__BackingField;
		}
		set
		{
			AgeAtLastEarnings__BackingField = value;
		}
	}

	public bool Verified
	{
		get
		{
			return Verified__BackingField;
		}
		set
		{
			Verified__BackingField = value;
		}
	}

	public int AgeAtLastVerificationRequest
	{
		get
		{
			return AgeAtLastVerificationRequest__BackingField;
		}
		set
		{
			AgeAtLastVerificationRequest__BackingField = value;
		}
	}

	public int AgeAtLastBoughtFollowers
	{
		get
		{
			return AgeAtLastBoughtFollowers__BackingField;
		}
		set
		{
			AgeAtLastBoughtFollowers__BackingField = value;
		}
	}

	public int NumFollowersBoughtAtCurrentAge
	{
		get
		{
			return NumFollowersBoughtAtCurrentAge__BackingField;
		}
		set
		{
			NumFollowersBoughtAtCurrentAge__BackingField = value;
		}
	}

	public ArrayList CelebrityFollowerArray
	{
		get
		{
			return CelebrityFollowerArray__BackingField;
		}
		set
		{
			CelebrityFollowerArray__BackingField = value;
		}
	}

	public int NumTimesActedCrazy
	{
		get
		{
			return NumTimesActedCrazy__BackingField;
		}
		set
		{
			NumTimesActedCrazy__BackingField = value;
		}
	}

	public bool Suspended
	{
		get
		{
			return Suspended__BackingField;
		}
		set
		{
			Suspended__BackingField = value;
		}
	}

	public int SuspensionReason
	{
		get
		{
			return SuspensionReason__BackingField;
		}
		set
		{
			SuspensionReason__BackingField = value;
		}
	}

	public int AgeAtSuspension
	{
		get
		{
			return AgeAtSuspension__BackingField;
		}
		set
		{
			AgeAtSuspension__BackingField = value;
		}
	}

	public int AgeAtLastSuspensionAppeal
	{
		get
		{
			return AgeAtLastSuspensionAppeal__BackingField;
		}
		set
		{
			AgeAtLastSuspensionAppeal__BackingField = value;
		}
	}

	public int NumSuccessfulAppeals
	{
		get
		{
			return NumSuccessfulAppeals__BackingField;
		}
		set
		{
			NumSuccessfulAppeals__BackingField = value;
		}
	}

	public int AgeAtLastOfferedProductPromotion
	{
		get
		{
			return AgeAtLastOfferedProductPromotion__BackingField;
		}
		set
		{
			AgeAtLastOfferedProductPromotion__BackingField = value;
		}
	}

	public int NumProductsOfferedToPromoteAtCurrentAge
	{
		get
		{
			return NumProductsOfferedToPromoteAtCurrentAge__BackingField;
		}
		set
		{
			NumProductsOfferedToPromoteAtCurrentAge__BackingField = value;
		}
	}

	public int NumTipsAvailableThisAgeUp
	{
		get
		{
			return NumTipsAvailableThisAgeUp__BackingField;
		}
		set
		{
			NumTipsAvailableThisAgeUp__BackingField = value;
		}
	}

	public string LastCollabPersonOnlyFans
	{
		get
		{
			return LastCollabPersonOnlyFans__BackingField;
		}
		set
		{
			LastCollabPersonOnlyFans__BackingField = value;
		}
	}

	public string LastCollabPostOnlyFans
	{
		get
		{
			return LastCollabPostOnlyFans__BackingField;
		}
		set
		{
			LastCollabPostOnlyFans__BackingField = value;
		}
	}

	public string LastCollabPostTypeOnlyFans
	{
		get
		{
			return LastCollabPostTypeOnlyFans__BackingField;
		}
		set
		{
			LastCollabPostTypeOnlyFans__BackingField = value;
		}
	}

	public int LastCollabPostOnlyFansControversy
	{
		get
		{
			return LastCollabPostOnlyFansControversy__BackingField;
		}
		set
		{
			LastCollabPostOnlyFansControversy__BackingField = value;
		}
	}

	public int AgeAtOnlyFanArgue
	{
		get
		{
			return AgeAtOnlyFanArgue__BackingField;
		}
		set
		{
			AgeAtOnlyFanArgue__BackingField = value;
		}
	}

	public int AgeAtLastCollabOnlyFans
	{
		get
		{
			return AgeAtLastCollabOnlyFans__BackingField;
		}
		set
		{
			AgeAtLastCollabOnlyFans__BackingField = value;
		}
	}

	public double EarningFeesLastYear
	{
		get
		{
			return EarningFeesLastYear__BackingField;
		}
		set
		{
			EarningFeesLastYear__BackingField = value;
		}
	}

	private List<string> _OnlyFansWishList
	{
		get
		{
			return _OnlyFansWishList__BackingField;
		}
		set
		{
			_OnlyFansWishList__BackingField = value;
		}
	}

	public List<string> OnlyFansWishList
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _BlockListArray
	{
		get
		{
			return _BlockListArray__BackingField;
		}
		set
		{
			_BlockListArray__BackingField = value;
		}
	}

	public ArrayList BlockListArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	private ArrayList _TrollArray
	{
		get
		{
			return _TrollArray__BackingField;
		}
		set
		{
			_TrollArray__BackingField = value;
		}
	}

	public ArrayList TrollArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public float OnlyFansFee
	{
		get
		{
			return OnlyFansFee__BackingField;
		}
		set
		{
			OnlyFansFee__BackingField = value;
		}
	}

	public int AgeAtLastChangeOnlyFansFee
	{
		get
		{
			return AgeAtLastChangeOnlyFansFee__BackingField;
		}
		set
		{
			AgeAtLastChangeOnlyFansFee__BackingField = value;
		}
	}

	public int AgeAtLastCrossPromotion
	{
		get
		{
			return AgeAtLastCrossPromotion__BackingField;
		}
		set
		{
			AgeAtLastCrossPromotion__BackingField = value;
		}
	}

	public SimSocialMediaChannel(int theChannel)
	{
	}

	public string GetChannelName()
	{
		return null;
	}

	public void IncrementYears()
	{
	}

	public void BleedFollowers()
	{
	}

	public int GetFollowers()
	{
		return 0;
	}

	public bool ShouldGetSuspended()
	{
		return false;
	}

	public bool WillPostTypeGoViral(DataSocialMediaPostType postType)
	{
		return false;
	}

	public int GetLikesForPostTypeAndWillGoViral(DataSocialMediaPostType postType, bool goingViral)
	{
		return 0;
	}

	public int GetViewsForPostTypeWithNewFollowersAndWillGoViral(DataSocialMediaPostType postType, int newFollowers, bool goingViral)
	{
		return 0;
	}

	public string GetMostCommonPostTypeString()
	{
		return null;
	}

	public double GetEarningsForPostWithViews(int views)
	{
		return 0.0;
	}

	public bool ShouldBeApprovedToMonetize()
	{
		return false;
	}

	public void RecordEarnings(double earnings)
	{
	}

	public SimSocialMediaTroll GetPostTroll()
	{
		return null;
	}

	public SimSocialMediaTroll GenerateATroll()
	{
		return null;
	}

	public void DevPrintTrollUsernames()
	{
	}

	public void AcquireCelebrityFollower(DataCelebrity celebrity)
	{
	}

	public void LoseCelebrityFollower(DataCelebrity celebrity)
	{
	}

	public bool IsCelebrityAFollower(DataCelebrity celebrity)
	{
		return false;
	}

	public void AddPersonToBlockList(string personDescription)
	{
	}

	public bool IsPersonOnBlockList(string personDescription)
	{
		return false;
	}

	public bool IsFollowerEligibleForProductPromotion()
	{
		return false;
	}

	public bool IsPostEligibleForProductPromotion()
	{
		return false;
	}

	public float GetUnadjustedCompensationForPromotion()
	{
		return 0f;
	}

	public string GetRelationString(SimPerson person)
	{
		return null;
	}

	public SimPerson GetPersonFromHeroLife()
	{
		return null;
	}

	public float GetOnlyFansPercentile()
	{
		return 0f;
	}

	public string GetOnlyFansPercentileString(float value)
	{
		return null;
	}

	public int GetRandomFollowersValueFromPercentile(float percentile)
	{
		return 0;
	}

	private bool IsHeroInSocialMediaOtherThanOnlyFans()
	{
		return false;
	}

	private int GetAllMediaFollowersExceptOnlyFans()
	{
		return 0;
	}

	public string GenerateUsername(SimPerson person)
	{
		return null;
	}

	public double GetEarningsForPostCollab(int totalViewers)
	{
		return 0.0;
	}
}
