public interface IGenericPaywallContent
{
	void Setup(IAP_TYPE iapType);

	void Rebuild();

	string GetPaywallAccessibilityDescription();
}
