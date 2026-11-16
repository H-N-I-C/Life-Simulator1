public interface IRichSocialMedia
{
	int Posts { get; }

	int GetFollowerCount();

	string GetEmoji();
}
