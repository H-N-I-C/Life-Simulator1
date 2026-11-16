public interface SimThirdPersonScriptDelegate
{
	SimPerson ThirdPerson();

	string ThirdPersonLogIdentityStringWithStartOfYearName(SimPersonName cachedName);

	float ThirdPersonSuccessPropensityScore();

	string ThirdPersonSexualityString();

	void ThirdPersonComesOutOfTheCloset();

	bool ThirdPersonIsEligibleToComeOutOfTheCloset();

	void ThirdPersonIdentifiesGenderTrans();

	void ThirdPersonChangesNameAfterIdentifiesGenderTrans();

	void ThirdPersonIdentifiesGenderOther();

	string ThirdPersonGenderIdentityString();

	void ThirdPersonStartsWearingAccessoryHeadwear();

	void ThirdPersonStopsWearingAccessoryHeadwear();

	void ThirdPersonStartsWearingAccessorySunglasses();

	void ThirdPersonStopsWearingAccessorySunglasses();

	bool ThirdPersonShouldObserveVaccination();

	void ThirdPersonEarnsIncome(float income);

	bool ThirdPersonMovesOutAndShouldAnnounce();

	void ThirdPersonMarries();

	void ThirdPersonChangesNameAfterMarriage();

	void ThirdPersonDivorces();

	void ThirdPersonChangesNameAfterDivorce();

	void ThirdPersonBreaksUp();

	bool ThirdPersonTriesForBaby();

	void ThirdPersonHasHerosUnknownBaby();

	string ThirdPersonBabyBirthString();

	string ThirdPersonSpouseIdentityStringWithDescription(bool includeDescription);

	void ThirdPersonStartsWearingAccessoryMask();

	void ThirdPersonStopsWearingAccessoryMask();
}
