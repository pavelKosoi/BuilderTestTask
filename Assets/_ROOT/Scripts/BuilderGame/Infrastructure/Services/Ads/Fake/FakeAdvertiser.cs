using System.Threading.Tasks;

namespace BuilderGame.Infrastructure.Services.Ads.Fake
{
    public class FakeAdvertiser : IAdvertiser
    {
        private FakeAdsWindow window;
        
        private readonly FakeAdsSettings fakeAdsSettings;

        public FakeAdvertiser(FakeAdsSettings fakeAdsSettings)
        {
            this.fakeAdsSettings = fakeAdsSettings;
        }
        
        public Task<AdWatchResult> ShowInterstitialAd(string placement)
        {
            return ShowAd(placement, false);
        }
        
        public Task<AdWatchResult> ShowRewardedAd(string placement)
        {
            return ShowAd(placement, true);
        }
        
        public async Task<AdWatchResult> ShowAd(string placement, bool isRewarded)
        {
            var tcs = new TaskCompletionSource<AdWatchResult>();
            window = UnityEngine.Object.Instantiate(fakeAdsSettings.FakeAdsWindowPrefab);
            window.Placement = placement;
            window.IsRewarded = isRewarded;
            window.Tcs = tcs;
            var result = await tcs.Task;
            window = null;
            return result;
        }
    }
}