using System.Threading.Tasks;

namespace BuilderGame.Infrastructure.Ads
{
    public interface IAdvertiser
    {
        Task<AdWatchResult> ShowInterstitialAd(string placement);
        Task<AdWatchResult> ShowRewardedAd(string placement);
    }
}